using System.Threading.Tasks;

namespace Assets.Scripts.YandexIntegration
{
    public interface IYandexService
    {
        public bool CanShowAdv { get; }

        public bool IsMobile { get; }

        public void ShowFullscreenAdv();

        public void ShowRewardedVideo();

        public void PauseGame();

        public void UnpauseGame();

        public void AllowShowAdv();
    }
}
