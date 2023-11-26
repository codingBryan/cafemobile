using CafeMobile_api.DTO;
using CafeMobile_api.Repository.StudentRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CafeMobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository repository;

        public StudentController(StudentRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody]Stu_signupDTO newStudent)
        {
            var response = await repository.SignUp(newStudent);
            return Ok(response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO student)
        {
            var response = await repository.Login(student);
            return Ok(response);

        }
        [HttpPost("buyCoupon"), Authorize(Roles = "student")]
        public async Task<IActionResult> BuyCoupon([FromBody] IEnumerable<BuyCouponDTO> coupons)
        {
            var response = await repository.BuyCoupons(coupons);
            return Ok(response);
        }
        [HttpGet("menu")]
        public async Task<IActionResult> menu()
        {
            var response = await repository.GetMenu();
            return Ok(response);
        }
        [HttpPost("RedeemCp"), Authorize(Roles = "Student")]
        public async Task<IActionResult> RedeemCP([FromBody] NewRedemptionDTO redeem)
        {
            var response = await repository.RedeemCP(redeem);
            return Ok(response);
        }
        [HttpPost("RedeemCoupon"), Authorize(Roles = "Student")]
        public async Task<IActionResult> RedeemCoupo([FromBody] CouponRedemption redeem)
        {
            var response = await repository.RedeemCoupon(redeem);
            return Ok(response);
        }

        [HttpGet("redemptions"), Authorize(Roles = "Student")]
        public async Task<IActionResult> redemptions()
        {
            var  response = await repository.GetRedemtions();
            return Ok(response);
        }
        [HttpGet("myCoupons"), Authorize(Roles = "Student")]
        public async Task<IActionResult> myCoupons()
        {
            var response = await repository.GetMyCoupons();
            return Ok(response);
        }

    }
}
