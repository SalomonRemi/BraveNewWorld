using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandManager : MonoBehaviour {

    [Header("Tools Settings")]
    public int selectedTool;
    public bool canSwitch = true;

    private bool isGrabbing;
    private int toolSelectedWhileGrab;

    private GameObject grabbedObject;

    PlayerStats ps;


    private void Start()
    {
        ps = FindObjectOfType<PlayerStats>();

        ToolsSelection();
    }

    void Update ()
    {
        // DO GRAB
        if (Input.GetKeyDown(KeyCode.E) && !isGrabbing)
        {
            //Grab();
        }
        else if(Input.GetKeyDown(KeyCode.E) && isGrabbing)
        {
            Drop();
        }

        if (isGrabbing)
        {
            HoldObject();
            if (Input.GetButtonDown("Fire1")) ThrowObject();
        }


        // DO TOOL SELECTION
        int previousSelectedTool = selectedTool;

        if (canSwitch)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedTool >= transform.childCount - 1)
                    selectedTool = 0;
                else
                    selectedTool++;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedTool <= 0)
                    selectedTool = transform.childCount - 1;
                else
                    selectedTool--;
            }

            if (previousSelectedTool != selectedTool)
            {
                ToolsSelection();
            }
        }
    }


    private void ToolsSelection()
    {
        int i = 0;
        foreach (Transform tools in transform)
        {
            if (i == selectedTool)
                tools.gameObject.SetActive(true);
            else
                tools.gameObject.SetActive(false);
            i++;
        }
    }


    //private void Grab()
    //{
    //    InteractibleObject interactibleObject = PlayerManagers.instance.LOS_objectCol.gameObject.GetComponent<InteractibleObject>();

    //    if (interactibleObject != null)
    //    {
    //        if (interactibleObject.GetIsLooking())
    //        {
    //            grabbedObject = interactibleObject.gameObject;
    //            grabbedObject.transform.parent = transform;
    //            isGrabbing = true;

    //            Rigidbody objectRb = grabbedObject.GetComponent<Rigidbody>();
    //            Collider objectCol = grabbedObject.GetComponent<Collider>();
    //            objectRb.isKinematic = true;
    //            objectCol.enabled = false;

    //            PlayerManagers.instance.haveAnObject = true;

    //            toolSelectedWhileGrab = selectedTool;
    //            selectedTool = transform.childCount - 1;
    //            ToolsSelection();
    //            canSwitch = false;
    //        }
    //    }
    //}


    private void Drop()
    {
        grabbedObject.transform.parent = null;
        isGrabbing = false;

        Rigidbody objectRb = grabbedObject.GetComponent<Rigidbody>();
        Collider objectCol = grabbedObject.GetComponent<Collider>();
        objectRb.isKinematic = false;
        objectCol.enabled = true;

        grabbedObject = null;

        PlayerManagers.instance.haveAnObject = false;

        selectedTool = toolSelectedWhileGrab;
        ToolsSelection();
        canSwitch = true;
    }


    private void HoldObject()
    {
        grabbedObject.transform.position = transform.position;
    }


    private void ThrowObject()
    {
        Rigidbody objectRb = grabbedObject.GetComponent<Rigidbody>();

        Vector3 throwingDir = Camera.main.transform.forward;

        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit))
        {
            throwingDir = hit.point - grabbedObject.transform.position;
        }

        Drop();

        objectRb.AddForce(throwingDir.normalized * ps.throwingForce.GetValue() * 25f);
        objectRb.AddForce(Camera.main.transform.up * 30);
    }
}
