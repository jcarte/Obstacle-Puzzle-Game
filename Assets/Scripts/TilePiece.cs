using UnityEngine;
using System.Collections;
using System.Linq;

public class TilePiece : BasePiece {

    //TODO implement
    //private Animator animator;

    public bool CanBeLandedOn;
    public bool CanBeJumpedOver;


    public bool IsDestroyedOnJumpOver;
    public bool IsDestroyedOnMoveOn;

    public bool KillsPieceOnLand;
    public bool KillsPieceOnJumpOver;
    public bool IsRedirector;


    //public Vector2 RedirectDirection;//TODO FIX THIS

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
