using System.Collections;
using UnityEngine;

public class SmoothSoundChange : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private EntryAndExitTrigger _entryAndExitTrigger;

    private Coroutine _fadingJob;
    private float _recoveryRate = 0.3f;
    private float _target;

    private void Start()
    {
        RestartCoroutineSignaling();
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

    public void RestartCoroutineSignaling()
    {
        _target = _entryAndExitTrigger.Target;
        StopCoroutine();
        _fadingJob = StartCoroutine(Fading(_target));
    }
}