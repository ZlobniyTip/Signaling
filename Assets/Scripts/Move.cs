using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _direction;

    private void Start()
    {
        var _spawn = GameObject.FindObjectOfType<Spawn>();
        _direction = _spawn.SetDirection();
    }

    void Update()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }
}