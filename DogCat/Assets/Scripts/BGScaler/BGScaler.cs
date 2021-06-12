using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        float height = spriteRenderer.bounds.size.y; // BG Height
        float width = spriteRenderer.bounds.size.x; // BG Width

        float worldHeight = Camera.main.orthographicSize * 2f; // Camera Height
        float worldWidth = worldHeight * Screen.width / Screen.height; // Camera Width

        tempScale.x = worldWidth / width;
        tempScale.y = worldHeight / height;

        transform.localScale = tempScale;
    }
}
