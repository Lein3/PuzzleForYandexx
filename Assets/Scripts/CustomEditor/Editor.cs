using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CustomEditor
{
    #if UNITY_EDITOR
    public class CustomEditor : EditorWindow
    {
        [MenuItem("Game/ReorganizeLevels")]
        public static void ReorganizeLevels()
        {
            var levels = Resources.LoadAll<Model.Level>("Levels");
            levels = levels.OrderBy(item => Convert.ToInt32(item.name)).ToArray();
            int kek = 3;

            for (int i = 0; i < levels.Length - 2; i++)
            {
                levels[i].NextLevel = levels[i + 1];
                levels[i].Unlocked = false;
                levels[i].Completed = false;
                levels[i].GridSize = new Vector2(kek, kek);
                kek++;

                if (kek % 9 == 0)
                    kek = 3;
            }
        }
    }
    #endif
}