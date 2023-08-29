using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BeingTrygger _endPoint;

    private Coroutine _fadingJob;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _recoveryRate = 0.3f;
    private bool _inTrigger = true;
    private bool _outTrigger = false;

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

    private IEnumerator Fading()
    {
        while (_audioSource.volume != _maxVolume || _audioSource.volume != _minVolume)
        {
            SetVolume(_inTrigger, _maxVolume);

            yield return null;

            SetVolume(_outTrigger, _minVolume);

            yield return null;
        }
    }

    private void SetVolume(bool isTrigger, float target)
    {
        if (_endPoint._isCollision == isTrigger)
        {
            float volume = Mathf.MoveTowards(_audioSource.volume, target, _recoveryRate * Time.deltaTime);
            _audioSource.volume = volume;
        }
    }

    public void RestartCoroutine()
    {
        Stop();
        _fadingJob = StartCoroutine(Fading());
    }
}