using System;
using System.Linq;
using UnityEngine;

public class Tuto1 : TutorialManager
{

    public WallElement[] Walls = new WallElement[4];

    public PlayerStateMachine Player;

    private bool[] wallTouched = { false, false, false, false };

    protected override void Init()
    {
        Player.CollisionEvent += OnPlayerCollision;
    }

    private void OnPlayerCollision(Collision collision)
    {
        if (clipIndex != 1) return;

        foreach (WallElement wallElem in Walls)
        {
            if (wallElem.gameObject.Equals(collision.gameObject))
            {
                int wallIndex = Array.IndexOf(Walls, wallElem);
                Debug.Log("Wall" + wallIndex + "touched !");
                wallTouched[wallIndex] = true;
            }
        }

        if (! wallTouched.Contains(false))
        {
            PlayNextClip(3f);
            Invoke(nameof(destroyWall), 6f);
        }
    }

    private void destroyWall()
    {
        Destroy(Walls[0].gameObject);
    }

    void Update()
    {
        
    }
}
