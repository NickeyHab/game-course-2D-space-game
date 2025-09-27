using UnityEngine;

public class BGScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.02f;
    Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
