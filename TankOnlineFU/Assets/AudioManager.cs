using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource m_Source;
    [SerializeField] AudioClip SFXsource;
    public AudioClip background;
    void Start()
    {
        m_Source.clip=background; m_Source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
