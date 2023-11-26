using CafeMobile.Models.DTOs;
using CafeMobile.ViewModels.StudentVms;

namespace CafeMobile.Pages.Student;

public partial class CouponDetails : ContentPage
{
    private readonly CouponDetailsViewModel viewmodel;
    private readonly IMapper mapper;

    public CouponDetails(CouponDetailsViewModel viewmodel,IMapper mapper)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		this.viewmodel = viewmodel;
        this.mapper = mapper;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewmodel.Meals = (viewmodel.Coupon.meals).Select(m=>mapper.Map<MealDisplay>(m)).ToList();
    }
}