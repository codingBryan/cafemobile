using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class CreateCoupon : ContentPage
{
    private readonly CreateCouponViewModel viewmodel;

    public CreateCoupon(CreateCouponViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		this.viewmodel = viewmodel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewmodel.Init();
    }


}