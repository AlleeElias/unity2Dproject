using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField]private Transform leftEdge;
    [SerializeField]private Transform rightEdge;

    [SerializeField] private Transform enemy;

    [SerializeField] private float enemySpeed;
    private Vector3 initScale;
    private bool MovingLeft;

    [SerializeField]private Animator anim;
    private float idleTime;
    private float idleTimer;

    private void Awake()
    {
        idleTime = 2.0f;
        initScale = enemy.localScale;
    }
    private void Update()
    {
        if (MovingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x) { MoveInDirection(-1); }
            else { changeDirection(); }
        }
        else {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else { changeDirection(); }
        }
        
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void changeDirection() {

        anim.SetBool("moving", false);

        idleTimer += Time.deltaTime;
        if (idleTimer > idleTime) {
            MovingLeft = !MovingLeft;
        }
    }

    private void MoveInDirection(int direction) {

        idleTimer = 0;
        anim.SetBool("moving", true);
        //face right direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction,initScale.y,initScale.z);

        //move right direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * enemySpeed,
            enemy.position.y,enemy.position.z);
    }
}
