using System.Resources;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private GameObject m_pauseMenuUI;
    private GameObject m_firstSelected;
    private GameObject m_currentSelectedButton;
    private InputAction m_gamePause;
    public AudioMixerSnapshot AudioUnpaused;
    public AudioMixerSnapshot AudioPaused;
    void Start()
    {
        m_gamePause = InputSystem.actions.FindAction("PauseGame");
        m_gamePause.performed += OnGamePaused;
        m_pauseMenuUI = transform.Find("PausePanel").gameObject;
        m_firstSelected = transform.Find("PausePanel").gameObject.transform.Find("Reprendre").gameObject;
    }

    private void OnGamePaused(InputAction.CallbackContext ctx)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        if (m_pauseMenuUI)
        {
            m_pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
            AudioUnpaused.TransitionTo(0f);
        }
        
    }
    public void Pause()
    {
        if (m_pauseMenuUI)
        {
            m_pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            AudioPaused.TransitionTo(0f);
            //Censé être géré par EventSystem directement mais ne marche pas, donc besoin de la ligne suivante
            EventSystem.current.SetSelectedGameObject(m_firstSelected);
            m_currentSelectedButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log(m_currentSelectedButton.name);
            m_currentSelectedButton.GetComponent<AudioSource>().Play();
        }
       
    }

    public void Update()
    {
        if(GameIsPaused && EventSystem.current.currentSelectedGameObject != m_currentSelectedButton)
        {
            m_currentSelectedButton.GetComponent<AudioSource>().Stop();
            m_currentSelectedButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log(m_currentSelectedButton.name);
            m_currentSelectedButton.GetComponent<AudioSource>().Play();
        }
    }
    public void GoToScene(string sceneName)
    {
        Resume();
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jeu quitté !");
    }
}
