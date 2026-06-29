using Microsoft.Extensions.Logging;
using AppAhorro.Backend.Data;
using AppAhorro.Backend.Services;
using AppAhorro.Backend.Dao;

namespace AppAhorro.Desktop;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		ConexionBD.InicializarBasedeDatos();

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
		builder.Services.AddSingleton<UsuarioService>();
		builder.Services.AddSingleton<FinanzasService>();
		builder.Services.AddSingleton<MetaAhorroDAO>(); 

		return builder.Build();
	}
}