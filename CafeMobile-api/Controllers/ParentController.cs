using AutoMapper;
using CafeMobile_api.DTO;
using CafeMobile_api.Repository.ParentRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeMobile_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly ParentRepository repository;
        private readonly IMapper mapper;

        public ParentController(ParentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] Parent_SignupDTO newParent)
        {
            var response = await repository.SignUp(newParent);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO parent)
        {
            var response = await repository.Login(parent);
            return Ok(response);
        }
        [HttpGet("searchStudent")]
        public async Task<IActionResult> SearchStudent(string reg)
        {
            var response = await repository.SearchStudent(reg);
            return Ok(response);
        }
        [HttpPost("addStudent"), Authorize(Roles = "Parent")]
        public async Task<IActionResult> AddStudent(int id)
        {
            var response = await repository.AddStudent(id);
            return Ok(response);
        }
        [HttpGet("fetchStudents"), Authorize(Roles = "Parent")]
        public async Task<IActionResult> FetchStudents()
        {
            var response = await repository.GetStudents();
            return Ok(response);
        }

        [HttpGet("fetchCoupons")]
        public async Task<IActionResult> FetchCoupons()
        {
            var response = await repository.FetchCoupons();
            return Ok(response);
        }

        [HttpPost("purchaseCp"),Authorize(Roles = "Parent")]
        public async Task<IActionResult> PurchaseCp(CP_PurchaseDTO cps)
        {
            var response = await repository.BuyCP(cps);
            return Ok(response);
        }
        [HttpPost("purchaseCoupon")]
        public async Task<IActionResult> PurchaseCoupon(BuyCouponDTO c)
        {
            var response = await repository.BuyCoupons(c);
            return Ok(response);
        }

        [HttpGet("fetchStudentRedemptions")]
        public async Task<IActionResult> GetStudentRedemption(int id)
        {
            var response = await repository.GetStudentRedemptions(id);
            return Ok(response);
        }

        [HttpGet("fetchStudentCoupons")]
        public async Task<IActionResult> GetStudentCoupons(int id)
        {
            var response = await repository.GetStudentCoupons(id);
            return Ok(response);
        }
    }
}
