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


    IEnumerator TimingCoroutine()
    {
		//FindObjectOfType<CameraShake>().StartShake (elevatorLongShake);

        yield return new WaitForSeconds(MissionManager.instance.timeInElevator + 5f);

		FindObjectOfType<CameraShake>().StartShake(elevatorEndShake);
    }
}