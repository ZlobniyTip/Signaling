using UnityEngine;
using UnityEngine.Events;

public class BeingTrygger : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Fader _fader;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    public float Target { get; private set; }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_collider.TryGetComponent<Player>(out Player player))
        {
            Target = _maxVolume;
            _fader.RestartCoroutine();
            _reached?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collider.TryGetComponent<Player>(out Player player))
        {
            Target = _minVolume;
            _fader.RestartCoroutine();
        }
    }
}