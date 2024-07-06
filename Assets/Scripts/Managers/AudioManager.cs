using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private float sfxMinimumDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBgm;
    private int bgmIndex;

    private bool canPlaySFX;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Invoke("AllowSFX", 0.5f);
    }    

    // Update is called once per frame
    private void Update()
    {
        if (!playBgm)
        {
            StopAllBGM();
        }
        else
        {
            if(bgm.Length > 0)
            {
                if (!bgm[bgmIndex].isPlaying)
                {
                    PlayBGM(bgmIndex);
                }
            }            
        }
    }

    public void PlaySFX(int _sfxIndex, Transform _source)
    {        
        if (canPlaySFX == false)
        {
            return;
        }

        if(PlayerManager.instance.currentPlayer != null)
        {
            if (_source != null && Vector2.Distance(PlayerManager.instance.currentPlayer.transform.position, _source.position) > sfxMinimumDistance)
            {
                return;
            }

            if (_sfxIndex < sfx.Length)
            {
                sfx[_sfxIndex].pitch = Random.Range(0.85f, 1.1f);
                sfx[_sfxIndex].Play();
            }
        }        
    }

    public void PlaySFXUI(int _sfxIndex)
    {
        if (canPlaySFX == false)
        {
            return;
        }

        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].pitch = Random.Range(0.85f, 1.1f);
            sfx[_sfxIndex].Play();
        }        
    }

    public void StopSFX(int _index) => sfx[_index].Stop();            

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {       
        bgmIndex = _bgmIndex;

        StopAllBGM();
        if(bgmIndex < bgm.Length)
        {
            bgm[bgmIndex].Play();
        }
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    private void AllowSFX() => canPlaySFX = true;
}
