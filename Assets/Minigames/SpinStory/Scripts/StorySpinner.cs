using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Picture
{
    public Sprite s1, s2, s3, s4;
}

public class StorySpinner : MonoBehaviour {

    public static StorySpinner instance;

    public Picture[] picture;

    public int difficulty = 1;
    public float timer = 15;

    public GameObject square1;
    public GameObject square2;
    public GameObject square3;
    public GameObject square4;

    public GameObject winText;
    public Text timerText;

    private bool victory = false;

    // Use this for initialization
    void Start()
    {
        instance = this;
        PrepareGame(difficulty);
    }

    public void StartGame() {
        PrepareGame(difficulty+1);
        StartCoroutine(StartTimer());
    }

    public void PrepareGame(int diff) {
        difficulty = diff;
        int randomPic = Random.Range(0, picture.Length);
        timer = Mathf.Max(4, 15 - difficulty);
        timerText.text = "Time: " + timer + "s";
        square1.GetComponent<SpriteRenderer>().sprite = picture[randomPic].s1;
        square2.GetComponent<SpriteRenderer>().sprite = picture[randomPic].s2;
        square3.GetComponent<SpriteRenderer>().sprite = picture[randomPic].s3;
        square4.GetComponent<SpriteRenderer>().sprite = picture[randomPic].s4;

        //makes sure at least one square is off
        setSquareRotation(square1, 3);
        setSquare(square2);
        setSquare(square3);
        setSquare(square4);
        victory = false;
    }

    public void setSquare(GameObject square)
    {
        setSquareRotation(square, Random.Range(1, 4));
    }

    public void setSquareRotation(GameObject square, int rotation)
    {
        switch (Random.Range(1, 4))
        {
            case 1: square.transform.localRotation = Quaternion.Euler(0, 0, 0); break;
            case 2: square.transform.localRotation = Quaternion.Euler(0, 0, 90); break;
            case 3: square.transform.localRotation = Quaternion.Euler(0, 0, 180); break;
            case 4: square.transform.localRotation = Quaternion.Euler(0, 0, 270); break;
            default: break;
        }
    }

    public void rotate90(GameObject square)
    {
        int rot = (int)(square.transform.localRotation.eulerAngles.z);

        switch (rot)
        {
            case 270: square.transform.localRotation = Quaternion.Euler(0, 0, 0); break;
            case 0: square.transform.localRotation = Quaternion.Euler(0, 0, 90); break;
            case 90: square.transform.localRotation = Quaternion.Euler(0, 0, 180); break;
            case 180: square.transform.localRotation = Quaternion.Euler(0, 0, 270); break;
            default: break;
        }
    }

    private void Update()
    {
        //check win condition
        if (square1.transform.localRotation.eulerAngles.z == 0
        && square2.transform.localRotation.eulerAngles.z == 0
        && square3.transform.localRotation.eulerAngles.z == 0
        && square4.transform.localRotation.eulerAngles.z == 0
        && timer >= 0 && victory == false)
        {
            victory = true;
        }


        //keyboard controls
        if (Input.GetKeyDown("1"))
        {
            rotate90(square1);
        }
        if (Input.GetKeyDown("2"))
        {
            rotate90(square2);
        }
        if (Input.GetKeyDown("3"))
        {
            rotate90(square3);
        }
        if (Input.GetKeyDown("4"))
        {
            rotate90(square4);
        }
    }

    IEnumerator StartTimer()
    {
        while (timer > 0)
        {
            if (victory) break;
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = "Time: "+timer+"s";
        }
        gameOver();
    }

    IEnumerator CleanTheGame() {
        yield return new WaitForSeconds(2f);
        winText.SetActive(false);
    }

    public void gameOver()
    {
        if (victory)
        {
            winText.SetActive(true);
            GameManager.instance.OnFinishSpinTheStory(true);
            StartCoroutine("CleanTheGame");
        }
        else
        {
            GameManager.instance.OnFinishSpinTheStory(false);
        }
    }
    }
