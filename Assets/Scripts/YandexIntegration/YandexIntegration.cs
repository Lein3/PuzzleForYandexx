using UnityEngine;
using Assets.Scripts.YandexIntegration;

public class YandexIntegration : MonoBehaviour
{
    private static IYandexService Instance { get; set; }

    private void Awake()
    {
#if UNITY_EDITOR
        Instance ??= new OfflineYandexService();
#else
        Instance ??= new OnlineYandexService();
#endif
    }

    public static void ShowFullscreenAdv()
    {
        Instance?.ShowFullscreenAdv();
    }

    public static void ShowRewardedVideo()
    {
        Instance?.ShowRewardedVideo();
    }

    public void PauseGame()
    {
        Instance?.PauseGame();
    }

    public void UnpauseGame()
    {
        Instance?.UnpauseGame();
    }
}
