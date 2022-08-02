using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLerper : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    public Color startColor;
    public Color endColor;
    private Renderer cubeRenderer;
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float t = Mathf.Sin(Time.time *  speed) + 1;
        t /= 2;
        cubeRenderer.material.color = Color.Lerp(startColor, endColor, t);
    }
}
