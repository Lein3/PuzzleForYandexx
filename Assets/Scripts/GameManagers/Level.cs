using Common;
using CustomEditor;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagers
{
    public class Level : MonoBehaviour
    {
        public static Model.Level CurrentLevel { get; set; }

        public static int CompletedLevelsCount { get; set; } = 0;

        [SerializeField] private Button _gallerylButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _watchAddButton;
        [SerializeField] private RawImage _image;
        [SerializeField] private Image _background;
        [SerializeField] private Puzzle _puzzle;

        void Start()
        {
            if (YandexManager.IsMobileDevice)
            {
                AdaptForMobile();
            }

            SceneTransition.SceneLoadStart += SceneTransition_Global_sceneLoadStart;
            _gallerylButton.onClick.AddListener(ToGallery);
            _nextLevelButton.onClick.AddListener(LoadNextLevel);
            _watchAddButton.onClick.AddListener(WatchAdd);

            _image.texture = CurrentLevel.Image.texture;
            _background.sprite = CurrentLevel.Background;

            _puzzle.imageToSplit = _image;
            _puzzle.gridSize = CurrentLevel.GridSize;

            if (CompletedLevelsCount % 3 == 0 && CompletedLevelsCount != 0)
            {
                YandexManager.ShowFullscreenAdv();
            }
        }

        private void SceneTransition_Global_sceneLoadStart()
        {
            _gallerylButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _watchAddButton.gameObject.SetActive(false);
            SceneTransition.SceneLoadStart -= SceneTransition_Global_sceneLoadStart;
        }

        private void ToGallery()
        {
            SceneTransition.SwitchToScene(SceneStorage.Select);
        }    

        private void LoadNextLevel()
        {
            if (CurrentLevel.NextLevel != null)
            {
                CurrentLevel = CurrentLevel.NextLevel;
                SceneTransition.SwitchToScene(SceneStorage.Level);
            }
            else
            {
                SceneTransition.SwitchToScene(SceneStorage.Select);
            }    
        }

        private void WatchAdd()
        {
            YandexManager.ShowRewardedVideo();
        }

        public void AdaptForMobile()
        {
            _puzzle.transform.Find("ImageObject").GetComponent<RectTransform>().localScale *= new Vector2(1.3f, 1);
            _puzzle.transform.Find("GridObject").GetComponent<RectTransform>().localScale *= new Vector2(1.3f, 1);
        }
    }
}
