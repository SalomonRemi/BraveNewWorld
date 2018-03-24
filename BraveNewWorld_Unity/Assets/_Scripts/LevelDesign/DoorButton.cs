using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {

    public string displayName;

    public Animator doorAnim;

    public void OpenDoor()
    {
        doorAnim.SetBool("Open", true);
        AudioManager.instance.PlaySound("buttonFalse");
    }
}