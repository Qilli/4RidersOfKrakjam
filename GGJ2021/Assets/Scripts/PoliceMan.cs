using System;
using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    [SerializeField] Transform _startingPos = null;
    [SerializeField] Transform _target = null;
    [SerializeField] PoliceResponder _responder = null;

    [SerializeField] float _dodgeSpeed = 1.0f;

    [SerializeField] float _speed = 1.0f;

    [SerializeField] bool _isAlive = true;

    public bool IsAlive { get => _isAlive; }

    public void Die()
    {
        _isAlive = false;
        // Play some death effects!
    }

    private void OnEnable()
    {
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
        Debug.Log("Trigger enter: " + collision.gameObject.name);

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
