﻿using System;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    [SerializeField] Transform _startingPos = null;
    [SerializeField] Transform _target = null;
    [SerializeField] PoliceResponder _responder = null;

    [SerializeField] float _dodgeSpeed = 1.0f;

    [SerializeField] float _speed = 1.0f;

    [SerializeField] bool _isAlive = true;

    [SerializeField] float _animRunSpeed = 2.0f;

    [SerializeField] AudioPlayer _player = null;
    [SerializeField] AudioClip _impactClip = null;

    public bool IsAlive { get => _isAlive; }

    public void Die()
    {
        _isAlive = false;
        // Play some death effects!

        _player.PlayUIClick(_impactClip);

        var animator = GetComponentInChildren<Animator>();
        animator.SetBool("IsWalking", false);
    }

    private void OnEnable()
    {
        var animator = GetComponentInChildren<Animator>();
/*        animator.SetBool("IsWalking", true);
        animator.SetFloat("Speed", _animRunSpeed);*/

        animator.SetBool("IsRunning", true);

        this.transform.position = _startingPos.transform.position;
    }

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!_isAlive) return;

        this.transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var person = collision.gameObject.GetComponent<Person>();

        if(person)
            _responder.PersonCaughtByPolice(person);
    }

    internal void Crouch()
    {
        if(_isAlive)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + _dodgeSpeed * Time.deltaTime, 0);
    }

    internal void Jump()
    {
        if (_isAlive)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - _dodgeSpeed * Time.deltaTime, 0);
    }

    internal void SetAlive()
    {
        _isAlive = true;
    }
}
