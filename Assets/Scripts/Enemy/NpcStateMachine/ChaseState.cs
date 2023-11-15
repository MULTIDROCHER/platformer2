using UnityEngine;

public class ChaseState : State
{
    private readonly Npc _npc;
    private readonly Zombie _target;

    public ChaseState(Npc npc, Zombie player)
    {
        _npc = npc;
        _target = player;
    }

    public override void Update()
    {
        if (_target != null)
        {
            _npc.transform.position = Vector3.MoveTowards(_npc.transform.position, _target.transform.position, _npc.Speed * Time.deltaTime);

            if (_npc.GetComponent<Collider2D>().IsTouching(_target.GetComponent<Collider2D>()))
                _npc.Attack();
        }
    }
}