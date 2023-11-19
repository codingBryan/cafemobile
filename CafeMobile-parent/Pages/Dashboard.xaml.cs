using CafeMobile_parent.ViewModels;

namespace CafeMobile_parent.Pages;

public partial class Dashboard : ContentPage
{
    private readonly DashboardViewModel dashboardVM;

    public Dashboard(DashboardViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		this.dashboardVM = vm;
		
	}
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await dashboardVM.FetchStudents();
    }
}