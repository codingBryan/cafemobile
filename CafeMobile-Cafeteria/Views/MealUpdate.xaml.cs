using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class MealUpdate : ContentPage
{
	public MealUpdate(MealUpdateViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}