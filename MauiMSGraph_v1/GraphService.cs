using Microsoft.Graph.Auth;
using Microsoft.Graph;
using Microsoft;
using Azure.Identity;
using Microsoft.Graph.Models;
using Microsoft.Identity.Client;
using Microsoft.Kiota.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Graph.Constants;

namespace MauiMSGraph_v1
{
    internal class GraphService
    {
        private string[]? _scopes;
        private string? ClientId;
        //private const string TenantId = "d1f82296-ad39-4f41-865f-4a23e4b6b1d6";
        private GraphServiceClient? _client;

        public GraphService(string clientId, string tenantId, string[] scopes)
        {
            this.ClientId = clientId;
            this._scopes = scopes;

            InteractiveBrowserCredential credential = new InteractiveBrowserCredential(new InteractiveBrowserCredentialOptions
            {
                ClientId = this.ClientId,
                TenantId = tenantId
            });

            this._client = new GraphServiceClient(credential, this._scopes);
        }

        public async Task<User> GetMyDetailsAsync()
        {
            try
            {
                return await _client.Me.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user details: {ex}");
                return null;
            }
        }

        // revisar para poder usar el agregar el evento
        public async Task AddEvent(string subject, string emoji, DateTimeOffset expires)
        {
            var reminderEvent = new Event
            {
                Subject = String.IsNullOrEmpty(emoji) ? $"{subject}" : $"{emoji} {subject}",
                Start = new DateTimeTimeZone
                {
                    DateTime = expires.ToUniversalTime().ToString("o"),
                    TimeZone = TimeZoneInfo.Utc.Id
                },
                End = new DateTimeTimeZone
                {
                    DateTime = expires.ToUniversalTime().AddHours(1).ToString("o"),
                    TimeZone = TimeZoneInfo.Utc.Id
                }
            };

            // Add the event to the user's calendar
            await this._client!.Me.Calendar.Events.PostAsync(reminderEvent);
        }

        public async Task AddTodoItem(string subject, string emoji, DateTimeOffset expires)
        {
            // check if the reminder already exists
            var taskList = await this.GetDefaultTaskList();

            if (taskList != null)
            {
                var reminderTask = new TodoTask
                {
                    Title = String.IsNullOrEmpty(emoji) ? $"{subject}" : $"{emoji} {subject}",
                    DueDateTime = new DateTimeTimeZone
                    {
                        DateTime = expires.Date.ToString("o"),
                        TimeZone = TimeZoneInfo.Utc.Id
                    }
                };

                // Add the event to the user's calendar
                await this._client!.Me.Todo.Lists[taskList.Id].Tasks.PostAsync(reminderTask);
            }
        }

        // Get default task list from graph api
        public async Task<TodoTaskList?> GetDefaultTaskList()
        {
            try
            {
                var taskLists = await this._client!.Me.Todo.Lists.GetAsync(r =>
                {
                    r.QueryParameters.Top = 100;
                });

                if (taskLists?.Value?.Count > 0)
                {
                    // get the first task list that is the default list
                    return taskLists?.Value?.FirstOrDefault(l => l.WellknownListName == WellknownListName.DefaultList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<Event> func_postasync(Event requestBody)
        {
             var result = await _client.Me.Events.PostAsync(requestBody);

            return result;
        }
        public async Task<TodoTask> func_postasync1(TodoTask requestBody)
        {
            var result = await _client.Me.Todo.Lists[""].Tasks.PostAsync(requestBody);

            return result;
        }

        public async Task ReadEvents(Grid grid)
        {
            // Obtener la fecha de inicio y la fecha de finalización desde hoy hasta el final del año
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset start = new DateTimeOffset(now.Year, now.Month, now.Day, 0, 0, 0, now.Offset);
            DateTimeOffset end = new DateTimeOffset(now.Year, 12, 31, 23, 59, 59, now.Offset);



            var result = await _client.Me.Events.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = new string[] { "subject", "body", "bodyPreview", "organizer", "attendees", "start", "end", "location" };

            });


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///                                                                                                         ////
            ///  AQUI HAY QUE MIRAR QUE ES LO QUE SIRVE Y APROVECHARLO PARA PONERLO EN EL GRID DEL MAIN PAGE            ////
            ///                                                                                                         ////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*
            // Obtener los eventos programados desde hoy hasta el final del año
            var events = await _client.Me.Events.Request()                     
                            .Header("Prefer", "outlook.timezone=\"Pacific Standard Time\"") // Especifica la zona horaria de la solicitud
                            .QueryOptions($"startDateTime={start.ToString("o")}&endDateTime={end.ToString("o")}") // Especifica la ventana de tiempo de la solicitud
                            .Select(e => new
                            {
                                e.Subject,
                                e.Body,
                                e.BodyPreview,
                                e.Organizer,
                                e.Attendees,
                                e.Start,
                                e.End,
                                e.Location
                            })
                            .GetAsync();
            // Agregar los eventos a un Grid
            grid.RowDefinitions.Clear();
            grid.Children.Clear();
            int rowIndex = 0;

            */
            /*int rowIndex = 0;


            for (int i = 0; i < result.OdataCount; i++)
            {
                // Crear un nuevo Label para mostrar el sujeto del evento
                Label label = new Label();
                label.Text = ev.Subject;

                // Crear un nuevo Label para mostrar la hora de inicio y la hora de finalización del evento
                Label timeLabel = new Label();
                timeLabel.Text = $"{ev.Start.DateTime} - {ev.End.DateTime}";

                // Agregar los controles al Grid
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                Grid.SetRow(label, grid.RowDefinitions.Count - 1);
                Grid.SetColumn(label, 2);
                grid.Children.Add(label);

                Grid.SetRow(timeLabel, grid.RowDefinitions.Count - 1);
                Grid.SetColumn(timeLabel, 2);
                grid.Children.Add(timeLabel);

                // Incrementar el índice de fila para el siguiente evento
                rowIndex++;
            }*/ 
        } 

        internal Task<bool> HasEvent(string subject, DateTimeOffset expires)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> HasTodoItem(string subject, DateTimeOffset expires)
        {
            throw new NotImplementedException();
        }
    }
}
