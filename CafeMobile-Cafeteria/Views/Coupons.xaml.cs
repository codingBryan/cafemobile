using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class Coupons : ContentPage
{
	private CouponsViewModel viewmodel;
	public Coupons(CouponsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		this.viewmodel = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await viewmodel.Init();
    }
}