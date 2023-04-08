using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class GlobalSceneTransition : MonoBehaviour
    {
        public static event Action SceneLoadStart;
        public static GlobalSceneTransition Instance { get; private set; }
        public static bool IsSceneLoading { get; private set; }
        public static bool ShouldPlayOpeningAnimation { get; private set; } = false;

        private Animator componentAnimator;
        private AsyncOperation loadingSceneOperation;
        private Scene currentScene;

        private void Start()
        {
            Cursor.visible = true;
            Instance = this;
            componentAnimator = GetComponent<Animator>();
            currentScene = SceneManager.GetActiveScene();

            if (ShouldPlayOpeningAnimation)
            {
                componentAnimator.SetTrigger("SceneAppear");
                ShouldPlayOpeningAnimation = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !IsSceneLoading && currentScene.buildIndex == 2)
            {
                SwitchToScene(0);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && !IsSceneLoading && currentScene.buildIndex != 0)
            {
                SwitchToPreviousScene();
            }
        }

        public static void ExitGame()
        {
            Application.Quit();
        }

        public static void SwitchToScene(int sceneIndex)
        {
            if (IsSceneLoading)
                return;

            IsSceneLoading = true;
            SceneLoadStart?.Invoke();
            Instance.componentAnimator.SetTrigger("SceneDissapear");
            Instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex);
            Instance.loadingSceneOperation.allowSceneActivation = false;
        }

        public static void SwitchToScene(string sceneName)
        {
            if (IsSceneLoading)
                return;

            IsSceneLoading = true;
            SceneLoadStart?.Invoke();
            Instance.componentAnimator.SetTrigger("SceneDissapear");
            Instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            Instance.loadingSceneOperation.allowSceneActivation = false;
        }

        public void SwitchToNextScene()
        {
            if (IsSceneLoading)
                return;

            IsSceneLoading = true;
            SceneLoadStart?.Invoke();
            Instance.componentAnimator.SetTrigger("SceneDissapear");
            Instance.loadingSceneOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex + 1);
            Instance.loadingSceneOperation.allowSceneActivation = false;
        }

        public void SwitchToPreviousScene()
        {
            if (IsSceneLoading)
                return;

            IsSceneLoading = true;
            SceneLoadStart?.Invoke();
            Instance.componentAnimator.SetTrigger("SceneDissapear");
            Instance.loadingSceneOperation = SceneManager.LoadSceneAsync(currentScene.buildIndex - 1);
            Instance.loadingSceneOperation.allowSceneActivation = false;
        }

        public async void OnAnimationOver()
        {
            await Task.Delay(700);
            IsSceneLoading = false;
            ShouldPlayOpeningAnimation = true;
            loadingSceneOperation.allowSceneActivation = true;
        }
    }
}