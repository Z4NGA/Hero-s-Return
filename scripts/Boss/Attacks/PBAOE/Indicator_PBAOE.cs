using System.Collections;
using UnityEngine;

public class Indicator_PBAOE : MonoBehaviour
{
	public Transform perimeter;
	public Transform area;

	private float targetScale;
	private float duration;

	public void StartIndicator(float targetScale, float duration)
	{
		this.targetScale = targetScale;
		this.duration = duration;

		area.localScale = new Vector3(0, 0, 1);
		perimeter.localScale = new Vector3(targetScale, targetScale, 1);
		StartCoroutine("ScaleArea");
	}

	private IEnumerator ScaleArea()
	{
		float scalePerSecond = targetScale / duration;
		float currentScale = 0;
		while(currentScale < targetScale)
		{
			currentScale += scalePerSecond * Time.deltaTime;
			area.localScale = new Vector3(currentScale, currentScale, 1);
			yield return null;
		}
	}
}
