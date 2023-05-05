using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public float gap = 1f;
    public float delayperiod = 2f;   
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public AudioSource caughtgameoverAudio;

    public int curHealth;
    public int maxHealth = 100;
    public HealthBar healthBar;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    float m_Timejunction;
    float m_AudioTime = 2f;

    void Start()
    {
        curHealth = maxHealth;
         healthBar.SetMaxHealth(maxHealth);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }

    void Update ()
    {   m_AudioTime+=Time.deltaTime;
        if (m_IsPlayerAtExit)
        {
            EndLevel (exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (curHealth<=0){
            EndLevel (caughtBackgroundImageCanvasGroup, true, caughtgameoverAudio);
        }
        else if (m_IsPlayerCaught)
        { m_Timejunction += Time.deltaTime;
            if(m_Timejunction > gap){
            DamagePlayer(10, caughtAudio);
            m_Timejunction = 0;
            m_IsPlayerCaught = false;
            }
        }
    }

    public void DamagePlayer( int damage,AudioSource audioSource)
    {
        if (!m_HasAudioPlayed && m_AudioTime>2f)
        {
            audioSource.Play();
            m_AudioTime = 0f;
        }
        curHealth -= damage;

        healthBar.SetHealth( curHealth );
    }

    void EndLevel (CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {  
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
            
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene (0);
            }
            else
            {
                Application.Quit ();
            }
        }
    }
}