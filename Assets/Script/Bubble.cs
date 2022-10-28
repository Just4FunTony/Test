using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer), typeof(Animator))]
public class Bubble : MonoBehaviour, IDestructible
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;

    IEnumerator _waitOnDestroy, _lifeTime;

    private Vector3 _starPos;
    private Vector3 _targetPos;

    public Action<Bubble> onCliked;
    public Action<Bubble> onDestroyOvertime;

    private void Start()
    {
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _animator = this.GetComponent<Animator>();
    }

    public void InitBubble(Color color, Vector3 pos, Vector3 targetPos , float timeToDestroy)
    {
        transform.position = pos;
        _starPos = pos;
        _targetPos = targetPos;
        gameObject.SetActive(true);
        _spriteRenderer.color = color;
        DestroyOnOvertime(timeToDestroy);
    }

    public void DestroyOnClick()
    {
        StopCoroutine(_lifeTime);
        _waitOnDestroy = waitOnDestroy(); 
        StartCoroutine(_waitOnDestroy);
    }
    public void DestroyOnOvertime(float timeToDestroy)
    {
        _lifeTime = lifeTime(timeToDestroy);
        StartCoroutine(_lifeTime);
    }

    public void OnMouseDown() => DestroyOnClick();
    private IEnumerator lifeTime(float time)
    {
        for(float i = 0; i < 1; i += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(_starPos, _targetPos, i);
            yield return null;
        }
        gameObject.SetActive(false);
        onDestroyOvertime(this);
    }
    private IEnumerator waitOnDestroy()
    {
        _animator.Play("Explosion");
        for (float i = 0; i < 1; i += Time.deltaTime / 0.3f)//Animation Time 0.3f
        {
            yield return null;
        }
        gameObject.SetActive(false);
        onCliked(this);
    }
}
