using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWallReplacer : MonoBehaviour
{
	public GameObject[] walls;

	[SerializeField]
	private int currentWall = 0;

	private void Start()
	{
		foreach(GameObject g in walls)
		{
			g.SetActive(false);
		}
		walls[0].SetActive(true);

	}

	public void CycleWalls()
	{
		walls[currentWall].SetActive(false);
		currentWall++;
		if (currentWall >= walls.Length)
			currentWall = 0;
		walls[currentWall].SetActive(true);
	}

	private void OnGUI()
	{
		/*if (GUI.Button(new Rect(10, 50, 75, 25), "Cycle Walls"))
			CycleWalls();
		*/
	}
}
