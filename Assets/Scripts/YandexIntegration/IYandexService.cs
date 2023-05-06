using System.Threading.Tasks;

namespace Assets.Scripts.YandexIntegration
{
    public interface IYandexService
    {
        public bool CanShowAdv { get; }

        public void ShowFullscreenAdv();

        public void ShowRewardedVideo();

        public void PauseGame();

        public void UnpauseGame();

        public Task WaitYandexAdvDelay();
    }
}
