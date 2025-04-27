using BlazorApp.Models;
using BlazorApp.Services;

using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components
{
    public partial class Portfolio : ComponentBase
    {
        private class ProjectCategoryInfo
        {
            public string Name { get; set; }
            public string Logo { get; set; }
            public ProjectCategory Category { get; set; }
        }
        [Inject]
        public ProjectService ProjectService { get; set; }
        [Inject]
        public LogoService LogoService { get; set; }
        public List<Project>? _projects { get; private set; }
        
        private bool isCarouselOpen = false;

        private Logos? logos;
        private HeroImage? hero;

        private IDictionary<string, object>[]? projectsParameters;
        
        private string currentProjectId;

        private ProjectCategory _currentProjectCategory = ProjectCategory.All;
        private ProjectCategoryInfo[] _projectCategories;        

        protected override async Task OnInitializedAsync()
        {
            logos = await LogoService.GetLogos();
            var categoriesInfo = Enum.GetValues(typeof(ProjectCategory));
            _projectCategories = new ProjectCategoryInfo[categoriesInfo.Length];
            for (int i = 0; i < _projectCategories.Length; i++)
            {
                _projectCategories[i] = new ProjectCategoryInfo
                {
                    Name = ((ProjectCategory)i).ToString(),
                    Logo = logos.GetLogoByCategory((ProjectCategory)i),
                    Category = (ProjectCategory)i
                };
            }
            await SetProjectCategory(ProjectCategory.All);
        }

        private async Task SetProjectCategory(ProjectCategory category)
        {
            _currentProjectCategory = category;
            _projects = await ProjectService.GetProjectsByCategory(category);
            if (_projects != null)
            {
                int projectsCount = _projects.Count();
                this.projectsParameters = new Dictionary<string, object>[projectsCount];
                for (int i = 0; i < projectsCount; i++)
                {
                    this.projectsParameters[i] = new Dictionary<string, object>();
                    this.projectsParameters[i].Add("Project", _projects[i]);
                }
            }
        }        

        private void OnItemSelected(string Id)
        {            
            isCarouselOpen = true;
            currentProjectId = Id;                           
            StateHasChanged();
        }

        private int GetSelectedProjectIndex()
        {
            var index = _projects?.FindIndex(p => p.Id == currentProjectId) ?? 0;
            if (index == -1)
                index = 0;
            return index;            
        }
    }
}
