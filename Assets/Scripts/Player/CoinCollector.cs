using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    [SerializeField] private AudioSource _audioSource;

    private int _coins;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Coin>())
        {
            _audioSource.PlayOneShot(_audioSource.clip);
            Destroy(collider.gameObject);
            _coins++;
            ShowCoinsAmount();
        }
    }

    private void ShowCoinsAmount()
    {
        _coinText.text = _coins.ToString();
    }
}