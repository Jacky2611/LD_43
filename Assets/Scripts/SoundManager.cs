﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    //created automatically
    DoubleAudioSource musicSource;
    AudioSource uiSource;


    public AudioClip initialBackgroundMusic;

    public static SoundManager instance;

    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

    // Use this for initialization
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = gameObject.AddComponent(typeof(DoubleAudioSource)) as DoubleAudioSource;
        uiSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;

        musicSource.CrossFade(initialBackgroundMusic, 1, 0);

        DontDestroyOnLoad(this);
	}


    public static void PlayNext(AudioClip clip, float delay = 0)
    {
        instance.musicSource.QueueNext(clip, delay);
    }
    
    public static void CrossfadeMusic(AudioClip clip, float fadingTime, float delayBeforeFade=0)
    {
        instance.musicSource.CrossFade(clip, 1, fadingTime, delayBeforeFade);
    }



    public static void PlayUISound(AudioClip clip)
    {
        instance.uiSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        instance.uiSource.Play(); // start the sound
    }


    //Used to play single sound clips.
    public static void PlaySingleAt(AudioClip clip, Vector3 pos)
    {
        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(instance.lowPitchRange, instance.highPitchRange);

        AudioSource asource = PlayClipAt(clip, pos);

        //Set the pitch of the audio source to the randomly chosen pitch.
        asource.pitch = randomPitch;        
    }


    //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
    public static void PlayRandomAt(AudioClip[] clips, Vector3 pos)
    {

        if (clips.Length > 0) {

            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);

            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(instance.lowPitchRange, instance.highPitchRange);


            AudioSource asource = PlayClipAt(clips[randomIndex], pos);

            //Set the pitch of the audio source to the randomly chosen pitch.
            asource.pitch = randomPitch;
        }

    }

    private static AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
    {
        GameObject tempGO = new GameObject("TempAudio"); // create the temp object
        tempGO.transform.position = pos; // set its position
        AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
        aSource.clip = clip; // define the clip
        // set other aSource properties here, if desired
        aSource.Play(); // start the sound
        Destroy(tempGO, clip.length); // destroy object after clip duration
        return aSource; // return the AudioSource reference
    }
}
