using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private readonly NpcStateMachine _stateMachine = new NpcStateMachine();
    private readonly float _speed = 3;
    
    private IdleState _idleState;
    private PlayerDetector _playerDetector;
    private ZombieDamager _damager;
    private int _health = 50;
    
    public float Speed => _speed;

    private void Awake()
    {
        _playerDetector = transform.GetComponentInChildren<PlayerDetector>();
        TryGetComponent(out _damager);

        _idleState = new(this, _path);
        _stateMachine.Initialize(_idleState);
    }

    private void Update()
    {
        _stateMachine.CurrentState.Update();
    }

    private void OnEnable()
    {
        _playerDetector.LostPlayer += GoToIdle;
        _damager.PlayerDead += OnPlayerDead;
    }

    private void OnDisable()
    {
        _playerDetector.LostPlayer -= GoToIdle;
        _damager.PlayerDead -= OnPlayerDead;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Debug.Log("npc is dead!!!");
            Destroy(gameObject);
        }
    }

    public void StartChasing(Zombie player)
    {
        ChaseState chasing = new(this, player);
        _stateMachine.ChangeState(chasing);
    }

    public void GoToIdle()
    {
        _damager.enabled = false;
        _stateMachine.ChangeState(_idleState);
    }

    public void Attack()
    {
        _damager.enabled = true;
    }

    private void OnPlayerDead()
    {
        _damager.enabled = false;
        GoToIdle();
    }
}