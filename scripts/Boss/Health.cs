using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
	public Image healthBar;
	public float maxHealth = 100;
	[SerializeField]
	private float _currentHealth = 100;
	public float CurrentHealth
	{
		get => _currentHealth;
		set
		{
			if (value > maxHealth)
				_currentHealth = maxHealth;
			else if (value <= 0)
				_currentHealth = 0;
			else
				_currentHealth = value;
			healthBar.rectTransform.localScale = new Vector3((_currentHealth / maxHealth) * initialScale, 1, 1);
		}
	}
	private float initialScale;

	private void Start()
	{
		initialScale = healthBar.rectTransform.localScale.x;
	}

	public void modifyHealth(float amount)
	{
		CurrentHealth += amount;
	}

	private string damageTest = "-50";
	private void OnGUI()
	{
		/*damageTest = GUI.TextField(new Rect(10, 10, 75, 20), damageTest);
		if (GUI.Button(new Rect(10, 35, 75, 25), "Damage"))
		{
			modifyHealth(float.Parse(damageTest));
		}*/
	}
}
