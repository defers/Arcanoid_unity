using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerMain : MonoBehaviour {

    public enum GameStatus : int { NotStarted, Started, Win, Loose };
    public int healths = 3;
    public bool gameStarted;
    public bool gameHasWon;
    public GameStatus gameStatus;
    public GameObject brick;
    Canvas canvas;
    Text[] text;
    public int allBricksLen;
    public int scores;
    string status;


    BrickScript[] allBricks1;

    // Use this for initialization
    void Start() {
        CreateBrickWall();
        gameStatus = GameStatus.NotStarted;
        canvas = FindObjectOfType<Canvas>();
        text = canvas.GetComponentsInChildren<Text>();
        scores = 0;

       // gameStarted = true; //Отладка
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate()
    {
        CheckGameStatus();
        UpdateText();
    }

    void CheckGameStatus (){
        if (gameStarted && healths > 0) {
            gameStatus = GameStatus.Started;
        }
        if (healths == 0) {
            gameStatus = GameStatus.Loose;
            status = "LOOSE! Click left button to restart";

        }
        if (gameHasWon) {
            gameStatus = GameStatus.Win;
            status = "WIN! Click left button to restart";
        }
        if (gameStatus == GameStatus.Loose) {
            if (Input.GetMouseButtonDown(0)) {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (gameStatus == GameStatus.Started && allBricksLen == 0)
        {
            gameHasWon = true;
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    public void LoseHealth() {
        healths--;
    }

    void CreateBrickWall() {
        int wallLength = 8;
        int wallWidth = 7;
        //Vector3 screenPos = -1*Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 screenPos = new Vector3(-5.75f,3.45f, 0f );

        float posRowX = screenPos.x;
        float posRowY = screenPos.y;

        for (int row = 0; row < wallLength; row++) {
            
            for (int col = 0; col < wallWidth; col++) {
                Vector3 position = new Vector3(posRowX, posRowY, 0);
                Quaternion quater = Quaternion.identity;
                float rand = (float) System.Math.Round(Random.value);
                if (rand < 0.5) {
                }
                else {

                    Instantiate(brick, position, quater);


                }
                
                posRowX += brick.transform.localScale.x;
            }

            posRowX = screenPos.x;
            posRowY -= 0.4f ;
        }

        allBricks1 = FindObjectsOfType<BrickScript>();
        allBricksLen = allBricks1.Length;

    }

    void UpdateText() {
        foreach (Text elem in text) {
            if (elem.name == "Health") {
                elem.text = "Healths: " + System.Convert.ToString(healths);
            }
            if (elem.name == "BricksRemaining")
            {
                elem.text = "Bricks remaining: " + System.Convert.ToString(allBricksLen);
            }
            if (elem.name == "Score")
            {
                elem.text = "Scores: " + System.Convert.ToString(scores);
            }
            if (elem.name == "StatusText" && (gameStatus == GameStatus.Loose || gameStatus == GameStatus.Win))
            {
                elem.text = "Status: " + status;
            }
        }
    }

}
