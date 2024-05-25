namespace BlazorApp
{
    public static class GlobalValues
    {
        public static string JsonPath { get; private set; } = "sample-data";
        public static string PortfolioFolderPath { get; private set; } = "portfolio";
        public static string ProjectsFolderPath { get; private set; } = Path.Combine(PortfolioFolderPath, "unity", "unity-projects.json");
    }
}
