using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeMusic : MonoBehaviour
{
    //set in inspector
    [SerializeField] private AudioClip sandSpear;
    [SerializeField] private AudioClip coconutMall;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void SandSpear()
    {
        source.clip = sandSpear;
        source.Play();
    }

    public void CoconutMall()
    {
        source.clip = coconutMall;
        source.Play();
    }
}
