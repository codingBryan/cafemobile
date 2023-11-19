using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages;

public partial class SignIn : ContentPage
{
	public SignIn()
	{
		InitializeComponent();
		BindingContext = new SignInViewModel();
	}
}