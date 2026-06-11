using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleElement : TerrainElement
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerStateMachine>().Rumble(1f, 0.1f, 1f);
            if (other.gameObject.GetComponent<PlayerStateMachine>().State is ActionPlayerState)
            {
                Source.PlayOneShot(OnContactSound);
                Invoke(nameof(HoleHit), 1f);
            }
        }
    }
    void HoleHit()
    {
        Debug.Log("Hit a hole");
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
