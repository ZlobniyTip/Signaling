using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Enemy _enemy;
    private Transform[] _points;
    private Coroutine _createEnemyJob;

    private void Start()
    {
        _points = new Transform[_transform.childCount];
        _createEnemyJob = StartCoroutine(CreateEnemy());
    }

    private void RestartCoroutine()
    {
        StopCoroutine(CreateEnemy());
        _createEnemyJob = StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        var waitForTwoSeconds = new WaitForSeconds(2);

        for (int i = 0; i < _transform.childCount; i++)
        {
            _points[i] = _transform.GetChild(i);
            Instantiate(_enemy, _points[i].transform.position, Quaternion.identity);
            yield return waitForTwoSeconds;
        }

        RestartCoroutine();
    }

    public Vector3 SetDirection()
    {
        int random = Random.Range(0, 2);

        if (random == 1)
        {
            Debug.Log("Left");
            return Vector3.left;
        }

        Debug.Log("Rigth");
        return Vector3.right;
    }
}
