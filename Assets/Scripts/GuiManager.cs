using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Text scoreText;
    public Transform livesParent;
    GameObject[] _lives;
    Transform _transform;

    private float _latestScore = 0;
    private float _targetScore;

    // Use this for initialization
    void Awake()
    {
        _transform = transform;
        GameManager.onScoreUpdated += OnScoreUpdated;
        GameManager.onLifeChanged += OnLifeChanged;
    }
    void OnScoreUpdated(int score)
    {
        _targetScore = score;
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
        _latestScore = Mathf.Lerp(_latestScore, _targetScore, Time.deltaTime * 2);
        scoreText.text = _latestScore.ToString("N0");
    }
}
