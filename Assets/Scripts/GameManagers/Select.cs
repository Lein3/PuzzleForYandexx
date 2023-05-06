using Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagers
{
    public class Select : MonoBehaviour
    {
        [SerializeField] private LevelPlaceholder _levelPlaceHolderPrefab;
        [SerializeField] private GridLayoutGroup _layoutGroup;
        [SerializeField] private List<Model.Level> _levels;

        private void Start()
        {
            _levels.Where(item => item.GridSize.x == 3).ToList().ForEach(item => item.Unlock());
            ClearLayoutGroup();
            foreach (var level in _levels)
            {
                var kek = Instantiate(_levelPlaceHolderPrefab);
                kek.Initialize(level);
                kek.transform.SetParent(_layoutGroup.transform);
                kek.GetComponent<RectTransform>().localScale = Vector3.one;
            }

            YandexIntegration.ShowFullscreenAdv();
        }

        public void ClearLayoutGroup()
        {
            for (int i = _layoutGroup.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(_layoutGroup.transform.GetChild(i).gameObject);
        }
    }
}