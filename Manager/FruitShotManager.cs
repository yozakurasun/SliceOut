using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitShotManager : MonoBehaviour
{
    [SerializeField] private float _minShotSpan;
    [SerializeField] private float _maxShotSpan;
    private float _currentShotSpan = 0;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private float _minTargetAngle;
    [SerializeField] private float _maxTargetAngle;
    [SerializeField] private Transform _fruitParent;

    void Start()
    {
        _currentShotSpan = Random.Range(_minShotSpan, _maxShotSpan);
    }

    void Update()
    {
        if(GameController.Instance.IsFinish) return;
        _currentShotSpan -= Time.deltaTime;
        if (_currentShotSpan <= 0)
        {
            ShotFruit();
            _currentShotSpan = Random.Range(_minShotSpan, _maxShotSpan);
        }
    }

    private void ShotFruit()
    {
        Fruit fruit = ObjectPool.Instance.Get<Fruit>(_fruitParent);
        fruit.InitFruit(transform);
        fruit.Shot(_targetPos, Random.Range(_minTargetAngle, _maxTargetAngle));
    }
}
