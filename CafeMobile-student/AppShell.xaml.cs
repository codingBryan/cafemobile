using CafeMobile.Pages.Student;
using CouponDetailsStudent = CafeMobile.Pages.Student.CouponDetails;

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
            Routing.RegisterRoute(nameof(CouponDetailsStudent), typeof(CouponDetailsStudent));
            Routing.RegisterRoute(nameof(Cart), typeof(Cart));
            Routing.RegisterRoute(nameof(Redemptions), typeof(Redemptions));
            Routing.RegisterRoute(nameof(DisplayQRCode), typeof(DisplayQRCode));



        }
    }
}