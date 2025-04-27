using System.Text;
using System.Text.Json;

using BlazorApp.Components;
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
        private List<Project>? _projects;
        private DynamicTextList _keywords { get; set; }
        private DynamicTextList _images { get; set; }
        private DynamicTextList _shortVideos { get; set; }
        private DynamicTextList _videos { get; set; }
        private DynamicTextList _webVideos { get; set; }        
        private ProjectCard? _projectCard { get; set; }


        protected override Task OnInitializedAsync()
        {
            if (!Environment.IsDevelopment())
            {
                NavigationManager.NavigateTo("/");
                return Task.CompletedTask;
            }

            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {            
            await base.OnAfterRenderAsync(firstRender);
            await RefreshProjectCard();
        }        

        private async Task RefreshProjectCard()
        {
            await Task.Delay(1000).ContinueWith(_ =>
            {
                _projectCard?.Rerender();
            });
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

            Reset();
        }

        private void SelectProject(string id)
        {
            Reset();
            var foundProject = _projects?.FirstOrDefault(x => x.Id == id);
            if (foundProject != null)
                project = new Project
                {
                    Id = foundProject.Id,
                    Title = foundProject.Title,
                    Description = foundProject.Description,
                    Category = foundProject.Category,
                    Keywords = foundProject.Keywords,
                    MediaContentDirectories = foundProject.MediaContentDirectories
                };
            else project = new Project();
            
            StateHasChanged();
        }

        private void Reset()
        {
            project = new Project();
            _keywords?.RemoveAllItems();
            _images?.RemoveAllItems();
            _shortVideos?.RemoveAllItems();
            _videos?.RemoveAllItems();
            _webVideos?.RemoveAllItems();
            _projectCard?.Reset();
        }

    }
}
