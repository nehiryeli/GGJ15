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

    public List<Player> players;

    public static Action
        a_onGameStart,
        a_onGameEnd,
        a_onInversTrigger;

    public static Action<int>
        onScoreUpdated,
        onLifeChanged;

    public int lives = 3;
    public int score = 0;
    public int score_for_harder_game = 40;
    void Start()
    {
        players = new List<Player>
            {
			new Player (KeyCode.Q, KeyCode.W, Pozisyon.NW,Element.Toprak),
			    new Player (KeyCode.R, KeyCode.T, Pozisyon.NE,Element.Su),
			new Player (KeyCode.Y, KeyCode.U, Pozisyon.SW,Element.Ateş),
			    new Player (KeyCode.O, KeyCode.P, Pozisyon.SE,Element.Elektrik)
            };
    }

    public static Action<List<int>> blame_func;

    public void LifeDec()
    {
        if (l_active)
        {
            if (pMove != null && l_met != null)
            {
                var bad_mv = pMove.HataYapanlarKimler(l_met.partToRemove, l_met.element);

                if (blame_func != null)
                    blame_func(bad_mv);

                /*var str = "";

                foreach (var t_str in bad_mv)
                    str += t_str.ToString() + "; ";

                Debug.Log("Part -> " + l_met.partToRemove + " | " + str);*/
            }

            StartCoroutine(waitForLive());
            lives -= 1;

            if (lives < 0)
                Application.LoadLevel(0);
            else
                if (onLifeChanged != null)
                    onLifeChanged(lives);
        }
    }

    meteor l_met;

    public void Rem_Met(meteor met)
    {
        l_met = met;
    }

    public void MeteorDestroyed(meteor met)
    {
        //l_met = met;
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
        if (score > score_for_harder_game * 2)
        {
            pMove.IsGravityInverse = rand < 2;

        }

        if (bq != pMove.IsGravityInverse)
            if (a_onInversTrigger != null)
                a_onInversTrigger();
    }

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