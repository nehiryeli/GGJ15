using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    public Text scoreText;
    public GameObject lifePrefab;
    public float lifePadding = 50;
    public Transform livesParent;
    GameObject[] _lives;
    Transform _transform;

    // Use this for initialization
    void Start()
    {
        _transform = transform;

        int _lifeCount = 3;
        GameObject[] _lives = new GameObject[_lifeCount];
        for (int i = 0; i < _lifeCount; i++)
        {
            _lives[i] = Instantiate(lifePrefab) as GameObject;
            _lives[i].transform.SetParent(livesParent, false);
            _lives[i].GetComponent<RectTransform>().localScale = _lives[i].GetComponent<RectTransform>().localScale + Vector3.one * lifePadding * i;
        }

        #region ImageBasedLives
        //int _lifeCount = 3;
        //float _firstLifePosition;
        //if (_lifeCount % 2 != 0)
        //{
        //    _firstLifePosition = -lifePadding * ((_lifeCount - 1) * 0.5f);
        //}
        //else
        //{
        //    _firstLifePosition = (-lifePadding * ((_lifeCount - 1) * 0.5f)) + (lifePrefab.GetComponent<RectTransform>().rect.size.x * 0.25f);
        //}
        //_lives = new GameObject[_lifeCount];

        //for (int i = 0; i < _lifeCount; i++)
        //{
        //    _lives[i] = Instantiate(lifePrefab) as GameObject;
        //    _lives[i].transform.SetParent(livesParent, false);
        //    Vector2 _newPosition = _lives[i].GetComponent<RectTransform>().anchoredPosition;
        //    _lives[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(_firstLifePosition + (i * lifePadding), _newPosition.y);
        //} 
        #endregion
    }

    void OnScoreUpdated(int score)
    {
        scoreText.text = score.ToString("N0");
    }

    void OnLifeChanged(int livesLeft)
    {
        #region ImageBasedLives
        //for (int i = 0; i < livesParent.childCount; i++)
        //{
        //    if (i > livesLeft - 1) livesParent.GetChild(i).gameObject.SetActive(false);
        //    else livesParent.GetChild(i).gameObject.SetActive(true);
        //} 
        #endregion
    }
}
