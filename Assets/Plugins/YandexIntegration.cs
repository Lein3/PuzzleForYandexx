using System.Runtime.InteropServices;

public static class YandexIntegration
{
    [DllImport("__Internal")]
    public static extern void ShowFullscreenAdv();

    [DllImport("__Internal")]
    public static extern void ShowRewardedVideo();

    [DllImport("__Internal")]
    public static extern void RedirectToYandexGames();
}
