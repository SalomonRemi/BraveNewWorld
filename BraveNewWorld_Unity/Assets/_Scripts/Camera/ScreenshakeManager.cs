using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshakeManager : MonoBehaviour {

    public CameraShake.Properties elevatorLongShake;
    public CameraShake.Properties elevatorEndShake;

    private bool isInElevator = true;


    private void Start()
    {
        StartCoroutine(TimingCoroutine());
    }


    void Update ()
    {
        if (isInElevator) FindObjectOfType<CameraShake>().StartShake(elevatorLongShake);
        else FindObjectOfType<CameraShake>().StartShake(elevatorEndShake);
	}


    IEnumerator TimingCoroutine()
    {
        isInElevator = true;

        yield return new WaitForSeconds(MissionManager.instance.timeInElevator);

        isInElevator = false;
    }
}