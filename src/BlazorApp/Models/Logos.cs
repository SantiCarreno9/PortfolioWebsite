namespace BlazorApp.Models
{
    public class Logos
    {
        public string Unity { get; set; }
        public string Unreal { get; set; }
        public string Dotnet { get; set; }

        public string GetLogoByCategory(ProjectCategory category)
        {
            //switch (category)
            //{
            //    case Category.Unity:
            //        return Unity;
            //    case Category.Unreal:
            //        return Unreal;
            //    case Category.Dotnet:
            //        return Dotnet;
            //    default:
            //        return null;
            //}
            return null;
        }
    }
}
