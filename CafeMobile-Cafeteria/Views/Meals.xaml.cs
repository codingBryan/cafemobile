using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class Meals : ContentPage
{
	private MealsViewModel viewmodel;
	public Meals(MealsViewModel vm)
	{
		this.viewmodel = vm;
		InitializeComponent();
		BindingContext = viewmodel;

	}

	protected override async void OnAppearing()
    {
		base.OnAppearing();
		await viewmodel.init();
    }
}