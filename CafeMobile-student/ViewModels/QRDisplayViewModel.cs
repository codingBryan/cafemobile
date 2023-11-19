using CafeMobile.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZXing.Net.Maui;

namespace CafeMobile.ViewModels
{
    [QueryProperty(nameof(Redemption),"redemption")]
    public partial class QRDisplayViewModel:BaseViewModel
    {
        [ObservableProperty]
        public Redemption redemption;

        [ObservableProperty]
        public ImageSource qrCodeSource;
        public QRDisplayViewModel()
        {
            Redemption = new();
        }
    }
}
