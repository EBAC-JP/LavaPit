using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : Singleton<Camera> {

    [SerializeField] float smoothTime;
    [SerializeField] float maxLimit;
    [SerializeField] float minLimit;

    Transform _target;
    Vector2 _velocity;

    void Update() {
        if (_target != null) {
            float posY = Mathf.SmoothDamp(transform.position.y, _target.position.y, ref _velocity.y, smoothTime);
            if (posY < 0) posY = 0;
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }
    }

    public void SetTarget(GameObject target) {
        _target = target.GetComponent<Transform>();
    }
}
