using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private EndPoint _endPoint;
    private Coroutine _fadingJob;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _recoveryRate = 0.3f;
    private bool _inTrigger = true;
    private bool _outTrigger = false;
    private string _true = "True";
    private string _false = "False";

    private void Start()
    {
        RestartCoroutine();
    }

    private void Stop()
    {
        if (_fadingJob != null)
        {
            StopCoroutine(_fadingJob);
        }
    }

    public void RestartCoroutine()
    {
        Stop();
        _fadingJob = StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        while (_audioSource.volume != _maxVolume || _audioSource.volume != _minVolume)
        {
            SetVolume(_inTrigger, _maxVolume, _true);

            yield return null;

            SetVolume(_outTrigger, _minVolume, _false);

            yield return null;
        }
    }

    private void SetVolume(bool isTrigger, float target, string trueOrFalse)
    {
        if (_endPoint.IsCollision() == isTrigger)
        {
            float volume = Mathf.MoveTowards(_audioSource.volume, target, _recoveryRate * Time.deltaTime);
            _audioSource.volume = volume;
            Debug.Log(trueOrFalse);
        }
    }
}