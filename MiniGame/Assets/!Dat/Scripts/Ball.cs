using System;
using Rewired;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    /// <summary>
    /// The ball's number.
    /// </summary>
    [SerializeField]
    private byte number;
    /// <summary>
    /// The ball's color.
    /// </summary>
    [SerializeField]
    private BallColor color;
    /// <summary>
    /// Whether the ball is a full color or a half color.
    /// </summary>
    [SerializeField]
    private bool isFullType;

    /// <summary>
    /// The ball's unique ID. 
    /// </summary>
    [ReadOnly]
    public Guid guid; // Is only needed during the break to register whether 4 or more balls have hit the rims. Somehow, it didn't work with only the class reference. Are components not passed by reference?

    private Rigidbody _rigidbody;

    /// <summary>
    /// The ball's number.
    /// </summary>
    public byte Number => number;
    /// <summary>
    /// The ball's number.
    /// </summary>
    public BallColor BallColor => color;
    /// <summary>
    /// Whether the ball is a full color or a half color.
    /// </summary>
    public bool IsFullType => isFullType;

    /// <summary>
    /// Used to draw a debug line to visualize the incoming angle during a rim collision.
    /// </summary>
    private Vector3 _startPosition;

    /// <summary>
    /// Whether or not the ball is currently dragged.
    /// </summary>
    [ReadOnly]
    public bool isDragged; // Technically works for all the balls but is only set for the cue ball.

    /// <summary>
    /// How many balls this ball is currently touching.
    /// </summary>
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
            // Don't allow dropping if the ball is currently touching other balls.
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

            if (GameManager.Instance.allowCuePlacementX && mousePoint.x > -1.15 && mousePoint.x < 1.15) // Don't allow dragging the ball over the rim.
                newVector.x = mousePoint.x;

            if (GameManager.Instance.allowCuePlacementZ && mousePoint.z > -.5 && mousePoint.z < .5) // Don't allow dragging the ball over the rim.
                newVector.z = mousePoint.z;

            transform.position = newVector;
        }

        // Sometimes the balls just slide forever at a low speed before finally coming to a stop, even with drag set. Therefore, they are stopped manually.
        if (_rigidbody.velocity.magnitude < 0.02f)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Push the ball.
    /// </summary>
    /// <param name="force">The force (and direction) to apply to the ball.</param>
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

            var reflect = Vector3.Reflect(_rigidbody.velocity, collision.contacts[0].normal); // Doesn't work. AAAAAAAAAAAH. Ball just straight up does not move in this direction.

            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.velocity = reflect;

            reflect.y = _rigidbody.position.y; // Adjusting height, so the debug line dasn't draw to world zero.

            Debug.DrawLine(collision.contacts[0].point, reflect, Color.green, 10f); // But the line does move in the right direction????
        }
        else if (collision.gameObject.TryGetComponent(out Ball ball))
        {
            /*
             * I tried so many different things here but the physics just never behave right. Vector3.Reflect doesn't work, using OB - OA as direction doesn't work either...I don't know what else I tried but nothing really worked.
             * So the balls don't behave like during actual Pool at all, sadly. And if the cue ball moves too fast, it'll just phase through the other balls.
             */
            _collisionCount++;
            
            var impulse = collision.impulse;
            impulse.y = 0;

            Debug.DrawLine(collision.contacts[0].point, transform.position + impulse, Color.yellow, 5f);
            _rigidbody.AddForceAtPosition(impulse, collision.contacts[0].point, ForceMode.Impulse);

            if (color == BallColor.White)
                GameManager.Instance.HandleCueBallHit(ball);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Ball _))
            _collisionCount--;
    }
}