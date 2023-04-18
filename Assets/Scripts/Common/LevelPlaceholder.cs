using CustomEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class LevelPlaceholder : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _image;
        [SerializeField] private Image _lockFade;
        [SerializeField] private Image _lockedImage;
        [SerializeField] private Image _completedImage;

        private Model.Level _level;

        public void Initialize(Model.Level level)
        {
            _level = level;

            _button.enabled = level.Unlocked;
            _button.animator.enabled = level.Unlocked;
            _image.sprite = level.Image;
            _lockFade.enabled = !level.Unlocked;
            _lockedImage.enabled = !level.Unlocked;
            _completedImage.enabled = level.Completed;

            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            GameManagers.Level.CurrentLevel = _level;
            SceneTransition.SwitchToScene(SceneStorage.Level);
        }
    }
}