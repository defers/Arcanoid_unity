using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject GM;
    public GameObject Paddle;
    GameManagerMain GMScript;
    Rigidbody2D ballRigidBody;
    float thrust;
    public bool ballIsActive;

    // Use this for initialization
    void Start () {
        thrust = Random.Range(100.0f, 150.0f);
        ballIsActive = false;
        GMScript = GM.GetComponent<GameManagerMain>();
        ballRigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        MoveBall();

    }

    private void FixedUpdate()
    {
        
    }

    void MoveBall()
    {
        if (GMScript.gameStatus == GameManagerMain.GameStatus.NotStarted || !ballIsActive)
        {
            float y_paddle = Paddle.GetComponent<BoxCollider2D>().offset.y;
            transform.position = new Vector3(Paddle.transform.position.x, Paddle.transform.position.y + y_paddle + 0.5f, Paddle.transform.position.z);
        }

        if (GMScript.gameStatus == GameManagerMain.GameStatus.NotStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                GMScript.gameStatus = GameManagerMain.GameStatus.Started;
                ballIsActive = true;
    
                HitBall();
            }
        }

        if (GMScript.gameStatus == GameManagerMain.GameStatus.Started && !ballIsActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ballIsActive = true;
          
                HitBall();
            }
        }
    }
    void HitBall() {
        ballRigidBody.AddForce(new Vector2(transform.position.x, transform.position.y)*thrust);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Underground"))
        {
            GMScript.LoseHealth();
            ballIsActive = false;
            ballRigidBody.Sleep();

            MoveBall();
        }
        if (collision.gameObject.name.Equals("Paddle"))
        {
            HitBall();
        }
    }
}
