using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMan : MonoBehaviour
{
    public static MusicMan instance;

    public AudioSource music;

    public List<AudioClip> musics;

    private int num;
    void PlayMusic(int num = 0)
    {
        music.Play();
        music.playOnAwake = true;
    }

    public void ChangeMusic()
    {
        if(!music.isPlaying)
        {
            num = Mathf.CeilToInt(Random.Range(0, 3));
            music.clip = musics[num];
            music.Play();

        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    private void Update()
    {
        ChangeMusic();
    }
}
