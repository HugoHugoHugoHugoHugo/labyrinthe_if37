using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class MainMenu : MonoBehaviour
{
    static private bool m_firstAccess = true;
    public GameObject FirstSelected;
    private GameObject m_currentSelectedButton;
    private AudioSource m_audioSource;
    void Start()
    {
        StartCoroutine(Initiate());
    }
    IEnumerator Initiate()
    {
        m_audioSource = GetComponent<AudioSource>();
        if (m_firstAccess)
        {
            m_audioSource.Play();
            yield return new WaitWhile(() => m_audioSource.isPlaying);
            m_firstAccess = false;
        }
        //Censé être géré par EventSystem directement mais ne marche pas, donc besoin de la ligne suivante
        EventSystem.current.SetSelectedGameObject(FirstSelected);
        m_currentSelectedButton = EventSystem.current.currentSelectedGameObject;
        Debug.Log(m_currentSelectedButton.name);
        m_currentSelectedButton.GetComponent<AudioSource>().Play();
    }

    public void Update()
    {
        if(EventSystem.current.currentSelectedGameObject != m_currentSelectedButton)
        {
            m_currentSelectedButton.GetComponent<AudioSource>().Stop();
            m_currentSelectedButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log(m_currentSelectedButton.name);
            m_currentSelectedButton.GetComponent<AudioSource>().Play();
        }
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Jeu quitté !");
    }
}