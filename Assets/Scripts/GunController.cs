using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    int ammo = 6;
    bool reloading = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.any)) && (ammo != 0) && (!reloading))
        {
            Transform instance = Instantiate(bullet.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
            ammo -= 1;
        }
        if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.any))
        {
            StartCoroutine(reload);
        }
    }

    IEnumerable reload()
    {
        reloading = true;
        yield return new WaitForSeconds(2.0f);
        ammo = 6;
        reloading = false;
    }
}
