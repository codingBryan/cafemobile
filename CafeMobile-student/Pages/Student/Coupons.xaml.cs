using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class Coupons : ContentPage
{
	public Coupons()
	{
		InitializeComponent();
		BindingContext = new CouponsViewModel();
	}
}