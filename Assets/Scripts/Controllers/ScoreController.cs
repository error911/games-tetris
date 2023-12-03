using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TMP_Text m_TextScore;
    [SerializeField] Animator m_TextScoreAnimator;
    [SerializeField] TMP_Text m_TextLevel;
    [SerializeField] Animator m_TextLevelAnimator;
    

    private int _score = 0;
    private int _level = 1;

    void Start()
    {
        m_TextScore.text = "0";
        m_TextLevel.text = "1";
    }


    public void AddScore(int value)
    {
        m_TextScoreAnimator.SetTrigger("Play");
        _score += value;
        m_TextScore.text = _score.ToString();
    }

    public void AddLevel(int value)
    {
        m_TextLevelAnimator.SetTrigger("Play");
        Map.Sounds.FX_LvlUp();
        _level += value;
        m_TextLevel.text = _level.ToString();
    }

    public void Reset()
    {
        _score = 0;
        _level = 1;
        m_TextScore.text = _score.ToString();
        m_TextLevel.text = _level.ToString();
    }


}
