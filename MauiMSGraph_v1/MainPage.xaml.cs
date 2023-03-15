using Microsoft.Graph.Models;
using Microsoft.Maui.Graphics.Text;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace MauiMSGraph_v1;

public partial class MainPage : ContentPage
{
	//int count = 0;
    private GraphService graphService;
    private User user;
    public List<Eventos> eventos_ { get; set; }

    public MainPage()
    {
        InitializeComponent();

    }

    private async void GetUserInfoBtn_Clicked(object sender, EventArgs e)
    {
        var settings = Settings.LoadSettings();
        if (graphService == null)
        {
            graphService = new GraphService(settings.ClientId!, settings.TenantId!, settings.Scopes!);
        }
        user = await graphService.GetMyDetailsAsync();
        GetUserInfoBtn.Text = $"Hello, {user.DisplayName}!";
    }

    private async void Peticion_Lectura_Eventos()
    {
        
    }

	private async void Nuevo_evento_Clicked(object sender, EventArgs e) 
	{
        if (graphService == null)
        {
            await DisplayAlert("Error", "You have to log in to create a new event", "Ok");
        }
        else
        {
            await Navigation.PushAsync(new NewEvent());
        }
    }

	private async void Editar_evento_Clicked(Object sender, EventArgs e)
	{
        if(graphService == null)
        {
            await DisplayAlert("Error", "You have to log in to edit any event", "Ok");
        }
        else
        {
            await Navigation.PushAsync(new EditEvent());
        }
        
	}
	private void Actualizar_Eventos()
	{
        // Obtener el Grid desde el archivo de diseño XAML
        var grid = this.FindByName<Grid>("Eventos_Principal");

        // Crear una nueva fila
        var row = new RowDefinition { Height = GridLength.Auto };

        // Agregar la fila al Grid
        grid.RowDefinitions.Add(row);

        // Crear un nuevo elemento y agregarlo a la nueva fila
        var nuevoElementoID = new Label 
        { 
            Text = "Nuevo elemento"
        };

        var nuevoElementoName = new Label
        {
            Text = "Nombre del elemento"
        };

        var nuevoElementoHora = new Label
        {
            Text = "18:30"
        };

        var nuevoElemntoFecha = new Label
        {
            Text = "Lunes 15"
        };

        var nuevoElementoLocation = new Label
        {
            Text = "ToDoList"
        };

        grid.Add(nuevoElementoID, 0, (grid.RowDefinitions.Count - 1));
        grid.Add(nuevoElementoName, 1, (grid.RowDefinitions.Count - 1));
        grid.Add(nuevoElementoHora, 2, (grid.RowDefinitions.Count - 1));
        grid.Add(nuevoElemntoFecha, 3, (grid.RowDefinitions.Count - 1));
        grid.Add(nuevoElementoLocation, 4, (grid.RowDefinitions.Count - 1));
       

    }

    private void Actualizaciontabla_Clicked(object sender, EventArgs e)
    {

        Eventos_Principal.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        // Crear una nueva fila

        // Agregar la fila al Grid
        //grid.RowDefinitions.Add(row);

        // Crear un nuevo elemento y agregarlo a la nueva fila
        var nuevoElementoID = new Label
        {
            Text = "Id del elemento",
            HorizontalOptions = LayoutOptions.Center

        };

        var nuevoElementoName = new Label
        {
            Text = "Nombre del elemento",
            HorizontalOptions = LayoutOptions.Center

        };

        var nuevoElementoHora = new Label
        {
            Text = "18:30",
            HorizontalOptions = LayoutOptions.Center
        };

        var nuevoElementoFecha = new Label
        {
            Text = "Lunes 15",
            HorizontalOptions = LayoutOptions.Center
        };

        var nuevoElementoLocation = new Label
        {
            Text = "ToDoList",
            HorizontalOptions = LayoutOptions.Center
        };
        Grid.SetRow(nuevoElementoID, Eventos_Principal.RowDefinitions.Count - 1);
        Grid.SetColumn(nuevoElementoID, 0);
        Eventos_Principal.Children.Add(nuevoElementoID);

        Grid.SetRow(nuevoElementoName, Eventos_Principal.RowDefinitions.Count - 1);
        Grid.SetColumn(nuevoElementoName, 1);
        Eventos_Principal.Children.Add(nuevoElementoName);

        Grid.SetRow(nuevoElementoHora, Eventos_Principal.RowDefinitions.Count - 1);
        Grid.SetColumn(nuevoElementoHora, 2);
        Eventos_Principal.Children.Add(nuevoElementoHora);

        Grid.SetRow(nuevoElementoFecha, Eventos_Principal.RowDefinitions.Count - 1);
        Grid.SetColumn(nuevoElementoFecha, 3);
        Eventos_Principal.Children.Add(nuevoElementoFecha);

        Grid.SetRow(nuevoElementoLocation, Eventos_Principal.RowDefinitions.Count - 1);
        Grid.SetColumn(nuevoElementoLocation, 4);
        Eventos_Principal.Children.Add(nuevoElementoLocation);

        //  Eventos_Principal.Children.Add(nuevoElementoID);//, 0, (Eventos_Principal.RowDefinitions.Count - 1));
        //  Eventos_Principal.Add(nuevoElementoName, 1, (Eventos_Principal.RowDefinitions.Count - 1));
        //  Eventos_Principal.Add(nuevoElementoHora, 2, (Eventos_Principal.RowDefinitions.Count - 1));
        //  Eventos_Principal.Add(nuevoElementoFecha, 3, (Eventos_Principal.RowDefinitions.Count - 1));
        //  Eventos_Principal.Add(nuevoElementoLocation, 4, (Eventos_Principal.RowDefinitions.Count - 1));
    }
}

