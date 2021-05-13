using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    float speed = 10.0f;
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * speed);
        if (Vector3.Distance(gameObject.GetComponent<Transform>().position, player.GetComponent<Transform>().position) > 50.0f)
        {
            Destroy(gameObject);
        }
    }
}
