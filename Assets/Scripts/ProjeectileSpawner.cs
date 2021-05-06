using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjeectileSpawner : MonoBehaviour {

	[SerializeField]
	GameObject block;
	Transform blockPos;
	[SerializeField]
	int distance = 30;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnBlock(float note, float octave, float timer) {
		Transform blockInstance = Instantiate(block.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
		float octSig = (100 / (1 + Mathf.Exp(0.07f * (32 - octave)))) - 10;
		float noteSig = (410 / (1 + Mathf.Exp(0.07f * (32 - note)))) - 50;
		blockInstance.position = new Vector3 (distance * Mathf.Sin (1 - octSig) * Mathf.Cos (noteSig),
			distance * Mathf.Cos (1 - octSig), distance * Mathf.Sin (1 - octSig) * Mathf.Sin (noteSig));
		blockInstance.gameObject.GetComponent<AsteroidController>().time = timer;
	}
}
