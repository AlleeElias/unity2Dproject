using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    private bool pickedUp;
    private BoxCollider2D box;
    private PlayerMovement p;
    private Transform tr;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (pickedUp) {
            Destroy(box);
            tr.position = new Vector2(p.GetComponent<Rigidbody2D>().position.x,p.GetComponent<Rigidbody2D>().position.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            //Destroy(gameObject);
            pickedUp = true;
            anim.SetTrigger("pickup");
            p = collision.gameObject.GetComponent<PlayerMovement>();
            p.addShield();
        }
    }}