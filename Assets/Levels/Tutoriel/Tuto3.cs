using UnityEngine;

public class Tuto3 : TutorialManager
{

    public PlayerStateMachine Player;


    void Update()
    {
        if (Player.State is ExplorationPlayerState && clipIndex == 1)
        {
            PlayNextClip();
        }
        else if (Player.State is ActionPlayerState && clipIndex == 2)
        {
            PlayNextClip();
            Invoke(nameof(PlayNextClip), 10f);
        }
    }
}
