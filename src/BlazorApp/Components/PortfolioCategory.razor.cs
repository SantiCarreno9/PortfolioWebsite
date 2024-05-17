using System.Net.Http.Json;

using BlazorApp.Models;
using BlazorApp.Services;

using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components
{
    public partial class PortfolioCategory : ComponentBase
    {
        [Parameter, EditorRequired]
        public required HttpClient Http { get; set; }

        [Parameter, EditorRequired]
        public required HeroImageService HeroImageService { get; set; }

        private string containerStyle = "custom-container unity";

        private HeroImage? hero;

        private PortfolioCategoryInfo? portfolioCategoryInfo;        
        private IDictionary<string, object>[]? projectsParameters;

        

        protected override async Task OnInitializedAsync()
        {
            portfolioCategoryInfo = new PortfolioCategoryInfo
            {
                ProjectsInfoPath = Path.Combine(GlobalValues.PortfolioFolderPath, "unity", "unity-projects.json"),
                Logo = "/images/icons/unity-logo.png"
            };
            Project[]? projects = await Http.GetFromJsonAsync<Project[]>(portfolioCategoryInfo.ProjectsInfoPath);
            if (projects != null)
            {
                this.projectsParameters = new Dictionary<string, object>[projects.Length];                
                for (int i = 0; i < projects.Length; i++)
                {
                    this.projectsParameters[i] = new Dictionary<string, object>();
                    this.projectsParameters[i].Add("Project", projects[i]);
                }                
            }


            //hero = await HeroImageService.GetHeroAsync(img => img.Name is "portfolio");
        }

    }
}
