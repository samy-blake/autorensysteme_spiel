using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Demonstriert, wie man über ein Script den Audio-Mixer manipulieren kann.
public class Demo_AudioMixer : MonoBehaviour
{
    public AudioMixer mixer;
    [Range(-80f, 0)] public float volume = 0f;

	private void Update ()
    {
        mixer.SetFloat("BGM_Volume", volume);
	}
}
