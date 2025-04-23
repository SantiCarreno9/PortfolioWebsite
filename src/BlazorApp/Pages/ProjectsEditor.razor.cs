using System.Text;
using System.Text.Json;

using BlazorApp.Models;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
namespace BlazorApp.Pages
{
    public partial class ProjectsEditor
    {
        [Inject]
        public IWebAssemblyHostEnvironment Environment { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private string jsonContent = string.Empty;
        private JsonDocument? document;
        private List<Project>? _projects;

        protected override Task OnInitializedAsync()
        {
            if (!Environment.IsDevelopment())
            {
                NavigationManager.NavigateTo("/");
                return Task.CompletedTask;
            }

            return base.OnInitializedAsync();
        }

        private async Task LoadJsonFile(InputFileChangeEventArgs e)
        {
            var file = e.File;
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            jsonContent = await reader.ReadToEndAsync();

            _projects = JsonSerializer.Deserialize<List<Project>>(jsonContent);
            // Optional: parse and manipulate
            // document = JsonDocument.Parse(jsonContent);
        }

        private async Task DownloadJson()
        {
            var bytes = Encoding.UTF8.GetBytes(jsonContent);
            var fileName = "projects.json";

            using var stream = new MemoryStream(bytes);
            using var streamRef = new DotNetStreamReference(stream);
            await JSRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }

        private async Task SaveChanges()
        {
            if (_projects == null)
                _projects = new List<Project>();
            var existingProject = _projects.FindIndex(p => p.Id == project.Id);
            if (existingProject != -1)
                _projects[existingProject] = project;
            else _projects.Add(project);

            jsonContent = JsonSerializer.Serialize(_projects);

            project = new Project();
        }

        private void SelectProject(string id)
        {
            this.project = _projects?.FirstOrDefault(x => x.Id == id) ?? new Project();
            StateHasChanged();
        }

    }
}
