using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArena : MonoBehaviour
{
    RectTransform rectTransform;
    Rect safeArena;
    Vector2 minAnchor;
    Vector2 maxAnchor;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArena = Screen.safeArea;
        minAnchor = safeArena.position;
        maxAnchor = minAnchor + safeArena.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }
}
