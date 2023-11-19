using CafeMobile_api.DTO;
using CafeMobile_api.Models.System;

namespace CafeMobile_api.Repository.StudentRepo
{
    public interface StudentRepository
    {
        Task<Response<StudentInfo>> SignUp(Stu_signupDTO user);
        Task<Response<Token<StudentInfo>>> Login(LoginDTO user);
        Task<Response<IEnumerable<GetCouponDTO>>> GetMyCoupons();
        Task<Response<IEnumerable<GetMealDTO>>> GetMenu();
        Task<Response<GetCouponDTO>> BuyCoupons(IEnumerable<BuyCouponDTO> coupons);
        Task<Response<GetRedemptionDTO>> RedeemCoupon(NewRedemptionDTO redem);
        Task<Response<GetRedemptionDTO>> RedeemCP(NewRedemptionDTO redem);
        Task<Response<IEnumerable<GetRedemptionDTO>>> GetRedemtions();
    }
}
