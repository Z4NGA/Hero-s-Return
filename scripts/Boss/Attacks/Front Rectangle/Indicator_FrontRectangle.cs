using System.Collections;
using UnityEngine;

public class Indicator_FrontRectangle : MonoBehaviour
{
	public Transform perimeter;
	public Transform area;

	private Vector2 targetScale;
	private float duration;

	public void StartIndicator(float lengthTargetScale, float widthTargetScale, float duration)
	{
		this.targetScale = new Vector2(lengthTargetScale, widthTargetScale);
		this.duration = duration;

		area.localScale = new Vector3(0, widthTargetScale, 1);
		perimeter.localScale = new Vector3(lengthTargetScale, widthTargetScale, 1);
		StartCoroutine("ScaleArea");
	}

	private IEnumerator ScaleArea()
	{
		float scalePerSecond = targetScale.x / duration;
		float currentScale = 0;
		while (currentScale < targetScale.x)
		{
			currentScale += scalePerSecond * Time.deltaTime;
			area.localScale = new Vector3(currentScale, targetScale.y, 1);
			yield return null;
		}
	}
}
