using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeformAndShineOnHit : MonoBehaviour
{
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private float _maxScaleChange = 0.1f;
    [SerializeField] private Color _colorShine = Color.white;
    [SerializeField] private MeshRenderer _meshRenderer;    
    private Vector3 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
    }

    public void OnHit(float ratio)
    {
        Vector3 newScale = new Vector3(_originalScale.x + _maxScaleChange * ratio, _originalScale.y - _maxScaleChange * ratio, _originalScale.z + _maxScaleChange * ratio);
        transform.DOScale(newScale,_duration).SetEase(Ease.InOutQuad).OnComplete(() => transform.DOScale(_originalScale, _duration).SetEase(Ease.InOutQuad));


        Material mat = _meshRenderer.materials[0];
        Color returnColor = mat.GetColor("_BaseColor");
        mat.DOColor(_colorShine, "_BaseColor", _duration).OnComplete(() => mat.DOColor(returnColor, "_BaseColor", _duration));
    }
}
