using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    [SerializeField] float smoothTime;
    [SerializeField] float maxLimit;
    [SerializeField] float minLimit;
    [SerializeField] Transform target;

    Vector2 _velocity;

    void Update() {
        if (target != null) {
            float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref _velocity.y, smoothTime);
            if (posY < 0) posY = 0;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }
    }
}
