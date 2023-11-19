using Amazon.S3;
using Amazon.S3.Model;
using AutoMapper;
using CafeMobile_api.Context;
using CafeMobile_api.DTO;
using CafeMobile_api.Models.Entities;
using CafeMobile_api.Models.System;
using System.Net;

namespace CafeMobile_api.Repository.CafeteriaRepo
{
    public class CafeteriaService : CafeteriaRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly Authentication authentication;
        //private readonly IAmazonS3 s3;

        public CafeteriaService(IMapper mapper,DataContext context, IHttpContextAccessor httpContextAccessor,Authentication authentication)
        {
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.authentication = authentication;
            ///this.s3 = s3;
            
        }
        public async Task<Response<Coupon>> CreateCoupon(NewCouponDTO coupon)
        {
            Response<Coupon> res = new Response<Coupon>();
            Coupon newCoupon = mapper.Map<Coupon>(coupon);
            if(httpContextAccessor.HttpContext != null)
            {      

                await context.SaveChangesAsync();
                //Updating the Coupon_meal table
                foreach(int id in coupon.meal_Ids)
                {
                    Meal meal = await context.meals.Where(m=>m.MealId == id).FirstOrDefaultAsync();
                    Coupon_meal cm = new Coupon_meal
                    {
                        Coupon = newCoupon,
                        Meal = meal
                    };
                    await context.coupon_meals.AddAsync(cm);
                }
                await context.SaveChangesAsync();

                res.data = newCoupon;
                res.message = "Coupon created";
                res.status_code = HttpStatusCode.Created;
            }
            else
            {
                res.message = "Unauthorized";
                res.status_code = HttpStatusCode.Unauthorized;
            }
            return res;
        }

        public async Task<Response<GetMealDTO>> CreateMeal(NewMealDTO meal)
        {
            Response<GetMealDTO> response = new Response<GetMealDTO>();
            
            Meal newMeal = mapper.Map<Meal>(meal);
            if (httpContextAccessor.HttpContext != null) 
            {
                newMeal.price_CP = meal.price / 10;
                await context.meals.AddAsync(newMeal);
                await context.SaveChangesAsync();
                GetMealDTO m = mapper.Map<GetMealDTO>(newMeal);
                /*
                PutObjectRequest s3_req = new PutObjectRequest
                {
                    BucketName = "cafemobile-bucket",
                    Key = $"images/{m.MealId}",
                    ContentType = image.ContentType,
                    InputStream = image.OpenReadStream(),
                    Metadata =
                    {
                        ["x-amz-meta-originalname"] = image.FileName,
                        ["x-amz-meta-extension"] = Path.GetExtension(image.FileName)
                    }
                };
                await s3.PutObjectAsync(s3_req);
                */
                response.data = m;
            }
            else
            {
                response.message = "Unauthorized user";
            }
            return response;
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
                    IEnumerable<Coupon_meal> couponMeals = await context.coupon_meals.Where(cm=>cm.CouponId == coupon.CouponId).ToListAsync();
                    foreach (Coupon_meal meal in couponMeals) 
                    {
                        Meal? mealData = await context.meals.Where(m=>m.MealId == meal.MealId).FirstOrDefaultAsync();
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
            }catch(Exception ex)
            {
                res.message = "Failed to fetch coupons";
                res.status_code = HttpStatusCode.InternalServerError;
                res.success = false;
            }
            return res;
        }
        public async Task<Response<IEnumerable<GetMealDTO>>> FetchMenu()
        {
            Response<IEnumerable<GetMealDTO>> res = new();
            try
            {
                var meals = await context.meals.ToListAsync();
                var data = meals.Select(m=>mapper.Map<GetMealDTO>(m)).ToList();
                res.data = data;
            }
            catch(Exception ex)
            {
                res.message = ex.Message;  
                res.status_code = HttpStatusCode.InternalServerError;
            }

            return res;
        }

        public Task<Response<IEnumerable<Notification>>> FetchNotifications()
        {
            throw new NotImplementedException();
        }
        
        public async Task<Response<IEnumerable<GetRedemptionDTO>>> FetchRedemptions()
        {
            Response <IEnumerable<GetRedemptionDTO>> res = new();
            IEnumerable<GetRedemptionDTO> redeems = (await context.redemptions.Where(r => r.scanned == false).ToListAsync()).Select(r=>mapper.Map<GetRedemptionDTO>(r));
            res.data = redeems;
            res.message = "Redemption fetched successfully";
            return res;
        }

        public Task<Response<IEnumerable<Student>>> FetchStudents()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<IEnumerable<ItemSales>>> GetTopSellingItems()
        {
            string today_date = (DateTime.Now).ToString("dd-MM-yyyy");
            Response<IEnumerable<ItemSales>> response = new Response<IEnumerable<ItemSales>>();
            try
            {
                var today_sales = await context.item_sales.Where(s => s.sold_on == today_date).ToListAsync();
                response.data=today_sales;
            }
            catch(Exception ex)
            {
                response.message = ex.Message;
            }
            return response;
        }

        public async Task<Response<Token<StaffInfo>>> Login(Caf_loginDTO caf)
        {

            var res = new Response<Token<StaffInfo>>();
            if (caf is null)
            {
                res.success = false;
                res.status_code = HttpStatusCode.NotFound;
                return res;
            }
            try
            {
                Staff? staff = await context.staff.Where(s => s.email == caf.email).FirstOrDefaultAsync();
                //Checking existence of staff
                if (staff is null)
                {
                    res.message = "Invalid email";
                    res.success = false;
                    res.status_code = HttpStatusCode.NotFound;
                }
                else
                {
                    //Checking if password is correct
                    if (!authentication.verifyPasswordHash(caf.password, staff.password_hash))
                    {
                        res.message = "Incorrect password";
                        res.success = false;
                        res.status_code = HttpStatusCode.Conflict;
                    }
                    else
                    {
                        StaffInfo data = mapper.Map<StaffInfo>(staff);
                        //Generating token
                        var token = authentication.createToken((staff.id).ToString(), staff.role);
                        Token<StaffInfo> access_token = new Token<StaffInfo>
                        {
                            user = data,
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

        public async Task<Response<StaffInfo>> Register(NewStaff staff)
        {
            Response<StaffInfo> response = new Response<StaffInfo>();
            if (staff is not null)
            {
                Staff newStaff = mapper.Map<Staff>(staff);
                authentication.CreatePasswordHash(staff.password, out string passwordHash);
                newStaff.password_hash = passwordHash;

                await context.staff.AddAsync(newStaff);
                await context.SaveChangesAsync();
                response.data = mapper.Map<StaffInfo>(newStaff);
                response.message = "staff created";
                response.success = true;
                response.status_code = HttpStatusCode.Created;

            }
            else
            {

                response.status_code = HttpStatusCode.BadRequest;
                response.message = "Fill in all the credentials";
                response.success = false;
            }
            return response;

            
        }

        public async Task<Response<GetRedemptionCafeteria>> ScanQRCode(Guid RedemptionCode)
        {
            Response<GetRedemptionCafeteria> res = new();
            IEnumerable<GetSoldItem> soldItems = new List<GetSoldItem>();
            Redemption? redemption = await context.redemptions.Where(r=>r.RedemptionId == RedemptionCode).FirstOrDefaultAsync();
            if (redemption != null)
            {
                GetRedemptionCafeteria data = mapper.Map<GetRedemptionCafeteria>(redemption);
                Student? student = await context.students.Where(s => s.StudentId == redemption.StudentId).FirstOrDefaultAsync();
                if(student != null)
                {
                    string name = $"{student.first_name} {student.last_name}";
                    data.student_name = name;
                }
                IEnumerable<Sale> sales = await context.sales.Where(r => r.RedemptionId == RedemptionCode).ToListAsync();
                foreach (Sale s in sales)
                {
                    Meal? meal = await context.meals.Where(m => m.MealId == s.mealId).FirstOrDefaultAsync();
                    GetSoldItem item = mapper.Map<GetSoldItem>(meal);
                    item.saleId = s.Id;
                    item.quantity = s.units;
                    item.pending = s.pending;
                    item.cooking = s.cooking;
                    item.prepared = s.prepared;
                    soldItems = soldItems.Append(item);
                }
                data.meals = soldItems;
                redemption.scanned = true;
                redemption.scanned_at = new DateTime();
                await context.SaveChangesAsync();

                res.data = data;
            }
            else 
            {
                res.message = "invalid code";
                res.success = false;
                res.status_code = HttpStatusCode.NotFound;
            }
            return res;
        }
    }
}
