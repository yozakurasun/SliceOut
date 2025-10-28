using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject _fruitObject;
    [SerializeField] private GameObject _fruitSlicedObject;
    [SerializeField] private GameObject _bottom;
    [SerializeField] private GameObject _top;
    [SerializeField] private ParticleSystem _juce;
    private bool _isSliced = false;

    Rigidbody2D _rb;

    private void Update()
    {
        if (_isSliced == false)
        {
            _bottom.transform.position = transform.position;
            _top.transform.position = transform.position;
        }     
    }

    public void InitFruit(Transform firstPos)
    {
        transform.position = firstPos.position;
        _rb = GetComponent<Rigidbody2D>();
        _fruitSlicedObject.SetActive(false);
        _bottom.transform.position = Vector3.zero;
        _top.transform.position = Vector3.zero;
        _fruitObject.SetActive(true);
        _isSliced = false;
    }

    public void Shot(Transform targetPos, float angle)
    {
        Vector3 velocity = CalculateVelocity(transform.position, targetPos.position, angle);
        _rb.AddForce(velocity * _rb.mass * 1.5f, ForceMode2D.Impulse);
    }

    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;
        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));
        // 垂直方向の距離y
        float y = pointA.y - pointB.y;
        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));
        if (float.IsNaN(speed)) return Vector3.zero;
        else return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Blade")
        {
            if (!_isSliced)
            {
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                _fruitObject.SetActive(false);
                _fruitSlicedObject.SetActive(true);
                _juce.Play();
                GameController.Instance.AddScore();
                _isSliced = true;
                SoundManager.Instance.PlaySE(SoundType.SE_Slash);
            }
        }
        if(collision.tag == "Player")
        {
            ObjectPool.Instance.Release(this.gameObject);
            InitFruit(transform);
            this.gameObject.SetActive(false);
        }
    }
}
