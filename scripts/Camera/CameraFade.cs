using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
	public Image fadeImage;
	float fadeValue = 1;

	public IEnumerator Fade(float speed, bool black)
	{
		float time = 0;
		while(time <= 1)
		{
			fadeValue = black ? time : 1 - time;
			fadeImage.color = new Color(0, 0, 0, fadeValue);
			time += Time.deltaTime * speed;
			yield return null;
		}
		fadeValue = black ? 1 : 0;
	}
}
