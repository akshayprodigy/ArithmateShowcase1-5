using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;





public class panel : MonoBehaviour
{
    public GameObject pnl;
    public AudioMixer masterMixer;
    public AudioSource audioSource_bg, audioSource_event, audioSource_interact;
    public AudioClip[] audioClips_event, audioClips_interact;


    /// <summary>
    /// for interaction such as button click and swipe
    /// </summary>
   public void Button_Click()
    {
        audioSource_event.Stop();
        
        audioSource_interact.PlayOneShot(audioClips_interact[0],2.0f);
    }

    public void swipe()
    {
        audioSource_event.Stop();
      
        audioSource_interact.PlayOneShot(audioClips_interact[1], 2.0f);
    }


    /// <summary>
    /// for events such as correct, wrong, timeout, activity complete
    /// </summary>
    public void correct()
    {
        audioSource_interact.Stop();
        
        audioSource_event.PlayOneShot(audioClips_event[0], 2.0f);
    }

    public void wrong()
    {
        audioSource_interact.Stop();
        
        audioSource_event.PlayOneShot(audioClips_event[1], 2.0f);
    }

    public void time_out()
    {
        audioSource_interact.Stop();
        
        audioSource_event.PlayOneShot(audioClips_event[2], 2.0f);
    }
    public void activity_complete()
    {
        audioSource_interact.Stop();
        
        audioSource_event.PlayOneShot(audioClips_event[3], 2.0f);
    }
    public void open()
    {
        pnl.SetActive(true);
    }
    /// <summary>
    /// configuring audio mixer
    /// </summary>
    /// <param name="soundLevel"></param>
    /// 
    public void SetBgSound(float soundLevel)
    {
        masterMixer.SetFloat("musicVol", soundLevel);
    }
    public void SetEventSound(float soundLevel)
    {
        masterMixer.SetFloat("eventVol", soundLevel);
    }
    public void SetinteractSound(float soundLevel)
    {
        masterMixer.SetFloat("interactVol", soundLevel);
    }
    public void SetwholeSound(float soundLevel)
    {
        masterMixer.SetFloat("masterVol", soundLevel);
    }




}
