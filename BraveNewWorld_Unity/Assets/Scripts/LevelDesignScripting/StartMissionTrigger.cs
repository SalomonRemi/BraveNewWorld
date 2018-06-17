using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMissionTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			MissionManager.instance.isAtDesk = true;
		}
	}
}