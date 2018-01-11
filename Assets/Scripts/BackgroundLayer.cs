using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundLayer : MonoBehaviour
{
    public Transform container;                 //Contains the layer elements

    public float startingSpeed;                 //The starting speed of the layer

    public float startAt;                       //The spawned layer elements starts on this X coordinate
    public float resetAt;                       //Reset a layer element, when it reaches this global X coordinates
    public float tileSize;

    public float delayBeforeFirst;              //How much delay should be applied before the spawn of the first element

                                               //    protected List<Transform> inactive;         //Contains the inactive layer elements
    protected List<Transform> tileObjects;
    protected Transform lastTile;               // Information about last tile we need to attach reseted tile to the last one
    protected Transform tileNeedToReset;
    public List<Sprite> textures;         //Contains textures



    protected float speedMultiplier;            //The current speed multiplier
    protected float deltaPosition;

    protected bool paused;                      //True, if the level is paused
    protected bool resetLast;                  //True, if the first element has passed the reset position



    // Use this for initialization
    public virtual void Start()
    {
        speedMultiplier = 1;
        paused = false;

        tileObjects = new List<Transform>();
        
        foreach (Transform child in container)
        {
            tileObjects.Add(child);
        }
        lastTile = tileObjects[tileObjects.Count - 1];

    }
    // Update is called once per frame
    void Update()

    {
        deltaPosition = startingSpeed * speedMultiplier * Time.deltaTime;

        if (!paused)
        {

            //Loop through the active elemets
            foreach (Transform tileObject in tileObjects)
            {
                //Move the item to the left
                tileObject.transform.position -= Vector3.right * deltaPosition;

                //If the item has reached the reset position
                if (tileObject.transform.position.x < resetAt)
                {
                    resetLast = true;
                    tileNeedToReset = tileObject;
                }
            }
            if (resetLast)
            {
                ResetTile(tileNeedToReset);
                lastTile = tileNeedToReset;
                resetLast = false;
            }

        }
    }

    //Removes the first element
    private void ResetTile(Transform tileObject)
    {
        //Reset it's position
        Tile tile = tileObject.GetComponent<Tile>();
        tile.PlaceOverLastTile(lastTile.position.x, tileSize);
        Debug.Log(lastTile.position.x);
        //       tile.transform.position = new Vector3(startAt, tile.transform.position.y, 0);
        //Change it's texture
        tile.ChangeTexture2D(textures[Random.Range(0, textures.Count)]);
        

    }

    //Spawns a new layer element with a delay
   /* private IEnumerator SpawnDelayedElement(float time)
    {
        //Declare starting variables
        float i = 0.0f;
        float rate = 1.0f / time;

        //Wait for time
        while (i < 1.0)
        {
            if (!paused)
                i += Time.deltaTime * rate;

            yield return 0;
        }

        //Spawn the element
        StartCoroutine("Generator");
    } */
    //Spawn new layer elements at the given rate
 /*   private IEnumerator Generator()
    {
        //Spawn a new element
        SpawnElement(false);

        //Declare variables, get the starting position, and move the object
        float i = 0.0f;
        float rate = 1.0f / spawningRate;

        while (i < 1.0)
        {
            //If the game is not paused, increase t, and scale the object
            if (!paused)
            {
                i += (Time.deltaTime * rate) * speedMultiplier;
            }

            //Wait for the end of frame
            yield return 0;
        }

        //Restart the generator
        StartCoroutine("Generator");
    } */

    //Starts the generation of this layer
    public virtual void StartScrolling()
    {
        //Unpause the generator, and spawn the first element
        paused = false;
                
    }
    //Spawns a new layer element
    /* public virtual void SpawnElement(bool generateInMiddle)
    {
        //Get a random item from the inactive elements
        Transform item = inactive[Random.Range(0, inactive.Count)];

        //If it is a background change texture

        if (isBackground)
        {
            Renderer renderer = item.GetComponent<Renderer>();
            renderer.material.SetTexture("_MainTex", textures[Random.Range(0, textures.Count)]);
        }

        if (generateInMiddle)
            item.position = new Vector3(0, item.position.y, 0);


        //Activate it, and add it to the active elements
        item.gameObject.SetActive(true);
        inactive.Remove(item);
        activeElements.Add(item);
    } */
    //Sets the pause state
    public virtual void SetPauseState(bool newState)
    {
        paused = newState;
    }
    //Removes every item from the level
    public virtual void Reset()
    {
        paused = true;

        StopAllCoroutines();

//        foreach (Transform item in tiles)
//        {
            
//        }

 //       activeElements.Clear();

 //       if (generateInMiddle)
 //           SpawnElement(true);
    }

    //Updates the speed multiplier
    public void UpdateSpeedMultiplier(float n)
    {
        speedMultiplier = n;
    }
}
