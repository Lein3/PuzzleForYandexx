using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _songs;
        private AudioSource _audioSource;
        private int _songIndex = 0;

        private void Start()
        {
            _songs = _songs.OrderBy(item => new System.Random().Next()).ToList();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _songs[_songIndex];
            _audioSource.volume = 1;
            _audioSource.Play();
            StartCoroutine(PlayNextSong());
        }

        private IEnumerator PlayNextSong()
        {
            while (true)
            {
                yield return new WaitForSeconds(_audioSource.clip.length);
                _audioSource.Stop();
                yield return new WaitForSeconds(3);
                _songIndex++;
                _songIndex %= _songs.Count;
                _audioSource.clip = _songs[_songIndex];
                _audioSource.Play();
            }
        }
    }
}