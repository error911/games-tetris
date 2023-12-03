using UnityEngine;

public class VFXController : MonoBehaviour
{
    [SerializeField] GameObject _vfxCellDestroy;
    [SerializeField] GameObject _vfxCellSmoke;
    [SerializeField] GameObject _vfxCellSpawn;
    [SerializeField] GameObject _vfxFirewerk;

    
    public void CellDestroy(Vector3 position)
    {
        Instantiate(_vfxCellDestroy, position, Quaternion.identity);
    }

    public void CellSmoke(Vector3 position)
    {
        Instantiate(_vfxCellSmoke, position, Quaternion.identity);
    }

    public void CellSpawn(Vector3 position)
    {
        var go = Instantiate(_vfxCellSpawn, position - new Vector3(0,1.0f,0), Quaternion.identity);
        go.transform.SetParent(this.transform);

    }

    public void Firewerk(Vector3 position)
    {
        Instantiate(_vfxFirewerk, position, Quaternion.identity);
    }


}
