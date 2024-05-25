using BlazorApp.Models;

using Microsoft.AspNetCore.Components;

using RazorSCLibrary;
using RazorSCLibrary.Interfaces;

namespace BlazorApp.Components
{
    public partial class ProjectCard : ComponentBase, ICarouselComponent
    {
        [Inject]
        JsMethods JsMethods { get; set; }

        [Parameter]
        public Project Project { get; set; }
        [Parameter]
        public Logos Logos { get; set; }

        private List<(MediaContentType, string)> mediaContent = new List<(MediaContentType, string)>();

        private (MediaContentType, string) currentMediaContent;
        private int currentMediaIndex = 0;

        private ElementReference videoPlayer;
        private ElementReference shortVideoPlayer;
        private ElementReference iFramePlayer;

        protected override void OnInitialized()
        {
            if (Project.MediaContent == null)
                return;

            for (int i = 0; i < 4; i++)
            {
                if (!Project.MediaContent.ContainsKey(i))
                    continue;
                foreach (var item in Project.MediaContent[i])
                {
                    mediaContent.Add(((MediaContentType)i, item));
                }
            }
            SelectContent(0);
        }

        private void SelectContent(int index)
        {
            currentMediaContent.Item1 = mediaContent[index].Item1;
            currentMediaContent.Item2 = mediaContent[index].Item2;
            currentMediaIndex = index;
        }    

        public async void StartRendering()
        {
            switch (currentMediaContent.Item1)
            {
                case MediaContentType.ShortVideo:
                    await JsMethods.PlayVideo(shortVideoPlayer);
                    break;
                default:
                    StateHasChanged();
                    break;
            }
        }

        public async void StopRendering()
        {
            SelectContent(0);
            switch (currentMediaContent.Item1)
            {
                case MediaContentType.ShortVideo:
                    await JsMethods.PauseVideo(shortVideoPlayer);                    
                    break;
                case MediaContentType.Video:
                    await JsMethods.PauseVideo(videoPlayer);                    
                    break;
                case MediaContentType.WebVideo:
                    await JsMethods.PauseVideo(iFramePlayer,isIFrame:true);                    
                    break;
            }
        }
    }

}
