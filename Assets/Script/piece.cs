using UnityEngine;
using System.Collections;
using System;

public class piece : MonoBehaviour {


	public int[] values;
	public float speed;
	float realRotation;
	[SerializeField] bool HasChild;

	[SerializeField] GameManager gamemanager;
	public GameObject Highlight;

    // Use this for initialization
    void Start()
    {
		gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if(Highlight!=null)
        {
			Highlight.SetActive(false);
		}
    }

    // Update is called once per frame
    void Update () {
	

		if (transform.root.eulerAngles.z != realRotation) {
            if (!HasChild)
            {
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);

            }
            else
            {
				transform.GetChild(0).transform.rotation = Quaternion.Lerp(transform.GetChild(0).transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
			}
		}
       /* if (Input.GetMouseButtonDown(0))
        {
			OnMouseDown

		}*/

	}



	void OnMouseDown()
	{
		AudioManager.Instance.PlayOneshot();
		int difference = -gamemanager.QuickSweep((int)transform.position.x,(int)transform.position.y);


		//if(!Cannotrotate)
		RotatePiece ();


		difference += gamemanager.QuickSweep((int)transform.position.x,(int)transform.position.y);




		gamemanager.puzzle.curValue += difference;



		if (gamemanager.puzzle.curValue == gamemanager.puzzle.winValue)
			PlayerWon();
	}
	void PlayerWon()
    {
		Debug.LogWarning("Player Won");
		gamemanager.Win();
	}
	public void RotatePiece()
	{
		realRotation += 90;

		/*if (realRotation == 360)
			realRotation = 0;*/

		RotateValues ();
	}



	public void RotateValues()
	{

		int aux = values [0];

		for (int i = 0; i < values.Length-1; i++) {
			values [i] = values [i + 1];
		}
		values [3] = aux;
	}



}
