using CafeMobile_parent.ViewModels;
using Microcharts;

namespace CafeMobile_parent.Pages;

public partial class ChildTrack : ContentPage
{
    private readonly ChildTrackViewModel viewmodel;

    public ChildTrack(ChildTrackViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
		this.viewmodel = viewmodel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		viewmodel.Init();
		spendingGraph.Chart = new LineChart()
		{
			Entries = viewmodel.StudentSpendingData,
			LabelOrientation = Orientation.Horizontal,
			ValueLabelOrientation = Orientation.Horizontal,
			ValueLabelTextSize = 14
		};
	}
}