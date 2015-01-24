using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    List<Player> players;

    public static Action
        a_onGameStart,
        a_onGameEnd;

    public static Action<int>
        onScoreUpdated,
        onLifeChanged;
    //a_onPowerupUpdate

    void Start()
    {
        players = new List<Player>
            {
                 new Player()
                ,new Player()
                ,new Player()
                ,new Player()
            };
    }

    void SwitchPlayers(Direction d)
    {

    }

    void Awake()
    { inst = this; }
}

public class Player
{

}

public enum Direction
{
    Left,
    Right
}