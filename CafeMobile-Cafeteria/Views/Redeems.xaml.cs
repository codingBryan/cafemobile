using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class Redeems : ContentPage
{
    private readonly RedeemsViewModel viewmodel;

    public Redeems(RedeemsViewModel viewmodel)
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