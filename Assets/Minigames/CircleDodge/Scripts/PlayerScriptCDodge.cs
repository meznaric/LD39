using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScriptCDodge : MonoBehaviour {

    public float RotationSpeed = 1;

    public float timer = 5;
    public int difficulty = 1;

    public Text timerText;

    private bool alive = true;

	// Use this for initialization
	void Start ()
    {
        alive = true;
        timer += difficulty;
        timerText.text = "Time: " + timer + "s";
        StartCoroutine(StartTimer());
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(-Vector3.forward * Input.GetAxis("Horizontal") * Time.deltaTime * RotationSpeed);
    }

    IEnumerator StartTimer()
    {
        while (timer > 0 && alive)
        {
            yield return new WaitForSeconds(1);
            timer--;
            timerText.text = "Time: " + timer + "s";
        }
        gameOver();
    }

    //gameOver
    void OnTriggerEnter2D(Collider2D other)
    {
        alive = false;
        gameOver();
        gameObject.SetActive(false);
        other.gameObject.SetActive(false);
    }

    public void gameOver()
    {
        if(alive) Debug.Log("You win");
        else Debug.Log("You lose");
    }
}
