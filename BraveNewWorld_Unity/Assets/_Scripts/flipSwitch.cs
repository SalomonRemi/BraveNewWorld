using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipSwitch : MonoBehaviour {

    private Animator anim;
    public bool switchOn = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void flip()
    {
        if (switchOn)
        {
            switchOn = false;
        }
        else
        {
            switchOn = true;
        }

        anim.SetTrigger("switch");

        AudioManager.instance.PlaySound("leverMove");
    }
}