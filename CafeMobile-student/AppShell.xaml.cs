using CafeMobile.Pages.Student;

namespace CafeMobile
{
    public partial class AppShell : Shell
    {

        public AppShell()
        {
            InitializeComponent();
            //student Routes
            Routing.RegisterRoute(nameof(StudentLogin), typeof(StudentLogin));
            Routing.RegisterRoute(nameof(StudentSignUp), typeof(StudentSignUp));
            Routing.RegisterRoute(nameof(StudentMenu), typeof(StudentMenu));
            Routing.RegisterRoute(nameof(Coupons), typeof(Coupons));
            Routing.RegisterRoute(nameof(CouponDetails), typeof(CouponDetails));
            Routing.RegisterRoute(nameof(Cart), typeof(Cart));
            Routing.RegisterRoute(nameof(Redemptions), typeof(Redemptions));
            Routing.RegisterRoute(nameof(DisplayQRCode), typeof(DisplayQRCode));



        }
    }
}