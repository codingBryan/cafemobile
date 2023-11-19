

using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class StudentMenu : ContentPage
{
    private readonly StudentMenuViewModel viewmodel;

    public StudentMenu(StudentMenuViewModel _vm)
	{
		InitializeComponent();
        BindingContext = _vm;
        viewmodel = _vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        viewmodel.FetchMeals();
    }
    private void Menu_MealAdded(object sender, Controls.MealAddEventArgs e)
    {
       viewmodel.AddMealToCartCommand.Execute(e.Meal);
    }
}