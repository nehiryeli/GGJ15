using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public enemySpawner eSpawn;
    public PlanetMovement pMove;

    public float
        s_inc;

    List<Player> players;

    public static Action
        a_onGameStart,
        a_onGameEnd;

    public static Action<int>
        onScoreUpdated,
        onLifeChanged;

    public int lives = 3;
    public int score = 0;

    void Start()
    {
        players = new List<Player>
            {
			    new Player (KeyCode.A, KeyCode.Z, Pozisyon.NW,Element.Ateş),
			    new Player (KeyCode.G, KeyCode.B, Pozisyon.NE,Element.Elektrik),
			    new Player (KeyCode.UpArrow, KeyCode.DownArrow, Pozisyon.SW,Element.Su),
			    new Player (KeyCode.Keypad6, KeyCode.Keypad3, Pozisyon.SE,Element.Toprak)
            };
    }

    public void LifeDec()
    {
        if (l_active)
        {
            StartCoroutine(waitForLive());
            lives -= 1;

            if (lives < 0)
                Application.LoadLevel(0);
            else
                if (onLifeChanged != null)
                    onLifeChanged(lives);
        }
    }

    public void MeteorDestroyed()
    {
        if (l_active)
        {
            score += 5;
        }

        eSpawn.spawnTime -= s_inc;

        var rand = UnityEngine.Random.Range(0, 10);

        pMove.IsGravityInverse = rand < 2;//pMove.IsGravityReverse ? rand < 8 : rand < 2;

        Debug.Log(pMove.IsGravityInverse);
    }

    void OnGUI()
    {
        GUI.color = Color.black;

        GUILayout.Label("-> " + pMove.IsGravityInverse.ToString());
    }

    bool l_active = true;
    IEnumerator waitForLive()
    {
        l_active = false;
        yield return new WaitForSeconds(1f);
        l_active = true;
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
    {
        inst = this;
        onScoreUpdated = onLifeChanged = null;
    }
}

public enum Direction
{
    Left,
    Right
}