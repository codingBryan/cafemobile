using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class MealUpdate : ContentPage
{
	public MealUpdate()
	{
		InitializeComponent();
		BindingContext = new MealUpdateViewModel();
	}
}