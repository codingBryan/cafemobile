using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages;

public partial class TopUpCP : ContentPage
{
	public TopUpCP()
	{
		InitializeComponent();
		BindingContext = new TopUpCpViewModel();
	}
}