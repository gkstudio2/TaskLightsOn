using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public GameObject canvas;

	[SerializeField] GameObject[] piecePrefabs;
	
	float score = 100;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] TextMeshProUGUI winmenuscoretext;
	bool CanReduceScore = false;

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
	public GameObject Clonedlevel;

    private void Awake()
    {
        if (Instance == null)
        {
			Instance = this;
		}
    }
	int unlockedlevels;
	int Currentlevel;

	
    private void Update()
    {
        if(CanReduceScore)
        {
            if (score > 1)
            {
				score = score - 1 * Time.deltaTime;
				scoretext.text = Mathf.RoundToInt(score).ToString();
			}
			/*else
			{
				canvas.GetComponent<GameUIController>().Exitpopup.SetActive(true);
			}*/
		}
       
    }
	public void startlevel()
    {
		unlockedlevels = Playerlevel.Instance.GetMaxUnlockedlevels();
		Currentlevel = Playerlevel.Instance.GetCurrentselectedlevels();
		if (Currentlevel == 0)
		{
			Clonelevel(unlockedlevels);
		}
		else
		{
			Clonelevel(Currentlevel);
		}
        CanReduceScore = true;
		scoretext.transform.parent.gameObject.SetActive(true);
	}
    public void Clonelevel(int lvl)
    {
		
		Clonedlevel = (GameObject)Instantiate(Resources.Load("Level_" + lvl.ToString()));
		//Clonedlevel.transform.SetParent(this.transform, false);
		piecePrefabs = new GameObject[0];
		piecePrefabs = GameObject.FindGameObjectsWithTag("Piece");
		Vector2 dimensions = CheckDimensions();

		puzzle.width = (int)dimensions.x;
		puzzle.height = (int)dimensions.y;


		puzzle.pieces = new piece[puzzle.width, puzzle.height];



		foreach (var piece in piecePrefabs)
		{
			Debug.LogWarning(piece.name);
			puzzle.pieces[(int)piece.transform.position.x, (int)piece.transform.position.y] = piece.GetComponent<piece>();

		}


		puzzle.winValue = GetWinValue();

		ShufflePiece();

		puzzle.curValue = Sweep();
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
        AudioManager.Instance.LevelWin();

		
        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (piecePrefabs[i].GetComponent<piece>().Highlight != null)
            {
                Debug.Log(piecePrefabs[i].name);
                piecePrefabs[i].GetComponent<piece>().Highlight.SetActive(true);
            }
        }
       
		Invoke(nameof(nextmove), 2f);
		
	}
	void nextmove()
    {
		if (unlockedlevels == Currentlevel)
		{
			CanReduceScore = false;
			Playerlevel.Instance.Scoresaver(unlockedlevels, (int)score);
			unlockedlevels++;
			Playerlevel.Instance.SetMaxUnlockedlevels(unlockedlevels);
			canvas.GetComponent<GameUIController>().Winmenupopup.SetActive(true);
			winmenuscoretext.text = Playerlevel.Instance.Getsavedscore(Currentlevel).ToString();
		}
		else
		{
			Playerlevel.Instance.SetCurrentselectedlevels(0);
		}
	}
	public int QuickSweep(int w,int h)
	{
		int value = 0;

		//compares top
		if(h!=puzzle.height-1)
		if (puzzle.pieces [w, h].values [0] == 1 && puzzle.pieces [w, h + 1].values [2] == 1)
            {
				Debug.LogWarning(puzzle.pieces[w, h].name + puzzle.pieces[w, h + 1]);
				value++;
			}
			


		//compare right
		if(w!=puzzle.width-1)
		if (puzzle.pieces [w, h].values [1] == 1 && puzzle.pieces [w + 1, h].values [3] == 1)
            {
				Debug.LogWarning(puzzle.pieces[w, h].name + puzzle.pieces[w + 1, h]);
				value++;
			}
			


		//compare left
		if (w != 0)
		if (puzzle.pieces [w, h].values [3] == 1 && puzzle.pieces [w - 1, h].values [1] == 1)
			{
				Debug.LogWarning(puzzle.pieces[w, h].name + puzzle.pieces[w - 1, h]);
				value++;
			}

		//compare bottom
		if (h != 0)
		if (puzzle.pieces [w, h].values [2] == 1 && puzzle.pieces [w, h-1].values [0] == 1)
			{
				Debug.LogWarning(puzzle.pieces[w, h].name + puzzle.pieces[w, h - 1]);
				value++;
			}


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
	
	//Loadnextlevel
	public void NextLevel()
	{
		
		if (Clonedlevel!=null)
        {
			piecePrefabs = new GameObject[0];
			Destroy(Clonedlevel);
        }
		Playerlevel.Instance.SetCurrentselectedlevels(0);
		SceneManager.LoadScene("Game");
	}

	public void Testnextlevel()
    {
		unlockedlevels++;
		Playerlevel.Instance.SetMaxUnlockedlevels(unlockedlevels);
		NextLevel();

	}
}
