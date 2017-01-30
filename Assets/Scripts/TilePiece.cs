using UnityEngine;

/// <summary>
/// Background piece that moving pieces are placed on and interact with
/// </summary>
public class TilePiece : MonoBehaviour
{
    //TODO implement
    //private Animator animator;
    
    /// <summary>
    /// Colour of piece, only used for destination tiles to match with moving pieces
    /// </summary>
    public Color PieceColour;

    /// <summary>
    /// Can moving piece land on this tile?
    /// </summary>
    public bool CanBeLandedOn;

    /// <summary>
    /// Can moving pieces jump over this tile?
    /// </summary>
    public bool CanBeJumpedOver;

    /// <summary>
    /// Is this tile destroyed when a moving piece jumps over it?
    /// </summary>
    public bool IsDestroyedOnJumpOver;

    /// <summary>
    /// Is this tile destroyed when a moving piece lands on it?
    /// </summary>
    public bool IsDestroyedOnMoveOn;

    /// <summary>
    /// Is the moving piece killed when it lands on this tile?
    /// </summary>
    public bool KillsPieceOnLand;

    /// <summary>
    /// Is the moving piece killed when it jumps over this tile?
    /// </summary>
    public bool KillsPieceOnJumpOver;

    /// <summary>
    /// Does this tile redirect a moving piece along a vector once it lands here?
    /// </summary>
    public bool IsRedirector;

    /// <summary>
    /// The number of columns this a moving tile is offset if this tile is a redirector
    /// </summary>
    public int RedirectColumnOffset;

    /// <summary>
    /// The number of rows this a moving tile is offset if this tile is a redirector
    /// </summary>
    public int RedirectRowOffset;

    /// <summary>
    /// Is this tile a destination (target) for a moving tile of the same colour to land on?
    /// </summary>
    public bool IsDestination;

    /// <summary>
    /// Moving piece currently on this tile
    /// </summary>
    [HideInInspector]
    public MovablePiece MovingPiece;

    /// <summary>
    /// Column location of this tile on the board (zero based)
    /// </summary>
    public int Column { get { return (int)transform.position.x; } }

    /// <summary>
    /// Row location of this tile on the board (zero based)
    /// </summary>
    public int Row { get { return -(int)transform.position.y; } }

    /// <summary>
    /// Does this tile currently have a moving piece on it?
    /// </summary>
    public bool HasMovingPiece { get { return MovingPiece != null; } }

    /// <summary>
    /// Is this tile a destination and does it currently have a moving piece of the same colour on it?
    /// </summary>
    public bool IsCompleted { get { return IsDestination && MovingPiece != null && this.PieceColour == MovingPiece.PieceColour; } }

    /// <summary>
    /// Could a moving piece currently land on this tile?
    /// </summary>
    public bool IsLandable { get { return MovingPiece == null && CanBeLandedOn; } }

}
