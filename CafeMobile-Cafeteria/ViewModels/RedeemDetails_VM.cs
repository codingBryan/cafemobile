using Cafemobile_Cafeteria.Models.DTO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Cafemobile_Cafeteria.ViewModels
{
    [QueryProperty(nameof(Redemption),"redemption")]
    public partial class RedeemDetails_VM:BaseViewModel
    {
        [ObservableProperty]
        public GetRedeemedItems redemption;

        public RedeemDetails_VM()
        {
            Redemption = new();
        }
    }
}
