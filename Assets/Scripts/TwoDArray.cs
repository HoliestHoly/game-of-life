﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDArray : MonoBehaviour
{
	 
	public GameObject cubes;
	public int Columns = 10;
	public int Rows = 10;
	[Range (0, 1)]
	public float cellFill;

	public static GameObject[,] grid;
	int neighbors;

	int GetNeighbors(int cellx, int celly)
	{
		 neighbors = 0;
	
			
		for (int x = -1; x < 2; x++) {
			for (int y = -1; y < 2; y++) {
				if (
				cellx + x >= 0
				&& cellx + x < Columns
				&& celly + y >= 0
				&& celly + y < Rows) {


					if (x != 0 || y != 0) {
						if (grid [cellx + x, celly + y].GetComponent<AliveOrNot> ().Alive) {
							neighbors++;
						}
					}
				}
			}
		}
	



		return neighbors;



	}
	void Start ()
	{
		
		grid = new GameObject[Columns, Rows];

		for (int i = 0; i < Columns; i++) {
			for (int j = 0; j < Rows; j++) {
				grid [i, j] = (GameObject)Instantiate (cubes, new Vector2 (i+0.5f, j+0.5f), Quaternion.identity);
				grid [i, j].AddComponent<AliveOrNot> ();
				grid [i, j].transform.localScale = Vector3.one * cellFill;
			}
		}
		Camera.main.transform.position = new Vector3 (Columns / 2, Rows / 2, -10);
		Camera.main.orthographicSize = Rows / 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			var pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			int xindex = (int)pos.x % Columns;
			int yindex = (int)pos.y % Rows;
		Toggle (xindex, yindex);	
		}

	
		if (Input.GetKeyDown ("a")) {
			IterateGameOfLife ();
		}

		

	}

	void Toggle (int x, int y)
	{
		var cell = grid [x, y];
		var aliveOrNot = cell.GetComponent<AliveOrNot> ();
		var rend = cell.GetComponent<Renderer> ();
		if (!aliveOrNot.Alive) 
		{
			aliveOrNot.Alive = true;
			rend.material.color = new Color (1, 0, 1);
		}else 
		
		{
			aliveOrNot.Alive = false;
			rend.material.color = new Color (0, 0, 0);
		}
	
	}

	void IterateGameOfLife()
	{
		List<Vector2> changeIndexes = new List<Vector2> ();


		for (int j = 0; j < Columns; j++) {
			for (int i = 0; i < Rows; i++) {
				var cell = grid [j, i];
				var aliveOrNot = cell.GetComponent<AliveOrNot> ().Alive;
				int neighborsAlive = GetNeighbors (j, i);
				print (GetNeighbors (0,0));


				if (aliveOrNot && (neighborsAlive <= 1 || neighborsAlive >= 4))
				{
					// aliveOrNot = false;
					// rend.material.color = new Color (1, 1, 1);
					changeIndexes.Add (new Vector2(j, i));
				}

				else if(!aliveOrNot && neighborsAlive == 3)
				{
					//aliveOrNot = true;
					//rend.material.color = new Color (1, 0, 1);
					changeIndexes.Add (new Vector2(j, i));
				}

			}
		}
		for (int k = 0; k < changeIndexes.Count; k++) 
		{
			Toggle ((int)changeIndexes [k].x, (int)changeIndexes [k].y); 



		}

	}




}