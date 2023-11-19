using CafeMobile.ViewModels;

namespace CafeMobile.Pages.Student;

public partial class Redemptions : ContentPage
{
    private readonly RedemptionsViewModel viewmodel;

    public Redemptions(RedemptionsViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		this.viewmodel = viewmodel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (viewmodel.Redemptions.Count() == 0)
        {
            viewmodel.Init();
        }
        
    }
}