using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Zombie : MonoBehaviour
{
    private readonly int _leftKick = Animator.StringToHash("attackWest");
    private readonly int _rightKick = Animator.StringToHash("attackEast");

    private ZombieMovement _movement;
    private Animator _animator;
    private Npc _enemy;
    private bool _isKicking = false;
    private int _damage = 10;

    private void Start()
    {
        TryGetComponent(out _movement);
        TryGetComponent(out _animator);
    }

    private void Update()
    {
        if (_movement.IsGrounded
        && Input.GetMouseButtonDown(0)
        && _isKicking == false)
            Kick(_movement.Movement);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.TryGetComponent(out _enemy);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == _enemy)
            _enemy = null;
    }

    private void Kick(Vector2 movement)
    {
        _isKicking = true;

        if (movement.x > 0)
            _animator.Play(_rightKick);
        else if (movement.x < 0)
            _animator.Play(_leftKick);

        if (_enemy != null)
            _enemy.TakeDamage(_damage);

        _isKicking = false;
    }
}