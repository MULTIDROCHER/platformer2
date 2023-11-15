using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    private Npc _npc;
    private Zombie _player;

    public UnityAction LostPlayer;

    private void Start()
    {
        transform.parent.TryGetComponent(out _npc);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out _player))
            _npc.StartChasing(_player);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out Zombie zombie);

        if (zombie != null && zombie == _player)
        {
            _player = null;
            LostPlayer?.Invoke();
        }
    }
}