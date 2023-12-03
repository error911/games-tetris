using UnityEngine;

[CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Game/" + nameof(LevelConfig))]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private int _height = 20;
    [SerializeField] private int _width = 10;

    [Space(16)]
    [SerializeField] private float _MOOVE_SPEED_MODIFIER = 30.0f;
    [SerializeField] private float _MOOVE_SPEED_NORMAL = 0.9f;

    [Space(16)]
    [SerializeField] private GameObject[] _figures;


    public int height => _height;
    public int width => _width;

    public float MOOVE_SPEED_MODIFIER => _MOOVE_SPEED_MODIFIER;
    public float MOOVE_SPEED_NORMAL => _MOOVE_SPEED_NORMAL;

    public GameObject[] figures => _figures;


}
