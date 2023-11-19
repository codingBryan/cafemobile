using Cafemobile_Cafeteria.ViewModels;
using ZXing.Net.Maui;

namespace Cafemobile_Cafeteria.Views;

public partial class ScanQR : ContentPage
{
    private readonly Scanqr_VM viewmodel;

    public ScanQR(Scanqr_VM viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
        this.viewmodel = viewmodel;
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = true
        };
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
            viewmodel.ScanQR(Guid.Parse(barcode.Value));
    }

}