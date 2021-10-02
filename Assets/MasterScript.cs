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
    private bool Paused = false;
    private bool GameStart = false;

    public GameObject PauseMenu;
    public int Difficulty; // 0 - Easy, 1 - Medium, 2 - Normal
    private float minTime;
    private float maxTime;

    IEnumerator InstabilityMatrix()
    {
        TargetValue = 0f;
        ControlValue = 1f;
        CoroutineStop = false;
        float waitTime = 0;
        float currentTime = 0;
        while (!CoroutineStop)
        {
            if (waitTime <= currentTime)
            {
                float TVDif = (Mathf.Abs(TargetValue - 0.5f) + 0.3f);
                float CVDif = (Mathf.Abs(ControlValue - 0.5f) + 0.3f);
                TargetValue += UnityEngine.Random.Range(-TVDif, TVDif);
                ControlValue += UnityEngine.Random.Range(-CVDif, CVDif);
                TargetValue = Mathf.Clamp(TargetValue, 0f, 1f);
                ControlValue = Mathf.Clamp(ControlValue, 0f, 1f);
                currentTime = 0;
                waitTime = Random.Range(minTime, maxTime);
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        return null;
    }

    private void Start()
    {
        Pause();
    }
    public void StartGame()
    {
        Restart();
        Pause();
    }

    public void setEasy()
    {
        Difficulty = 0;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        minTime = 1f;
        maxTime = 2f;
    }

    public void setMedium()
    {
        Difficulty = 1;
        player.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        minTime = 0.75f;
        maxTime = 1.5f;
    }

    public void setNormal()
    {
        Difficulty = 3;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        minTime = 0.5f;
        maxTime = 1f;
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

    void Pause()
    {
        if (Paused)
        {
            Time.timeScale = 1;
            Paused = false;
            PauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            Paused = true;
            PauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStart)
        {
            if (Difficulty == 0 && Input.GetKeyDown(KeyCode.Space))
            {
                ground.GetComponent<Rigidbody2D>().angularVelocity = 0;
                ground.GetComponent<Rigidbody2D>().SetRotation(0);
            }
            if (TargetValue < 0.5f)
            {
                TargetValueText.text = "Player";
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
                TargetValueText.text = "Ground";
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

                ControlValueText.text = "Rotation";
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

                ControlValueText.text = "Vertical";
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, Force * Time.deltaTime));
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    ground.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -Force * Time.deltaTime));
                }
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            if (Score < player.transform.position.y)
            {
                Score = player.transform.position.y;
            }

            ScoreValueText.text = Score.ToString("#.000");

            if (Input.GetKeyDown(KeyCode.P))
            {
                Pause();
            }
        }


    }
}
