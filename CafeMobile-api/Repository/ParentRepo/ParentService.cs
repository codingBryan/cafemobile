using AutoMapper;
using CafeMobile_api.Context;
using CafeMobile_api.DTO;
using CafeMobile_api.Models.Entities;
using CafeMobile_api.Models.System;
using System.Net;
using System.Security.Claims;

namespace CafeMobile_api.Repository.ParentRepo
{
    public class ParentService : ParentRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly Authentication authentication;
        private readonly IHttpContextAccessor httpContextAccesssor;

        public ParentService(IMapper mapper, DataContext context,Authentication authentication,IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.authentication = authentication;
            this.httpContextAccesssor = httpContextAccessor;
        }

        public async Task<Response<IEnumerable<GetCouponDTO>>> FetchCoupons()
        {
            Response<IEnumerable<GetCouponDTO>> res = new();
            try
            {
                IEnumerable<Coupon> coupons = await context.coupons.ToListAsync();
                IEnumerable<GetCouponDTO> CouponDTOs = coupons.Select(c => mapper.Map<GetCouponDTO>(c)).ToList();
                foreach (GetCouponDTO coupon in CouponDTOs)
                {
                    coupon.meals = new List<GetMealDTO>();
                    IEnumerable<Coupon_meal> couponMeals = await context.coupon_meals.Where(cm => cm.CouponId == coupon.CouponId).ToListAsync();
                    foreach (Coupon_meal meal in couponMeals)
                    {
                        Meal? mealData = await context.meals.Where(m => m.MealId == meal.MealId).FirstOrDefaultAsync();
                        GetMealDTO mealDTO = mapper.Map<GetMealDTO>(mealData);
                        if (coupon.meals is not null)
                        {
                            coupon.meals = coupon.meals.Append(mealDTO);
                        }
                    }
                }
                res.data = CouponDTOs;
                res.message = "Coupons fetched successfully";
                res.status_code = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                res.message = "Failed to fetch coupons";
                res.status_code = HttpStatusCode.InternalServerError;
                res.success = false;
            }
            return res;
        }

        public async Task<Response<StudentInfo>> AddStudent(int id)
        {
            Response<StudentInfo> res = new();
            if (httpContextAccesssor.HttpContext != null)
            {
                var parent_id = int.Parse(httpContextAccesssor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
                Parent? parent = await context.parents.FindAsync(parent_id);
                try
                {
                    Student? student = await context.students.FindAsync(id);
                    if (student != null) 
                    {
                        Parents_Students ps = new()
                        {
                            Student = student,
                            Parent = parent
                        };
                        await context.parents_students.AddAsync(ps);
                        await context.SaveChangesAsync();
                        res.data = mapper.Map<StudentInfo>(student);
                        res.status_code = HttpStatusCode.Accepted;
                    }
                }catch(Exception ex)
                {
                    res.message = ex.Message;
                    res.success = false;
                }
            }
            else
            {
                res.message = "Unauthorized";
                res.status_code = HttpStatusCode.Unauthorized;
            }
            return res;
        }

        public async Task<Response<string>> BuyCoupons(BuyCouponDTO coupons)
        {
            Response<string> res = new();
            Coupon? coupon = await context.coupons.Where(c=>c.CouponId == coupons.CouponId).FirstOrDefaultAsync();
            Student? student = await context.students.Where(s=>s.StudentId == coupons.studentId).FirstOrDefaultAsync();
            if (coupon != null && student != null)
            {
                StudentCoupon newStudentCoupon = new()
                {
                    Coupon = coupon,
                    Student = student,
                    price = coupons.price,
                    balance = coupons.price / 10,
                };
                await context.studentCoupons.AddAsync(newStudentCoupon);
                await context.SaveChangesAsync();
                res.data = newStudentCoupon.Id.ToString();
                res.message = "Coupon purchased successfully";
            }
            else
            {
                res.message = "Failed to purchase coupon";
                res.success = false;
            }

            return res;
        }
        public async Task<Response<string>> BuyCP(CP_PurchaseDTO cps)
        {
            Response<string> res = new();
            if (httpContextAccesssor.HttpContext is not null)
            {
                int parent_id = int.Parse(httpContextAccesssor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
                try
                {
                    Student student = await context.students.Where(s => s.StudentId == cps.studentId).FirstOrDefaultAsync();
                    if (student != null)
                    {
                        student.cp_balance += cps.amount_cp;
                        Transaction newTransaction = new()
                        {
                            ParentId = parent_id,
                            amount = cps.amount_ksh,
                            purpose = "CP purchase"
                        };
                        await context.transactions.AddAsync(newTransaction);
                        await context.SaveChangesAsync();
                        
                    }
                }
                catch(Exception ex)
                {
                    res.message = ex.Message;       
                    res.success = false;
                }
            }
            

            return res;
        }

        public async Task<Response<IEnumerable<GetStudentCouponDTO>>> GetStudentCoupons(int id)
        {
            Response<IEnumerable<GetStudentCouponDTO>> res = new();
            try
            {
                IEnumerable<StudentCoupon> coupons = await context.studentCoupons.Where(c => c.StudentId == id).ToListAsync();
                if (coupons != null)
                {
                    var studentcpns = coupons.Select(c => mapper.Map<GetStudentCouponDTO>(c)).ToList();
                    foreach(GetStudentCouponDTO c in studentcpns)
                    {
                        Coupon? coupon = await context.coupons.Where(cp => cp.CouponId == c.CouponId).FirstOrDefaultAsync();
                        c.name = coupon.name;
                        c.image = coupon.image;
                    }
                    res.data = studentcpns;

                }
                else
                {
                    res.message = "No coupons";
                }
                
            }catch(Exception ex)
            {
                res.message = ex.Message;
            }

            return res;
            }
        
        public async Task<Response<IEnumerable<StudentPurchase>>> GetStudentRedemptions(int id)
        {
            Response<IEnumerable<StudentPurchase>> res = new();
            IEnumerable<Sale> sales = await context.sales.Where(s => s.studentId == id).ToListAsync();
            IEnumerable<StudentPurchase> purchases = sales.Select(c => mapper.Map<StudentPurchase>(c)).ToList();
            foreach (StudentPurchase purchase in purchases)
            {
                GetMealDTO? meal = mapper.Map<GetMealDTO>(await context.meals.Where(m => m.MealId == purchase.mealId).FirstOrDefaultAsync());
                purchase.meal = meal;
                purchase.image = meal.image;
            };

            res.data = purchases;
            return res;
        }

        public async Task<Response<IEnumerable<StudentInfo>>> GetStudents()
        {
            Response<IEnumerable<StudentInfo>> res = new();
            if (httpContextAccesssor.HttpContext is not null)
            {
                var parent_id = int.Parse(httpContextAccesssor.HttpContext.User.FindFirstValue(ClaimTypes.Name));
                try
                {
                    var ps = await context.parents_students.Where(ps => ps.parentId == parent_id).ToListAsync();
                    var students = new List<Student>();
                    foreach(var x in ps)
                    {
                        var student = await context.students.FindAsync(x.studentId);
                        students.Add(student);
                    }
                    var studentsData = students.Select(s=>mapper.Map<StudentInfo>(s)).ToList();
                    res.data = studentsData;
                    
                }
                catch (Exception ex)
                {
                    res.message = "No students";
                    res.status_code = HttpStatusCode.NotFound;
                    res.success = false;
                }
            }
            else
            {
                res.message = "Unauthorized";
                res.status_code = HttpStatusCode.Unauthorized;
            }            
            return res;
        }

        public async Task<Response<Token<ParentInfo>>> Login(LoginDTO user)
        {
            var res = new Response<Token<ParentInfo>>();
            if (user is null)
            {
                res.success = false;
                res.status_code = HttpStatusCode.NotFound;
                return res;
            }
            try
            {
                Parent? parent = await context.parents.Where(s => s.email == user.email).FirstOrDefaultAsync();
                //Checking existence of student
                if (parent is null)
                {
                    res.message = "No parent with such an email exists";
                    res.success = false;
                    res.status_code = HttpStatusCode.NotFound;
                }
                else
                {
                    //Checking if password is correct
                    if (!authentication.verifyPasswordHash(user.password, parent.password_hash))
                    {
                        res.message = "Incorrect password";
                        res.success = false;
                        res.status_code = HttpStatusCode.Conflict;
                    }
                    else
                    {
                        ParentInfo prt = mapper.Map<ParentInfo>(parent);
                        //Generating token
                        var token = authentication.createToken((parent.ParentId).ToString(), "Parent");
                        Token<ParentInfo> access_token = new Token<ParentInfo>
                        {
                            user = prt,
                            token = token,
                            expiresAt = DateTime.Now.AddDays(1)
                        };
                        res.data = access_token;
                        res.message = "Login sucessful";
                        res.status_code = HttpStatusCode.Found;
                    }

                }



            }
            catch (Exception ex)
            {
                res.message = ex.Message;
                res.success = false;
                res.status_code = HttpStatusCode.InternalServerError;
            }

            return res;
        }

        public async Task<Response<StudentInfo>> SearchStudent(string reg)
        {
            Response<StudentInfo> res = new();
            try
            {
                Student? student = await context.students.Where(s=>s.adm_no == reg).FirstAsync();
                StudentInfo info = mapper.Map<StudentInfo>(student);
                res.data = info;
            }catch(Exception ex)
            {
                res.message = ex.Message;
                res.success = false;
            }
            return res;
        }

        public async Task<Response<ParentInfo>> SignUp(Parent_SignupDTO user)
        {
            var res = new Response<ParentInfo>();
            if (user is not null)
            {
                Parent newParent = mapper.Map<Parent>(user);
                authentication.CreatePasswordHash(user.password, out string password_hash);
                newParent.password_hash = password_hash;
                await context.parents.AddAsync(newParent);
                await context.SaveChangesAsync();
                res.data = mapper.Map<ParentInfo>(newParent);
                res.message = "parent created";
                res.status_code = HttpStatusCode.Created;
                return res;
            }
            else
            {
                res.status_code = HttpStatusCode.BadRequest;
                res.message = "Fill in all the credentials";
                res.success = false;
            }

            return res;
        }
    }
}
