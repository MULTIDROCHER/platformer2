using UnityEngine;

public class Npc : MonoBehaviour
{
    [SerializeField] private Transform _path;
    private NpcStateMachine _stateMachine = new NpcStateMachine();
    private IdleState _idleState;
    private PlayerDetector _playerDetector;
    private ZombieDamager _damager;
    private int _health = 50;
    private float _speed = 3;
    public float Speed => _speed;
    public string text;

    private void Awake()
    {
        _playerDetector = transform.GetComponentInChildren<PlayerDetector>();
        TryGetComponent(out _damager);

        _idleState = new IdleState(this, _path);
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
        ChaseState chasing = new ChaseState(this, player);
        text = "chasing";
        _stateMachine.ChangeState(chasing);
    }

    public void GoToIdle()
    {
        _damager.enabled = false;
        text = "idle";
        _stateMachine.ChangeState(_idleState);
    }

    public void Attack(Zombie player)
    {
        text = "attack";
        _damager.enabled = true;
    }

    private void OnPlayerDead()
    {
        _damager.enabled = false;
        GoToIdle();
    }
}