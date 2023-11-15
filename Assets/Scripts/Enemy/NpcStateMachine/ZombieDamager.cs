using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ZombieDamager : MonoBehaviour
{
    private readonly int _damage = 5;
    private readonly float _delay = 1;
    
    private HealthManager _player;
    private bool _canDamage = false;
    private WaitForSeconds _waitForSeconds;

    public UnityAction PlayerDead;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_delay);
        enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out HealthManager healthManager))
        {
            _player = healthManager;
            _canDamage = true;
            StartCoroutine(Attack(_player));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out HealthManager healthManager) || other == null)
        {
            _canDamage = false;
            StopCoroutine(Attack(_player));
            _player = null;
        }
    }

    private IEnumerator Attack(HealthManager player)
    {
        while (_player.Health > 0 && _canDamage)
        {
            player.TakeDamage(_damage);

            yield return _waitForSeconds;

            if (_player == null || _player.Health <= 0)
                PlayerDead?.Invoke();
        }
    }
}