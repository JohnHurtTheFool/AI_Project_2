using UnityEngine;
using System.Collections;
public enum pieces{wPAWN, wKNIGHT, wBISHOP, wROOK, wQUEEN, wKING, 
	bPAWN, bKNIGHT, bBISHOP, bROOK, bQUEEN, bKING, EMPTY};
public class gameScript : MonoBehaviour {
	public class Board
	{
		public pieces[,] gameBoard = new pieces[8,8];
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
	public bool isWhitesTurn;
	public GameObject boardObject;

	public GameObject wPawn, wRook, wKnight, wBishop, wKing, wQueen,
	bPawn, bKnight, bBishop, bRook, bQueen, bKing; 

	Board board = new Board();
	float squareWidth;
	float squareHeight;
	// Use this for initialization
	void Start () {
		isWhitesTurn = true;
		squareWidth = boardObject.GetComponent<RectTransform> ().rect.width / 8.0f;
		squareHeight = boardObject.GetComponent<RectTransform> ().rect.height / 8.0f;
	}

	bool hasPiece(int x, int y){
		return true;
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
			testBoard(x, y);
			//Debug.Log ("Board Width " + board.GetComponent<RectTransform>().rect.width +
			           //"Board Height " + board.GetComponent<RectTransform>().rect.height);
		}
	}
	// Update is called once per frame
	void Update () {
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
		for (int a = 0; a < 8; a++){
			for (int b = 0; b < 8; b ++){
				switch(board.gameBoard[a,b]){
					case pieces.bBISHOP:
						GameObject bb = Instantiate(bBishop, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						bb.transform.parent = boardObject.transform;
						break;
					case pieces.bPAWN:
						GameObject bp = Instantiate(bPawn, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						bp.transform.parent = boardObject.transform;
						break;
					case pieces.bKING:
						GameObject bk = Instantiate(bKing, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						bk.transform.parent = boardObject.transform;
						break;
					case pieces.bROOK:
						GameObject br = Instantiate(bRook, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						br.transform.parent = boardObject.transform;
						break;
					case pieces.bQUEEN:
						GameObject bq = Instantiate(bQueen, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						bq.transform.parent = boardObject.transform;
						break;
					case pieces.bKNIGHT:
						GameObject bkn = Instantiate(bKnight, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						bkn.transform.parent = boardObject.transform;
						break;
					case pieces.wBISHOP:
						GameObject wb = Instantiate(wBishop, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						wb.transform.parent = boardObject.transform;
						break;
					case pieces.wPAWN:
						GameObject wp = Instantiate(wPawn, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						wp.transform.parent = boardObject.transform;
						break;
					case pieces.wKING:
						GameObject wk = Instantiate(wKing, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						wk.transform.parent = boardObject.transform;
						break;
					case pieces.wROOK:
						GameObject wr = Instantiate(wRook, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						wr.transform.parent = boardObject.transform;
						break;
					case pieces.wQUEEN:
						GameObject wq = Instantiate(wQueen, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						wq.transform.parent = boardObject.transform;
						break;
					case pieces.wKNIGHT:
						GameObject wkn = Instantiate(wKnight, new Vector3(a * squareWidth + boardObject.transform.position.x - (squareWidth * 4) + 16.5f, 
					                                               b * squareHeight + 20.0f, 1.0f) ,Quaternion.identity) as GameObject; 
						wkn.transform.parent = boardObject.transform;
						break;
				}
				                           
			}
		}
	}
}
