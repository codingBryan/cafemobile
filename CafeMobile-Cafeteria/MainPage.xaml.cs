using Cafemobile_Cafeteria.Views;

namespace Cafemobile_Cafeteria
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

      

        private async void GoToLogin(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Login));
        }
    }
}