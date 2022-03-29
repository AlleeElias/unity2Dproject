using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Following the player
    [SerializeField] private Transform player;

    private void Awake()
    {
        speed = 5;
    }

    private void Update()
    {
        //Player following
        transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);


        //Per room camera
        //transform.position = Vector3.SmoothDamp(transform.position,new Vector3(currentPosX,transform.position.y,transform.position.z),ref velocity, speed);
    }

    //Moving rooms for the per room camera
    public void moveToNewRoom(Transform newRoom) {
        currentPosX = newRoom.position.x;
    }
}
