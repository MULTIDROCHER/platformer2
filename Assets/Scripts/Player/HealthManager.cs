using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Text _healthText;

    private int _maxHealth = 100;
    private int _health;

    public int Health => _health;

    private void Start()
    {
        _health = _maxHealth - 90;
        ShowHealthAmount();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        TryTakeHeal(collider);
    }

    private void ShowHealthAmount()
    {
        _healthText.text = _health.ToString();
    }

    private void TryTakeHeal(Collider2D collider)
    {
        if (collider.TryGetComponent(out Heal heal)
        && _health + heal.RecoveryAmount <= _maxHealth)
        {
            _health += heal.RecoveryAmount;
            Destroy(heal.gameObject);
            ShowHealthAmount();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Debug.Log("player is dead!!!");
            _health = 0;
            Destroy(gameObject);
        }

        ShowHealthAmount();
    }
}