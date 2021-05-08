using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.Experimental.Rendering.Universal;

public class MakeLavaCold : MonoBehaviour
{
	public GameObject arenaLight;
	[Space(20)]
	public Light2D arenaWallLight;
	public Color arenaWallHotColor;
	public Color arenaWallColdColor;
	[Space(20)]
	public GameObject pointLight;
	[Space(20)]
	public Material lava;

	private bool isLavaHot = true;

	public void Activate()
	{
		StartCoroutine("ChangeLava", isLavaHot);
		if (isLavaHot)
		{
			arenaLight.SetActive(false);
			arenaWallLight.color = arenaWallColdColor;
			pointLight.SetActive(true);
		}
		else
		{
			arenaLight.SetActive(true);
			arenaWallLight.color = arenaWallHotColor;
			pointLight.SetActive(false);
		}
		isLavaHot = !isLavaHot;
	}

	IEnumerator ChangeLava(bool freeze)
	{
		float i = 0;
		while(i < 1)
		{
			lava.SetFloat("_Overlay", freeze ? 1 - i : i);

			i += Time.deltaTime;
			yield return null;
		}
	}

	private void OnGUI()
	{
		/*if(GUI.Button(new Rect(10, 120, 75, 25), "Change Lava"))
		{
			Activate();
		}*/
	}
}
