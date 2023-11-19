using CafeMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CafeMobile.ViewModels.StudentVms
{
    public partial class CouponDetailsViewModel:ObservableObject
    {
        [ObservableProperty]
        public Coupon coupon = new Coupon
        {
            CouponId = 1,
            name = "Gold coupon",
            price = 100,
            image_url = "https://images.unsplash.com/photo-1533910534207-90f31029a78e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80",
            is_active = true,
            meals = new ObservableCollection<Meal>
            {
                new Meal
                {
                    MealId=1,
                    description="This is a good meal",
                    name = "Dougnuts",
                    price = 10,
                    price_CP = 1,
                    image_url= "https://images.unsplash.com/photo-1533910534207-90f31029a78e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80"
                },
                 new Meal
                {
                    MealId=2,
                    description="This is a good meal",
                    name = "Ugali Beaf Stew",
                    price = 150,
                    price_CP = 15,
                    image_url= "https://www.mostell.co.ke/wp-content/uploads/2018/03/Beef-Stew-Ugali.jpg"
                },
                 new Meal
                {
                    MealId=3,
                    description="This is a good meal",
                    name = "Rice Dengu",
                    price = 80,
                    price_CP = 8,
                    image_url= "https://img-global.cpcdn.com/recipes/046da7680d5bc213/640x640sq70/photo.webp"
                },

            }
        };
        public CouponDetailsViewModel()
        {
            
        }
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
