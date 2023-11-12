using UnityEngine;

public class Npc : MonoBehaviour
{
    private NpcStateMachine _stateMachine;
    private int _health = 50;

    public int Health => _health;

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Debug.Log("npc is dead!!!");
            Destroy(gameObject);
        }
    }
}