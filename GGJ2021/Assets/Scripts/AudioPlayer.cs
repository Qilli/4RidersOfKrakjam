using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _ambientAudioSource = null;
    [SerializeField] AudioSource _uiClickAudioSource = null;
    [SerializeField] AudioSource _cameraMovementSource = null;

    public void PlayAmbientSound(AudioClip clip)
    {
        _ambientAudioSource.Stop();
        _ambientAudioSource.loop = true;
        _ambientAudioSource.clip = clip;
        _ambientAudioSource.Play();
    }

    public void PlayUIClick(AudioClip clip)
    {
        _uiClickAudioSource.Stop();
        _uiClickAudioSource.loop = false;
        _uiClickAudioSource.clip = clip;
        _uiClickAudioSource.Play();
    }

    internal void PlayTransportAudio(AudioClip announcmentsClip)
    {
        var newAudioSource = Instantiate(_uiClickAudioSource, this.transform);

        newAudioSource.loop = false;
        newAudioSource.clip = announcmentsClip;
        newAudioSource.Play();
    }

    internal void PlayCameraZoomingSound(AudioClip clip)
    {
        var newAudioSource = Instantiate(_cameraMovementSource, this.transform);

        newAudioSource.loop = false;
        newAudioSource.clip = clip;
        newAudioSource.Play();
    }

    internal void PlayCameraMovingSound(AudioClip movingClip)
    {
        if (_cameraMovementSource.isPlaying) return;

        _cameraMovementSource.clip = movingClip;
        _cameraMovementSource.Play();
    }
}
