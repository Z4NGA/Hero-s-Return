using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class ArenaColliderGenerator : MonoBehaviour
{
	public float radius;
	public int NumberOfPoints;

	new EdgeCollider2D collider;

    void Start()
    {
		collider = GetComponent<EdgeCollider2D>();
		GenerateEdges();
    }

	void GenerateEdges()
	{
		Vector2[] points = new Vector2[NumberOfPoints + 1];
		float circumference = 2 * Mathf.PI;
		for(int i = 0; i < NumberOfPoints; i++)
		{
			float radians = i * (circumference / NumberOfPoints);
			Vector2 point = new Vector2();
			point.x = radius * Mathf.Cos(radians);
			point.y = radius * Mathf.Sin(radians);
			points[i] = point;
		}
		points[NumberOfPoints] = points[0];

		collider.points = points;
	}
}
