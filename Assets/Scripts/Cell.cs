using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Cell : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        Map.VFX.CellSpawn(transform.position);
    }

    public void Release()
    {
        if (Random.Range(0, 6) == 1)
        {
            Map.VFX.CellDestroy(transform.position);
        }
        else
            Map.VFX.CellSmoke(transform.position);

        Destroy(this.gameObject);
    }

    public void Frize()
    {
        Map.VFX.CellSmoke(transform.position - new Vector3(0,1,0));
    }

    public void MoveDelta(Vector3 delta)
    {
        transform.position -= new Vector3(0, 1, 0);
    }


#if UNITY_EDITOR

    private void OnValidate()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

#endif
}
