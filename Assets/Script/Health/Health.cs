using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float startingHealth;
    private float curHealth;
    private Animator anim;
    private bool dead;

    private float invDuration;
    private int numbFlashes;
    private SpriteRenderer renderer;

    private void Awake()
    {
        invDuration = 2;
        renderer = GetComponent<SpriteRenderer>();
        numbFlashes = 3;
        dead = false;
        anim = GetComponent<Animator>();
        startingHealth = 10f;
        curHealth = startingHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            takeDamage(1.0f);
        }
    }

    public void takeDamage(float damage) {

        curHealth = Mathf.Clamp(curHealth - damage , 0 , startingHealth);

        if (curHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else {
            if (!dead) {
                if (GetComponent<PlayerMovement>() != null) {
                    //player
                    GetComponent<PlayerMovement>().enabled = false;
                    anim.SetTrigger("deaded");
                }
                else if (GetComponentInParent<EnemyPatrol>()) {
                    //enemy
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    GetComponent<EnemyAI>().enabled = false;
                    anim.SetTrigger("deaded");
                }
                dead = true;
            }
        }
    }
    public void takeHealth(float health) {
        curHealth = Mathf.Clamp(curHealth + health, 0, startingHealth);
    }

    public float getCurHealth() {
        return curHealth;
    }

    private IEnumerator Invunerability() {
        Physics2D.IgnoreLayerCollision(3,8,true);
        //Duration

        for (int i = 0; i <numbFlashes; i++)
        {
            renderer.color= new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(invDuration/(numbFlashes*2));
            renderer.color = Color.white;
            yield return new WaitForSeconds(invDuration / (numbFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(3, 8, false);
    }
}
