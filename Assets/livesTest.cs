using UnityEngine;
using System.Collections;

public class livesTest : MonoBehaviour
{
    public GameObject[] lives;
    void Start()
    {
        GameManager.onLifeChanged += life;
    }

    void life(int i)
    {
        if (lives[i] != null)
            lives[i].SetActive(false);
    }
}
