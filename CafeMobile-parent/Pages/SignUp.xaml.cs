using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages;

public partial class SignUp : ContentPage
{
	public SignUp()
	{
		InitializeComponent();
		BindingContext = new SignUpViewModel();
	}
}