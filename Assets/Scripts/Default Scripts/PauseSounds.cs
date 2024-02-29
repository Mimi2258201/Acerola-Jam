using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSounds : MonoBehaviour
{
    public bool setPitchToTimeScale;
    AudioSource[] sounds;
    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (setPitchToTimeScale)
        {
            foreach (var source in sounds)
            {
                source.pitch = Time.timeScale;
            }
        }
        else
        {
            if (Time.timeScale == 0f && isPaused == false)
            {
                isPaused = true;
                foreach (var source in sounds)
                {
                    source.Pause();
                }
            }
            else if (Time.timeScale == 1f && isPaused == true)
            {
                isPaused = false;
                foreach (var source in sounds)
                {
                    source.UnPause();
                }
            }
        }
    }
}
