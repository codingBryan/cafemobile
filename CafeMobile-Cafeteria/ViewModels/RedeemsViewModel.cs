using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class RedeemsViewModel:BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<GetRedemptionDTO> redeems;
        public void Init()
        {
            FetchRedemptions();
        }

        [RelayCommand]
        async Task FetchRedemptions()
        {
            CreateClient();
            IsBusy = true;
            var response = await client.GetAsync("api/cafeteria/redemptions");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<ObservableCollection<GetRedemptionDTO>>>(response_string);
                Redeems = response_data.data;
            }
            IsBusy = false;

        }
    }
}
