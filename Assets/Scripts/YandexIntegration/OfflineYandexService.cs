using GameManagers;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.YandexIntegration
{
    internal class OfflineYandexService : IYandexService
    {
        public bool CanShowAdv { get; private set; } = true;

        public bool IsMobile => false;

        public async void ShowFullscreenAdv()
        {
            if (!CanShowAdv)
            {
                Debug.Log("Cannot Show Adv by Delay");
                return;
            }

            PauseGame();
            Debug.Log("ShowFullscreenAdvShowed");
            await Task.Delay(3000);
            Debug.Log("ShowFullscreenAdvEnded");
            UnpauseGame();
            AllowShowAdv();
        }

        public async void ShowRewardedVideo()
        {
            PauseGame();
            Debug.Log("ShowRewardedVideoShowed");
            await Task.Delay(3000);
            Debug.Log("ShowRewardedVideoEnded");
            GameObject.FindObjectOfType<Puzzle>().ForceComplete();
            UnpauseGame();
        }

        public void PauseGame()
        {
            GameObject.FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.Pause());
        }

        public void UnpauseGame()
        {
            GameObject.FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.Play());
            GameObject.FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.UnPause());
        }

        public async void AllowShowAdv()
        {
            CanShowAdv = false;
            await Task.Delay(10000);
            CanShowAdv = true;
        }
    }
}
