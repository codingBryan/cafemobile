using Cafemobile_Cafeteria.ViewModels;

namespace Cafemobile_Cafeteria.Views;

public partial class RedeemDetails : ContentPage
{
	public RedeemDetails()
	{
		InitializeComponent();
        BindingContext = new RedeemDetails_VM();
    }
}