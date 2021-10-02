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
    public TextMeshProUGUI ScoreValueText;
    public float Score = 0;
    public float TargetValue;
    public float ControlValue;
    public bool CoroutineStop = false;

    IEnumerator InstabilityMatrix()
    {
        TargetValue = 0f;
        ControlValue = 1f;
        yield return new WaitForSeconds(1.5f);
        CoroutineStop = false;
        while (!CoroutineStop)
        {
            float TVDif = (Mathf.Abs(TargetValue - 0.5f) + 0.3f);
            float CVDif = (Mathf.Abs(ControlValue - 0.5f) + 0.3f);
            TargetValue += UnityEngine.Random.Range(-TVDif, TVDif);
            ControlValue += UnityEngine.Random.Range(-CVDif, CVDif);
            TargetValue = Mathf.Clamp(TargetValue, 0f, 1f);
            ControlValue = Mathf.Clamp(ControlValue, 0f, 1f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1f));
        }
    }

    private void Start()
    {
        StartCoroutine("InstabilityMatrix");
    }

    void Restart()
    {
        ground.transform.position = new Vector3(0, -5, 0);
        player.transform.position = new Vector3(0, 0, 0);

        ground.GetComponent<Rigidbody2D>().SetRotation(0);
        ground.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ground.GetComponent<Rigidbody2D>().angularVelocity = 0;
        player.GetComponent<Rigidbody2D>().SetRotation(0);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().angularVelocity = 0;

        CoroutineStop = true;
        StartCoroutine("InstabilityMatrix");
    }

    // Update is called once per frame
    void Update()
    {

        if (TargetValue < 0.5f)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force * Time.deltaTime, 0f));
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force * Time.deltaTime, 0f));
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Force * Time.deltaTime, 0f));
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(Force * Time.deltaTime, 0f));
            }
        }
        if (ControlValue < 0.5f)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddTorque(-Force * Time.deltaTime, 0f);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddTorque(Force * Time.deltaTime, 0f);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Force * Time.deltaTime));
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -Force * Time.deltaTime));
            }
        }

        TargetValueText.text = TargetValue.ToString("#.00");
        ControlValueText.text = ControlValue.ToString("#.00");

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();


        }

    }
}
