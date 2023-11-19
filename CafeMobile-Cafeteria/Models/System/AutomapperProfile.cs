using AutoMapper;
using Cafemobile_Cafeteria.Models.DTO;

namespace Cafemobile_Cafeteria.Models.System
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<GetMealDTO, MealDisplay>();
            CreateMap<GetCouponDTO, CouponDisplay>();
        }
    }
}
