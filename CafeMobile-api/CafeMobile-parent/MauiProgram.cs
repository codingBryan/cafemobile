using CafeMobile_parent.Pages;
using CafeMobile_parent.ViewModels;
using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;

namespace CafeMobile_parent
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts() //enabling microcharts
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Jost-Regular.ttf", "JostRegular");
                    fonts.AddFont("Jost-ExtraBold.ttf", "JostExtraBold");
                    fonts.AddFont("Jost-Medium.ttf", "JostMedium");
                    fonts.AddFont("Jost-SemiboldItalic.ttf", "JostSemiBoldItalic");
                    fonts.AddFont("Jost-ThinItalic.ttf", "JostThinItalic");
                });
            builder.Services.AddSingleton<Dashboard>();
            builder.Services.AddSingleton<DashboardViewModel>();

            builder.Services.AddSingleton<Coupons>();
            builder.Services.AddSingleton<CouponsViewModel>();

            builder.Services.AddSingleton<ChildTrack>();
            builder.Services.AddSingleton<ChildTrackViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}