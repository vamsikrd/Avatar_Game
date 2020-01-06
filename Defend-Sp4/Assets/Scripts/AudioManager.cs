using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private AudioSource source;
    private float soundVolume;

    

    public float SoundVolume
    {
        get { return soundVolume; }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        VolumeControlling();
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        VolumeControlling();
    }

    private void VolumeControlling()
    {
        source.volume = musicSlider.value;
        soundVolume = soundSlider.value;
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
