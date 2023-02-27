using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager _Instance;


    public void PlayAudio(AudioSource clip)
    {
        clip.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_Instance == null)
            _Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
