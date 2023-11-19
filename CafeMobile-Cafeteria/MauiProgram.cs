using Cafemobile_Cafeteria.Models.System;
using CommunityToolkit.Maui;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using Cafemobile_Cafeteria.Views;
using Cafemobile_Cafeteria.ViewModels;
using ZXing.Net.Maui.Controls;

namespace Cafemobile_Cafeteria
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UseMauiCommunityToolkit()
                .UseMicrocharts()
                // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Jost-Regular.ttf", "JostRegular");
                    fonts.AddFont("Jost-SemiboldItalic.ttf", "JostSemiboldItalic");
                    fonts.AddFont("Jost-ExtraBold.ttf", "JostExtraBold");
                    fonts.AddFont("Jost-ThinItalic.ttf", "JostThinItalic");
                    fonts.AddFont("Jost-Medium.ttf", "JostMedium");
                });
            builder.Services.AddAutoMapper(typeof(AutomapperProfile));

            builder.Services.AddSingleton<DashBoard>();
            builder.Services.AddSingleton<DashboardVM>();

            builder.Services.AddSingleton<Meals>();
            builder.Services.AddSingleton<MealsViewModel>();

            builder.Services.AddSingleton<Coupons>();
            builder.Services.AddSingleton<CouponsViewModel>();

            builder.Services.AddSingleton<CreateCoupon>();
            builder.Services.AddSingleton<CreateCouponViewModel>();

            builder.Services.AddSingleton<UpdateCoupon>();
            builder.Services.AddSingleton<UpdateCouponViewModel>();

            builder.Services.AddSingleton<ScanQR>();
            builder.Services.AddSingleton<Scanqr_VM>();

            builder.Services.AddSingleton<Redeems>();
            builder.Services.AddSingleton<RedeemsViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}