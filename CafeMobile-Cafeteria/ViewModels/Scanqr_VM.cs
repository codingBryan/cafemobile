
using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Views;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;
using ZXing.Net.Maui;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class Scanqr_VM:BaseViewModel
    {

        public Scanqr_VM()
        {
            
        }
        public async Task ScanQR(Guid code)
        {
            CreateClient();
            var json = JsonConvert.SerializeObject(code);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/cafeteria/qrscan", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<GetRedeemedItems> data = JsonConvert.DeserializeObject<Response<GetRedeemedItems>>(response_string);
                if (data.data != null)
                {
                    await Shell.Current.DisplayAlert("Success", "Scan successful", "Ok");
                    await Shell.Current.GoToAsync(nameof(RedeemDetails), new Dictionary<string, object>
                    {
                        ["string"] = data.data
                    });
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Failed", "Failed to create coupon", "Ok");
            }
        }

    }
}
