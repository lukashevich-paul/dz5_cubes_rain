using UnityEngine;

public class Detonator : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Cube>(out Cube _cube)) {
            _cube.Detonate();
        }
    }
}
