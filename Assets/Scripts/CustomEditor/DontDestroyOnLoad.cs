using UnityEngine;

namespace CustomEditor
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            foreach (var @object in FindObjectsOfType<DontDestroyOnLoad>())
                if (@object.name == gameObject.name && @object != this)
                    Destroy(gameObject);

            DontDestroyOnLoad(this.gameObject);
        }
    }
}