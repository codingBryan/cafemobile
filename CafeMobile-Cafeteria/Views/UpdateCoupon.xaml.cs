using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class UpdateCoupon : ContentPage
{
    private readonly UpdateCouponViewModel viewmodel;

    public UpdateCoupon(UpdateCouponViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		this.viewmodel = viewmodel;
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();
		viewmodel.MealCount = viewmodel.Coupon.meals.Count();
	}
}