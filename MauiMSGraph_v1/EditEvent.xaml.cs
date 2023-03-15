namespace MauiMSGraph_v1;

public partial class EditEvent : ContentPage
{
	public EditEvent()
	{
		InitializeComponent();
	}

    public async void Return_clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void Accept_Clicked(object sender, EventArgs e)
    {

    }
}