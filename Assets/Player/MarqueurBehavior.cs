using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class MarqueurBehavior : MonoBehaviour
{

    public AudioClip[] AudioMarqueurs;
    public int MarqueurNumber;
    public bool Active = true;

    private InputAction marqueurAction;
    

    private void Start()
    {
        marqueurAction = InputSystem.actions.FindAction("MarqueurAction");
    }

    public void Init(int marqueurNb)
    {
        this.MarqueurNumber = marqueurNb;
        GetComponent<AudioSource>().generator = AudioMarqueurs[marqueurNb];
    }

    void Update()
    {
        if (!Active)
        {
            GetComponent<MeshRenderer>().enabled = false;
            return;
        }

        GetComponent<MeshRenderer>().enabled = true;

        Vector2 buttonPressed = marqueurAction.ReadValue<Vector2>();
        
        if (Vec2ToMarqueur(buttonPressed) == MarqueurNumber)
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public static int Vec2ToMarqueur(Vector2 vec)
    {
        if (vec.x != 0)
        {
            return Mathf.RoundToInt((vec.x + 1f) / 2f); // 0 ou 1
        }
        else if (vec.y != 0)
        {
            return Mathf.RoundToInt((vec.y + 1f) / 2f) + 2; // 2 ou 3
        }
        else
        {
            return -1;
        }
    }
}
