using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Microsoft.Kiota.Abstractions;
using Microsoft.Maui.Graphics.Text;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using System;

namespace MauiMSGraph_v1;

public partial class NewEvent : ContentPage
{

    private GraphService graphService;
    private string mensaje;
    private int wheremsg = 0; // donde 1 va a ser TODO, donde 2 va a ser MICROSOFT CALENDAR y donde 3 va a ser BOTH;

    public NewEvent()
	{
		InitializeComponent();
	}

    public async void Return_clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private static async Task AddCalendarEvent(GraphService graphService, string subject, string emoji, DateTimeOffset expires)
    {

        var settings = Settings.LoadSettings();
        if (graphService == null)
        {
            graphService = new GraphService(settings.ClientId!, settings.TenantId!, settings.Scopes!);
        }

        if (!await graphService.HasEvent(subject, expires))
        {
           // await graphService.AddEvent(subject, emoji, expires);
            Console.WriteLine($"Reminder '{subject}' on {expires} was added");
        }
        else
        {
            Console.WriteLine($"Reminder '{subject}' on {expires} already exists");
        }
    }

    private static async Task AddTodoItem(GraphService graphService, string subject, string emoji, DateTimeOffset expires)
    {

        var settings = Settings.LoadSettings();
        if (graphService == null)
        {
            graphService = new GraphService(settings.ClientId!, settings.TenantId!, settings.Scopes!);
        }

        if (!await graphService.HasTodoItem(subject, expires))
        {
            await graphService.AddTodoItem(subject, emoji, expires);
            Console.WriteLine($"Task '{subject}' on {expires} was added");
        }
        else
        {
            Console.WriteLine($"Task '{subject}' on {expires} already exists");
        }
    }

    private void entry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void entry_Completed(object sender, EventArgs e)
    {
         mensaje = entry.Text;
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        //var graphClient = new GraphServiceClient(requestAdapter);

        TimeSpan selectedTime = Timepicker.Time;
        DateTime selectedDate = Datepicker.Date;
        DateTimeOffset myDateTimeOffset = new DateTimeOffset(selectedDate).Add(selectedTime);

        var subject = mensaje;

        var settings = Settings.LoadSettings();
        if (graphService == null)
        {
            graphService = new GraphService(settings.ClientId!, settings.TenantId!, settings.Scopes!);
        }

        
        if(wheremsg == 2 || wheremsg == 3)
        {
            var requestBody = new Event
            {
                Subject = subject,
                Start = new DateTimeTimeZone
                {
                    DateTime = myDateTimeOffset.ToString(),
                    TimeZone = "UTC",
                },
                End = new DateTimeTimeZone
                {
                    DateTime = myDateTimeOffset.ToString(),
                    TimeZone = "UTC",
                },
            };

            var result = graphService.func_postasync(requestBody);
        }
        else if(wheremsg == 1 || wheremsg == 3)
        {
            var requestBody = new TodoTask
            {
                Title = subject + myDateTimeOffset.ToString(),
            };
            var result = await graphService.func_postasync1(requestBody);
        }
        
    }

    private void ToDo_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        wheremsg = 1;
    }

    private void MicrosoftCalen_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        wheremsg = 2;
    }

    private void Both__CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        wheremsg = 3;
    }
}