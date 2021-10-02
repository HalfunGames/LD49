using System.Collections;
using UnityEngine;
using TMPro;

public class MasterScript : MonoBehaviour
{

    public GameObject ground;
    public GameObject player;
    public float Force;
    public TextMeshProUGUI TargetValueText;
    public TextMeshProUGUI ControlValueText;
    public float TargetValue;
    public float ControlValue;
    public bool CoroutineStop = false;

    IEnumerator InstabilityMatrix()
    {
        while (!CoroutineStop)
        {
            TargetValue += UnityEngine.Random.Range(-(Mathf.Abs(TargetValue - 0.5f)), (TargetValue - 0.5f));
            ControlValue += UnityEngine.Random.Range(-0.1f, 0.1f);
            Mathf.Clamp(TargetValue, 0f, 1f);
            Mathf.Clamp(ControlValue, 0f, 1f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));

        }
    }

    private void Start()
    {
        TargetValue = 0.5f;
        ControlValue = 0.5f;
        StartCoroutine("InstabilityMatrix");
    }

    // Update is called once per frame
    void Update()
    {

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
        if (ControlValue < 0.5f)
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

        TargetValueText.text = TargetValue.ToString("#.00");
        ControlValueText.text = ControlValue.ToString("#.00");

    }
}
