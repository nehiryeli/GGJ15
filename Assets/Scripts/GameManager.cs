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
                 new Player("1")
                ,new Player("2")
                ,new Player("3")
                ,new Player("4")
            };

        SwitchPlayers(Direction.Left);

        foreach (var p in players)
            Debug.Log(p.pn);
    }

    public static void SwitchPlayers(Direction d)
    {
        var r_from = d == Direction.Left ? inst.players.Count - 1 : 0;
        var r_to = d == Direction.Left ? 0 : inst.players.Count - 1;

        var item = inst.players[r_from];

        inst.players.RemoveAt(r_from);
        inst.players.Insert(r_to, item);
    }

    void Awake()
    { inst = this; }
}

public class Player
{
    public string pn;
    public Player(string nm = "p")
    { pn = nm; }
}

public enum Direction
{
    Left,
    Right
}