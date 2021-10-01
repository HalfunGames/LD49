using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{

    public GameObject ground;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0f, 1f) < 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(1f, 0f));
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1f, 0f));
            }
        }
    }
}
