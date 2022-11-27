using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {
    
    [SerializeField] float upSpeed;

    Rigidbody2D _myRigid;

    void Start() {
        _myRigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        _myRigid.velocity = Vector2.up * upSpeed;
    }

    void OnBecameInvisible()
    {
        Update();
    }
}
