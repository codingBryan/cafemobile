using CafeMobile.Models.DTOs;
using System.Windows.Input;

namespace CafeMobile.Controls;

public partial class MealList : ContentView
{
    public ICommand AddMealToCartCommand { get; private set; }
    public event EventHandler<MealAddEventArgs> onMealAdded;
    public MealList()
	{
		InitializeComponent();
        AddMealToCartCommand = new Command(ExecuteAddMealToCart);
	}
    public static BindableProperty HeadingProperty = BindableProperty.Create(nameof(Heading), typeof(string), typeof(CategoryRow), String.Empty);
    public static BindableProperty MealsProperty = BindableProperty.Create(nameof(Meals), typeof(IEnumerable<MealDisplay>), typeof(MealList), Enumerable.Empty<MealDisplay>());

    public string Heading
    {
        get => (string)GetValue(MealList.HeadingProperty);
        set => SetValue(MealList.HeadingProperty, value);
    }

    public IEnumerable<MealDisplay> Meals
    {
        get => (IEnumerable<MealDisplay>)GetValue(MealList.MealsProperty);
        set => SetValue(MealList.MealsProperty, value);
    }

    private void ExecuteAddMealToCart(object parameter)
    {
        if(parameter is MealDisplay meal && meal is not null)
        {
            onMealAdded?.Invoke(this, new MealAddEventArgs(meal));
        }
    }
}
public class MealAddEventArgs : EventArgs
{
    public MealDisplay Meal { get; set; }
    public MealAddEventArgs(MealDisplay meal) => Meal = meal;
}