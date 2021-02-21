using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 500)] private float _movingSpeed = 350;
    [SerializeField] [Range(0, 500)] private float _jumpForce = 350;
    [SerializeField] private LayerMask _ground;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Collider2D _collider2D;
    private float _xMovement;
    private bool _isGrounded;
    private bool _isJumped = false;
    private bool _facingRight = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _xMovement = Input.GetAxis("Horizontal") * _movingSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_xMovement));

        if (Input.GetButtonDown("Jump"))
            _isJumped = true;
    }

    private void FixedUpdate()
    {
        Move(_xMovement * Time.deltaTime, _isJumped);
        _isJumped = false;
    }

    private void Move(float xMovement, bool isJumped)
    {
        _rigidbody2D.velocity = new Vector2(xMovement, _rigidbody2D.velocity.y);
        _isGrounded = Physics2D.BoxCast(_collider2D.bounds.center, _collider2D.bounds.size, 0f, Vector2.down, 0.1f, _ground);

        if (isJumped && _isGrounded)
            _rigidbody2D.AddForce(new Vector2(0f, _jumpForce));

        if (xMovement > 0 && !_facingRight)
            Flip();
        else if (xMovement < 0 && _facingRight)
            Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}