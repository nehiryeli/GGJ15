using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
	public string[] elementsList = new string[] { "fire", "water", "lightning", "earth" };

    public Text scorePW;
    public enemySpawner eSpawn;
    public PlanetMovement pMove;

    public float
        s_inc;

    List<Player> players;

    public static Action
        a_onGameStart,
        a_onGameEnd,
        a_onInversTrigger;

    public static Action<int>
        onScoreUpdated,
        onLifeChanged;

    public int lives = 3;
    public int score = 0;

    void Start()
    {
        players = new List<Player>
            {
			new Player (KeyCode.UpArrow, KeyCode.Q, Pozisyon.NW,Element.Toprak),
			    new Player (KeyCode.RightArrow, KeyCode.W, Pozisyon.NE,Element.Su),
			new Player (KeyCode.DownArrow, KeyCode.E, Pozisyon.SE,Element.Elektrik),
			    new Player (KeyCode.LeftArrow, KeyCode.R, Pozisyon.SW,Element.Ateş)
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
            if (onScoreUpdated != null)
                onScoreUpdated(score);

            scorePW.text = score.ToString();
        }

        eSpawn.spawnTime -= s_inc;

        var rand = UnityEngine.Random.Range(0, 10);

        var bq = pMove.IsGravityInverse;
        pMove.IsGravityInverse = rand < 2;

        if (bq != pMove.IsGravityInverse)
            if (a_onInversTrigger != null)
                a_onInversTrigger();
    }

    /*void OnGUI()
    {
        GUI.color = Color.black;
        GUILayout.Label("->  " + pMove.IsGravityInverse.ToString());
    }*/

    bool l_active = true;
    IEnumerator waitForLive()
    {
        l_active = false;
        yield return new WaitForSeconds(1f);
        l_active = true;
    }

    void Awake()
    {
        inst = this;
        onScoreUpdated = onLifeChanged = null;
    }

    public static List<Player> getPlayers()
    { return inst.players; }
}

public enum Direction
{
    Left,
    Right
}