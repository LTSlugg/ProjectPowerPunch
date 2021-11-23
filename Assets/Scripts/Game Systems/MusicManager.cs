using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]private AudioClip[] musicClips;
    [SerializeField]private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = musicClips[0];
        PlayMusic();
    }
    
    //TODO: PRIORITY-5 Add Random AudioClip on Restart

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
