using IksNativeClient.Data;
using IksNativeClient.Logic.Chat;
using Microsoft.Extensions.Logging;

namespace IksNativeClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

#if WINDOWS
            builder.Services.AddSingleton<IksNativeClient.Interface.IChatLogic, WindowsChatLogic>();
#elif ANDROID
            builder.Services.AddSingleton<IksNativeClient.Interface.IChatLogic, WindowsChatLogic>();
#endif
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}