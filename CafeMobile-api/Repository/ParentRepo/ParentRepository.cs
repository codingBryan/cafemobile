using CafeMobile_api.DTO;
using CafeMobile_api.Models.Entities;
using CafeMobile_api.Models.System;

namespace CafeMobile_api.Repository.ParentRepo
{
    public interface ParentRepository
    {
        Task<Response<ParentInfo>> SignUp(Parent_SignupDTO user);
        Task<Response<Token<ParentInfo>>> Login(LoginDTO user);
        Task<Response<IEnumerable<StudentInfo>>> GetStudents();
        Task<Response<string>> BuyCoupons(BuyCouponDTO coupons);
        Task<Response<string>> BuyCP(CP_PurchaseDTO cps);
        Task<Response<IEnumerable<GetCouponDTO>>> FetchCoupons();
        Task<Response<StudentInfo>> SearchStudent(string reg);
        Task<Response<StudentInfo>> AddStudent(int id);
        Task<Response<IEnumerable<GetStudentCouponDTO>>> GetStudentCoupons(int id);
        Task<Response<IEnumerable<StudentPurchase>>> GetStudentRedemptions(int id);
    }
}
