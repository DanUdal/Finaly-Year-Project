using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class GameController : MonoBehaviour 
{
    public static readonly float TimeStepScaler = 256f;

	[SerializeField] private Scene menuScene;
	[SerializeField] private Scene gameScene;
    [SerializeField] private AudioSource source;
    [SerializeField] private ProjeectileSpawner spawner;

    private IEnumerable<ITimedObject> notes;

	List<Note> notesToRemove = new List<Note>();
    [HideInInspector] public static string songName;
    
    AudioClip clip;




    private void Awake()
    {
        var midiFile = MidiFile.Read("Assets/Resources/Songs/" + songName + ".mid");

        if (midiFile != null)
        {
            notes = GetObjectsUtilities.GetObjects(midiFile, ObjectType.Note);
            clip = Resources.Load<AudioClip>("Songs/" + songName);

            StartSong();
        }
        else
        {
            Debug.Log("Midi file not found");
        }
    }





    private void StartSong()
    {
        StartCoroutine(SpawnMidiNotes());
        StartCoroutine(StartMusicTrack(2.41f + ProjeectileSpawner.NoteDelay));
    }

    private IEnumerator StartMusicTrack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        source.PlayOneShot(clip);

        yield return null;
    }

    private IEnumerator SpawnMidiNotes()
    {
        float timeElapsed = 0;
        foreach (Note note in notes)
        {
            float noteHitTimeInSeconds = (note.Time / TimeStepScaler);
            float waitTime = noteHitTimeInSeconds - timeElapsed;

            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
                timeElapsed = noteHitTimeInSeconds;
            }

            spawner.SpawnNote(note.NoteNumber, note.Octave);
        }

        yield return null;
    }





    /*
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
                    //spawner.spawnBlock(o.NoteNumber, o.Octave, (o.Time / 1000.0f));
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
    */

    public void endGame() {
        SceneManager.LoadSceneAsync("MainMenu");
		SceneManager.UnloadSceneAsync ("Song");
	}
}
