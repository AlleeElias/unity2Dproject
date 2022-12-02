using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Main : NetworkBehaviour
{
   [SerializeField] private Rigidbody2D followTransform;
   private Rigidbody2D body;
    private int target = 60;

    // Start is called before the first frame update
    void Awake()
    {
        body =GetComponent<Rigidbody2D>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
}
