using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    //Do Tween
    [SerializeField]
    private float _jumpDuration = 1.3f, _jumpForwardDistance = 15f, _jumpPower = 1f;
    
    [SerializeField]
    private int _jumpNumber = 1;
    
    //Movement
    private Transform _playerParent;
    private Vector3 _movementVector;

    //Input Control
    private float _mouseDeltaPositionX = 0; // Take the position of the finger
    private float _maxDistanceRightLeft = 1.3f; // How far can you go left and right?

    private bool _onGroundedFlag = true;
    private int _randomNumber = 0;

    void Start()
    {
        _playerParent = transform.parent;
        Jump();
    }

    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        MoveRightLeft();
    }
    
    void CheckInput()
    {
        if (Input.touchCount > 0) // If there is touch on the screen
            _mouseDeltaPositionX = Input.GetTouch(0).deltaPosition.x; // Take the position of the finger
        else
            _mouseDeltaPositionX = Input.GetAxis("Horizontal") * 8; // Take positions if the movement is from the keyboard

        if (Input.touchCount < 1) // If there is touch no touch the screen
            _mouseDeltaPositionX = 0;
    }

    void Jump()
    {
        transform.DOKill(false);
        transform.DOLocalJump(new Vector3(0f, 0f, _jumpForwardDistance), _jumpPower, _jumpNumber, _jumpDuration, false).SetRelative(true).
            SetUpdate(UpdateType.Fixed, false).SetEase(Ease.Linear).OnComplete(() => Jump());

        _randomNumber = Random.Range(1, 5);
        ResetRotation();
        if (_randomNumber == 1)
        {
            transform.DORotate(new Vector3(0f, 359f, 0f), 0.8f).SetRelative(true).
            SetUpdate(UpdateType.Fixed, false).SetEase(Ease.InOutQuad);
        }
        else if (_randomNumber == 2)
        {
            transform.DORotate(new Vector3(359f, 0f, 0f), 0.8f).SetRelative(true).
            SetUpdate(UpdateType.Fixed, false).SetEase(Ease.InOutQuad);
        }

        OnGroundControl();
    }

    void MoveRightLeft()
    {
        // Creates the default motion vector
        _movementVector = new Vector3(_mouseDeltaPositionX * _maxDistanceRightLeft, 0, 0) * Time.fixedDeltaTime;

        //It prevents it from going beyond these boundaries
        if (transform.position.x + _movementVector.x > 10.1f || transform.position.x + _movementVector.x < -10.1f)
            _movementVector = new Vector3(0, 0, 0);

        //Makes the movement
        _playerParent.Translate(_movementVector, Space.World);
    }

    void OnGroundControl()
    {
        //Player on the ground?
        if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0f), -transform.up, 1f) || Physics.Raycast(transform.position + new Vector3(0.31f, 1f, 0), -transform.up, 1f) || Physics.Raycast(transform.position + new Vector3(-0.31f, 1f, 0), -transform.up, 1f))
            _onGroundedFlag = true;
        else
            _onGroundedFlag = false;

        if (!_onGroundedFlag)
        {
            DOTween.Kill(transform, false);
            FindObjectOfType<UIManager>().Finish(false);
        }
    }

    void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Last Dancer"))
        {
            DOTween.Kill(transform, false);
            FindObjectOfType<UIManager>().Finish(true); 
        }
    }

}
