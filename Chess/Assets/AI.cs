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
    public pieces piece;
	//pinned to King
	bool isPinned = false;
	//pinned to higher value piece
	bool isSoftPinned = false;
    public int xPos, yPos;
    public List<Move> legalMoves = new List<Move> { };
}



public class AI : MonoBehaviour {

    public Board board = new Board();
    public GameObject boardObject;
    public Move myMove = new Move();
    List<PieceInfo> myPieces = new List<PieceInfo>();
	// Use this for initialization


	List<Move> getKingMoves(int x, int y){


	}
	List<Move> getKnightMoves(int x, int y){
		List<Move> moves = new List<Move> ();
		if (x - 2 >= 0 && x - 1 >= 0) {
			Move move = new Move();
			move.startX = x;
			move.startY = y;
			move.endX = x - 2;
			move.endY = x - 1;
			move.piece = pieces.bKNIGHT;
		}
	}
	List<Move> getBishopMoves(int x, int y){
	
	}
	List<Move> getPawnMoves(int x, int y){

	}
	List<Move> getRookMoves(int x, int y){

	}
	List<Move> getQueenMoves(int x, int y){

	}
	List<Move> getMoves(pieces piece, int x, int y){
		switch (piece) {
			case pieces.bKING:
				getKingMoves(x, y);
			break;
			case pieces.bBISHOP:
			break;
			case pieces.bKNIGHT:
			break;
			case pieces.bROOK:
			break;
			case pieces.bQUEEN:
			break;
			case pieces.bPAWN:
			break;
		}
	}
	void Start () {
        board = boardObject.GetComponent<gameScript>().board;
        for (int a = 0; a < 8; a++)
        {
            for (int b = 0; b < 8; b++)
            {
                if (board.isBlack(a, b))
                {
					PieceInfo temp = new PieceInfo();
					temp.piece = board.gameBoard[a,b];
					temp.xPos = a;
					temp.yPos = b;
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
            myMove.startX = rand.Next(0, 8);
            myMove.startY = rand.Next(0, 8);
            myMove.endX = rand.Next(0, 8);
            myMove.endY = rand.Next(0, 8);
            Debug.Log("startX: " + myMove.startX + " start Y " + myMove.startY+ " End X: " + myMove.endX + "End Y" + myMove.endY);
            
        }
    }
}
