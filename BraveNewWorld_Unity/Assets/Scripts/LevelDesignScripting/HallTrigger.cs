using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallTrigger : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MissionManager.instance.isInHall = false;
        }
    }
}