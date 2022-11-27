using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class Player : MonoBehaviour {

    [Header("Moviments")]
    [SerializeField] float speed;
    [SerializeField] KeyCode leftKey;
    [SerializeField] KeyCode rightKey;
    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float spaceToJump;
    [SerializeField] KeyCode jumpKey;
    [Header("Animation")]
    [SerializeField] string runVariable;
    [SerializeField] string groundVariable;
    [SerializeField] string jumpTrigger;

    Rigidbody2D _myRigid;
    Animator _myAnim;
    float _distToGround;

    void Start() {
        _myRigid = GetComponent<Rigidbody2D>();
        _myAnim = GetComponent<Animator>();
        _distToGround = GetComponent<Collider2D>().bounds.extents.y;
    }

    void Update() {
        HandleAnimations();
        HandleMoviments();
    }

    void HandleAnimations() {
        _myAnim.SetBool(groundVariable, IsGrounded());
        if (_myRigid.velocity.x != 0) _myAnim.SetBool(runVariable, true);
        else _myAnim.SetBool(runVariable, false);
    }

    #region Jump
    bool IsGrounded() {
        return Physics2D.Raycast(transform.position, -Vector2.up, _distToGround + spaceToJump);
    }

    [Button]
    void Jump() {
        _myRigid.velocity = Vector2.up * jumpForce;
        _myAnim.SetTrigger(jumpTrigger);
    }
    #endregion

    #region Moviments
    void HandleMoviments() {
        if (Input.GetKeyDown(jumpKey) && IsGrounded()) Jump();
        if (Input.GetKey(leftKey)) {
            _myRigid.velocity = new Vector2(-speed, _myRigid.velocity.y);
            Flip(-1);
        } else if (Input.GetKey(rightKey)) {
            _myRigid.velocity = new Vector2(speed, _myRigid.velocity.y);
            Flip(1);
        }
    }

    void Flip(int scale) {
        transform.DOScaleX(scale, .1f);
    }
    #endregion
}
