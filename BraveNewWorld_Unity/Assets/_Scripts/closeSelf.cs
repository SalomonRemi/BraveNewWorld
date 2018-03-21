using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeSelf : MonoBehaviour {

	public void closeNow()
    {
		GameManager.instance.closeDocs = true;
	}
}
