using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Zombie : MonoBehaviour
{
    [SerializeField] private AnimationClip _leftKick;
    [SerializeField] private AnimationClip _rightKick;

    private ZombieMovement _movement;
    private Animator _animator;
    private int _damage = 10;
    private bool _isKicking = false;

    private void Start()
    {
        TryGetComponent(out _movement);
        TryGetComponent(out _animator);
    }

    private void Update()
    {
        if (_movement.IsGrounded && Input.GetMouseButtonDown(0))
        {
            if (_movement.Movement.x > 0)
                Kick(_rightKick);
            else if (_movement.Movement.x < 0)
                Kick(_leftKick);
        }

        _isKicking = false;
    }

    private void Kick(AnimationClip animation)
    {
        _isKicking = true;
        _animator.Play(animation.name);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Npc npc) && _isKicking)
            npc.TakeDamage(_damage);
    }
}