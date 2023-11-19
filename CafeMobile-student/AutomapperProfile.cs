using CafeMobile.Models;
using CafeMobile.Models.DTOs;

namespace CafeMobile
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Meal, MealDisplay>();
            CreateMap<GetMealDTO, MealDisplay>();
            CreateMap<CartItem, RedeemMealDTO>();
        }
    }
}
