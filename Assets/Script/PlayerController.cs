using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    public static Action<int> scoreUpdate;
    public static Action enimyCollision;
    public float speed;
    public float maxSpeed;
    [Range(0,1)]
    public float difficultyFactor;
    private int score = 0;
    private float distance;
    private int coinsCollected;
    bool isGameOver = false;

    public GameObject gameOver;
    public Text hudScore, finalScore;

    private void Start()
    {
        SwipeGuesture.onSwipLeft += SwipeLeft;
        SwipeGuesture.onSwipeRight += SwipeRight;
    }
    private void OnDestroy()
    {
        SwipeGuesture.onSwipLeft -= SwipeLeft;
        SwipeGuesture.onSwipeRight -= SwipeRight;
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameOver) return;
        movement = transform.forward * speed * Time.deltaTime;
        Swipe();
        transform.Translate(movement);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x,-1f,1f), transform.position.y, transform.position.z);
        var speedIncrement = Time.deltaTime * difficultyFactor;
        if (speed < maxSpeed)
        {
            speed += speedIncrement;
        }
        distance = Time.time * speed;
        ScoreUpdate();
    }

    private void SwipeLeft()
    {
        movement.x -= 1f;
    }

    private void SwipeRight()
    {
        movement.x += 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                coinsCollected++;
                ScoreUpdate();
                Destroy(other.gameObject);
                break;
            case "Enemy":
                isGameOver = true;
                GameOver();
                break;
        }
       
    }
    void ScoreUpdate()
    {
        score =(int) distance + (coinsCollected * 5);
        hudScore.text = "Score : " + score.ToString();
    }
    void GameOver()
    {
        finalScore.text = "Your score : " + hudScore.text;
        gameOver.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

           
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
                SwipeLeft();
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
        {
                SwipeRight();
            }
        }
    }
}
