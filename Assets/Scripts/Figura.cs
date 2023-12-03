using UnityEngine;

public class Figura : MonoBehaviour
{
    [SerializeField] double PAR_Id;
    [Range(-4, +4)] [SerializeField] private int rotationPointX;
    [Range(-4, +4)] [SerializeField] private int rotationPointY;
    private Vector3 rotationPoint => new Vector3(rotationPointX / 2.0f, rotationPointY / 2.0f, 0);

    private float previusTime = 0f;
    private float endTime;

    void Start()
    {
        endTime = Map.MoveSpeedNormal;
    }

    private bool _isDownKey;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
            Move(+1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _isDownKey = true;
        }

        MoveStep();
    }

    private void Move(int x, int y)
    {
        Vector3 direction = new Vector3(x, y, 0);
        transform.position += direction;

        if (!CheckValidMove())
        {
            transform.position -= direction;
            if (y < 0)
            {
                Frize();
            }
        }
        else
        {
            Map.SetMoved();
        }
    }

    private void Frize()
    {
        Map.Sounds.FX_DownEnd();
        AddToGrid();
        Map.CheckFullLine();
        this.enabled = false;
        Map.NewFigure();
    }

    private void MoveStep()
    {
        float hasPassed = Time.time - previusTime;
        float tmpEndTime = endTime;
        
        if (_isDownKey)
        {
            tmpEndTime = endTime / Map.MoveSpeedModifier;
        }
//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            tmpEndTime = endTime / Map.MoveSpeedModifier;
//        }

        if (hasPassed > tmpEndTime)
        {
            previusTime = Time.time;
            Move(0, -1);
        }
    }

    private bool CheckValidMove()
    {
        foreach (Transform item in transform)
        {
            int roundedX = Mathf.RoundToInt(item.transform.position.x);
            int roundedY = Mathf.RoundToInt(item.transform.position.y);

            if(roundedX < 0 || roundedX >= Map.Width || roundedY < 0 || roundedY >= Map.Height)
            {
                return false;
            }

            if (Map.GridGetElement(roundedX, roundedY) != null)
            {
                return false;
            }
        }
        return true;
    }

    private void AddToGrid()
    {
        foreach (Transform item in transform)
        {
            int roundedX = Mathf.RoundToInt(item.transform.position.x);
            int roundedY = Mathf.RoundToInt(item.transform.position.y);

            var cell = item.GetComponent<Cell>();
            cell.Frize();
            Map.AddToGrid(roundedX, roundedY, cell);
        }
    }


    private void Rotate()
    {
        var rotPoint = transform.TransformPoint(rotationPoint);
        transform.RotateAround(rotPoint, new Vector3(0, 0, 1), -90);
        if (!CheckValidMove())
        {
            transform.RotateAround(rotPoint, new Vector3(0, 0, 1), +90);
        }
        else
        {
            Map.Sounds.FX_Rotate();
        }
    }

    public void Release()
    {
        Destroy(this.gameObject);
    }

    



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + rotationPoint, 0.25f);
    }
#endif

}
