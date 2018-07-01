using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public static InputManager instance = null;

    private string[] joyNames;

    [HideInInspector] public bool isControllerPlugged; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start ()
    {
        joyNames = Input.GetJoystickNames();

		if(Input.GetJoystickNames().Length == 0)
        {
            isControllerPlugged = false;
        }
        else
        {
            isControllerPlugged = true;
        }
	}

    private void Update()
    {
        Debug.Log(joyNames.Length);
    }
}