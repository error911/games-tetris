using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Map : MonoBehaviour
{
    [SerializeField] private LevelConfig levelConfig;

    [Space(16)]
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private SoundController _soundController;
    [SerializeField] private ScoreController _scoreController;
    [SerializeField] private VFXController _VFXController;

    public static Map Instance => _instance;

    private float MOOVE_SPEED_NORMAL = 0.9f;
    private int lastScore = 0;
    private const int scoreToNextLevel = 10;
    private const int scoreToRow = 5;
    private const float incraseSpeed = 0.06f;

    public static int Height => _instance.levelConfig.height;
    public static int Width => _instance.levelConfig.width;
    public static float MoveSpeedNormal => _instance.levelConfig.MOOVE_SPEED_NORMAL;
    public static float MoveSpeedModifier => _instance.levelConfig.MOOVE_SPEED_MODIFIER;

    private static Map _instance;
    private SpriteRenderer _spriteRenderer;
    private Cell[,] _grid;

    public static void NewFigure() => _instance.NewFigureInternal();
    public static void AddToGrid(int x, int y, Cell cell) => _instance.AddToGridInternal(x, y, cell);
    public static void CheckFullLine() => _instance.CheckFullLineInternal();
    public static Cell GridGetElement(int x, int y) => _instance.GridGetElementInternal(x, y);
    public static SoundController Sounds => _instance._soundController;
    public static ScoreController Score => _instance._scoreController;
    public static VFXController VFX => _instance._VFXController;

    private List<GameObject> _figuresGO = new List<GameObject>();

//    private NeuralNetwork.NN nn;
//    public NeuralNetwork.NN NN => nn;

    void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        MOOVE_SPEED_NORMAL = levelConfig.MOOVE_SPEED_NORMAL;
        _grid = new Cell[levelConfig.width, levelConfig.height];

        //CreateNN();

        NewFigureInternal();
        _soundController.BG_Play();
    }

    private void ConstructNN()
    {
        //int inputs = 200;// levelConfig.width * levelConfig.height;
        // OUTPUT: LEFT, RIGHT, ROTATE, DOWN

//        nn = new NeuralNetwork.NN(0.25, new int[7] { 200, 600, 900, 600, 200, 80, 4 });
    }


    private bool _prevFiguraMoved = true;
    private void NewFigureInternal()
    {
        if (!_prevFiguraMoved)
        {
            ResetGame();
            return;
        }
        var newFigure = levelConfig.figures[Random.Range(0, levelConfig.figures.Length)];
        var go = Instantiate(newFigure, _spawnPoint.position, _spawnPoint.rotation);
        _figuresGO.Add(go);
        _prevFiguraMoved = false;
    }

    private void ResetGame()
    {
        Map.Sounds.FX_EndGame();
        for (int x = 0; x < levelConfig.width; x++)
        {
            for (int y = 0; y < levelConfig.height; y++)
            {
                if (_grid[x, y] != null)
                    if (_grid[x, y].gameObject != null)
                    Destroy(_grid[x, y].gameObject);

            }
        }

        foreach (var item in _figuresGO)
        {
            if (item != null)
                Destroy(item);
        }
        _figuresGO = new List<GameObject>();

        _soundController.BG_Stop();
        MOOVE_SPEED_NORMAL = 0.9f;
        Score.Reset();
        _prevFiguraMoved = true;
        Start();
    }

    public static void SetMoved()
    {
        _instance._prevFiguraMoved=true;
    }

    private void AddToGridInternal(int x, int y, Cell cell)
    {
        //_prevFiguraMoved = true;
        _grid[x, y] = cell;
    }

    private Cell GridGetElementInternal(int x, int y)
    {
        return _grid[x, y];
    }

    #region === Lines ===

    private void CheckFullLineInternal()
    {
        for (int line = levelConfig.height - 1; line >= 0; line--)
        {
            if (LineHas(line))
            {
                Map.Sounds.FX_DeleteRow();
                LineDelete(line);
                RowDown(line);
            }
        }
    }


    
    private bool LineHas(int r)
    {
        for (int c = 0; c < levelConfig.width; c++)
        {
            if (_grid[c,r] == null) return false;
        }

        UpdateUI();

        return true;
    }

    private void UpdateUI()
    {
        _scoreController.AddScore(scoreToRow);
        lastScore += scoreToRow;
        if (lastScore >= scoreToNextLevel)
        {
            lastScore = 0;
            _scoreController.AddLevel(1);
            MOOVE_SPEED_NORMAL -= incraseSpeed;
        }
    }

    private void LineDelete(int r)
    {
        for (int c = 0; c < levelConfig.width; c++)
        {
            var element = _grid[c,r];
            element.Release();
            _grid[c,r] = null;
        }
    }

    private void RowDown(int r)
    {
        for (int y = r; y < levelConfig.height; y++)
        {
            for (int x = 0; x < levelConfig.width; x++)
            {
                if (_grid[x,y] != null)
                {
                    _grid[x,y-1] = _grid[x,y];
                    _grid[x, y] = null;
                    _grid[x, y - 1].MoveDelta(new Vector3(0, 1, 0));
                }
            }
        }
    }


    #endregion


#if UNITY_EDITOR

    private void OnValidate()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();

        int roundedX = Mathf.RoundToInt(levelConfig.width);
        int roundedY = Mathf.RoundToInt(levelConfig.height);

        _spriteRenderer.transform.localScale = new Vector3(roundedX, roundedY, 1);
    }

#endif

}
