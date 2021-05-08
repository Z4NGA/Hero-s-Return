using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCinematicCamera : MonoBehaviour
{
	public Transform boss;
	public Transform player;

	public float minDistance = 7;
	public float maxDistance = 10;

	public float maxTargetDistance = 9f;

	public float positionDamping = 3;
	public float sizeDamping = 3;

	Camera cam;
	Vector3 basePosition;
	Vector3 positionDampingVelocity;
	float sizeDampingVelocity;

	private void Start()
	{
		cam = GetComponent<Camera>();
		basePosition = transform.position;
	}

	private void LateUpdate()
	{
		CalculateSize();
		CalculatePosition();
	}

	private void CalculateSize()
	{
		float distance = Vector2.Distance(boss.position, player.position);
		float t = Mathf.InverseLerp(0, maxTargetDistance, distance);

		float target = Mathf.Lerp(minDistance, maxDistance, t);
		cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, target, ref sizeDampingVelocity, sizeDamping);
	}

	private void CalculatePosition()
	{
		Vector3 newPos = basePosition + (player.position / 2.0f);
		newPos.z = basePosition.z;
		transform.position = Vector3.SmoothDamp(transform.position, basePosition + newPos, ref positionDampingVelocity, positionDamping);
	}
}
