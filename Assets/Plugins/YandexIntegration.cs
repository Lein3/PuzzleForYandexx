using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

public class YandexIntegration : MonoBehaviour
{
    public static YandexIntegration Instance { get; private set; }

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdvExtern();

    [DllImport("__Internal")]
    private static extern void ShowRewardedVideoExtern();

    public static bool CanShowAdv { get; private set; } = true;

    private void Awake()
    {
        Instance = this;
    }

    public async void ShowFullscreenAdv()
    {
        if (!CanShowAdv)
            return;

        StartCoroutine(WaitYandexAdvDelay());
        PauseGame();

#if UNITY_EDITOR
        Debug.Log("ShowFullscreenAdvExtern");
        await Task.Delay(10000);
        UnpauseGame();
#else
        ShowFullscreenAdvExtern();
#endif
    }

    public async void ShowRewardedVideo()
    {
        PauseGame();
#if UNITY_EDITOR
        Debug.Log("ShowRewardedVideoExtern");
        await Task.Delay(10000);
        UnpauseGame();
#else
        ShowRewardedVideoExtern();
#endif
    }

    public void PauseGame()
    {
        FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.Pause());
    }

    //Деграется из браузера
    public void UnpauseGame()
    {
        FindObjectsOfType<AudioSource>().ToList().ForEach(item => item.UnPause());
    }

    public IEnumerator WaitYandexAdvDelay()
    {
        CanShowAdv = false;
        yield return new WaitForSecondsRealtime(10);
        CanShowAdv = true;
    }
}
