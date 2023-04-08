using Common;
using CustomEditor;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagers
{
    public class Level : MonoBehaviour
    {
        public static Model.Level CurrentLevel { get; set; }

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _gallerylButton;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _watchAddButton;
        [SerializeField] private RawImage _image;
        [SerializeField] private Image _background;
        [SerializeField] private Puzzle _puzzle;

        void Start()
        {
            GlobalSceneTransition.SceneLoadStart += SceneTransition_Global_sceneLoadStart;
            _mainMenuButton.onClick.AddListener(ToMainMenu);
            _gallerylButton.onClick.AddListener(ToGallery);
            _nextLevelButton.onClick.AddListener(LoadNextLevel);
            _watchAddButton.onClick.AddListener(WatchAdd);

            _image.texture = CurrentLevel.Image.texture;
            _background.sprite = CurrentLevel.Background;

            _puzzle.imageToSplit = _image;
            _puzzle.gridSize = CurrentLevel.GridSize;
        }

        private void SceneTransition_Global_sceneLoadStart()
        {
            _mainMenuButton.gameObject.SetActive(false);
            _gallerylButton.gameObject.SetActive(false);
            _nextLevelButton.gameObject.SetActive(false);
            _watchAddButton.gameObject.SetActive(false);
            GlobalSceneTransition.SceneLoadStart -= SceneTransition_Global_sceneLoadStart;
        }

        private void ToMainMenu()
        {
            GlobalSceneTransition.SwitchToScene(SceneStorage.MainMenu);
        }

        private void ToGallery()
        {
            GlobalSceneTransition.SwitchToScene(SceneStorage.Select);
        }    

        private void LoadNextLevel()
        {
            if (CurrentLevel.NextLevel != null)
            {
                CurrentLevel = CurrentLevel.NextLevel;
                GlobalSceneTransition.SwitchToScene(SceneStorage.Level);
            }
            else
            {
                GlobalSceneTransition.SwitchToScene(SceneStorage.Select);
            }    
        }

        private void WatchAdd()
        {
            _puzzle.ForceComplete();
        }
    }
}
