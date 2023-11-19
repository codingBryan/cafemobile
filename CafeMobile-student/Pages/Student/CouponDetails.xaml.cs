using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class CouponDetails : ContentPage
{
	public CouponDetails()
	{
		InitializeComponent();
		BindingContext = new CouponDetailsViewModel();
	}
}