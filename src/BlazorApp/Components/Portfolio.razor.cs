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

        private Dictionary<int, RenderFragment> projectCards = new Dictionary<int, RenderFragment>();
        private int currentProjectId = 0;

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

        //protected override async Task OnInitializedAsync()
        //{
        //    Projects = await ProjectService.GetProjects();            
        //    logos = await LogoService.GetLogos();



        //    ////hero = await HeroImageService.GetHeroAsync(img => img.Name is "portfolio");
        //}

        protected IEnumerable<Project> GetGroupedProjectsByCategory()
        {
            //return (IEnumerable<IGrouping<int, Project>>)(from project in Projects
            //       group project by project.Technologies into projByCatGroup
            //       orderby projByCatGroup.Key
            //       select projByCatGroup);
            return _projects;
        }

        private Project GetProject(int Id)
        {
            try
            {
                //return _projects.First(p => p.Id == Id);
                return _projects.First();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OnItemSelected(string Id)
        {            
            isCarouselOpen = true;
            //currentProjectId = Id;
            //Console.WriteLine($"{_projects.First(x => x.Id == Id).Title} was selected");
            StateHasChanged();
        }

        private int GetProjectIndexById(int Id)
        {
            return Id;
            //return _projects.ToList().FindIndex(p => p.Id == Id);
        }

        private void SelectNext()
        {
            Console.WriteLine("Next");
        }

        private void SelectPrevious()
        {
            Console.WriteLine("Previous");
        }
    }
}
