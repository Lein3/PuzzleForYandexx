using Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagers
{
    public class Select : MonoBehaviour
    {
        [SerializeField] private LevelPlaceholderInitializer _levelPlaceHolderPrefab;
        [SerializeField] private GridLayoutGroup _layoutGroup;
        [SerializeField] private List<Model.Level> _levels;

        private void Start()
        {
            _levels[0].Unlock();
            ClearLayoutGroup();
            foreach (var level in _levels)
            {
                var kek = Instantiate(_levelPlaceHolderPrefab);
                kek.Initialize(level);
                kek.transform.SetParent(_layoutGroup.transform);
                kek.GetComponent<RectTransform>().localScale = Vector3.one;
            }
        }

        public void ClearLayoutGroup()
        {
            for (int i = _layoutGroup.transform.childCount - 1; i >= 0; i--)
                DestroyImmediate(_layoutGroup.transform.GetChild(i).gameObject);
        }
    }
}