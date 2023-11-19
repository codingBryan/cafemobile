using CafeMobile.ViewModels;

namespace CafeMobile.Pages.Student;

public partial class DisplayQRCode : ContentPage
{
	public DisplayQRCode()
	{
        InitializeComponent();
        BindingContext = new QRDisplayViewModel();
    }
}