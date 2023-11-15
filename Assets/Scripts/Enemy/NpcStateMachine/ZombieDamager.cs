using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieDamager : MonoBehaviour
{
    private HealthManager _player;
    private int _damage = 5;
    private float _delay = 1;
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
            Debug.Log("can hit");
            _player = healthManager;
            _canDamage = true;
            StartCoroutine(Attack(_player));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out HealthManager healthManager) || other == null)
        {
            Debug.Log("not hit");
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

            Debug.Log("hit");
            yield return _waitForSeconds;

            if (_player == null || _player.Health <= 0)
            {

                Debug.Log("ended corout");
                PlayerDead?.Invoke();
            }
        }
    }
}