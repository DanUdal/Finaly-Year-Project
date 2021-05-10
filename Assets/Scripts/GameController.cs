using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class GameController : MonoBehaviour {

	public float timer = -6.0f; //set timer to 5 seconds earlier than the song so that blocks spawn 5 seconds in advance of their strike time
	[SerializeField]
	Scene menuScene;
	[SerializeField]
	Scene gameScene;
	[SerializeField]
	ProjeectileSpawner spawner;
	ICollection<ITimedObject> notes;
	List<Note> notesToRemove;
	public static string songName;

	// Use this for initialization
	void Start () {
		var midiFile = MidiFile.Read(songName +".mid");
		ChunksCollection chunks = midiFile.Chunks;
		notes = GetObjectsUtilities.GetObjects(midiFile, ObjectType.Note);
		int size = notes.Count;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; //global timer for the point in the song
		foreach(Note o in notes)
		{
			if (o.Time - 6 <= timer) //spawn blocks 6 seconds ahead of time so there is a 1 second window for if the processing is slow
			{ //this way if there's a delay in spawning the blocks they won't be out of time with the song
				spawner.spawnBlock(o.NoteNumber, o.Octave, o.Time);
				notesToRemove.Add(o);
			}
		}
		foreach(Note o in notesToRemove)
		{
			notes.Remove(o); //remove the notes that were spawned so they won't be spawned again
		}
	}

	public void endGame() {
        SceneManager.LoadSceneAsync("MainMenu");
		SceneManager.UnloadSceneAsync ("Song");
	}
}
