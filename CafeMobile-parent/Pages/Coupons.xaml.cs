using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages;

public partial class Coupons : ContentPage
{
    private readonly CouponsViewModel viewmodel;

    public Coupons(CouponsViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		this.viewmodel = viewmodel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        viewmodel.Init();
    }
}