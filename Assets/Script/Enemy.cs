using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Create enemy values
    [SerializeField]private float hp;
    private bool isDead;
    private BoxCollider2D enemCol;
    [SerializeField] LayerMask playerLayer;
    private Animator anim;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemCol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();   }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hp <= 0) { gameObject.SetActive(false); }
    }
    //Hit detection is done in the player class
    public void isHit(float damage) {
        anim.SetTrigger("isKicked");
        anim.Play("kicked");
        this.hp = hp - damage;
    }
    public float getHP() { return this.hp; }
}
