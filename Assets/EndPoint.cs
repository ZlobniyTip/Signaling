using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Fader _fader;
    private bool _isCollision;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_collider.TryGetComponent<Player>(out Player player))
        {
            _isCollision = true;
            _fader.RestartCoroutine();
            _reached?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_collider.TryGetComponent<Player>(out Player player))
        {
            _isCollision = false;
            _fader.RestartCoroutine();
        }
    }

    public bool IsCollision()
    {
        if (_isCollision)
        {
            return true;
        }

        return false;
    }
}