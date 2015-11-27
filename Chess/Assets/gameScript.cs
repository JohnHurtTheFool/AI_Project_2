using UnityEngine;
using System.Collections;

public class gameScript : MonoBehaviour {
	public bool isWhitesTurn;
	public GameObject board;
	float squareWidth;
	float squareHeight;
	// Use this for initialization
	void Start () {
		isWhitesTurn = true;
		squareWidth = board.GetComponent<RectTransform> ().rect.width / 8.0f;
		squareHeight = board.GetComponent<RectTransform> ().rect.height / 8.0f;
	}
	public void takeTurn(){
		if (isWhitesTurn) {
			int x = 1 + (int)(((board.GetComponent<RectTransform>().rect.width/2 
			          + Input.mousePosition.x - board.transform.position.x))/ squareWidth);
			int y = 1 + (int)((board.GetComponent<RectTransform>().rect.height/2
			              + Input.mousePosition.y - board.transform.position.y)/squareHeight);
			Debug.Log("X Pos: " + x + "Y Pos: " + y);
			//Debug.Log ("Board Width " + board.GetComponent<RectTransform>().rect.width +
			           //"Board Height " + board.GetComponent<RectTransform>().rect.height);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
