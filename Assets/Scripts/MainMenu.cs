using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField]
	Object[] wavs;
	[SerializeField]
	Button button;
    // Start is called before the first frame update
    void Start()
    {
		wavs = Resources.LoadAll("Songs", typeof(AudioClip));
		//wavs.Sort((s1, s2) => s1.name.CompareTo(s2.name));
		for (int i = 0; i < wavs.Length; i++)
		{
			Transform instance = Instantiate(button.GetComponent<Transform>(), gameObject.GetComponent<Transform>());
			instance.position = new Vector3(0, 3.33f - (1.35f * i), 7);
			instance.gameObject.GetComponent<ButtonPress>().songName = wavs[i].name;
		}
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
