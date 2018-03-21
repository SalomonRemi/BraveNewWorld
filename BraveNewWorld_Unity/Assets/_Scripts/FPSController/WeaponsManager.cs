using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour {

    [Header("Equipped Weapons Lists")]
    public List<GameObject> leftWeapons;
    public List<GameObject> rightWeapons;

    [HideInInspector] public bool canUseLeft;
    [HideInInspector] public bool canUseRight;

    private float leftTimer;
    private float rightTimer;

    void Start()
    {
        canUseLeft = true;
        canUseRight = true;
    }


    void Update()
    {
        CheckTimers();
    }


    private void CheckTimers()
    {
        leftTimer += Time.deltaTime;
        rightTimer += Time.deltaTime;

        if(leftTimer > 0)
        {
            canUseLeft = true;
        }

        if (rightTimer > 0)
        {
            canUseRight = true;
        }
    }
}
