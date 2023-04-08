using UnityEngine;

namespace Model
{
    [CreateAssetMenu]
    public class Level : ScriptableObject
    {
        [SerializeField] private Sprite _image;
        [SerializeField] private Sprite _background;
        [SerializeField] private Vector2 _gridSize;
        [SerializeField] private bool _unlocked;
        [SerializeField] private bool _completed;
        [SerializeField] private Level _nextLevel;

        public Sprite Image { get => _image; }

        public Sprite Background { get => _background; }

        public Vector2 GridSize { get => _gridSize; set => _gridSize = value; }

        public bool Unlocked { get => _unlocked; set => _unlocked = value; }

        public bool Completed { get => _completed; set => _completed = value; }

        public Level NextLevel { get => _nextLevel; set => _nextLevel = value; }

        private void Awake()
        {
            _unlocked = PlayerPrefs.GetInt(this.name + "Unlocked") == 1;
            _completed = PlayerPrefs.GetInt(this.name + "Completed") == 1;
        }

        public void Unlock()
        {
            PlayerPrefs.SetInt(this.name + "Unlocked", 1);
            _unlocked = true;
        }

        public void Complete()
        {
            PlayerPrefs.SetInt(this.name + "Completed", 1);
            _completed = true;
        }
    }
}