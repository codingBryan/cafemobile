using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages
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