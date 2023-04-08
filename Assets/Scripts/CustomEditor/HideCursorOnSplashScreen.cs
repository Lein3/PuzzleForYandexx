using UnityEngine;

namespace CustomEditor
{
    public class HideCursorOnSplashScreen : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void OnBeSplashScreenRuntimeMethod()
        {
            Cursor.visible = false;
        }
    }
}