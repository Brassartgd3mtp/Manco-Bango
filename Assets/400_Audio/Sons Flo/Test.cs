using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if  (Input.GetKeyDown(KeyCode.Space))
        {
            SoundOne();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SoundTwo();
        }
    }
    public void SoundOne()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(0, audioSource);
    }

    public void SoundTwo()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.PlaySound(1, audioSource);
    }
}
