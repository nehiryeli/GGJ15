using UnityEngine;
using System.Collections;

public class livesTest : MonoBehaviour
{
    void Awake()
    {
        GameManager.onLifeChanged += life;
    }

    void life(int i)
    {
        if (transform.GetChild(i) != null)
            transform.GetChild(i).gameObject.SetActive(false);
    }
}
