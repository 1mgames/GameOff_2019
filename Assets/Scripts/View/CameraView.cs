using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class CameraView : MonoBehaviour
{
    private Camera _camera = null;
    private PostProcessVolume _postProcessVolume = null;

    private DepthOfField _depthOfField = null;
    private Vignette _vignette = null;
    private Bloom _bloom = null;

    private Tween _shakeTween = null;
    private Tween _colorTween = null;

    private void Awake()
    {
        _camera = this.GetComponent<Camera>();
        _postProcessVolume = this.GetComponent<PostProcessVolume>();

        if (_postProcessVolume.sharedProfile.TryGetSettings<DepthOfField>(out _depthOfField) == false)
        {
            Debug.LogWarning("DepthOfFieldの取得に失敗");
        }

        if (_postProcessVolume.sharedProfile.TryGetSettings<Vignette>(out _vignette) == false)
        {
            Debug.LogWarning("Vignetteの取得に失敗");
        }

        if (_postProcessVolume.sharedProfile.TryGetSettings<Bloom>(out _bloom) == false)
        {
            Debug.LogWarning("Bloomの取得に失敗");
        }
        
    }

    private void OnDestroy()
    {
        _depthOfField.focusDistance.value = 5;
        _vignette.roundness.value = 0.7f;
        _bloom.intensity.value = 1.0f;
    }

    public void ChangeDepthOfField(float to_value, float length)
    {
        DOVirtual.Float(_depthOfField.focusDistance.value, to_value, length, value =>
        {
            _depthOfField.focusDistance.value = value;
        });
    }

    public void ChangeVignette(float to_value, float length)
    {
        DOVirtual.Float(_vignette.roundness.value, to_value, length, value =>
        {
            _vignette.roundness.value = value;
        });
    }

    public void ChangeBloom(float to_value, float length)
    {
        DOVirtual.Float(_bloom.intensity.value, to_value, length, value =>
        {
            _bloom.intensity.value = value;
        });
    }

    public void Shake(float duration, float strength)
    {
        if(_shakeTween != null)
        {
            _shakeTween.Kill();
            _shakeTween = null;
            this.transform.position = new Vector3(0, 0, -10);
        }

        _shakeTween = this.transform.DOShakePosition(duration, strength);
    }

    public void ChangeColor(float duration, Color color)
    {
        if (_colorTween != null)
        {
            _colorTween.Kill();
            _colorTween = null;
        }

        _camera.backgroundColor = color;

        _colorTween = _camera.DOColor(Color.gray, duration);
    }
}
