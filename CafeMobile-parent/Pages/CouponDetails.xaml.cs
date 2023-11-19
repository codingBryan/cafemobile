using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages;

public partial class CouponDetails : ContentPage
{
	public CouponDetails()
	{
		InitializeComponent();
		BindingContext = new CouponDetailsViewModel();
	}
}