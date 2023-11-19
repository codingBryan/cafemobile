using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class CreateMeal : ContentPage
{
	public CreateMeal()
	{
		InitializeComponent();
		BindingContext = new CreateMealViewModel();
	}

}