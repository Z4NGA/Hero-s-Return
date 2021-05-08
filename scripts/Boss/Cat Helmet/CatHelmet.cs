using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class CatHelmet : MonoBehaviour
{
	public GameObject levelController;
	public GameObject boss;
	public GameObject spotLight;
	public GameObject arenaLight;

	public CameraFade cameraFade;
	public CameraShake shaker;
	public float shakeDuration;
	public float shakeStrenght;
	public BossCinematicCamera cineCam;

	CatHelmetMovement movement;
	new SpriteRenderer renderer;

	new AudioSource audio;
	public AudioClip questioningMeow;

	public GameObject vanishParticles;
	public GameObject healthCanvas;
	public GameObject fadeCanvas;

	public GameObject bgMusic;

	private void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
		renderer.enabled = false;
		movement = GetComponent<CatHelmetMovement>();
		movement.enabled = false;
		audio = GetComponent<AudioSource>();
		healthCanvas.SetActive(false);
		cineCam.enabled = false;
		shaker = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
	}

	public void Activate(bool skipIntro)
	{
		if(skipIntro)
		{
			spotLight.SetActive(false);
			boss.transform.position = Vector2.zero;
			boss.GetComponent<Boss>().locked = false;
			healthCanvas.SetActive(true);
			cameraFade.fadeImage.color = new Color(0, 0, 0, 0);
			fadeCanvas.SetActive(false);
			cineCam.enabled = true;
			bgMusic.SetActive(true);
		}
		else
		{
			Debug.Log("Playing Intro!");
			StartCoroutine("Run");
		}
			
	}

	IEnumerator Run()
	{
		// boss wegschieben und level dunkel machen (für dramatischen effekt): Hier soll der Spieler noch nichts sehen aka schwarzer Bildschirm
		boss.transform.position = new Vector3(100, 100);
		boss.GetComponent<Boss>().canBeDamaged = false;
		levelController.GetComponent<MakeLavaCold>().Activate();
		arenaLight.SetActive(false);
		yield return new WaitForSeconds(2);
		StartCoroutine(cameraFade.Fade(0.3f, false));
		yield return new WaitForSeconds(5);
		fadeCanvas.SetActive(false);

		// ab hier kann der spieler sehen
		spotLight.SetActive(true);
		renderer.enabled = true;
		movement.enabled = true;
		yield return new WaitForSeconds(movement.rattleInterval * 3);

		movement.enabled = false;
		yield return new WaitForSeconds(1);

		// hier muss die katze miaun
		audio.clip = questioningMeow;
		audio.Play();
		yield return new WaitForSeconds(1.5f);
		// hier muss noch irgeneine animation hin, damit der spieler merkt, dass die Katze ihn sieht
		GameObject g = Instantiate(vanishParticles);
		g.GetComponent<ParticleSystem>().Play();
		renderer.enabled = false;
		yield return new WaitForSeconds(0.6f);
		Destroy(g);

		spotLight.SetActive(false);
		boss.transform.position = Vector2.zero;
		yield return new WaitForSeconds(5);
		shaker.Shake(shakeDuration, shakeStrenght);
		bgMusic.SetActive(true);
		yield return StartCoroutine(boss.GetComponent<Boss>().TriggerStareC(3));

		levelController.GetComponent<MakeLavaCold>().Activate();
		yield return new WaitForSeconds(1.5f);

		boss.GetComponent<Boss>().locked = false;
		boss.GetComponent<Boss>().canBeDamaged = true;
		healthCanvas.SetActive(true);
		cineCam.enabled = true;


		gameObject.SetActive(false);
	}

	private void OnGUI()
	{
		/*if(GUI.Button(new Rect(10, 300, 75, 25), "Intro!"))
		{
			Activate(false);
		}

		if(GUI.Button(new Rect(10, 330, 75, 25), "Skip Intro"))
		{
			Activate(true);
		}*/
	}

}
