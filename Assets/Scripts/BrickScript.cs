using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour {
    int health;
    SpriteRenderer brickSpriteRendering;
    public GameObject gm;
    GameManagerMain gmm;
    // Use this for initialization
    void Start () {
        health = 2;
        brickSpriteRendering = GetComponent<SpriteRenderer>();
        gm = GameObject.Find("GameManager");
        gmm = gm.GetComponent<GameManagerMain>();
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") {
            float r, g, b;
            r = Random.Range(0f, 255.0f);
            g = Random.Range(0f, 255.0f);
            b = Random.Range(0f, 255.0f);
            brickSpriteRendering.color = new Color(r, g, b);
            health--;
            gmm.scores += 100;
            if (health <= 0) {
                gmm.allBricksLen -= 1;
                Destroy(this.gameObject);
                
            }
        }
    }

}
