using AutoMapper;
using CafeMobile_api.DTO;
using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.Models.System
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Stu_signupDTO, Student>();
            CreateMap<NewMealDTO, Meal>();
            CreateMap<NewMealDTO, Meal>();
            CreateMap<NewCouponDTO, Coupon>();
            CreateMap<BuyCouponDTO, StudentCoupon>();
            CreateMap<Student, StudentInfo>();
            CreateMap<NewStaff, Staff>();
            CreateMap<Staff, StaffInfo>();
            CreateMap<Meal, GetMealDTO>();
            CreateMap<Coupon, GetCouponDTO>();
            CreateMap<StudentCoupon, GetStudentCouponDTO>();
            CreateMap<Parent_SignupDTO, Parent>();
            CreateMap<Parent, ParentInfo>();
            CreateMap<Redemption, GetRedemptionDTO>();
            CreateMap<Redemption, GetRedemptionCafeteria>();
            CreateMap<Meal, GetSoldItem>();
            CreateMap<Sale, StudentPurchase>();
        }
    }
    
}
