using AutoMapper;

using CafeMobile_api.Context;
using CafeMobile_api.DTO;
using CafeMobile_api.Models.Entities;
using CafeMobile_api.Models.System;
using System.Net;
using System.Security.Claims;

namespace CafeMobile_api.Repository.StudentRepo
{
    public class StudentService : StudentRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly Authentication authentication;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StudentService(IMapper mapper,DataContext context,Authentication authentication,IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.authentication = authentication;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<Token<StudentInfo>>> Login(LoginDTO user)
        {
            var res = new Response<Token<StudentInfo>>();
            if (user is null)
            {
                res.success = false;
                res.status_code = HttpStatusCode.NotFound;
                return res;
            }
            try
            {
                Student? student = await context.students.Where(s => s.email == user.email).FirstOrDefaultAsync();
                //Checking existence of student
                if(student is null)
                {
                    res.message = "No student with such an email exists";
                    res.success = false;
                    res.status_code = HttpStatusCode.NotFound;
                }
                else
                {
                    //Checking if password is correct
                    if (!authentication.verifyPasswordHash(user.password,student.password_hash)) 
                    {
                        res.message = "Incorrect password";
                        res.success = false;
                        res.status_code = HttpStatusCode.Conflict;
                    }
                    else
                    {
                        StudentInfo std = mapper.Map<StudentInfo>(student);
                        //Generating token
                        var token = authentication.createToken((student.StudentId).ToString(), "Student");
                        Token<StudentInfo> access_token = new Token<StudentInfo>
                        {
                            user = std,
                            token = token,
                            expiresAt = DateTime.Now.AddDays(1)
                        };
                        res.data = access_token;
                        res.message = "Login sucessful";
                        res.status_code = HttpStatusCode.Found;
                    }
                    
                }



            }catch(Exception ex)
            {
                res.message = ex.Message;
                res.success = false;
                res.status_code = HttpStatusCode.InternalServerError;
            }

            return res;
        }

        public async Task<Response<IEnumerable<GetStudentCouponDTO>>> GetMyCoupons()
        {
            Response <IEnumerable<GetStudentCouponDTO>> res = new Response<IEnumerable<GetStudentCouponDTO>> ();
            if (httpContextAccessor.HttpContext != null)
            {
                var stu_id = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
                var stu_coupons = await context.studentCoupons.Where(c => c.StudentId == stu_id).ToListAsync();
                if (stu_coupons != null)
                {
                    var studentcpns = stu_coupons.Select(c => mapper.Map<GetStudentCouponDTO>(c)).ToList();
                    foreach (GetStudentCouponDTO c in studentcpns)
                    {
                        Coupon? coupon = await context.coupons.Where(cp => cp.CouponId == c.CouponId).FirstOrDefaultAsync();
                        c.name = coupon.name;
                        c.image = coupon.image;
                        c.meals = new List<GetMealDTO>();
                        IEnumerable<Coupon_meal> mealIds = await context.coupon_meals.Where(cm=>cm.CouponId == c.CouponId).ToListAsync();
                        foreach(Coupon_meal meal in mealIds)
                        {
                            GetMealDTO gmd = mapper.Map<GetMealDTO>(await context.meals.Where(m => m.MealId == meal.MealId).FirstOrDefaultAsync());
                            c.meals = c.meals.Append(gmd);
                        }
                    }
                    res.data = studentcpns;

                }
                else
                {
                    res.message = "No coupons";
                    res.success = false;
                }
            }
            return res;
        }

        public async Task<Response<GetRedemptionDTO>> RedeemCoupon(CouponRedemption redem)
        {
            string current_date = DateTime.Now.ToString("dd-MM-yyyy");
            Response<GetRedemptionDTO> res = new();
            var stu_Id = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            Student? student = await context.students.Where(s => s.StudentId == stu_Id).FirstOrDefaultAsync();
            StudentCoupon? coupon = await context.studentCoupons.Where(sc => sc.Id == redem.CouponId).FirstOrDefaultAsync();
            if(coupon != null && student != null)
            {
                coupon.balance -= redem.redemptionTotal;
                Redemption newRedemption = new()
                {
                    RedemptionId = Guid.NewGuid(),
                    Student = student,
                };
                await context.redemptions.AddAsync(newRedemption);
                foreach (var m in redem.meals)
                {
                    Redemption_meal rm;
                    Meal? meal = await context.meals.Where(m => m.MealId == m.MealId).FirstOrDefaultAsync();
                    if (meal is not null)
                    {
                        rm = new()
                        {
                            Redemption = newRedemption,
                            Meal = meal
                        };
                        await context.redemption_meals.AddAsync(rm);
                        Sale newSale = new()
                        {
                            Student = student,
                            Redemption = newRedemption,
                            mealId = m.MealId,
                            units = m.quantity,
                            price = m.price,
                            created_at = (DateTime.Now).ToString("dd-MM-yyyy"),
                        };
                        await context.sales.AddAsync(newSale);
                        ItemSales? isale = await context.item_sales.Where(s => s.ItemName == meal.name && s.sold_on == current_date).FirstOrDefaultAsync();
                        if (isale != null)
                        {
                            isale.unitsSold += m.quantity;
                            isale.totalSales += m.price;
                        }
                        else
                        {
                            ItemSales? isale_new = new()
                            {
                                ItemName = meal.name,
                                sold_on = (DateTime.Now).ToString("dd-MM-yyyy"),
                                unitsSold = m.quantity,
                                totalSales = m.price
                            };
                            await context.item_sales.AddAsync(isale_new);
                        }
                    }
                }
                await context.SaveChangesAsync();
                IEnumerable<Redemption_meal> rms = await context.redemption_meals.Where(rm => rm.RedemptionId == newRedemption.RedemptionId).ToListAsync();
                IEnumerable<GetMealDTO> meals = new List<GetMealDTO> { };
                foreach (var rm in rms)
                {
                    Meal? meal = await context.meals.Where(m => m.MealId == rm.MealId).FirstOrDefaultAsync();
                    GetMealDTO mealDTO = mapper.Map<GetMealDTO>(meal);
                    meals = meals.Append(mealDTO);
                }
                GetRedemptionDTO redemptionDTO = new()
                {
                    RedemptionId = newRedemption.RedemptionId,
                    meals = meals
                };
                res.data = redemptionDTO;
                res.message = "Redemption succesful";

            }
            return res;

        }

        public async Task<Response<StudentInfo>> SignUp(Stu_signupDTO user)
        {
            var res = new Response<StudentInfo>();
            if(user is not null)
            {
                Student newStudent = mapper.Map<Student>(user);
                authentication.CreatePasswordHash(user.password,out string passwordHash);
                newStudent.password_hash = passwordHash;

                await context.students.AddAsync(newStudent);
                await context.SaveChangesAsync();
                res.data = mapper.Map<StudentInfo>(newStudent);
                res.message = "student created";
                res.status_code = HttpStatusCode.Created;
                return res;
            }

            res.status_code = HttpStatusCode.BadRequest;
            res.message = "Fill in all the credentials";
            res.success = false;
            return res;
        }

        public async Task<Response<GetCouponDTO>> BuyCoupons(IEnumerable<BuyCouponDTO> c)
        {
            Response<GetCouponDTO> res = new Response<GetCouponDTO>();
            var coupons = c.Select(c => mapper.Map<StudentCoupon>(c)).ToList();
            var stu_Id = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            foreach (StudentCoupon coupon in coupons)
            {
                coupon.StudentId = stu_Id;
                var coupon_data = await context.coupons.Where(c => c.CouponId == coupon.CouponId).FirstOrDefaultAsync();
                coupon.price = coupon_data.price;
                coupon.CouponId = coupon_data.CouponId;
                await context.studentCoupons.AddAsync(coupon);

            }
            await context.SaveChangesAsync();
            res.message = "coupons bought";
            return res;

        }

        public async Task<Response<GetRedemptionDTO>> RedeemCP(NewRedemptionDTO redem)
        {
            Response<GetRedemptionDTO> res = new();
            string current_date = (DateTime.Now).ToString("dd-MM-yyyy");
            if (httpContextAccessor.HttpContext != null)
            {
                int? stu_id = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
                Student student = await context.students.Where(c => c.StudentId == stu_id).FirstOrDefaultAsync();
                if (student != null)
                {
                    if (student.cp_balance > redem.total)
                    {
                        student.cp_balance -= redem.total;
                        Redemption newRedemption = new()
                        {
                            RedemptionId = Guid.NewGuid(),
                            Student = student,
                        };

                        await context.redemptions.AddAsync(newRedemption);
                        foreach(var item in redem.meals)
                        {
                            Redemption_meal rm;
                            Meal? meal = await context.meals.Where(m => m.MealId == item.MealId).FirstOrDefaultAsync();
                            if (meal is not null)
                            {
                                rm = new()
                                {
                                    Redemption = newRedemption,
                                    Meal = meal
                                };

                                Sale newSale = new()
                                {
                                    Student = student,
                                    Redemption = newRedemption,
                                    mealId = meal.MealId,
                                    units = item.quantity,
                                    price = item.price,
                                    created_at = (DateTime.Now).ToString("dd-MM-yyyy"),
                                };
                                await context.sales.AddAsync(newSale);
                                ItemSales? isale = await context.item_sales.Where(s => s.ItemName == meal.name && s.sold_on == current_date).FirstOrDefaultAsync();
                                if(isale != null)
                                {
                                    isale.unitsSold += item.quantity;
                                    isale.totalSales += item.price;
                                }
                                else
                                {
                                    ItemSales? isale_new = new()
                                    {
                                        ItemName = meal.name,
                                        sold_on = (DateTime.Now).ToString("dd-MM-yyyy"),
                                        unitsSold = item.quantity,
                                        totalSales = item.price
                                    };
                                    await context.item_sales.AddAsync(isale_new);
                                }
                                await context.redemption_meals.AddAsync(rm);
                            }
                        }
                        await context.SaveChangesAsync();
                        IEnumerable<Redemption_meal> rms = await context.redemption_meals.Where(rm=>rm.RedemptionId == newRedemption.RedemptionId).ToListAsync();
                        IEnumerable<GetMealDTO> meals = new List<GetMealDTO>{ };
                        foreach (var rm in rms)
                        {
                            Meal? meal = await context.meals.Where(m => m.MealId == rm.MealId).FirstOrDefaultAsync();
                            GetMealDTO mealDTO = mapper.Map<GetMealDTO>(meal);
                            meals = meals.Append(mealDTO);
                        }
                        GetRedemptionDTO redemptionDTO = new()
                        {
                            RedemptionId = newRedemption.RedemptionId,
                            meals = meals
                        };
                        res.data = redemptionDTO;
                        res.message = "Redemption succesful";
                    }
                    else
                    {
                        res.message = "Insufficient CPs";
                        res.success = false;
                    }
                }
                else
                {
                    res.message = "Login to redeem";
                }
            }

            return res;
        }

        public async Task<Response<IEnumerable<GetMealDTO>>> GetMenu()
        {
            Response<IEnumerable<GetMealDTO>> res = new();

            var meals = await context.meals.ToListAsync();
            var mealDTOs = meals.Select(m => mapper.Map<GetMealDTO>(m)).ToList();
            res.data = mealDTOs;
            return res;
        }

        public async Task<Response<IEnumerable<GetRedemptionDTO>>> GetRedemtions()
        {
            Response<IEnumerable<GetRedemptionDTO>> res = new();
            int? stu_id = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
            IEnumerable<Redemption> redemptions = await context.redemptions.Where(r=>r.StudentId == stu_id).ToListAsync();
            IEnumerable<GetRedemptionDTO> redemptionDTOs = redemptions.Select(r => mapper.Map<GetRedemptionDTO>(r)).ToList();
            foreach(GetRedemptionDTO rd in redemptionDTOs)
            {
                IEnumerable<Redemption_meal> rms = await context.redemption_meals.Where(rm => rm.RedemptionId == rd.RedemptionId).ToListAsync();
                foreach(Redemption_meal rm in rms)
                {
                    GetMealDTO meal = mapper.Map<GetMealDTO>(await context.meals.Where(m=>m.MealId == rm.MealId).FirstOrDefaultAsync());
                    rd.meals = rd.meals.Append(meal);
                }
            }
            res.data = redemptionDTOs;
            res.message = "Redemptions Fecthed succesfully";
            return res;
        }
    }
}
