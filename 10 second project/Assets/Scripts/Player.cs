using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public int keys = 0;
    public float speed = 5.0f;

    public AudioSource audioSource;
    public AudioClip Background;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip wallBump;
    public float volume = 0.2f;

    private float gameTimer = 10.001f;

    public TextMeshProUGUI loss;
    public TextMeshProUGUI Win;
    private bool playerWins = false;

    private bool ending = false;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI howtoplay;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = "Time left: " + gameTimer.ToString("00.00");
        Invoke("startTimer", 2);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }

        if (playerWins == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walls")
                {
            audioSource.PlayOneShot(wallBump, volume);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);

            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, speed * Time.deltaTime, 0);

            }

        }

        if (collision.gameObject.tag == "End")
        {
            if (gameTimer > 0)
            {
                ending = true;
            }
            else
            {
                ending = false;
            }

        }
    }

    void FixedUpdate()
    {

        int timerCheck = (int)gameTimer;

        if (gameTimer > 0 && gameTimer <= 10)
        {
            gameTimer -= Time.deltaTime;
            timer.text = "Time left: " + timerCheck.ToString("00");
        }

        if (timerCheck > 0 && ending == true)
        {
            winConditions();
        }

        if (timerCheck == 0 && ending == false)
        {
            loseConditions();

        }

    }

    void startTimer()
    {
        gameTimer = 10;
        howtoplay.text = "";

    }

    void winConditions()
    {
        audioSource.Stop();
        playerWins = true;
        audioSource.PlayOneShot(winSound, volume);
        Win.text = "You Win!\nYou Made it!\nMade by Griffin Beverlin\nPress Escape to Quit\n";

    }



    void loseConditions()
    {
       
        if (playerWins == false)
            
        {
            audioSource.PlayOneShot(loseSound, volume);
            loss.text = "You Lose!\nYou didn't make it, try again?\nMade by Griffin Beverlin\nPress Escape to Quit\n";
            if (Input.GetKeyDown(KeyCode.R))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }


}
