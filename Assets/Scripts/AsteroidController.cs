using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

	Transform transform;
	public float time;
	[SerializeField]
	Transform player;
	[SerializeField]
	float playerRadius = 5.0f;
	[SerializeField]
	float rotationSpeed = 0.5f;
	PlayerController pController;
	[SerializeField]
	GameController gController;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Camera").GetComponent<Transform>();
		transform = gameObject.GetComponent<Transform> ();
		transform.LookAt (player.position);
		pController = player.gameObject.GetComponent<PlayerController> ();
		gController = player.gameObject.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((time - gController.timer) <= 1.0f) 
		{
			transform.position = Vector3.MoveTowards (transform.position, player.position, Time.deltaTime * Vector3.Distance (transform.position, player.position) * (time - gController.timer));
			transform.rotation = Quaternion.LookRotation (Vector3.RotateTowards (transform.rotation.eulerAngles, player.position - transform.position, rotationSpeed * Time.deltaTime, 0.0f));
		} 
		else if ((time - gController.timer) <= 5.0f) 
		{
			transform.Translate (Vector3.forward * Time.deltaTime * (Vector3.Distance (transform.position, player.position) - playerRadius) * ((time - 1.0f) - gController.timer));
		}
		if (time > (gController.timer + 0.5f)) 
		{
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "Sword" || collision.gameObject.tag == "Bullet") 
		{
			pController.health += 2;
			if (pController.health > 5) {
				pController.health = 5;
			}
			Destroy (gameObject);
		}
		else 
		{
			pController.health -= 1;
		}
	}
}
