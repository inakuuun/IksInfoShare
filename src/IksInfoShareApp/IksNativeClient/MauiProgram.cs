using IksNativeClient.Common.Init;
using IksNativeClient.Data;
using IksNativeClient.Logic.BootCertification;
using IksNativeClient.Logic.Chat;
using Microsoft.Extensions.Logging;
using NGLB = IksNativeClient.Common.Common.CommonDef;

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
            Task.Run(() =>
            {
                // バックグラウンドで初期処理を実行
                SystemInit();
            });
#endif
#if WINDOWS
            builder.Services.AddSingleton<IksNativeClient.Interface.IBootCertificationLogic, WindowsCertificationLogic>();
            builder.Services.AddSingleton<IksNativeClient.Interface.IChatLogic, WindowsChatLogic>();
#elif ANDROID
            //builder.Services.AddSingleton<IksNativeClient.Interface.IChatLogic, AndroidChatLogic>();
#endif
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }

        /// <summary>
        /// バックグラウンド初期処理
        /// </summary>
        private static void SystemInit()
        {
            // システムに必要な情報を生成
            _ = new SystemInit();
            NGLB.DbLogic.InitCreateTable();
            // 初回起動同期処理を実施
            // 通信クラスのインスタンス化
        }
    }
}