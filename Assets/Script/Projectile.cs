using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;
    private BoxCollider2D boxcol;
    private Animator anim;

    private void Awake()
    {
        speed = 5.0f;
        boxcol = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed,0,0);

        lifeTime += Time.deltaTime;
        if (lifeTime >= 5) deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxcol.enabled = false;
        anim.SetTrigger("explode");
    }

    public void setDirection(float direction) {
        lifeTime = 0;
        this.direction = direction;
        gameObject.SetActive(true);
        hit = false;
        boxcol.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction) {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX,transform.localScale.y,transform.localScale.z);
    }

    private void deactivate() {
        gameObject.SetActive(false);
    }
}
