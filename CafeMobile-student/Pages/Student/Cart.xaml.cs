using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class Cart : ContentPage
{
	public Cart(CartViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}