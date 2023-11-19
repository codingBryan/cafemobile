using Cafemobile_Cafeteria.ViewModels;
using Microcharts;

namespace Cafemobile_Cafeteria.Views;

public partial class DashBoard : ContentPage
{
    private readonly DashboardVM viewmodel;

    public DashBoard(DashboardVM viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;
        this.viewmodel = viewmodel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewmodel.Init();
        sales_per_hour_chart.Chart = new LineChart()
        {
            Entries = viewmodel.Sales_chart_data,
            LabelOrientation = Orientation.Horizontal,
            ValueLabelOrientation = Orientation.Horizontal,
            ValueLabelTextSize = 14

        };

        customer_traffic.Chart = new BarChart()
        {
            Entries = viewmodel.Customer_traffic_data,
            LabelOrientation = Orientation.Horizontal,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelTextSize = 14,
            ValueLabelTextSize = 10,
        };

        payment_methods.Chart = new DonutChart()
        {
            Entries = viewmodel.Payment_method_data,
            LabelMode = LabelMode.RightOnly,
            LabelTextSize = 13,


        };

    }

}