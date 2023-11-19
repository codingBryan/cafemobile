using Cafemobile_Cafeteria.Views;

namespace Cafemobile_Cafeteria
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(signUp), typeof(signUp));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(DashBoard), typeof(DashBoard));
            Routing.RegisterRoute(nameof(ScanQR), typeof(ScanQR));
            Routing.RegisterRoute(nameof(Meals), typeof(Meals));
            Routing.RegisterRoute(nameof(Redeems), typeof(Redeems));
            Routing.RegisterRoute(nameof(Coupons), typeof(Coupons));
            Routing.RegisterRoute(nameof(CreateMeal), typeof(CreateMeal));
            Routing.RegisterRoute(nameof(MealUpdate), typeof(MealUpdate));
            Routing.RegisterRoute(nameof(CreateCoupon), typeof(CreateCoupon));
            Routing.RegisterRoute(nameof(UpdateCoupon), typeof(UpdateCoupon));
            Routing.RegisterRoute(nameof(RedeemDetails), typeof(RedeemDetails));
        }
    }
}