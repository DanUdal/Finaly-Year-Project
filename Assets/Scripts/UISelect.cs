using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class UISelect : MonoBehaviour
{
	public SteamVR_LaserPointer pointer;
    // Start is called before the first frame update
    void Start()
    {
		pointer.PointerClick += onClick;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void onClick(object sender, PointerEventArgs e)
	{
		Button button = e.target.GetComponent<Button>();
		if (button != null)
		{
			button.onClick.Invoke();
		}
	}
}
