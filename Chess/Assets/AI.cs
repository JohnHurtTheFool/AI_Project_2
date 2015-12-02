using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move
{
    public int startX, startY, endX, endY;
    public pieces piece;
}
class PieceInfo
{
    pieces piece;
    int xPos, yPos;
    List<Move> legalMoves = new List<Move> { };
}

public class AI : MonoBehaviour {

    public Board board = new Board();
    public GameObject boardObject;
    public Move myMove = new Move();
    List<PieceInfo> myPieces = new List<PieceInfo>();
	// Use this for initialization
	void Start () {
        board = boardObject.GetComponent<gameScript>().board;
        for (int a = 0; a < 8; a++)
        {
            for (int b = 7; b > 0; b--)
            {
                if (board.isBlack(a, b))
                {

                }
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
        board = boardObject.GetComponent<gameScript>().board;
        System.Random rand = new System.Random();
        if (!boardObject.GetComponent<gameScript>().isWhitesTurn)
        {
            myMove.startX = rand.Next(6, 6);
            myMove.startY = rand.Next(6, 7);
            myMove.endX = rand.Next(5, 7);
            myMove.endY = rand.Next(0, 7);
            Debug.Log("startX: " + myMove.startX + " start Y " + myMove.startY+ " End X: " + myMove.endX + "End Y" + myMove.endY);
            
        }
    }
}
