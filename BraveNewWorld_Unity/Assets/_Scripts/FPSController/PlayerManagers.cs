using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagers : MonoBehaviour {

    #region Singleton

    public static PlayerManagers instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    [HideInInspector] public bool haveAnObject;
    [HideInInspector] public Collider LOS_objectCol;

    Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        
        Debug.DrawRay(lineOrigin, cam.transform.forward * 200, Color.green);

        LineOfSight();
    }

    public void LineOfSight()
    {
        RaycastHit hit;
        Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit))
        {
            LOS_objectCol = hit.collider;
        }
    }
}