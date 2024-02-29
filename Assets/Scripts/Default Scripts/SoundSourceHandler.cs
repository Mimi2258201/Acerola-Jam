using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundSourceHandler : MonoBehaviour
{
    [SerializeField]
    bool dontDestroyOnLoad;
    [SerializeField]
    bool killOnAudioEnd;
    AudioSource source;

    [SerializeField]
    UnityEvent OnClipEnd = new UnityEvent();

    bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();


        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        if (source.playOnAwake)
        {
            hasStarted = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (killOnAudioEnd && hasStarted && !source.isPlaying)
        {
            OnClipEnd.Invoke();

            Destroy(gameObject);
        }
    }

    public void Play()
    {
        source.Play();
        hasStarted = true;
    }
}
