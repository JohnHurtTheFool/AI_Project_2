using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//Enum to represent the pieces
public enum pieces{wPAWN, wKNIGHT, wBISHOP, wROOK, wQUEEN, wKING, 
	bPAWN, bKNIGHT, bBISHOP, bROOK, bQUEEN, bKING, EMPTY};
public class Board
{

	public void makeMove(Move move){
		gameBoard[move.endX, move.endY] = gameBoard[move.startX, move.startY];
		gameBoard [move.startX, move.startY] = pieces.EMPTY;
	}

    public bool isBlack(int x, int y)
    {
        pieces piece = gameBoard[x, y];
        if (piece == pieces.bKING || piece == pieces.bKNIGHT || piece == pieces.bBISHOP || piece == pieces.bPAWN || piece == pieces.bQUEEN || piece == pieces.bROOK)
        {
            return true;
        }
        return false;
    }
	//helps handle the X and Y value of the currently selected piece.
	public int selectedX, selectedY;
	//whether or not there is currently a piece selected.
	public bool selected = false;
	//the actual board that holds all the pieces' positions
	public pieces[,] gameBoard = new pieces[8,8];
	//Constructor to initialize the gameBoard
	public Board(){
		for(int a = 0; a < 8; a++){
			for (int b= 0; b< 8; b++){
				if(b == 0){
					if(a == 0 || a == 7){
						gameBoard[a,b] = pieces.wROOK;
					}
					else if(a == 1 || a == 6){
						gameBoard[a,b] = pieces.wKNIGHT;
					}
					else if(a == 2 || a == 5){
						gameBoard[a,b] = pieces.wBISHOP;
					}
					else if(a == 3){
						gameBoard[a,b] = pieces.wQUEEN;
					}
					else if(a == 4){
						gameBoard[a,b] = pieces.wKING;
					}
				}
				else if(b == 1){
					gameBoard[a,b] = pieces.wPAWN;
				}
				else if(b == 6){
					gameBoard[a,b] = pieces.bPAWN;
				}
				else if(b == 7){
					if(a == 0 || a == 7){
						gameBoard[a,b] = pieces.bROOK;
					}
					else if(a == 1 || a == 6){
						gameBoard[a,b] = pieces.bKNIGHT;
					}
					else if(a == 2 || a == 5){
						gameBoard[a,b] = pieces.bBISHOP;
					}
					else if(a == 3){
						gameBoard[a,b] = pieces.bQUEEN;
					}
					else if(a == 4){
						gameBoard[a,b] = pieces.bKING;
					}
				}
				else{
					gameBoard[a,b] = pieces.EMPTY;
				}
			}
		}
	}
}
public class gameScript : MonoBehaviour {
	
	public bool isWhitesTurn;
	//This is basically a reference to the image and is not used in chess logic.
	public GameObject boardObject;
	//Again, more Unity stuff. Not required for actual programming.
	public GameObject wPawn, wRook, wKnight, wBishop, wKing, wQueen,
	bPawn, bKnight, bBishop, bRook, bQueen, bKing; 


	public bool blackCanCastle = true;
	public bool whiteCanCastle = true;
	public Board board = new Board();
	float squareWidth;
	float squareHeight;
	Color white, whiteSelected, black, blackSelected;
	// Use this for initialization
	void Start () {
		isWhitesTurn = true;
		squareWidth = boardObject.GetComponent<RectTransform> ().rect.width / 8.0f;
		squareHeight = boardObject.GetComponent<RectTransform> ().rect.height / 8.0f;
		white = wPawn.GetComponent<Image> ().color;
		black = bPawn.GetComponent<Image> ().color;
		whiteSelected = white;
		whiteSelected.a -= 0.18f;
		blackSelected = black;
		blackSelected.a -= 0.18f;
	}

	bool hasPiece(int x, int y){
		if (board.gameBoard [x, y] != pieces.EMPTY) {
			return true;
		} else {
			return false;
		}
	}
	bool checkKnight(int startX, int startY, int endX, int endY){
		if (startX - endX == 1 || startX - endX == -1) {
			if (startY - endY == 2 || startY - endY == -2) {
				return true;
			}
		} else if (startX - endX == 2 || startX - endX == -2) {
			if (startY - endY == 1 || startY - endY == -1) {
				return true;
			}
		} 
		return false;
	}
	int abs(int val){
		if (val < 0) {
			return -val;
		}
		return val;
	}
	bool isWhite(pieces piece){
		if (piece == pieces.wKING || piece == pieces.wKNIGHT || piece == pieces.wBISHOP || piece == pieces.wPAWN || piece == pieces.wQUEEN || piece == pieces.wROOK) {
			return true;
		}
		return false;
	}
	bool checkPawn(int startX, int startY, int endX, int endY)
	{
		//check to see if pawn is white.
		if (isWhite (board.gameBoard [startX, startY])) {
			if(endY - startY != 1){
				//Pawn is in starting position and is pushed two
				if(endY- startY == 2 && startY == 1 && board.gameBoard[endX, endY] == pieces.EMPTY && board.gameBoard[endX, endY -1] == pieces.EMPTY && startX - endX == 0) {
					return true;
				}
			}
			//Pawn is capturing
			else if (abs (startX-endX) == 1){
				if(board.gameBoard[endX, endY] != pieces.EMPTY){
					return true;
				}
			}
			//Pawn is pushed
			else if(startX-endX == 0){
				if(board.gameBoard[endX, endY] == pieces.EMPTY){
					return true;
				}
			}
			else{
				return false;
			}
			} else {
			if(startY - endY != 1){
				//Pawn is in starting position and is pushed two
				if(startY- endY == 2 && startY == 6 && board.gameBoard[endX, endY] == pieces.EMPTY  && board.gameBoard[endX, endY + 1] == pieces.EMPTY && startX-endX == 0){
					return true;
				}
			}
			//Pawn is capturing
			else if (abs (startX-endX) == 1){
				if(board.gameBoard[endX, endY] != pieces.EMPTY){
					return true;
				}
			}
			//Pawn is pushed
			else if(startX-endX == 0){
				if(board.gameBoard[endX, endY] == pieces.EMPTY){
					return true;
				}
			}
			else{
				return false;
			}
		}
		return false;
	}
	bool checkBishop(int startX, int startY, int endX, int endY){
		//This makes sure that whatever square was clicked
		//is in fact diagonal from the bishop's position.
		if (abs(startX - endX) != abs(startY - endY)) 
		{
			return false;
		}
		int iterX = startX;
		int iterY = startY;
		while (iterX != endX && iterY != endY) {
			if(startX > endX){
				iterX--;
			}
			else{
				iterX++;
			}
			if(startY > endY){
				iterY--;
			}
			else{
				iterY++;
			}
			if(board.gameBoard[iterX,iterY] != pieces.EMPTY && iterX != endX){
				return false;
			}
		}
		return true;
			
	}
	bool checkRook(int startX, int startY, int endX, int endY){
		if (startX != endX && startY != endY) {
			return false;
		}
		int iterX = startX;
		int iterY = startY;
		while (iterX != endX) {
			if(startX > endX){
				iterX--;
			}
			else{
				iterX++;
			}
			if(board.gameBoard[iterX,iterY] != pieces.EMPTY && iterX != endX){
				return false;
			}
		}
		while (iterY != endY) {
			if(startY > endY){
				iterY--;
			}
			else{
				iterY++;
			}
			if(board.gameBoard[iterX,iterY] != pieces.EMPTY && iterY != endY){
				return false;
			}
		}
		return true;
	}
	bool checkQueen(int startX, int startY, int endX, int endY){
		return true;
	}
	bool checkKing(int startX, int startY, int endX, int endY){
		if (abs (startX - endX) <= 1 && abs (startY - endY) <= 1) {
			return true;
		}
		else if(isWhite(board.gameBoard[startX, startY])){

		}
		return false;
	}
	public bool isLegal(pieces piece, int startX, int startY, int endX, int endY){
		//This isn't a move. Therefore return false;
		if (startX == endX && startY == endY) {
			return false;
		}
		//not on the board
		if (endX > 7 || endY > 7 || endX < 0 || endY < 0) {
			return false;
		}
		//Can't capture own piece
		if (isWhite(board.gameBoard [startX, startY]) == isWhite (board.gameBoard [endX, endY]) && board.gameBoard [endX, endY] != pieces.EMPTY) {
			return false;
		}

		if (piece == pieces.bBISHOP || piece == pieces.wBISHOP) {
			return checkBishop (startX, startY, endX, endY);
		} 
		else if (piece == pieces.bKNIGHT || piece == pieces.wKNIGHT) {
			return checkKnight(startX, startY, endX, endY);
		}
		else if (piece == pieces.bROOK || piece == pieces.wROOK) {
			return checkRook(startX, startY, endX, endY);
		}
		else if (piece == pieces.bPAWN || piece == pieces.wPAWN) {
			return checkPawn(startX, startY, endX, endY);
		}
		else if (piece == pieces.bQUEEN || piece == pieces.wQUEEN) {
			return checkRook(startX, startY, endX, endY) || checkBishop(startX, startY, endX, endY);
		}
		else if(piece ==  pieces.bKING || piece == pieces.wKING){
			return checkKing(startX, startY, endX, endY);
		}
		return false;
	}
	void testBoard(int x, int y){
		Debug.Log ("This piece is a: " + ((pieces)board.gameBoard[x,y]).ToString());
	}
	public void takeTurn(){
		if (isWhitesTurn) {
			int x = (int)(((boardObject.GetComponent<RectTransform>().rect.width/2 
			                    + Input.mousePosition.x - boardObject.transform.position.x))/ squareWidth);
			int y = (int)((boardObject.GetComponent<RectTransform>().rect.height/2
			                   + Input.mousePosition.y - boardObject.transform.position.y)/squareHeight);

			if(!board.selected){
				if(hasPiece(x,y)){
					board.selectedX = x;
					board.selectedY = y;
					board.selected = true;
				}
			}
            //Illegal move, deselect the current piece
			else if(!isLegal(board.gameBoard[board.selectedX, board.selectedY], board.selectedX, board.selectedY, x, y)){
				board.selected = false;
			}
            //Move was legal, make the move;
			else{
				board.gameBoard[x, y] = board.gameBoard[board.selectedX, board.selectedY];
				board.gameBoard[board.selectedX, board.selectedY] = pieces.EMPTY;
				board.selected = false;
                isWhitesTurn = false;
			}
		}
	}
    public void AItakeTurn(Move AIMove)
    {
        if (isLegal(board.gameBoard[AIMove.startX, AIMove.startY], AIMove.startX, AIMove.startY, AIMove.endX, AIMove.endY) && board.isBlack(AIMove.startX, AIMove.startY))
        {
            board.gameBoard[AIMove.endX, AIMove.endY] = board.gameBoard[AIMove.startX, AIMove.startY];
            board.gameBoard[AIMove.startX, AIMove.startY] = pieces.EMPTY;
            isWhitesTurn = true;
        }
    }
	// Update is called once per frame
	void Update () {
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		Color blackInstance = black;
		Color whiteInstance = white;
        if (!isWhitesTurn)
        {
            AItakeTurn(boardObject.GetComponent<AI>().myMove);
        }
		for (int a = 0; a < 8; a++){
			for (int b = 0; b < 8; b ++){
				if (a == board.selectedX && b == board.selectedY && board.selected){
					whiteInstance = whiteSelected;
					blackInstance = blackSelected;
				}
				else{
					whiteInstance = white;
					blackInstance = black;
				}
				switch(board.gameBoard[a,b]){
					case pieces.bBISHOP:
						GameObject bb = Instantiate(bBishop, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						bb.transform.parent = boardObject.transform;
						bb.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						bb.GetComponent<Image>().color = blackInstance;
						break;
					case pieces.bPAWN:
						GameObject bp = Instantiate(bPawn, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						bp.transform.parent = boardObject.transform;
						bp.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						bp.GetComponent<Image>().color = blackInstance;
						break;
					case pieces.bKING:
						GameObject bk = Instantiate(bKing, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						bk.transform.parent = boardObject.transform;
						bk.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						bk.GetComponent<Image>().color = blackInstance;
					break;
					case pieces.bROOK:
						GameObject br = Instantiate(bRook, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						br.transform.parent = boardObject.transform;
						br.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						br.GetComponent<Image>().color = blackInstance;
						break;
					case pieces.bQUEEN:
						GameObject bq = Instantiate(bQueen, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                                b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						bq.transform.parent = boardObject.transform;
						bq.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						bq.GetComponent<Image>().color = blackInstance;
						break;
					case pieces.bKNIGHT:
						GameObject bkn = Instantiate(bKnight, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                                  b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						bkn.transform.parent = boardObject.transform;
						bkn.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						bkn.GetComponent<Image>().color = blackInstance;
						break;
					case pieces.wBISHOP:
						GameObject wb = Instantiate(wBishop, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                                 b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						wb.transform.parent = boardObject.transform;
						wb.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						wb.GetComponent<Image>().color = whiteInstance;
						break;
					case pieces.wPAWN:
						GameObject wp = Instantiate(wPawn, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						wp.transform.parent = boardObject.transform;
						wp.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						wp.GetComponent<Image>().color = whiteInstance;
						break;
					case pieces.wKING:
						GameObject wk = Instantiate(wKing, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						wk.transform.parent = boardObject.transform;
						wk.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						wk.GetComponent<Image>().color = whiteInstance;
						break;
					case pieces.wROOK:
						GameObject wr = Instantiate(wRook, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                               b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						wr.transform.parent = boardObject.transform;
						wr.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						wr.GetComponent<Image>().color = whiteInstance;
						break;
					case pieces.wQUEEN:
						GameObject wq = Instantiate(wQueen, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                                b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						wq.transform.parent = boardObject.transform;
						wq.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						wq.GetComponent<Image>().color = whiteInstance;
						break;
					case pieces.wKNIGHT:
						GameObject wkn = Instantiate(wKnight, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4.0f) + squareWidth/2.0f, 
					                                                  b * squareHeight + squareHeight/2.0f, 1.0f), Quaternion.identity) as GameObject; 
						wkn.transform.parent = boardObject.transform;
						wkn.GetComponent<RectTransform>().sizeDelta = new Vector2(squareWidth * 0.75f, squareHeight * 0.75f);
						wkn.GetComponent<Image>().color = whiteInstance;
					break;
				}
				                           
			}
		}
	}
}
