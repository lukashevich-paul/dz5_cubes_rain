using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Painter))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;
    [SerializeField] private bool _isBumped;
    [SerializeField] private Painter _painter;
    [SerializeField] private Rigidbody _rigidbody;

    public event Action<Cube> ItemDumped;

    private void Start()
    {
        _isBumped = false;
        _painter = GetComponent<Painter>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out _))
            Bumped();
    }

    private void Bumped()
    {
        if (_isBumped == false)
        {
            _isBumped = true;
            _painter.SetRandomColor();

            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        float lifeTime = Random.Range(_minLifetime, _maxLifetime);
        
        yield return new WaitForSeconds(lifeTime);

        ItemDumped?.Invoke(this);
    }

    public void ResetParameters()
    {
        _isBumped = false;

        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        _painter.ResetColor();
    }
}
