using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private AudioClip _explosionAudio;
    private AudioSource _audioSource;

    void Start()
    {
        //_audioSource.clip = _explosionAudio;
        //_audioSource.Play();

        Destroy(this.gameObject, 2.5f);
    }

}
