using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMovement : MonoBehaviour
{
	public float speed;
	public float jitter;

	private Vector2 initialPosition;

	private void Start()
	{
		initialPosition = transform.position;
	}

	void Update()
    {
		transform.position = initialPosition + new Vector2(0, Mathf.Sin(Time.time)) * jitter;
		transform.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward);
    }
}
