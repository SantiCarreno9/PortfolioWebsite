using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
namespace BlazorApp.Pages
{
    public partial class ProjectsEditor
    {
        [Inject]
        public IWebAssemblyHostEnvironment Environment { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override Task OnInitializedAsync()
        {
            if (!Environment.IsDevelopment())
            {
                NavigationManager.NavigateTo("/");
                return Task.CompletedTask;                
            }
            
            return base.OnInitializedAsync();
        }
    }
}
