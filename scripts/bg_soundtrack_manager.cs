using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_soundtrack_manager : MonoBehaviour
{
    
    /*
     *  public static void play_bg_music(int level)
    {
        
        bg_music = new GameObject("bg_music");
        bg_music_source = bg_music.AddComponent<AudioSource>();
        switch(level)
        {
            case 1:
                bg_music_source.PlayOneShot(get_clip(sound_type.level1_bg),bg_volume);
                Debug.Log("playing sound level 1!!");
                break;
            case 2:
                bg_music_source.PlayOneShot(get_clip(sound_type.level2_bg), bg_volume);
                Debug.Log("playing sound level 2!!");
                break;
            case 3:
                bg_music_source.PlayOneShot(get_clip(sound_type.level3_bg), bg_volume);
                Debug.Log("playing sound level 3!!");
                break;
            default:
                bg_music_source.PlayOneShot(get_clip(sound_type.level1_bg), bg_volume);
                Debug.Log("playing sound  level > 3!!");
                break;
        }
    }
    public static void set_bg_volume(float v)
    {
        bg_volume = v;
        if (bg_music != null)
        {
            if (bg_music_source != null)
            {
                bg_music_source.volume = bg_volume;
            }
            else
            {
                if (bg_music.GetComponent<AudioSource>() != null)
                {
                    bg_music_source = bg_music.GetComponent<AudioSource>();
                    bg_music_source.volume = bg_volume;
                }
                else
                {
                    bg_music_source = bg_music.AddComponent<AudioSource>();
                    bg_music_source.PlayOneShot(get_clip(sound_type.level1_bg), bg_volume);
                }
            }
        }
        else
        {
            bg_music = new GameObject("bg_music");
            bg_music_source = bg_music.AddComponent<AudioSource>();
            bg_music_source.PlayOneShot(get_clip(sound_type.level1_bg), bg_volume);
        }
    }
     * 
     */
    
    private AudioSource bg_soundtrack; 

    private void Awake()
    {
        bg_soundtrack = transform.GetComponent<AudioSource>();
        bg_soundtrack.volume = soundEngine.bg_volume;
    }
    public void set_bg_volume(float v)
    {
        bg_soundtrack.volume = v;
        soundEngine.bg_volume = v;
    }
    public void set_sfx_volume(float v)
    {
        soundEngine.sfx_volume = v;
    }
    public void play_click_sound()
    {
        soundEngine.play_sound_with_volume(soundEngine.sound_type.click,0.5f);
    }
   
}
