using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour
{

    public GameObject ground;
    public GameObject player;
    public float Force;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float TargetValue = Random.Range(0f, 1f);
        if (TargetValue < 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force * Time.deltaTime, 0f));
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force * Time.deltaTime, 0f));
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force * Time.deltaTime, 0f));
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force * Time.deltaTime, 0f));
            }
        }
        if (Random.Range(0f, 1f) < 0.5f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddTorque(-Force * Time.deltaTime, 0f);
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddTorque(Force * Time.deltaTime, 0f);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Force * Time.deltaTime));
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -Force * Time.deltaTime));
            }
        }

    }
}
