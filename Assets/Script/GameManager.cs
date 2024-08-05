﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour {

	public GameObject canvas;

	[SerializeField] GameObject[] piecePrefabs;


	[System.Serializable]
	public class Puzzle
	{

		public int winValue;
		public int curValue;

		public int width;
		public int height;
		public piece[,] pieces;

	}


	public Puzzle puzzle;


	// Use this for initialization
	void Start () {

		    canvas.SetActive (false);

			piecePrefabs = GameObject.FindGameObjectsWithTag("Piece");
			Vector2 dimensions = CheckDimensions ();

			puzzle.width = (int)dimensions.x;
			puzzle.height = (int)dimensions.y;


			puzzle.pieces = new piece[puzzle.width, puzzle.height];


			
			foreach (var piece in piecePrefabs) {

			    Debug.LogWarning(piece.name);
				puzzle.pieces [(int)piece.transform.position.x, (int)piece.transform.position.y] = piece.GetComponent<piece> ();

			}


        foreach (var piece in puzzle.pieces)
        {

            Debug.Log(piece.gameObject.name);
        }


        puzzle.winValue = GetWinValue ();

		ShufflePiece();

		puzzle.curValue=Sweep ();
	}

	void Clonelevel()
    {

    }

	public int Sweep()
	{
		int value = 0;

		for (int h = 0; h < puzzle.height; h++) {
			for (int w = 0; w < puzzle.width; w++) {


				//compares top
				if(h!=puzzle.height-1)
				if (puzzle.pieces [w, h].values [0] == 1 && puzzle.pieces [w, h + 1].values [2] == 1)
					value++;


				//compare right
				if(w!=puzzle.width-1)
				if (puzzle.pieces [w, h].values [1] == 1 && puzzle.pieces [w + 1, h].values [3] == 1)
					value++;


			}
		}

		return value;

	}


	public void Win()
	{

		canvas.SetActive (true);
		for(int i = 0; i < piecePrefabs.Length; i++)
        {
			if (piecePrefabs[i].GetComponent<piece>().Highlight != null)
			{
				Debug.Log(piecePrefabs[i].name);
				piecePrefabs[i].GetComponent<piece>().Highlight.SetActive(true);
			}
		}
		
	}

	public int QuickSweep(int w,int h)
	{
		int value = 0;

		//compares top
		if(h!=puzzle.height-1)
		if (puzzle.pieces [w, h].values [0] == 1 && puzzle.pieces [w, h + 1].values [2] == 1)
			value++;


		//compare right
		if(w!=puzzle.width-1)
		if (puzzle.pieces [w, h].values [1] == 1 && puzzle.pieces [w + 1, h].values [3] == 1)
			value++;


		//compare left
		if (w != 0)
		if (puzzle.pieces [w, h].values [3] == 1 && puzzle.pieces [w - 1, h].values [1] == 1)
			value++;

		//compare bottom
		if (h != 0)
		if (puzzle.pieces [w, h].values [2] == 1 && puzzle.pieces [w, h-1].values [0] == 1)
			value++;


		return value;

	}

	int GetWinValue()
	{
		int winValue = 0;
		foreach (var piece in puzzle.pieces) {


			foreach (var j in piece.values) {
				//if(piece.GetComponent<piece>().Cannotrotate!=true)
				winValue += j;
			}


		}
		winValue /= 2;
		return winValue;
	}

	void ShufflePiece()
	{
		foreach (var piece in puzzle.pieces) {

			int k = Random.Range (0, 4);

			for (int i = 0; i < k; i++) {
				piece.RotatePiece ();
			}
		}
	}


	Vector2 CheckDimensions()
	{
		Vector2 aux = Vector2.zero;

		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("Piece");

		foreach (var p in pieces) {
			if (p.transform.position.x > aux.x)
				aux.x = p.transform.position.x;

			if (p.transform.position.y > aux.y)
				aux.y= p.transform.position.y;
		}

		aux.x++;
		aux.y++;

		return aux;
	}
	
	public void NextLevel(string nextLevel)
	{

		SceneManager.LoadScene (nextLevel);

	}
}
