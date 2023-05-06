using System.Collections;
using UnityEngine;

public class PlayMusicDelayed : MonoBehaviour
{
    private AudioSource _audioSource;
    private bool _startAdvShowed = false;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayDelayed());
    }

    private void StartAdvShowed()
    {
        _startAdvShowed = true;
    }

    private IEnumerator PlayDelayed()
    {
        yield return new WaitForSecondsRealtime(5);
        if (!_startAdvShowed)
            _audioSource.Play();
    }
}
