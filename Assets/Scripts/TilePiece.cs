using UnityEngine;

/// <summary>
/// Background piece that moving pieces are placed on and interact with
/// </summary>
public class TilePiece : MonoBehaviour
{
    //TODO implement
    //private Animator animator;

    public int Column { get { return (int)transform.position.x; } }
    public int Row { get { return -(int)transform.position.y; } }

    public Color PieceColour;

    public bool CanBeLandedOn;
    public bool CanBeJumpedOver;


    public bool IsDestroyedOnJumpOver;
    public bool IsDestroyedOnMoveOn;

    public bool KillsPieceOnLand;
    public bool KillsPieceOnJumpOver;
    public bool IsRedirector;


    public int RedirectColumnOffset;
    public int RedirectRowOffset;

    public bool IsDestination;

    [HideInInspector]
    public MovablePiece MovingPiece;


    public bool HasMovingPiece
    {
        get
        {
            return MovingPiece != null;
        }
    }

    public bool IsCompleted 
    {
        get 
        {
            return IsDestination && MovingPiece != null && this.PieceColour == MovingPiece.PieceColour;
        } 
    }

    public bool IsLandable
    {
        get
        {
            return MovingPiece == null && CanBeLandedOn;
        }
    }


    //OLD
    //public bool HasMovableOnIt { get { return GameManager.Instance.MovablePieces.Any(m => m.Row == Row && m.Column == Column); } }
    //public bool IsLandable { get { return CanBeLandedOn && !HasMovableOnIt; } }



    public void AnimatePieceLandedOn() { }
    public void AnimatePieceJumpedOver() { }



    // Use this for initialization
    void Start()
    {
        //GameManager.Instance.AddTilePiece(this);
    }


    

    //// Update is called once per frame
    //void Update () {

    //}
}
