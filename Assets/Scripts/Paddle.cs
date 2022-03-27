using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public enum StateEnum : int {Idle, LightsRunning, HitLights}
    [SerializeField]
    public int stateAnim;
    public GameObject GM;
    public GameObject Ball;
    Ball BallScript;
    GameManagerMain GMScript;
    public Animator paddleAnimator;
    [SerializeField]
    public float speed;
    string additionalStatus;

    private void Awake()
    {
        paddleAnimator = GetComponent<Animator>();
        speed = 5.0f;
    }

    // Use this for initialization
    void Start () {
        GMScript = GM.GetComponent<GameManagerMain>();
        BallScript = Ball.GetComponent<Ball>();
        InvokeRepeating("CheckStatus", 0.5f, 1.5f);
    }
	
	// Update is called once per frame
	void Update () {
        

        

    }

    private void FixedUpdate()
    {
        if (GMScript.gameStatus == GameManagerMain.GameStatus.Started || GMScript.gameStatus == GameManagerMain.GameStatus.NotStarted)
        {
            Move();
        }
    }

    void CheckStatus()
  
    {
        switch (GMScript.gameStatus)
        {
            case GameManagerMain.GameStatus.NotStarted:
                stateAnim = (int)StateEnum.Idle;
                break;
            case GameManagerMain.GameStatus.Loose:
                stateAnim = (int)StateEnum.Idle;
                break;
            case GameManagerMain.GameStatus.Win:
                stateAnim = (int)StateEnum.Idle;
                break;
            case GameManagerMain.GameStatus.Started:
                if (additionalStatus == "hit_ball")
                {
                    stateAnim = (int)StateEnum.HitLights;
                }
                else { stateAnim = (int)StateEnum.LightsRunning; }
                
                break;
            

        }
        paddleAnimator.SetInteger("stateAnim", stateAnim);
        additionalStatus = "";

    }

    void Move() {
   
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
     

        ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Wall")) {
            CheckBorders(collision);
        }
        if (collision.gameObject.tag.Equals("Ball") && BallScript.ballIsActive) {
            additionalStatus = "hit_ball";
            CheckStatus();

        }

    }

    void CheckBorders(Collision2D collision) {
        float contactX = collision.contacts[0].point.x;
        transform.position = new Vector3(contactX, transform.position.y, transform.position.z);
        
    }


    
}
