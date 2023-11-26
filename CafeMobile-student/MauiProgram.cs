global using AutoMapper;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CafeMobile.ViewModels;
using CafeMobile.Pages.Shared;
using Microcharts.Maui;
using CafeMobile.Pages.Student;
using CafeMobile.ViewModels.StudentVms;
using ZXing.Net.Maui.Controls;

namespace CafeMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UseMicrocharts() //enabling microcharts
                .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                        fonts.AddFont("Jost-Regular.ttf", "JostRegular");
                        fonts.AddFont("Jost-ExtraBold.ttf", "JostExtraBold");
                        fonts.AddFont("Jost-Medium.ttf", "JostMedium");
                        fonts.AddFont("Jost-SemiboldItalic.ttf", "JostSemiBoldItalic");
                        fonts.AddFont("Jost-ThinItalic.ttf", "JostThinItalic");
                    }).UseMauiCommunityToolkit();
                    
            builder.Services.AddAutoMapper(typeof(AutomapperProfile));

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddSingleton<StudentMenu>();
            builder.Services.AddSingleton<StudentMenuViewModel>();

            builder.Services.AddSingleton<StudentSignUpViewModel>();
            builder.Services.AddSingleton<StudentSignUp>();

            builder.Services.AddSingleton<Cart>();
            builder.Services.AddSingleton<CartViewModel>();

            builder.Services.AddSingleton<Coupons>();
            builder.Services.AddSingleton<CouponsViewModel>();

            builder.Services.AddSingleton<Redemptions>();
            builder.Services.AddSingleton<RedemptionsViewModel>();

            builder.Services.AddSingleton<CouponDetails>();
            builder.Services.AddSingleton<CouponDetailsViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}