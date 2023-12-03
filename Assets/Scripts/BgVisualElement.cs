using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BgVisualElement : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

#endif

}
