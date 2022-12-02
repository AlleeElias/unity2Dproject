using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    //Create private fields for later use
    private Enemy enem;
    private Animator anim;
    private Rigidbody2D body;
    private BoxCollider2D boxcol2;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField] private LayerMask enemyLayer;
    private float speed;
    private float horInput;
    private float wallCooldown;
    //private bool isKicking;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Debug.Log("Dit is de server?");
        }
    }

    void Awake()
    {
        //Create reference to the needed components of the player
        body=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxcol2 = GetComponent<BoxCollider2D>();
        speed=1.5f;
        //isKicking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(onWall());
        //Check if an arrowkey is pressed
        horInput = Input.GetAxis("Horizontal");

        //Change sprite direction based on input
        if (horInput > 0.01f) { transform.localScale = Vector3.one; }
        else if (horInput < -0.01f) { transform.localScale = new Vector3(-1, 1, 1); }

        if (body.velocity.y < -1) { anim.SetBool("falling", true); }
        //if (isGrounded()) { anim.SetBool("falling", false);}

        //If horInput !=0, set run to true
        anim.SetBool("run", horInput != 0);
        //If !grounded -> player in air
        anim.SetBool("grounded", isGrounded());


        //Downkey for crouching while standing still
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isGrounded() && horInput == 0) { anim.SetBool("crouching", true); }
        else { anim.SetBool("crouching", false); }
        /*
        //Downkey while walking
        if (Input.GetKey(KeyCode.S)&& isGrounded() && (horInput>0.1f || horInput < -0.1f)) { anim.SetBool("crouchingrun",true); }
        else { anim.SetBool("crouchingrun",false); }*/
        //E key for kicking
        if (Input.GetKeyDown(KeyCode.E) && horInput == 0)
        {
            anim.SetTrigger("kick");
            if (enem != null) { enem.isHit(5.0f); }
        }
        //Check if the player has been of a wall long enough
        if (wallCooldown > 0.2f)
        {
            //Change speed based on the input
            body.velocity = new Vector2(horInput * speed, body.velocity.y);

            //Check if the player is touching a wall but not the ground
            if (onWall() && !isGrounded())
            {
                //If the player is touching a wall, he will be frozen
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                //If a player is not touching a wall, he will fall as he normally would
                body.gravityScale = 1;
            }
            //Space for jumping
            if (Input.GetKey(KeyCode.Space))
            {
                //This is implement in  a different void for optimization
                jump();
            }
        }
        else {
            wallCooldown += Time.deltaTime;
        }
    }
    /*
    //Implement player movement (redundant)
    private void playerControl(){

    }*/
	//jumping in seperate void for optimization
    private void jump() {
        if (isGrounded()) {
            body.velocity = new Vector2(body.velocity.x, speed * 3);
            anim.SetTrigger("jump");
        } else if (onWall() && !isGrounded()) {
            if (horInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 2);
            }
            
            wallCooldown = 0;
        }
    }
    
    //Not yet implemented
    public void addShield() {
        /*sp = new SpriteRenderer();
        Sprite sprite= Resources.Load<Sprite>("Sprites/Pickups/shieldp.png");
        sp.sprite = sprite;*/
    }
	//If the player collides with the ground =/= already standing on it
    //That is why it has been seperated from the isGrounded() method which can only see if the player is already standing on it
    private void OnCollisionEnter2D(Collision2D collision)
    {
		//If player collides with the ground, landing will be forcefully played
        if (collision.gameObject.tag == "Ground") {
            anim.Play("land");
            anim.SetBool("falling", false);
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.enem = collision.gameObject.GetComponent<Enemy>();
        }
        else { this.enem = null; }
    }
    //Checks if the player is standing on the ground
    private bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcol2.bounds.center, boxcol2.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    
	//Checks if the player is on a wall (not yet implemented)
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcol2.bounds.center, boxcol2.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canShoot() {
        return horInput==0 && isGrounded() && !onWall();
    }

}
