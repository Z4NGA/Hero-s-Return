using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Author: Saša 
 * Description: This script gives the player health and also enables a hurt flash
 * when being hit by an enemy
 */
public class PlayerHealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    private bool flashActive;

    [SerializeField]
    private float flashLength = 0f;
    private float flashCount = 0f;
    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flashActive)
        {
            // jede Sechstel Sekunde wird der Player "blinken"
            if(flashCount > flashLength * .99f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
                                    playerSprite.color.g,
                                    playerSprite.color.b,
                                    0f);
            }
            else if(flashCount > flashLength * .82f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
                                    playerSprite.color.g,
                                    playerSprite.color.b,
                                    1f);
            }
            else if(flashCount > flashLength * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
                                   playerSprite.color.g,
                                   playerSprite.color.b,
                                   0f);
            }
            else if (flashCount > flashLength * .49f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
                                   playerSprite.color.g,
                                   playerSprite.color.b,
                                   1f);
            }
            else if (flashCount > flashLength * .16f)
            {
                playerSprite.color = new Color(playerSprite.color.r,
                                   playerSprite.color.g,
                                   playerSprite.color.b,
                                   0f);
            }
            else if (flashCount > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r,
                                   playerSprite.color.g,
                                   playerSprite.color.b,
                                   1f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r,
                playerSprite.color.g,
                playerSprite.color.b,
                1f);

                flashActive = false;
            }
            flashCount -= Time.deltaTime;
        }
    }

    IEnumerator hurtFlash()
    {
        
        while(flashActive)
        {

            yield return null;
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        currentHealth -= damageToGive;
        flashActive = true;
        flashCount = flashLength; // Counter zurücksetzen

        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
