using CafeMobile_parent.Pages;

namespace CafeMobile_parent
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ChildTrack), typeof(ChildTrack));
            Routing.RegisterRoute(nameof(TopUpCP), typeof(TopUpCP));
            Routing.RegisterRoute(nameof(Coupons), typeof(Coupons));
            Routing.RegisterRoute(nameof(CouponDetails), typeof(CouponDetails));
            Routing.RegisterRoute(nameof(SignIn), typeof(SignIn));
            Routing.RegisterRoute(nameof(SignUp), typeof(SignUp));
            Routing.RegisterRoute(nameof(Dashboard), typeof(Dashboard));
            Routing.RegisterRoute(nameof(Notifications), typeof(Notifications));
            Routing.RegisterRoute(nameof(Transactions), typeof(Transactions));
        }
    }
}