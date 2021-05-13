using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int health = 5;
	[SerializeField]
	GameController gController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health == 0) {
			gController.endGame ();
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Asteroid") 
		{
			health -= 1;
			Destroy (collision.gameObject);
            Debug.Log("ow");
		}
	}
}
