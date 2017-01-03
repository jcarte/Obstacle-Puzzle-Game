using UnityEngine;
using System.Collections;

public class TilePiece : MonoBehaviour {

    //TODO implement
    //private Animator animator;

    public bool CanBeLandedOn;
    public bool CanBeJumpedOver;
    public void AnimatePieceLandedOn() { }
    public void AnimatePieceJumpedOver() { }


    public bool IsDestroyedOnJumpOver;
    public bool IsDestroyedOnMoveOn;

    public bool KillsPieceOnLand;
    public bool KillsPieceOnJumpOver;
    public bool IsRedirector;

    [HideInInspector]
    public Vector2 RedirectDirection;
    public bool IsDestination;

    public Color PieceColour;


    public int Column { get { return (int)transform.position.x; } }
    public int Row { get { return -(int)transform.position.y; } }

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.AddTilePiece(this);
    }


    

    //// Update is called once per frame
    //void Update () {

    //}
}
