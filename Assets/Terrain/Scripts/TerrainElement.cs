using UnityEngine;

public class TerrainElement : MonoBehaviour
{
    public TerrainElementCaracteristics Caracteristics;

    //Played when element is touched by player
    public AudioClip OnContactSound;
    //Played continuously when touched or not by player
    public AudioClip ContinuousSound;

    private AudioSource m_source;

    void Start()
    {
        m_source = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Player")
        {
            switch (Caracteristics.Type)
            {
                case TerrainElementCaracteristics.ElementType.MUR :
                    m_source.PlayOneShot(OnContactSound);
                    m_source.Play();
                    Debug.Log("Hit a wall");
                    break;
                case TerrainElementCaracteristics.ElementType.DRAPEAU :
                    m_source.PlayOneShot(OnContactSound);
                    Debug.Log("Hit the win condition !");
                    break;
                default :
                    break;
            }
            //If the GameObject has the same tag as specified, output this message in the console
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Caracteristics.Type == TerrainElementCaracteristics.ElementType.TROU)
            {
                Debug.Log("Hit a hole");
            }
            
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Caracteristics.Type == TerrainElementCaracteristics.ElementType.MUR)
            {
                m_source.Stop();
                Debug.Log("Left a wall");
            }
            
        }
    }
}
