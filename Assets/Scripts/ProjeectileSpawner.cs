using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjeectileSpawner : MonoBehaviour 
{
	public static readonly float NoteDelay = 6f;
	public static readonly float Timeout = 1f;


	[SerializeField] private GameObject notePrefab;
	[SerializeField] private Transform noteFolder;





	/*
	public void spawnBlock(float note, float octave, float timer) 
	{
		Transform blockInstance = Instantiate(block.GetComponent<Transform>(), gameObject.GetComponent<Transform>().position, Quaternion.identity);
        blockInstance.LookAt(gameObject.GetComponent<Transform>());
		float octSig = (100 / (1 + Mathf.Exp(0.07f * (32 - octave)))) - 10;
		float noteSig = (410 / (1 + Mathf.Exp(0.07f * (32 - note)))) - 50;
		blockInstance.position = new Vector3 (distance * Mathf.Sin (1 - octSig) * Mathf.Cos (noteSig),
			distance * Mathf.Cos (1 - octSig), distance * Mathf.Sin (1 - octSig) * Mathf.Sin (noteSig));
		blockInstance.gameObject.GetComponent<AsteroidController>().time = timer;
	}
	*/

	public void SpawnNote(int note, int octave)
    {
		Vector3 spawnPosition = new Vector3(((note - 60) * 0.1f), 10, 10);

		GameObject noteObject = GameObject.Instantiate(notePrefab, spawnPosition, Quaternion.identity, noteFolder);
		DebugNote noteScript = noteObject.GetComponent<DebugNote>();

		noteScript.Initialise(spawnPosition, note, octave);
	}
}
