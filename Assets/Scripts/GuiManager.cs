using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Text scoreText;
    public float lifePadding = 50;
    public Transform livesParent;
    GameObject[] _lives;
    Transform _transform;

    // Use this for initialization
    void Start()
    {
        _transform = transform;
    }
    void OnScoreUpdated(int score)
    {
        scoreText.text = score.ToString("N0");
    }

    void OnLifeChanged(int livesLeft)
    {
        for (int i = 0; i < livesParent.childCount; i++)
        {
            if (i > livesLeft - 1)
            {
                livesParent.GetChild(i).GetComponent<Animator>().SetTrigger("LifeLost");
            }
        } 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnLifeChanged(0);
        }
    }
}
