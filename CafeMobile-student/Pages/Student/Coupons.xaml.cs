using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

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
        viewmodel.Init();
        base.OnAppearing();
    }
}