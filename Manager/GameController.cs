using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController>
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private int _countMinutes;
    private float _countDownSeconds;
    public bool IsFinish => _countDownSeconds <= 0;

    [SerializeField] private GameObject _resultObject;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private TextMeshProUGUI _resultScore;
    [SerializeField] private GameObject _buttonObject;

    protected override void Awake()
    {
        _countDownSeconds = _countMinutes * 60;
        _buttonObject.SetActive(false);
    }

    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundType.BGM_InGame);
    }

    void Update()
    {
        _countDownSeconds -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int)_countDownSeconds);
        _timeText.text = span.ToString(@"mm\:ss");

        if (_countDownSeconds <= 0)
        {
            GameClear();
        }
    }

    public void AddScore()
    {
        _score++;
        _scoreText.text = $"Score:{_score}";
    }

    private void GameClear()
    {
        _scoreText.text = "";
        _timeText.text = "";
        _resultObject.SetActive(true);
        _resultText.text = "Finish!";
        _resultScore.text = $"Score:{_score}";
        _buttonObject.SetActive(true);
    }
}
