﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    //Templates
    public GameObject[] MovablePieces;          //100-109
    public GameObject[] DestinationPieces;      //200-209
    public GameObject EmptyPiece;               //0
    public GameObject LandingPiece;             //1
    public GameObject NonLandingPiece;          //2
    public GameObject ObstaclePiece;            //3
    public GameObject RedirectPiece;            //4
    public GameObject EnemyPiece;               //5
    public GameObject DisappearingPiece;        //6
    public GameObject FakeDisappearingPiece;    //7



    //public int NumberOfRows { get; private set; }
    //public int NumberOfColumns { get; private set; }

    public void SetupBoard(byte[,] config)
    {
        

        List<GameObject> normPieces = new List<GameObject>()
        { EmptyPiece, LandingPiece, NonLandingPiece, ObstaclePiece, RedirectPiece,EnemyPiece,DisappearingPiece,FakeDisappearingPiece};

        for (int r = 0; r <= config.GetUpperBound(0); r++)
        {
            for (int c = 0; c <= config.GetUpperBound(1); c++)
            {
                byte id = config[r, c];//TODO add check on id, more exceptions etc
                GameObject t;
                if (id < 8)
                    t = normPieces[id];
                else if (id >= 100 && id <= 109)
                    t = MovablePieces[id - 100];
                else if (id >= 200 && id <= 209)
                    t = DestinationPieces[id - 200];
                else
                    throw new System.Exception("Config Parsing Error, Unrecognised ID");

                Instantiate(t, new Vector3(c, -r, 0f), Quaternion.identity);
                
            }
        }

    }
    


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //If it's not the player's turn, exit the function.
        if (!GameManager.Instance.PlayerCanMove) return;

        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.

        //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER

        //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));

        //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        

        //Check if moving horizontally, if so set vertical to zero.
        if (horizontal != 0)
        {
            vertical = 0;
        }
        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			
			//Check if Input has registered more than zero touches
			if (Input.touchCount > 0)
			{
				//Store the first touch detected.
				Touch myTouch = Input.touches[0];
				
				//Check if the phase of that touch equals Began
				if (myTouch.phase == TouchPhase.Began)
				{
					//If so, set touchOrigin to the position of that touch
					touchOrigin = myTouch.position;
				}
				
				//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
				else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
				{
					//Set touchEnd to equal the position of this touch
					Vector2 touchEnd = myTouch.position;
					
					//Calculate the difference between the beginning and end of the touch on the x axis.
					float x = touchEnd.x - touchOrigin.x;
					
					//Calculate the difference between the beginning and end of the touch on the y axis.
					float y = touchEnd.y - touchOrigin.y;
					
					//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
					touchOrigin.x = -1;
					
					//Check if the difference along the x axis is greater than the difference along the y axis.
					if (Mathf.Abs(x) > Mathf.Abs(y))
						//If x is greater than zero, set horizontal to 1, otherwise set it to -1
						horizontal = x > 0 ? 1 : -1;
					else
						//If y is greater than zero, set horizontal to 1, otherwise set it to -1
						vertical = y > 0 ? 1 : -1;
				}
			}
			
#endif 
        //End of mobile platform dependendent compilation section started above with #elif
        //Check if we have a non-zero value for horizontal or vertical
        if (horizontal != 0 || vertical != 0)
        {
            
            //vertically down (-1) means increase row number (+1)
            Debug.Log("Input: " + horizontal + "," + -vertical);
            GameManager.Instance.MoveCurrentPiece(horizontal, -vertical);

        }
    }
}
