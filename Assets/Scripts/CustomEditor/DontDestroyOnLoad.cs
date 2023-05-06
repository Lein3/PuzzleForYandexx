using UnityEngine;

namespace CustomEditor
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            foreach (var @object in FindObjectsOfType<DontDestroyOnLoad>())
                if (@object.name == this.gameObject.name && @object != this)
                    Destroy(this.gameObject);

            DontDestroyOnLoad(this.gameObject);
        }
    }
}