namespace MauiMSGraph_v1;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; }

    public App()
	{
		InitializeComponent();
        // Registrar MyDataService como un objeto singleton en el contenedor de inyección de dependencias
        Services = new ServiceCollection()
            .AddSingleton<GraphService>()
            .BuildServiceProvider();

        //MainPage = new AppShell();
        MainPage = new AppShell();
	}
}
