using Common;
using CustomEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameManagers
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _galleryButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
            _galleryButton.onClick.AddListener(OnGalleryButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            _startGameButton.GetComponent<Animator>().ResetTrigger("Highlighted");
            GlobalSceneTransition.SwitchToScene(SceneStorage.Select);
        }

        private void OnGalleryButtonClick()
        {
            _galleryButton.GetComponent<Animator>().ResetTrigger("Highlighted");
            GlobalSceneTransition.SwitchToScene(1);
        }

        private void OnExitButtonClick()
        {
            _exitButton.GetComponent<Animator>().ResetTrigger("Highlighted");
            Application.Quit();
        }
    }
}