using CafeMobile.Models;
using CafeMobile.Models.DTOs;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace CafeMobile.ViewModels.StudentVms
{
    public partial class CartViewModel: BaseViewModel
    {
        private readonly IMapper mapper;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(count))]
        public ObservableCollection<CartItem> cartItems;
        public int count => CartItems.Count;
        [ObservableProperty]
        public double totalCp = 0;
        public CartViewModel(IMapper mapper)
        {
            this.mapper = mapper;
            cartItems = new ObservableCollection<CartItem>();
        }

        public int GetCartCount()
        {
            return CartItems.Count();
        }

        private void CalculateTotalCp()
        {
            double totalBill = 0;
            foreach (CartItem item in CartItems)
            {
                totalBill += item.bill;
            }
            TotalCp = totalBill;
        }
        
        [RelayCommand]
        public void AddMealToCart(CartItem item)
        {
            var cart_item = CartItems.FirstOrDefault(c => c.MealId == item.MealId);
            if (cart_item is not null)
            {
                item.quantity = item.quantity+1;
            }
            else
            {
                cart_item = item;
                CartItems.Add(cart_item);
            }
            CalculateTotalCp();
        }

        [RelayCommand]
        public void RemoveMealFromCart(CartItem item)
        {
            var cart_item = CartItems.FirstOrDefault(c => c.MealId == item.MealId);
            if (cart_item is not null)
            {
                if (cart_item.quantity == 1)
                {
                    CartItems.Remove(cart_item);
                }
                else
                {
                    cart_item.quantity = item.quantity - 1;
                }
                CalculateTotalCp();
            }
            else
            {
                return;
            }
           
        }
        [RelayCommand]
        public void ClearCart()
        {
            CartItems.Clear();
            CalculateTotalCp();
        }

        [RelayCommand]
        async Task RedeemCP()
        {
            CreateClient();
            NewRedemption redemption = new();;
            IEnumerable<RedeemMealDTO> meals = CartItems.Select(c => mapper.Map<RedeemMealDTO>(c)).ToList();
            redemption.total = TotalCp;
            redemption.meals = meals;
            Response<Redemption> res = new();
            var jwtToken = Preferences.Get("token", "defaultValue");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var json = JsonConvert.SerializeObject(redemption);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/student/redeemcp", content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Response<Redemption>>(responseString);
            }
            if (res.success)
            {
                await Shell.Current.DisplayAlert("Success", "Redemption was successful", "OK");
                CartItems.Clear();
                await Shell.Current.GoToAsync(nameof(Redemptions));
            }
        }

    }
}
