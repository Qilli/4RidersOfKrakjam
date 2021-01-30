using UnityEngine;

public class PoliceMan : MonoBehaviour
{
    [SerializeField] Transform _startingPos = null;
    [SerializeField] Transform _target = null;

    [SerializeField] float _speed = 1.0f;

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
        this.transform.position = Vector3.MoveTowards(this.transform.position, _target.transform.position, _speed * Time.deltaTime);

        // Listen to jump 

        // Listen to crouch
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect being hit with projectile

        Debug.Log("Trigger enter: " + collision.gameObject.name);
        // Detect catching that guy
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detect being hit with projectile
        Debug.Log("Collision enter: " + collision.gameObject.name);
        // Detect catching that guy 
    }
}
