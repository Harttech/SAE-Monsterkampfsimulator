using System;
using Rewired;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private byte number;
    [SerializeField]
    private BallColor color;
    [SerializeField]
    private bool isFullType;

    [ReadOnly]
    public Guid guid;

    private Rigidbody _rigidbody;

    public bool IsFullType => isFullType;
    public BallColor BallColor => color;
    public byte Number => number;

    private Vector3 _startPosition;

    [ReadOnly]
    public bool isDragged;

    private int _collisionCount;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        guid = Guid.NewGuid();
    }

    private void Update()
    {
        if (isDragged)
        {
            if (_collisionCount == 0 && ReInput.players.GetPlayer(0).GetButtonUp(3))
            {
                _rigidbody.isKinematic = false;
                isDragged = false;
                _rigidbody.velocity = Vector3.zero;
                GameManager.Instance.UnfreezeBalls();
                GameManager.Instance.SetStickActive(true);
                return;
            }

            var newVector = transform.position;
            var mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GameManager.Instance.allowCuePlacementX && mousePoint.x > -1.15 && mousePoint.x < 1.15)
                newVector.x = mousePoint.x;

            if (GameManager.Instance.allowCuePlacementZ && mousePoint.z > -.5 && mousePoint.z < .5)
                newVector.z = mousePoint.z;

            transform.position = newVector;
        }

        if (_rigidbody.velocity.magnitude < 0.02f)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    [Button]
    public void Push(Vector3 force)
    {
        _startPosition = transform.position;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rim"))
        {
            if (GameManager.Instance.gameStage != GameStage.GameLoop && !GameManager.Instance.touchedWalls.ContainsKey(guid))
                GameManager.Instance.touchedWalls.Add(guid, this);

            if (_startPosition == Vector3.zero)
                _startPosition = transform.position;

            Debug.Log($"{name} hit wall at {collision.contacts[0].point}");
            Debug.DrawLine(_startPosition, collision.contacts[0].point, Color.red, 10f);

            var reflect = Vector3.Reflect(_rigidbody.velocity, collision.contacts[0].normal);

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            reflect.y = _rigidbody.position.y;

            _rigidbody.velocity = reflect;

            Debug.DrawLine(collision.contacts[0].point, reflect, Color.green, 10f);
        }
        else if (collision.gameObject.TryGetComponent(out Ball ball) && _rigidbody.velocity.IsGreater(collision.rigidbody.velocity))
        {
            var impulse = collision.impulse;
            impulse.y = 0;

            Debug.DrawLine(collision.contacts[0].point, transform.position + impulse, Color.yellow, 5f);
            _rigidbody.AddForceAtPosition(impulse, collision.contacts[0].point, ForceMode.Impulse);

            if (color == BallColor.White)
                GameManager.Instance.HandleCueBallHit(ball);
        }
    }
}

public enum BallColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Black,
    White,
    None
}