using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class blame : MonoBehaviour
{
    public int playerID;

    public int b_index = 0;

    public Renderer[] b_render;

    public void r_blamePlayer(List<int> f)
    {
        if (f.Contains(playerID))
            if (b_index < b_render.Length)
            {
                b_render[b_index].material.color = Color.red;

                b_index++;
            }
    }

    void Awake()
    {
        GameManager.blame_func += r_blamePlayer;
    }

    void OnDestroy()
    {
        GameManager.blame_func -= r_blamePlayer;
    }
}
