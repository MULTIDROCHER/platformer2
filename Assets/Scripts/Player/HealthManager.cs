using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Text _healthText;

    private const int MaxHealth = 100;
    private int _currentHealth;

    public int Health => _currentHealth;

    private void Start()
    {
        _currentHealth = MaxHealth;
        ShowHealthAmount();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        TryTakeHeal(collider);
    }

    private void ShowHealthAmount()
    {
        _healthText.text = _currentHealth.ToString();
    }

    private void TryTakeHeal(Collider2D collider)
    {
        if (collider.TryGetComponent(out Heal heal)
        && _currentHealth + heal.RecoveryAmount <= MaxHealth)
        {
            _currentHealth += heal.RecoveryAmount;
            Destroy(heal.gameObject);
            ShowHealthAmount();
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Debug.Log("player is dead!!!");
            _currentHealth = 0;
            Destroy(gameObject);
        }

        ShowHealthAmount();
    }
}