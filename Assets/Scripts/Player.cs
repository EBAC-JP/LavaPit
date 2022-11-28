using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class Player : MonoBehaviour {

    [Header("Moviments")]
    [SerializeField] float speed;
    [SerializeField] Vector2 friction = new Vector2(-.1f, 0);
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
    int _totalCoins = 0;
    Vector3 _startPosition;

    void Start() {
        _myRigid = GetComponent<Rigidbody2D>();
        _myAnim = GetComponent<Animator>();
        _distToGround = GetComponent<Collider2D>().bounds.extents.y;
        _startPosition = transform.position;
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

    void NextLevel() {
        GameManager.Instance.NextLevel();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Coin")) {
            Destroy(collider.gameObject);
            _totalCoins++;
            Invoke(nameof(NextLevel), .5f);
        }
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
        HandleFriction();
    }

    void HandleFriction() {
        if (_myRigid.velocity.x > 0) _myRigid.velocity += friction;
        else if (_myRigid.velocity.x < 0) _myRigid.velocity -= friction;
    }

    void Flip(int scale) {
        transform.DOScaleX(scale, .1f);
    }
    #endregion

    public int GetTotalCoins() {
        return _totalCoins;
    }

    public void ResetPlayer() {
        _myRigid.velocity = Vector2.zero;
        transform.position = _startPosition;
    }
}
