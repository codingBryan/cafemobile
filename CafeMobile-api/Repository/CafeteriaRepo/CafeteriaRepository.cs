using CafeMobile_api.DTO;
using CafeMobile_api.Models.Entities;
using CafeMobile_api.Models.System;

namespace CafeMobile_api.Repository.CafeteriaRepo
{
    public interface CafeteriaRepository
    {
        Task<Response<StaffInfo>> Register(NewStaff cafeteria);
        Task<Response<Token<StaffInfo>>> Login(Caf_loginDTO caf);
        Task<Response<GetMealDTO>> CreateMeal(NewMealDTO meal);
        Task<Response<Coupon>> CreateCoupon(NewCouponDTO coupon);
        Task<Response<IEnumerable<ItemSales>>> GetTopSellingItems();
        Task<Response<IEnumerable<GetCouponDTO>>> FetchCoupons();
        Task<Response<IEnumerable<GetMealDTO>>> FetchMenu();
        Task<Response<IEnumerable<Student>>> FetchStudents();
        Task<Response<IEnumerable<Notification>>> FetchNotifications();
        Task<Response<IEnumerable<GetRedemptionDTO>>> FetchRedemptions();
        Task<Response<GetRedemptionCafeteria>> ScanQRCode(Guid RedemptionCode);


    }
}
