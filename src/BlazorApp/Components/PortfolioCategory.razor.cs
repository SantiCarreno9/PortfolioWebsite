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

        private Category currentCategory = Category.Unity;
        private string containerStyle = "custom-container unity";

        private HeroImage? hero;

        private RenderFragment[]? projectCardsUnity;
        private RenderFragment[]? projectCardsUnreal;
        private RenderFragment[]? projectCardsDotNet;

        protected override async Task OnInitializedAsync()
        {
            Project[] unityProjects = await Http.GetFromJsonAsync<Project[]>(GlobalValues.FolderPath + "unity-projects.json");
            if (unityProjects != null)            
                GenerateCards(unityProjects, out projectCardsUnity);

            Project[] unrealProjects = await Http.GetFromJsonAsync<Project[]>(GlobalValues.FolderPath + "unreal-projects.json");
            if (unrealProjects != null)
                GenerateCards(unrealProjects, out projectCardsUnreal);

            Project[] dotnetProjects = await Http.GetFromJsonAsync<Project[]>(GlobalValues.FolderPath + "dotnet-projects.json");
            if (dotnetProjects != null)
                GenerateCards(dotnetProjects, out projectCardsDotNet);
            //hero = await HeroImageService.GetHeroAsync(img => img.Name is "portfolio");
        }

        private void ChangePlatform(Category category)
        {
            switch (category)
            {
                case Category.Unity:
                    containerStyle = "custom-container unity";
                    break;
                case Category.Unreal:
                    containerStyle = "custom-container unreal";
                    break;
                case Category.Dotnet:
                    containerStyle = "custom-container dotnet";
                    break;
                default:
                    break;
            }

            currentCategory = category;
        }
    }
}
