using UnityEngine;

public class Tuto2 : TutorialManager
{

    public PlayerStateMachine Player;

    protected override void Init()
    {
        Player.CollisionEvent += OnPlayerCollision;
    }

    void Update()
    {
        if (Player.State is ExplorationPlayerState && clipIndex == 1)
        {
            PlayNextClip();
        }
    }

    void OnPlayerCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("Flag"))
        {
            if (Player.State is ExplorationPlayerState)
            {
                PlayNextClip();
            }
        }
    }
}
