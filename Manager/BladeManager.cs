using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeManager : MonoBehaviour
{
    [SerializeField] private GameObject _bladeTrail;
    Rigidbody2D _rb;
    CircleCollider2D _circleCollider;
    private bool _isSlash;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _circleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.IsFinish) return;
        if (Input.GetMouseButtonDown(0)) Slash();
        if (Input.GetMouseButtonUp(0)) StopSlath();

        if(_isSlash)_rb.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
    }

    private void Slash()
    {
        if(_isSlash) return;
        _isSlash = true;
        _circleCollider.enabled = true;
        _bladeTrail.SetActive(true);
    }

    private void StopSlath()
    {
        _circleCollider.enabled = false;
        _bladeTrail.SetActive(false);
        _isSlash = false;
    }
}
