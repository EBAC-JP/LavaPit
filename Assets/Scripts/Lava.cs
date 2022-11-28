using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    Rigidbody2D _myRigid;
    float _upSpeed = 0;
    Vector3 _startPosition;

    void Start() {
        _myRigid = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

    void Update() {
        _myRigid.velocity = Vector2.up * _upSpeed;
    }

    void OnBecameInvisible()
    {
        Update();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Player")) {
            GameManager.Instance.EndGame();
        }
    }

    public void SetSpeed(float speed) {
        _upSpeed = speed;
    }

    public void ResetLava() {
        _upSpeed = 0;
        transform.position = _startPosition;
    }
}
