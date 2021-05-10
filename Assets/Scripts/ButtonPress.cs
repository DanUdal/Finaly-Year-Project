using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
	public string songName;
	Button button;
    // Start is called before the first frame update
    void Start()
    {
		button = gameObject.GetComponent<Button>();
		button.onClick.AddListener(loadSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void loadSong()
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Song");
		GameController.songName = songName;
		SceneManager.UnloadSceneAsync("MainMenu");
	}
}
