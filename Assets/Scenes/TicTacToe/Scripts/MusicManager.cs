using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private class AudioToPlay
    {
        public AudioClip AudioClip;
        public bool Played;
    }

    [SerializeField] AudioClip[] gameplayAudio;
    private AudioSource audioSource;
    private List<AudioToPlay> playlist;
    private int idxLastAudioPlayed;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        playlist = new List<AudioToPlay>();

        foreach (AudioClip clip in gameplayAudio)
        {
            playlist.Add(new AudioToPlay
            {
                AudioClip = clip,
                Played = false,
            });
        }

        idxLastAudioPlayed = 0;
    }

    void Update()
    {
        if (audioSource == null) { return; }
        if (playlist == null) { return; }
        if (playlist.Count == 0) { return; }

        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                List<AudioToPlay> audiosNotPlayed = playlist.Where( p => !p.Played).ToList();

                int idxNextAudioToPlay = Random.Range(0, audiosNotPlayed.Count);
                idxLastAudioPlayed = idxNextAudioToPlay;

                audioSource.clip = audiosNotPlayed[idxNextAudioToPlay].AudioClip;
                audioSource.Play();

                if( audiosNotPlayed.Count <= 1 )
                {
                    ResetPlaylist();
                }
            }
        }
    }

    void ResetPlaylist()
    {
        for (int i = 0; i < playlist.Count; i++)
        {
            if (i == idxLastAudioPlayed)
            {
                if( playlist.Count == 1 )
                {
                    playlist[i].Played = false;
                } else
                {
                    playlist[i].Played = true;
                }

            } else
            {
                playlist[i].Played = false;
            }
        }
    }
}
