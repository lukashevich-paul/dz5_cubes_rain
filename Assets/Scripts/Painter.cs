using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Painter : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    private Renderer _objectRenderer;

    private void Awake() {
        _objectRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        ResetColor();
    }

    public void SetRandomColor()
    {
        _objectRenderer.material.color = Random.ColorHSV();
    }

    public void ResetColor()
    {
        _objectRenderer.material.color = _defaultColor;
    }
}
