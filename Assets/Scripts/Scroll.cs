using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Renderer bRenderer;

    // Update is called once per frame
    void Update()
    {
        bRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
