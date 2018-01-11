using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int health;

    private Rigidbody2D rb2d;
    private BoxCollider2D cld2d;
    private Vector2 collOffset;
    private Vector2 collSize;
    private float slideTime = 0.01f;
    private float trueScrollSpeed;

    public Vector2 jumpVelocity;
    public Vector2 slideOffset;
    public Vector2 slideSize;
    public float slideTimer;
    public LevelGenerator bgScroller;
    public float recoverTime;
    public float recoverSpeed;

    private enum PlayerStatus { Idle, Grounded, InAir, Sliding }

    private PlayerStatus playerStatus;

    // Use this for initialization
    void Start()
    {
        playerStatus = PlayerStatus.Grounded;
        rb2d = GetComponent<Rigidbody2D>();
        cld2d = GetComponent<BoxCollider2D>();
    }
	
	
    void FixedUpdate () {
     
              
        
        if (playerStatus == PlayerStatus.Sliding)
        {
            slideTime += Time.deltaTime;
            if (slideTime >= slideTimer)
            {
                slideTime = 0.0f;
                cld2d.offset = collOffset;
                cld2d.size = collSize;
                rb2d.simulated = true;
                playerStatus = PlayerStatus.Grounded;
            }
        }

	}

    void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.tag == "Enemy")
        {
            health--;

        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        playerStatus = PlayerStatus.Grounded;
    }

   public void UpdateInput(string playerInput)
    {
        switch (playerInput)
        {
            case "Up":
                if (playerStatus == PlayerStatus.Grounded)
                {
                    rb2d.velocity += jumpVelocity;
                    playerStatus = PlayerStatus.InAir;
                }
                
                break;
            
            case "Down":
                    if(playerStatus == PlayerStatus.Grounded)
                         {
                            playerStatus = PlayerStatus.Sliding;
                            collSize = cld2d.size;
                            collOffset = cld2d.offset;
                            cld2d.offset = slideOffset;
                            cld2d.size = slideSize;
                         }
                    else
                        rb2d.AddForce(-jumpVelocity * 20);
                   
                    break;

            case "Tap":
                Debug.Log("Tap"); //functional would be added later (or wouldn't)
                break;
        }
    }



}
