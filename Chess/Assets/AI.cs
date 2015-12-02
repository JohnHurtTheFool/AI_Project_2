using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move
{
    public int startX, startY, endX, endY;
    public pieces piece;
    public Move()
    {}
    public Move(pieces p,  int sX, int sY, int eX, int eY)
    {
        piece = p;
        startX = sX;
        startY = sY;
        endX = eX;
        endY = eY;
    }
}
class PieceInfo
{
	int value;
    public pieces piece;
    //pinned to King
    bool isPinned = false;
    //pinned to higher value piece
    bool isSoftPinned = false;
    public int xPos, yPos;
    public List<Move> legalMoves = new List<Move> { };
	
	public PieceInfo(pieces p, int x, int y){
		piece = p;
		xPos = x;
		yPos = y;
		if (piece == pieces.bBISHOP || piece == pieces.bKNIGHT || piece == pieces.wBISHOP || piece == pieces.wKNIGHT) {
			value = 3;
		} else if (piece == pieces.wPAWN || piece == pieces.bPAWN) {
			value = 1;
		} else if (piece == pieces.wROOK || piece == pieces.bROOK) {
			value = 5;
		} else if (piece == pieces.bQUEEN || piece == pieces.wQUEEN) {
			value = 9;
		}
	}
}



public class AI : MonoBehaviour
{

    public Board board = new Board();
    public GameObject boardObject;
    public Move myMove = new Move();
    List<PieceInfo> myPieces = new List<PieceInfo>();
    List<PieceInfo> theirPieces = new List<PieceInfo>();
    // Use this for initialization

    int abs(int input)
    {
        if (input < 0)
        {
            return -input;
        }
        return input;
    }
    List<Move> getKingMoves(int x, int y)
    {
        List<Move> moves = new List<Move>();
        for(int a = -1; a < 2; a++)
        {
            for(int b = -1; b < 2; b++)
            {
                if(boardObject.GetComponent<gameScript>().isLegal(pieces.bKING, x, y, a, b))
                {
                    Move move = new Move(pieces.bKING, x, y, a, b);
                    moves.Add(move);
                }
            }

        }
        return moves;

    }

	bool minimax(Move move){


		return false;
	}
    List<Move> getKnightMoves(int x, int y)
    {
        List<Move> moves = new List<Move>();

        for (int endX = -2; endX <= 2; endX++)
        {
            for (int endY = -2; endY <= 2; endY++)
            {
                //speed it up by not checking bad vals;
                if (abs(endY - endX) == 1)
                {
                    if (boardObject.GetComponent<gameScript>().isLegal(pieces.bKNIGHT, x, y, x + endX, y + endY))
                    {
                        Move move = new Move(pieces.bKNIGHT, x, y, x + endX, y + endY);
                        moves.Add(move);
                    }
                }
            }
        }
        return moves;
    }
    List<Move> getBishopMoves(int x, int y)
    {
        List<Move> moves = new List<Move>();
        //four directions for ze bishop
        for (int a = 0; a < 4; a++)
        {

            int iterX = x;
            int iterY = y;

            while (board.gameBoard[iterX, iterY] == pieces.EMPTY)
            {
                if(a == 0)
                {
                    iterX++;
                    iterY++;
                }
                if(a == 1)
                {
                    iterX++;
                    iterY--;
                }
                if (a == 2)
                {
                    iterX--;
                    iterY++;
                }
                if(a == 3)
                {
                    iterX--;
                    iterY--;
                }

                if (boardObject.GetComponent<gameScript>().isLegal(pieces.bBISHOP, x, y, iterX, iterY))
                {
                    Move move = new Move(pieces.bBISHOP, x, y, iterX, iterY);
                    moves.Add(move);
                }
            }
        }
        return moves;
    }
    List<Move> getPawnMoves(int x, int y)
    {
        List<Move> moves = new List<Move>();
        if(boardObject.GetComponent<gameScript>().isLegal(pieces.bPAWN, x, y, x, y - 1))
        {
            Move move = new Move(pieces.bPAWN, x, y, x, y-1);
            moves.Add(move);
        }
        if(boardObject.GetComponent<gameScript>().isLegal(pieces.bPAWN,x,y,x-1, y - 1))
        {
            Move move = new Move(pieces.bPAWN, x, y, x-1, y - 1);
            moves.Add(move);
        }
        if (boardObject.GetComponent<gameScript>().isLegal(pieces.bPAWN, x, y, x + 1, y - 1))
        {
            Move move = new Move(pieces.bPAWN, x, y, x + 1, y - 1);
            moves.Add(move);
        }
        if (boardObject.GetComponent<gameScript>().isLegal(pieces.bPAWN, x, y, x , y - 2))
        {
            Move move = new Move(pieces.bPAWN, x, y, x, y - 2);
            moves.Add(move);
        }
        return moves;
    }
    List<Move> getRookMoves(int x, int y)
    {
        List<Move> moves = new List<Move>();
        for (int a = 0; a < 2; a++){
            for(int b = 0; b < 2; b++)
            {
                int iterX = x;
                int iterY = y;
                while(true){
                    if (a == 0)
                    {
                        iterX++;
                    }
                    else
                    {
                        iterX--;
                    }
                    if (b == 0)
                    {
                        iterY++;
                    }
                    else
                    {
                        iterY--;
                    }
                    if(boardObject.GetComponent<gameScript>().isLegal(pieces.bROOK, x, y, iterX, iterY))
                    {
                        Move move = new Move(pieces.bROOK, x, y, iterX, iterY);
                        moves.Add(move);
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }
        return moves;
    }
    List<Move> getQueenMoves(int x, int y)
    {
        List<Move> moves = new List<Move>();
        moves.AddRange(getBishopMoves(x, y));
        moves.AddRange(getRookMoves(x, y));

        foreach(var move in moves)
        {
            move.piece = pieces.bQUEEN;
        }
        return moves;
    }
    List<Move> getMoves(pieces piece, int x, int y)
    {
        List<Move> moves = new List<Move>();
        switch (piece)
        {
            case pieces.bKING:
                return getKingMoves(x, y);
                break;
            case pieces.bBISHOP:
                return getBishopMoves(x, y);
                break;
            case pieces.bKNIGHT:
                return getKnightMoves(x, y);
                break;
            case pieces.bROOK:
                return getRookMoves(x, y);
                break;
            case pieces.bQUEEN:
                return getQueenMoves(x, y);
                break;
            case pieces.bPAWN:
                return getPawnMoves(x, y);
                break;
        }
        Debug.Log("GetMoves should never come here");
        return moves;
    }
    void Start()
    {
        board = boardObject.GetComponent<gameScript>().board;
        for (int a = 0; a < 8; a++)
        {
            for (int b = 0; b < 8; b++)
            {
                if (board.isBlack(a, b))
                {
					PieceInfo temp = new PieceInfo(board.gameBoard[a, b], a, b);
                    myPieces.Add(temp);
                }
                else if (board.gameBoard[a, b] != pieces.EMPTY)
                {
					PieceInfo temp = new PieceInfo(board.gameBoard[a, b], a, b);
                    theirPieces.Add(temp);
                }
            }
        }

		for(int a = 0; a < myPieces.Count; a++){
			myPieces[a].legalMoves = getMoves(myPieces[a].piece, myPieces[a].xPos, myPieces[a].yPos);
			Debug.Log (myPieces[a].legalMoves.Count);
		}


    }

	void updatePieceInfo(){
		myPieces.Clear ();
		theirPieces.Clear ();
		for (int a = 0; a < 8; a++)
		{
			for (int b = 0; b < 8; b++)
			{
				if (board.isBlack(a, b))
				{
					PieceInfo temp = new PieceInfo(board.gameBoard[a, b], a, b);
					myPieces.Add(temp);
				}
				else if (board.gameBoard[a, b] != pieces.EMPTY)
				{
					PieceInfo temp = new PieceInfo(board.gameBoard[a, b], a, b);
					theirPieces.Add(temp);
				}
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
        board = boardObject.GetComponent<gameScript>().board;
        System.Random rand = new System.Random();

        

        if (!boardObject.GetComponent<gameScript>().isWhitesTurn)
        {
			updatePieceInfo();

			for(int a = 0; a < myPieces.Count; a++){
				myPieces[a].legalMoves = getMoves(myPieces[a].piece, myPieces[a].xPos, myPieces[a].yPos);
				Debug.Log (myPieces[a].legalMoves.Count);
			}

            PieceInfo tempPiece = myPieces[rand.Next(0, myPieces.Count)];

            myMove = tempPiece.legalMoves[rand.Next(0, tempPiece.legalMoves.Count)];
            Debug.Log("startX: " + myMove.startX + " start Y " + myMove.startY + " End X: " + myMove.endX + "End Y" + myMove.endY);
        }
    }
}
