using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

/// <summary>
/// Pieces on the board that the user controls and moves around, one selected at a time
/// </summary>
public class MovablePiece : MonoBehaviour
{
    /// <summary>
    /// The tile has finished moving
    /// </summary>
    public event EventHandler MovementCompleted;
    
    /// <summary>
    /// The tile was clicked by the user
    /// </summary>
    public event EventHandler Clicked;

    /// <summary>
    /// Colour of piece, aiming to land on destination of same colour
    /// </summary>
    public Color PieceColour;

    /// <summary>
    /// Row on the game board (zero based)
    /// </summary>
    [HideInInspector]
    public int Row;

    /// <summary>
    /// Column on the game board (zero based)
    /// </summary>
    [HideInInspector]
    public int Column;

    /// <summary>
    /// The board tile piece that this object is sitting inside (on)
    /// </summary>
    [HideInInspector]
    public TilePiece PieceInside = null;

    /// <summary>
    /// Is this piece currently moving
    /// </summary>
    public bool IsMoving { get; private set; }

    /// <summary>
    /// Is the piece instructed to kill itself?
    /// </summary>
    public bool IsKillPending = false;

    public float moveTime = 0.1f;//Time it will take object to move, in seconds.
    private float inverseMoveTime;//Used to make movement more efficient.

    private Rigidbody2D rb2D;
    private SpriteRenderer sRender;

    private Queue<Vector2> moveq = new Queue<Vector2>(25);//Ordered queue of moves (destinations) to move the piece

    void Awake()
    {
        IsMoving = false;
        
        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;

        rb2D = GetComponent<Rigidbody2D>();
        sRender = GetComponent<SpriteRenderer>();

        Column = (int)transform.position.x;//starting position
        Row = -(int)transform.position.y;
    }

    //TODO add animation

    /// <summary>
    /// Move this piece through all given tiles in order
    /// </summary>
    /// <param name="ts">All tiles to move ontop of</param>
    public void Move(params TilePiece[] ts)
    {
        if (PieceInside != null)
            PieceInside.MovingPiece = null;
        
        //Move to each tile
        foreach (TilePiece t in ts)
        {
            moveq.Enqueue(t.transform.position);//add position of every tile to moveto queue
        }

        //Finish the piece in the final queued tile
        TilePiece last = ts.Last();
        last.MovingPiece = this;
        PieceInside = last;

        Row = last.Row;
        Column = last.Column;

        //if not already moving, start
        if(!IsMoving)
            StartCoroutine(SmoothMovement());
    }

    
    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    //protected IEnumerator SmoothMovement(Vector3 end)
    protected IEnumerator SmoothMovement()
    {
        sRender.sortingOrder = 1;//when moving put the sprite above all others
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

        sRender.sortingOrder = 0;//when moving complete return sprite to normal layer order

        if (MovementCompleted != null)
            MovementCompleted.Invoke(this, null);

        IsMoving = false;

        if(IsKillPending)
        {
            Kill();
        }
    }

    /// <summary>
    /// Set the piece to be killed
    /// </summary>
    public void Kill()
    {
        if(IsMoving)
        {
            IsKillPending = true;
        }
        else
        {
            StopAllCoroutines();

            Clicked = null;
            PieceInside.MovingPiece = null;
            PieceInside = null;
            gameObject.SetActive(false);
        }
    }
    
    //User clicked on this object
    private void OnMouseDown()
    {
        if (Clicked != null)
            Clicked.Invoke(this, null);
        //Debug.Log("sprite clicked");
    }

}
