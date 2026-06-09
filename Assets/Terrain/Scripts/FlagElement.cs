using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagElement : TerrainElement
{

    public Scene NextLevel;
   
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Source.PlayOneShot(OnContactSound);
            collision.gameObject.GetComponent<PlayerStateMachine>().Rumble(0.1f, 1f, 1f);
            if (collision.gameObject.GetComponent<PlayerStateMachine>().State is ActionPlayerState)
                FlagHit();
            
        }
    }
    void FlagHit()
    {
        Debug.Log("Hit the win condition !");
        if (NextLevel != null)
            SceneManager.LoadScene(NextLevel.name);
    }
}
