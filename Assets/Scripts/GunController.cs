using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class GunController : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    int ammo = 6;
    bool reloading = false;
    SteamVR_Action_Boolean fire;
    SteamVR_Action_Boolean reloadGun;

    // Start is called before the first frame update
    void Start()
    {
        fire = SteamVR_Actions._default.GrabPinch;
        fire[SteamVR_Input_Sources.LeftHand].onStateDown += shoot;
        reloadGun = SteamVR_Actions._default.GrabGrip;
        reloadGun[SteamVR_Input_Sources.LeftHand].onStateDown += startReload;
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.any)) && (ammo != 0) && (!reloading))
        {
            Transform instance = Instantiate(bullet.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
            ammo -= 1;
        }
        if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.any))
        {
            StartCoroutine(reload);
        }*/
    }

    void startReload(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        IEnumerator coroutine = reload();
        StartCoroutine(coroutine);
    }

    IEnumerator reload()
    {
        reloading = true;
        yield return new WaitForSeconds(2.0f);
        ammo = 6;
        reloading = false;
    }

    void shoot(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if ((ammo != 0) && (!reloading))
        {
            Transform instance = Instantiate(bullet.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
            ammo -= 1;
        }
    }
}
