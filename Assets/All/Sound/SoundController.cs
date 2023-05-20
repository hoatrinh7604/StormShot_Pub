using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController _instance;
    public static SoundController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //
    [SerializeField] GameObject prefab;

    public AudioClip[] bg;
    private AudioSource bgSource;

    public AudioClip gameOver;
    private AudioSource gameOverSource;  
    
    public AudioClip firing;
    private AudioSource firingSource;    

    public AudioClip bang;
    private AudioSource bangSource;

    public AudioClip explosion;
    private AudioSource explosionSource;

    public AudioClip playerDeath;
    private AudioSource playerDeathSource;

    public AudioClip enemySmile;
    private AudioSource enemySmileSource;

    public AudioClip enemyDeath;
    private AudioSource enemyDeathSource;

    public AudioClip bossSmile;
    private AudioSource bossSmileSource;

    public AudioClip bossDeath;
    private AudioSource bossDeathSource;

    public AudioClip hostageSmile;
    private AudioSource hostageSmileSource;

    public AudioClip hostageDeath;
    private AudioSource hostageDeathSource;

    public AudioClip victory;
    private AudioSource vitorySource;

    public AudioClip lose;
    private AudioSource loseSource;

    private AudioSource aud;

    int bgIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        bgIndex = Random.Range(0, bg.Length);
        PlayAudio(bg[bgIndex], PlayerPrefs.GetFloat("Volume", 0.3f), true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAudio(AudioClip audio, float volume, bool isLoopback)
    {
        if (audio == this.bg[bgIndex])
        {
            Play(audio, ref bgSource, volume, isLoopback);
            return;
        }
        if (audio == this.gameOver)
        {
            Play(audio, ref gameOverSource, volume, isLoopback);
            return;
        }
        if (audio == this.firing)
        {
            Play(audio, ref firingSource, volume, isLoopback);
            return;
        }
        if (audio == this.bang)
        {
            Play(audio, ref bangSource, volume, isLoopback);
            return;
        }
        if (audio == this.playerDeath)
        {
            Play(audio, ref playerDeathSource, volume, isLoopback);
            return;
        }
        if (audio == this.enemySmile)
        {
            Play(audio, ref enemySmileSource, volume, isLoopback);
            return;
        }
        if (audio == this.enemyDeath)
        {
            Play(audio, ref enemyDeathSource, volume, isLoopback);
            return;
        }
        if (audio == this.bossSmile)
        {
            Play(audio, ref bossSmileSource, volume, isLoopback);
            return;
        }
        if (audio == this.bossDeath)
        {
            Play(audio, ref bossDeathSource, volume, isLoopback);
            return;
        }
        if (audio == this.hostageSmile)
        {
            Play(audio, ref hostageSmileSource, volume, isLoopback);
            return;
        }
        if (audio == this.hostageDeath)
        {
            Play(audio, ref hostageDeathSource, volume, isLoopback);
            return;
        }
        if (audio == this.victory)
        {
            Play(audio, ref vitorySource, volume, isLoopback);
            return;
        }
        if (audio == this.lose)
        {
            Play(audio, ref loseSource, volume, isLoopback);
            return;
        }
        if (audio == this.explosion)
        {
            Play(audio, ref explosionSource, volume, isLoopback);
            return;
        }
    }

    private void Play(AudioClip audio, ref AudioSource audioSource, float volume, bool isLoopback = false)
    {
        if (audioSource != null) return;
        audioSource = Instantiate(Instance.prefab).GetComponent<AudioSource>();

        audioSource.volume = volume;
        audioSource.loop = isLoopback;
        audioSource.clip = audio;
        audioSource.Play();

        if(!isLoopback) Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void StopAudio(AudioClip audio)
    {
        try
        {
            if (audio == this.bg[bgIndex])
            {
                bgSource?.Stop();
                return;
            }
            if (audio == this.gameOver)
            {
                gameOverSource?.Stop();
                return;
            }
            if (audio == this.firing)
            {
                firingSource?.Stop();
                return;
            }
            if (audio == this.bang)
            {
                bangSource?.Stop();
                return;
            }
            if (audio == this.playerDeath)
            {
                playerDeathSource?.Stop();
                return;
            }
            if (audio == this.enemySmile)
            {
                enemySmileSource?.Stop();
                return;
            }
            if (audio == this.enemyDeath)
            {
                enemyDeathSource?.Stop();
                return;
            }
            if (audio == this.bossSmile)
            {
                bossSmileSource?.Stop();
                return;
            }
            if (audio == this.bossDeath)
            {
                bossDeathSource?.Stop();
                return;
            }
            if (audio == this.hostageSmile)
            {
                hostageSmileSource?.Stop();
                return;
            }
            if (audio == this.hostageDeath)
            {
                hostageDeathSource?.Stop();
                return;
            }
            if (audio == this.victory)
            {
                vitorySource?.Stop();
                return;
            }
            if (audio == this.lose)
            {
                loseSource?.Stop();
                return;
            }
            if (audio == this.explosion)
            {
                explosionSource?.Stop();
                return;
            }
        }
        catch(System.Exception e)
        {

        }
    }

    public void ChangeVolume(float value)
    {
        bgSource.volume = value;
    }
}
