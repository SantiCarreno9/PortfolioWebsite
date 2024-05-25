using BlazorApp.Models;
using BlazorApp.Services;

using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components
{
    public partial class Portfolio : ComponentBase
    {
        [Inject]
        public ProjectService ProjectService { get; set; }
        [Inject]
        public LogoService LogoService { get; set; }
        public IEnumerable<Project>? Projects { get; set; }

        private bool wasAProjectSelected = false;
        private bool isCarouselOpen = false;

        private Logos? logos;
        private HeroImage? hero;

        private IDictionary<string, object>[]? projectsParameters;

        private Dictionary<int, RenderFragment> projectCards = new Dictionary<int, RenderFragment>();
        private int currentProjectId = 0;



        protected override async Task OnInitializedAsync()
        {
            Projects = await ProjectService.GetProjects();            
            logos = await LogoService.GetLogos();
            if (Projects != null)
            {
                int projectsCount = Projects.Count();
                this.projectsParameters = new Dictionary<string, object>[projectsCount];
                for (int i = 0; i < projectsCount; i++)
                {
                    this.projectsParameters[i] = new Dictionary<string, object>();
                    this.projectsParameters[i].Add("Project", Projects.ElementAt(i));
                }
            }


            ////hero = await HeroImageService.GetHeroAsync(img => img.Name is "portfolio");
        }

        protected IEnumerable<Project> GetGroupedProjectsByCategory()
        {
            //return (IEnumerable<IGrouping<int, Project>>)(from project in Projects
            //       group project by project.Technologies into projByCatGroup
            //       orderby projByCatGroup.Key
            //       select projByCatGroup);
            return Projects;
        }

        private Project GetProject(int Id)
        {
            try
            {
                return Projects.First(p => p.Id == Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void OnItemSelected(int Id)
        {
            wasAProjectSelected = true;
            isCarouselOpen = true;
            currentProjectId = Id;
            Console.WriteLine($"{Projects.First(x => x.Id == Id).Title} was selected");
            StateHasChanged();
        }

        private int GetProjectIndexById(int Id)
        {
            return Projects.ToList().FindIndex(p => p.Id == Id);
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
