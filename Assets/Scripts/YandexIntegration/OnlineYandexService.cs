using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.YandexIntegration
{
    internal class OnlineYandexService : IYandexService
    {
        [DllImport("__Internal")]
        private static extern void ShowFullscreenAdvExtern();

        [DllImport("__Internal")]
        private static extern void ShowRewardedVideoExtern();

        public bool CanShowAdv { get; private set; } = true;

        public async void ShowFullscreenAdv()
        {
            if (!CanShowAdv)
                return;

            PauseGame();
            ShowFullscreenAdvExtern();
            await WaitYandexAdvDelay();
        }

        public void ShowRewardedVideo()
        {
            PauseGame();
            ShowRewardedVideoExtern();
        }

        public void PauseGame()
        {
            GameObject.FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.Pause());
        }

        public void UnpauseGame()
        {
            GameObject.FindObjectsOfType<AudioSource>().Where(item => !item.isPlaying).ToList().ForEach(item => item.Play());
            GameObject.FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.UnPause());
        }

        public async Task WaitYandexAdvDelay()
        {
            CanShowAdv = false;
            await Task.Delay(61000);
            CanShowAdv = true;
        }
    }
}
