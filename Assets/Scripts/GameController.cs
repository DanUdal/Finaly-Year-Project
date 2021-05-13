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
	List<Note> notesToRemove = new List<Note>();
    public static string songName;
    [SerializeField]
    AudioSource source;
    AudioClip clip;

	// Use this for initialization
	void Start () {
		var midiFile = MidiFile.Read("Assets/Resources/Songs/" + songName + ".mid");
        if (midiFile == null)
        {
            Debug.Log("shit went wrong");
        }
		notes = GetObjectsUtilities.GetObjects(midiFile, ObjectType.Note);
        clip = Resources.Load<AudioClip>("Songs/" + songName);
        source.clip = clip;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; //global timer for the point in the song
        if (notesToRemove != null)
        {
            notesToRemove.Clear();
        }
        if ((timer > 0.0f) && (!source.isPlaying))
        {
            source.Play();
        }
        if (notes != null)
        {
            foreach (Note o in notes)
            {
                if ((o.Time / 1000.0f) - 6.0f <= timer) //spawn blocks 6 seconds ahead of time so there is a 1 second window for if the processing is slow
                { //this way if there's a delay in spawning the blocks they won't be out of time with the song
                    spawner.spawnBlock(o.NoteNumber, o.Octave, (o.Time / 1000.0f));
                    notesToRemove.Add(o);
                }
            }
        }
        if (notesToRemove != null)
        {
            foreach (Note o in notesToRemove)
            {
                notes.Remove(o); //remove the notes that were spawned so they won't be spawned again
            }
        }
        else
        {
            Debug.Log("no notes removed");
        }
        if ((timer > 0.0f) && (!source.isPlaying))
        {
            endGame();
        }

    }

	public void endGame() {
        SceneManager.LoadSceneAsync("MainMenu");
		SceneManager.UnloadSceneAsync ("Song");
	}
}
