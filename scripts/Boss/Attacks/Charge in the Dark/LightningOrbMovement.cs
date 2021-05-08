using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrbMovement : MonoBehaviour
{
	public Transform[] targets;
	public float speed;
	public float jitter;

	private Vector2 initialPosition;

	private void Start()
	{
		initialPosition = transform.position;
	}

	void Update()
	{
		foreach(Transform t in targets)
		{
			t.localPosition = initialPosition + Random.insideUnitCircle.normalized * jitter;
			t.localRotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward);
		}
	}
}

