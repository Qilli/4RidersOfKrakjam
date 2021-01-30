using System;
using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    public bool MovesLeft = false;

    [SerializeField] Vector2 _speedMinMax = new Vector2();
    [SerializeField] float _rotationSpeed = 1.0f;

    PoliceResponder _responder;

    float _speed = 0;
    bool _canMove = true;

    private void Awake()
    {
        _speed = UnityEngine.Random.Range(_speedMinMax.x, _speedMinMax.y);
    }

    private void Update()
    {
        if (!_canMove) return;

        float movmementSpeed = MovesLeft ? _speed * Time.deltaTime : -_speed * Time.deltaTime;

        this.transform.position = new Vector3(this.transform.position.x + movmementSpeed, this.transform.position.y, 0);
        this.transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.ToLower().Contains("police"))
        {
            // Kill him

            var policeMan = collision.gameObject.GetComponent<PoliceMan>();
            policeMan.Die();
            this.transform.SetParent(collision.gameObject.transform);

            if (_canMove)
                SetImmobile();
            //Invoke(nameof(SetImmobile), 0.1f);
        }
    }

    void SetImmobile()
    {
        _canMove = false;
    }

    internal void SetResponder(PoliceResponder policeResponder)
    {
        _responder = policeResponder;
    }
}
