using CafeMobile_api.DTO;
using CafeMobile_api.Repository.CafeteriaRepo;
using CafeMobile_api.Repository.StudentRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeMobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeteriaController : ControllerBase
    {
        private readonly CafeteriaRepository repository;

        public CafeteriaController(CafeteriaRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] NewStaff staff)
        {
            var response = await repository.Register(staff);
            return Ok(response);
        }

        [HttpGet("menu")]
        public async Task<IActionResult> menu()
        {
            var response = await repository.FetchMenu();
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Caf_loginDTO caf_login)
        {
            var response = await repository.Login(caf_login);
            return Ok(response);

        }
        [HttpPost("CreateCoupon"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCoupon([FromBody] NewCouponDTO coupon)
        {
            var response = await repository.CreateCoupon(coupon);
            return Ok(response);

        }
        [HttpPost("CreateMeal")]
        public async Task<IActionResult> CreateMeal([FromBody] NewMealDTO meal)
        {
            var response = await repository.CreateMeal(meal);
            return Ok(response);

        }
        [HttpGet("fetchCoupons")]
        public async Task<IActionResult> FetchCoupons()
        {
            var response = await repository.FetchCoupons();
            return Ok(response);

        }
        [HttpPost("QRscan")]
        public async Task<IActionResult> QRScan(Guid qrcode)
        {
            var response = await repository.ScanQRCode(qrcode);
            return Ok(response);
        }
        [HttpGet("todaySales")]
        public async Task<IActionResult> TodaySales()
        {
            var response = await repository.GetTopSellingItems();
            return Ok(response);
        }
        [HttpGet("redemptions")]
        public async Task<IActionResult> FetchRedemptions()
        {
            var response = await repository.FetchRedemptions();
            return Ok(response);
        }
    }
}
