using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpForce;

    private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private Vector2 _movement;
    private float _speed;
    private bool _isGrounded = true;
    
    public Vector2 Movement => _movement;
    public bool IsGrounded => _isGrounded;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            DoJump();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_movement.x, _rigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Ground ground))
            _isGrounded = true;
    }

    private void MovePlayer()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _animator.SetFloat(HorizontalHash, _movement.x);

        if (_movement.sqrMagnitude > 0)
        {
            _speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;
            _animator.SetFloat(SpeedHash, _speed);
        }
        else
        {
            _speed = 0;
            _animator.SetFloat(SpeedHash, _speed);
        }

        _movement *= _speed;
    }

    private void DoJump()
    {
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }
}