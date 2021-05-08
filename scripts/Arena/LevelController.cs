using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
	public GameObject catHelmet;
	public GameObject boss;
	private Boss bossScript;
	private Health bossHealth;
	public GameObject player;
	private Player playerScript;
	public GameObject bossHealthCanvas;

	public CameraFade cameraFade;

	public GameObject YouDiedText;
	public TextMeshProUGUI tmp;

	MakeLavaCold makeLavaCold;

	bool anyScreenShown = false;

	private void Start()
	{
		makeLavaCold = GetComponent<MakeLavaCold>();
		tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0);
		catHelmet.GetComponent<CatHelmet>().Activate(SceneVariableContainer.skipIntro);
		playerScript = player.GetComponent<Player>();
		bossScript = boss.GetComponent<Boss>();

		GetComponent<AudioSource>().volume = 0.05f;
	}

	private void Update()
	{
		if (!playerScript.isAlive && !anyScreenShown)
		{
			anyScreenShown = true;
			StartCoroutine(OnPlayerDeath());
		}
	}

	public void RestartScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	private IEnumerator OnPlayerDeath()
	{
		SceneVariableContainer.skipIntro = true;
		bossHealthCanvas.SetActive(false);
		boss.GetComponent<Boss>().debugControl = true;
		playerScript.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		player.GetComponent<movement>().knockback(99, 0, transform);
		player.GetComponent<Animator>().SetBool("isDead", true);
		soundEngine.play_sound_timed(soundEngine.sound_type.boss_player_death, 10f);
		yield return new WaitForSeconds(2);
		YouDiedText.SetActive(true);
	}

	private IEnumerator FadeYouDiedText()
	{
		float time = 0;
		float fadeTime = 0.3f;
		while(time <= 1)
		{
			tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, time);
			time += Time.deltaTime * fadeTime;
			yield return null;
		}
	}
    
}
