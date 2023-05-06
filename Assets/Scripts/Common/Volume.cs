using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class Volume : MonoBehaviour
    {
        [SerializeField] private Button VolumeButton;
        [SerializeField] private Button CollapseButton;
        [SerializeField] private RectMask2D CollapsePanel;
        [SerializeField] private Slider Slider;
        [SerializeField] private Sprite EnabledSprite;
        [SerializeField] private Sprite DisabledSprite;
        [SerializeField] private Sprite CollapsedSprite;
        [SerializeField] private Sprite ExpandedSprite;

        private AudioSource _audioSource;
        private Sprite _currentSprite;
        private bool _isExpanded;
        private float _previousVolumeValue;

        private void Start()
        {
            _audioSource = GameObject.FindGameObjectWithTag("Music")?.GetComponent<AudioSource>();
            if(_audioSource == null )
            {
                this.gameObject.SetActive(false);
                return;
            }

            _isExpanded = false;
            _currentSprite = EnabledSprite;
            Slider.value = _audioSource.volume;

            if (Screen.width < 1000)
            {
                this.GetComponent<RectTransform>().anchorMin = new Vector2(0.97f, 0.97f);
                this.transform.localScale *= 2;
            }

            SceneTransition.SceneLoadStart += SceneTransition_Global_sceneLoadStart;
        }

        private void SceneTransition_Global_sceneLoadStart()
        {
            this.gameObject.SetActive(false);
        }

        public void OnVolumeButtonClick()
        {
            if (_currentSprite == EnabledSprite)
            {
                _previousVolumeValue = Slider.value;
                Slider.value = 0;
                OnVolumeSliderChanged(0);
            }

            else if (_currentSprite == DisabledSprite)
            {
                Slider.value = _previousVolumeValue;
                OnVolumeSliderChanged(_previousVolumeValue);
            }
        }

        public void OnVolumeSliderChanged(float sliderValue)
        {
            if (_audioSource is null)
                return;

            _audioSource.volume = sliderValue;
            if (sliderValue == 0)
            {
                _currentSprite = DisabledSprite;
                VolumeButton.GetComponent<Image>().sprite = DisabledSprite;
            }
            else if (sliderValue > 0 && _currentSprite == DisabledSprite)
            {
                _currentSprite = EnabledSprite;
                VolumeButton.GetComponent<Image>().sprite = EnabledSprite;
            }
        }

        public void OnExpandCollapseButtonClick()
        {
            var animator = CollapsePanel.GetComponent<Animator>();
            var buttonImage = CollapseButton.GetComponent<Image>();
            if (_isExpanded)
            {
                animator.SetTrigger("Collapse");
                buttonImage.sprite = CollapsedSprite;
                _isExpanded = false;
            }
            else
            {
                animator.SetTrigger("Expand");
                buttonImage.sprite = ExpandedSprite;
                _isExpanded = true;
            }
        }

        private void OnDestroy()
        {
            SceneTransition.SceneLoadStart -= SceneTransition_Global_sceneLoadStart;
        }
    }
}