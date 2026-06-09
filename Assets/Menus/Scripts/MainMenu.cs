using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject FirstSelected;
    private GameObject m_currentSelectedButton;
    void Start()
    {
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