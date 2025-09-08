using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(Painter))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;
    [SerializeField] private bool _isDetonate;
    [SerializeField] private Painter _painter;

    public event Action<Cube> DestroyItem;

    private void Start()
    {
        _isDetonate = false;
        _painter = GetComponent<Painter>();
    }

    private void RunDestroyEvent()
    {
        if (DestroyItem != null)
        {
            _isDetonate = false;
            _painter.ResetColor();

            DestroyItem(this);
        }
    }

    public void Detonate()
    {
        if (_isDetonate == false)
        {
            _isDetonate = true;
            _painter.SetRandomColor();

            this.Invoke(nameof(RunDestroyEvent), Random.Range(_minLifetime, _maxLifetime));
        }
    }
}
