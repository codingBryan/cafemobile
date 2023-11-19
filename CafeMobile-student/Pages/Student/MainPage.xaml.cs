using CafeMobile.ViewModels;

namespace CafeMobile.Pages.Shared
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

    }
}