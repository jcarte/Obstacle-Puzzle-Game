﻿using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

public class MovablePiece : BasePiece
{

    public int Row;
    public int Column;
    
    private Rigidbody2D rb2D;

    public float moveTime = 0.1f;			//Time it will take object to move, in seconds.
    private float inverseMoveTime;          //Used to make movement more efficient.

    //[HideInInspector]
    //public bool IsSelected = false;

    [HideInInspector]
    public TilePiece PieceInside = null;

    public event EventHandler MovementCompleted;
    public event EventHandler Clicked;

    private Queue<Vector2> moveq = new Queue<Vector2>();

    public bool IsMoving { get; private set; }

    // Use this for initialization
    void Start()
    {
        IsMoving = false;
        
        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;

        rb2D = GetComponent<Rigidbody2D>();


        Column = (int)transform.position.x;
        Row = -(int)transform.position.y;


    //GameManager.Instance.AddMovablePiece(this);
}

    // Update is called once per frame
    //void Update()
    //{

    //}



    public void Move(params TilePiece[] ts)
    {
        if (PieceInside != null)
            PieceInside.MovingPiece = null;

        foreach (TilePiece t in ts)
        {
            //Store start position to move from, based on objects current transform position.
            //Vector2 start = transform.position;

            // Calculate end position based on the direction parameters passed in when calling Move.
            //Vector2 end = start + new Vector2((t.Column - Column), -(t.Row - Row));

            Vector2 end = t.transform.position;

            moveq.Enqueue(end);
        }

        TilePiece last = ts.Last();
        last.MovingPiece = this;
        PieceInside = last;

        Row = last.Row;
        Column = last.Column;

        if(!IsMoving)
            StartCoroutine(SmoothMovement());
    }

    //public void Jump(TilePiece t)
    //{
    //    Jump(t.Row - Row, t.Column - Column);
    //}

    //TODO Merge move and jump? Need a redirect?
    //private void Move(int rows, int cols)
    //{

    //    //TODO add animation

        

        

    //    //Debug.Log("StartMove (" + start.y + "," + start.x + ") End (" + end.y + "," + end.x + ")");

    //    //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
    //    StartCoroutine(SmoothMovement(end));



    //}


 



    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    //protected IEnumerator SmoothMovement(Vector3 end)
    protected IEnumerator SmoothMovement()
    {

        IsMoving = true;

        while (moveq.Count > 0)
        {
            Vector3 end = moveq.Dequeue();

            //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
            //Square magnitude is used instead of magnitude because it's computationally cheaper.
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //While that distance is greater than a very small amount (Epsilon, almost zero):
            while (sqrRemainingDistance > float.Epsilon)
            {
                //Find a new position proportionally closer to the end, based on the moveTime
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rb2D.MovePosition(newPostion);

                //Recalculate the remaining distance after moving.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;


                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            }
        }
        

        if (MovementCompleted != null)
            MovementCompleted.Invoke(this, null);
        

        IsMoving = false;
    }

    public void Kill()
    {
        StopAllCoroutines();

        Clicked = null;
        PieceInside.MovingPiece = null;
        PieceInside = null;
        gameObject.SetActive(false);

        if (MovementCompleted != null)
            MovementCompleted.Invoke(this, null);
    }
    

    private void OnMouseDown()
    {
        if (Clicked != null)
            Clicked.Invoke(this, null);
        //Debug.Log("sprite clicked");
    }




}
