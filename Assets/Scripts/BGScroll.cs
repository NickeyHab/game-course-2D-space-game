using UnityEngine;

public class BGScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.02f;
    Renderer rend;
    private float offsetTimer = 0f;
    void Start()
    {
        rend = GetComponent<Renderer>();
        offsetTimer = 0f;
    }

    void Update()
    {
        offsetTimer += Time.deltaTime * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, offsetTimer));
    }
}
