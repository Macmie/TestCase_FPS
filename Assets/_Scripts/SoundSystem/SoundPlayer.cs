using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundController))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSteps;
    [SerializeField] private AudioSource _backgroundMusicSource, _footstepsSource, _shootSource;

    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] List<AudioClip> _pistolShots;
    [SerializeField] List<AudioClip> _rifleShots;
    [SerializeField] List<AudioClip> _shotgunShots;
    [SerializeField] List<AudioClip> _footsteps;

    private SoundController _soundController;
    private float _timeToNextStep;
    private bool _walking, _isInAir;
    

    private void Awake()
    {
        _soundController = GetComponent<SoundController>();
        _timeToNextStep = _timeBetweenSteps / 2; //To make first sound moment after walking started
    }

    private void Start()
    {
        _backgroundMusicSource.clip = _backgroundMusic;
        _backgroundMusicSource.loop = true;
        _backgroundMusicSource.Play();
    }

    private void Update()
    {
        if (_walking && _timeToNextStep < .01f && !_isInAir)
            PlayWalkingSounds();
        _timeToNextStep = _timeToNextStep > .01f ? _timeToNextStep - Time.deltaTime : 0f;
    }

    public void StartWalking() => _walking = true;

    public void StopWalking() => _walking = false;

    public void SetInAir() => _isInAir = true;

    public void SetOnGround() => _isInAir = false;

    private void PlayWalkingSounds()
    {
        _footstepsSource.clip = GetAudioClip(_footsteps);
        _footstepsSource.Play();
        _timeToNextStep = _timeBetweenSteps;
    }

    public void PlayPistolShot()
    {
        _shootSource.clip = GetPistolShot();
        _shootSource.Play();
    }

    public void PlayRifleShot()
    {
        _shootSource.clip = GetRifleShot();
        _shootSource.Play();
    }

    public void PlayShotgunShot()
    {
        _shootSource.clip = GetShotgunShot();
        _shootSource.Play();
    }

    private AudioClip GetPistolShot() => GetAudioClip(_pistolShots);

    private AudioClip GetRifleShot() => GetAudioClip(_rifleShots);

    private AudioClip GetShotgunShot() => GetAudioClip(_shotgunShots);

    private AudioClip GetAudioClip (List<AudioClip> audioClips) => audioClips[Random.Range(0, audioClips.Count)];
}
