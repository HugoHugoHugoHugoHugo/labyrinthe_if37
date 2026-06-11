using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagElement : TerrainElement
{

    public string NextLevel;
   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStateMachine>().Rumble(0.1f, 1f, 1f);
            if (collision.gameObject.GetComponent<PlayerStateMachine>().State is ActionPlayerState)
            {
                Source.PlayOneShot(OnContactSound);
                Invoke(nameof(FlagHit), 1f);
            }
                
            
        }
    }
    void FlagHit()
    {
        Debug.Log("Hit the win condition !");
        if (NextLevel != null)
            SceneManager.LoadScene(NextLevel);
    }
}
