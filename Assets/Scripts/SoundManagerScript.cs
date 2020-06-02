using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioClip readyUpSound, tankDestroySound;
    static AudioSource audiosource;
    void Start()
    {
        readyUpSound = (AudioClip) Resources.Load<AudioClip>("audio/LAWF_Tank_Ready_Up");
        tankDestroySound = (AudioClip)Resources.Load<AudioClip>("audio/rewardExplosion");
        audiosource = GetComponent<AudioSource>();

    }

   public static void playReadyUpSound()
    {
        audiosource.PlayOneShot(readyUpSound);
    }

    public static void playTankDestroySound()
    {
        audiosource.PlayOneShot(tankDestroySound);
    }
}
