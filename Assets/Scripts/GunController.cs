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
    [SerializeField]
    Renderer[] ammoLights;
    [SerializeField]
    Material light;
    [SerializeField]
    Material dark;

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
        
    }

    void startReload(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        IEnumerator coroutine = reload();
        StartCoroutine(coroutine);
    }

    IEnumerator reload()
    {
        reloading = true;
        for (int i = 0; i < 6; i++)
        {
            ammoLights[i].material = light;
            yield return new WaitForSeconds(0.33f);
        }
        ammo = 6;
        reloading = false;
    }

    void shoot(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if ((ammo != 0) && (!reloading))
        {
            Transform instance = Instantiate(bullet.GetComponent<Transform>(), gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
            instance.Rotate(new Vector3(90, 90, 0));
            instance.Translate(new Vector3(0, 1.3f, 0));
            ammo -= 1;
            ammoLights[ammo].material = dark;
        }
    }
}
