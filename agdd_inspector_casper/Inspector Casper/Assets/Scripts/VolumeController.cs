using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer mixer;


    public void setLevel(float slider_value)
    {
        mixer.SetFloat("Volume", Mathf.Log10(slider_value) * 20);
    }
}
