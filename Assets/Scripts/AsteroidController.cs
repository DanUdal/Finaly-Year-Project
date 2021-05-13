using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	Transform transform;
	public float time;
	[SerializeField]
	Transform player;
	[SerializeField]
	float playerRadius = 1.0f;
	[SerializeField]
	float rotationSpeed = 0.5f;
	PlayerController pController;
	[SerializeField]
	GameController gController;
    float hitTime;
    bool set = false;
    float distance;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Camera").GetComponent<Transform>();
		transform = gameObject.GetComponent<Transform> ();
		transform.LookAt (player.position);
		pController = player.gameObject.GetComponent<PlayerController> ();
		gController = player.gameObject.GetComponent<GameController>();
        hitTime = time - gController.timer;
        distance = Vector3.Distance(transform.position, player.position);
    }
	
	// Update is called once per frame
	void Update () {
		if ((time - gController.timer) <= 1.0f) 
		{
            transform.Translate(new Vector3(0, 0, 2f * Time.deltaTime));
            //transform.rotation = Quaternion.LookRotation (Vector3.RotateTowards (transform.rotation.eulerAngles, player.position, rotationSpeed * Time.deltaTime, 0.0f));
        }
        else if ((time - gController.timer) <= 5.0f) 
		{
			transform.Translate (Vector3.forward * ((distance - playerRadius) / ((hitTime - 1.0f))));
		}
		if (time < (gController.timer + 1.0f))
		{
			Destroy (gameObject);
            //Debug.Log("time out");
        }
	}

	void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.tag == "Sword") 
		{
			pController.health += 2;
			if (pController.health > 5)
            {
				pController.health = 5;
			}
			Destroy (gameObject);
		}
        if (collision.gameObject.tag == "Bullet")
        {
            pController.health += 2;
            if (pController.health > 5)
            {
                pController.health = 5;
            }
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }

    }
}
