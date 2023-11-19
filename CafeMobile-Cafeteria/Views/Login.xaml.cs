using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
		BindingContext = new SignInVM();
	}
}