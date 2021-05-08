using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
	public Vector2 stayTime = new Vector2(5, 5);

	public Sprite[] shapes;

	public int numberOfPoints;
	public float radius;
	private float playerHitBoxWidth;

	public LayerMask playerLayer;

	SpriteMask mask;
	public Boss boss;
	private in_targetable player;

	private const float soundLenght = 3.5f;
	private const float soundCutoff = 2.25f;

	private bool playerCanBeDamaged = true;
	private float triggerStayTime = 0;
	public float playerDamageIntervall = 0.5f;

	private void Start()
	{
		mask = GetComponent<SpriteMask>();
		int shape = Mathf.FloorToInt(Random.value * shapes.Length);
		mask.sprite = shapes[shape];
		StartCoroutine(Delete());
		transform.rotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
		player = boss.target.GetComponent<in_targetable>();
		playerHitBoxWidth = boss.target.GetComponent<CircleCollider2D>().radius;
	}

	private void Update()
	{
		if (Vector2.Distance(transform.position, boss.target.position) <= radius - playerHitBoxWidth && playerCanBeDamaged)
		{
			if (triggerStayTime == 0)
			{
				popup_damage.popup(boss.damage_RockLavaHole, boss.target.position, true);
				player.take_damage(boss.damage_RockLavaHole);
				soundEngine.play_sound(soundEngine.sound_type.player_burning);
			}
			triggerStayTime += Time.deltaTime;
			if (triggerStayTime >= playerDamageIntervall)
				triggerStayTime = 0;
		}
	}

	private IEnumerator Delete()
	{
		float delay = Random.value * (stayTime.y - stayTime.x) + stayTime.x;
		yield return new WaitForSeconds(delay - soundLenght);
		soundEngine.play_sound_timed(soundEngine.sound_type.rock_hole_reverse, soundLenght);
		StartCoroutine(FadeAlpha());
		Destroy(gameObject, soundLenght);
	}

	private IEnumerator FadeAlpha()
	{
		float time = 0;
		while(time <= soundCutoff)
		{
			mask.alphaCutoff = time / soundCutoff;
			time += Time.deltaTime;
			yield return null;
		}
		mask.alphaCutoff = 1;
		playerCanBeDamaged = false;
	}
}
