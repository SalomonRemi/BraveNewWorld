using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasierCollider : MonoBehaviour {

    private Locker localLocker;
    private Collider col;

    private void Start()
    {
        localLocker = GetComponentInChildren<Locker>();
        col = GetComponent<Collider>();
    }

    void Update ()
    {
		if(localLocker.codeOk)
        {
            col.enabled = false;
        }
	}
}
