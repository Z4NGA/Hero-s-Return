using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private bool shake = false;
	private float strength;

	private void LateUpdate()
	{
		if(shake)
		{
			Vector2 pos = Random.insideUnitCircle.normalized * strength;
			transform.position = new Vector3(pos.x, pos.y, 0) + transform.position;
		}
	}

	public void Shake(float duration, float s)
	{
		strength = s;
		shake = true;
		StartCoroutine(ResetShake(duration));
	}

	private IEnumerator ResetShake(float duration)
	{
		yield return new WaitForSeconds(duration);
		shake = false;
		//transform.position = initialPos;
	}
}
