using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Painter : MonoBehaviour
{
    private void Start()
    {
        ResetColor();
    }

    public void SetRandomColor()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Random.ColorHSV();
    }

    public void ResetColor()
    {
        Renderer objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = Color.white;
    }
}
