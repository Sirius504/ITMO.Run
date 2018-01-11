using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public List<BackgroundLayer> movingLayers;                      //Holds the moving layer
    
//    public GameObject playTriggerer;                            //Holds the play triggerer

    public float speedIncreaseRate;                             //The scrolling speed increase rate per second
    public float distance;                                      //The current distance

    private float speedMultiplier;                              //Holds the speed multiplier
    private float lastSpeedMultiplier;                          //Holds the last speed multiplier
    
    private bool canModifySpeed;                                //True, if the generator can modify the current scrolling speed

    // Used for initialization
    void Start()
    {
        speedMultiplier = 1;
        StartToGenerate();
    }
    // Update is called once per frame
    void Update()
    {
        //If the game is not paused
       
        
            //If the speed can be modified
            if (canModifySpeed)
            {
                //Increase scrolling speed
                speedMultiplier += speedIncreaseRate * Time.deltaTime;                           
            }

            //Increase distance
            distance += 10 * speedMultiplier * Time.deltaTime;

            //Pass speed multiplier to the layers
            foreach (BackgroundLayer item in movingLayers)
                item.UpdateSpeedMultiplier(speedMultiplier);         
        
    }
       
    //Changes the speed multiplier to newValue in time
    IEnumerator ChangeScrollingMultiplier(float newValue, float time, bool enableIncrease)
    {
        //Declare variables, get the starting position, and move the object
        float i = 0.0f;
        float rate = 1.0f / time;

        float startValue = speedMultiplier;

        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            speedMultiplier = Mathf.Lerp(startValue, newValue, i);
           

            //Wait for the end of frame
            yield return 0;
        }        
    }

   
    //Resume the generator after a revive
    public void ContinueGeneration()
    {
         StartCoroutine(ChangeScrollingMultiplier(lastSpeedMultiplier, 0.5f, true));
    }
    
    //Starts the level Generator
    public void StartToGenerate()
    {
        //       paused = false;
        canModifySpeed = true;

 //       playTriggerer.SetActive(false);

        foreach (BackgroundLayer item in movingLayers)
            item.StartScrolling();
       
    }
    //Stops the level generaton under time
    public void StopGeneration(float time)
    {
        lastSpeedMultiplier = speedMultiplier;
        canModifySpeed = false;      

        StartCoroutine(ChangeScrollingMultiplier(0, time, false));
    }
    
   
    //Return the current distance as an int
    public int CurrentDistance()
    {
        return (int)distance;
    }
    
}
        