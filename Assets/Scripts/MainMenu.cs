using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AudioModule;

public class MainMenu : MonoBehaviour
{
	Object[] songs;
	List<Object> wavs = new List<Object>();
	List<Object> midis = new List<Object>();
	[SerializeField]
	GameObject button;
    // Start is called before the first frame update
    void Start()
    {
		songs = Resources.LoadAll("songs", typeof(AudioClip));
		foreach (var s in songs)
		{
			if (s.name.Contains(".wav"))
			{
				wavs.Add(s);
			}
			if (s.name.Contains(".mid"))
			{
				midis.Add(s);
			}
			wavs.Sort((s1, s2) => s1.name.CompareTo(s2.name));
			midis.Sort((s1, s2) => s1.name.CompareTo(s2.name));
			for (int i = 0; i < wavs.Count; i++)
			{
				Transform instance = Instantiate(button.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
				instance.Translate(new Vector3(0, -2 * i, 0));
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
