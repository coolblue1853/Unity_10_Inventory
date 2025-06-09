using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    private AudioManager _audioManager;
    private RectTransform _rect;
    [SerializeField] private float duration =0.2f;
    [SerializeField] private float magnitude = 2f;
    [SerializeField] private ParticleSystem _bloodEffect;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _audioManager = GameManager.Instance.AudioManager;
    }

    public void Hit()
    {
        Shake();
        _bloodEffect.Play();
        _audioManager.PlaySFX("Hit");
    }

    private void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        Vector3 originalPos = _rect.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            _rect.anchoredPosition = (Vector2)originalPos + new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _rect.anchoredPosition = originalPos;
    }

}
