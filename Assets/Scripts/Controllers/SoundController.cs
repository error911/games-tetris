using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioSource m_AS_Bg;
    [SerializeField] AudioSource m_AS_FX_Rotate;
    [SerializeField] AudioSource m_AS_FX_DownEnd;
    [SerializeField] AudioSource m_AS_FX_DeleteRow;
    [SerializeField] AudioSource m_AS_FX_LvlUp;
    [SerializeField] AudioSource m_AS_FX_EndGame;


    void Start()
    {
        
    }

    public void BG_Play()
    {
        if(m_AS_Bg.isPlaying) m_AS_Bg.Stop();
        m_AS_Bg.Play();
    }

    public void BG_Stop()
    {
        m_AS_Bg.Stop();
    }

    public void FX_Rotate()
    {
        if (m_AS_FX_Rotate.isPlaying) m_AS_FX_Rotate.Stop();
        m_AS_FX_Rotate.Play();
    }
    
    public void FX_DownEnd()
    {
        if (m_AS_FX_DownEnd.isPlaying) m_AS_FX_DownEnd.Stop();
        m_AS_FX_DownEnd.Play();
    }

    public void FX_DeleteRow()
    {
        if (m_AS_FX_DeleteRow.isPlaying) m_AS_FX_DeleteRow.Stop();
        m_AS_FX_DeleteRow.Play();
    }

    public void FX_LvlUp()
    {
        if (m_AS_FX_LvlUp.isPlaying) m_AS_FX_LvlUp.Stop();
        m_AS_FX_LvlUp.Play();
    }

    public void FX_EndGame()
    {
        if (m_AS_FX_EndGame.isPlaying) m_AS_FX_EndGame.Stop();
        m_AS_FX_EndGame.Play();
    }


}
