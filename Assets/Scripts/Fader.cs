using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BeingTrygger _beingTrygger;

    private Coroutine _fadingJob;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private float _recoveryRate = 0.3f;
    private bool _inTrigger = true;
    private bool _outTrigger = false;
    private float _target;

    private void Start()
    {
        RestartCoroutine();
    }

    private void StopCoroutine()
    {
        if (_fadingJob != null)
        {
            StopCoroutine(_fadingJob);
        }
    }

    private IEnumerator Fading(float target)
    {
        while (_audioSource.volume != target)
        {
            SetVolume(target);

            yield return null;
        }
    }

    private void SetVolume(float target)
    {
        float volume = Mathf.MoveTowards(_audioSource.volume, target, _recoveryRate * Time.deltaTime);
        _audioSource.volume = volume;
    }

    public void RestartCoroutine()
    {
        _target = _beingTrygger.Target;
        StopCoroutine();
        _fadingJob = StartCoroutine(Fading(_target));
    }
}