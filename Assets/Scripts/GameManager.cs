﻿using System;
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

    public int lives = 3;

    void Start()
    {
        players = new List<Player>
            {
			    new Player (KeyCode.A, KeyCode.Z, Pozisyon.NW,Element.Ateş),
			    new Player (KeyCode.G, KeyCode.B, Pozisyon.NE,Element.Elektrik),
			    new Player (KeyCode.UpArrow, KeyCode.DownArrow, Pozisyon.SW,Element.Su),
			    new Player (KeyCode.Keypad6, KeyCode.Keypad3, Pozisyon.SE,Element.Toprak)
            };

        SwitchPlayers(Direction.Left);

    }

    public void LifeDec()
    {
        lives -= 1;
    }

    public static List<Player> getPlayers()
    {
        return inst.players;
    }
    public static void SwitchPlayers(Direction d)
    {
        var l_p = inst.players.Count - 1;
        var r_from = d == Direction.Left ? l_p : 0;
        var r_to = d == Direction.Left ? 0 : l_p;

        var item = inst.players[r_from];

        inst.players.RemoveAt(r_from);
        inst.players.Insert(r_to, item);
    }

    void Awake()
    { inst = this; }
}

/*public class Player
{
    public string pn;
    public Player(string nm = "p")
    { pn = nm; }
}*/

public enum Direction
{
    Left,
    Right
}