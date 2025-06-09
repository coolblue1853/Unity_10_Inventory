using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Enemy : MonoBehaviour
{
    private AudioManager _audioManager;
    private RectTransform _rect;
    [SerializeField] private float duration =0.2f;
    [SerializeField] private float magnitude = 2f;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private GameObject _damageTextPrefab;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Vector3 _pivot;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _audioManager = GameManager.Instance.AudioManager;
    }

    public void Hit(BigInteger dmg, bool isCrit = false)
    {
        Shake();
        _bloodEffect.Play();
        _audioManager.PlaySFX("Hit");

        if (isCrit)
            DamageTxtSpawn(transform.position + _pivot, Utils.FormatBigInteger(dmg)+"!", Constant.Red);
        else
            DamageTxtSpawn(transform.position + _pivot, Utils.FormatBigInteger(dmg), Constant.Red);
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

    public void DamageTxtSpawn(Vector3 worldPosition, string damage, Color color)
    {

        GameObject obj = Instantiate(_damageTextPrefab, _canvas.transform);
        obj.transform.position = worldPosition;

        var damageText = obj.GetComponent<DamageText>();
        damageText.Setup(damage, color);
    }
}
