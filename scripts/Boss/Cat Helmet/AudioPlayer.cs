using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	public AudioClip[] audioClips;

	AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
		audioSource = GetComponent<AudioSource>();
    }

	public void Play(int clip)
	{
		audioSource.clip = audioClips[clip];
		audioSource.Play();
	}
}
