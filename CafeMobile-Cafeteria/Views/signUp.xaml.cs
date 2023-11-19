using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class signUp : ContentPage
{
	public signUp()
	{
		InitializeComponent();
		BindingContext = new SignUpVM();
	}
}