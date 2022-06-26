using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leguar.SegmentDisplay;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    [SerializeField]
    private SegmentDisplay segmentDisplay;

    private BallColor _currentColor = BallColor.None;
    private byte _currentNumber = byte.MaxValue;

    public BallColor CurrentColor => _currentColor;
    public byte CurrentNumber => _currentNumber;

    void Start()
    {
        segmentDisplay.SetText("");
        var segments = segmentDisplay.transform.GetChild(0);
        for (int i = 0; i < 7; i++)
            segments.transform.GetChild(i).gameObject.SetActive(false);
        for (int i = 9; i < segments.childCount; i++)
            segments.transform.GetChild(i).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.transform.root.GetComponent<Ball>();
        if (ball != null)
        {
            if (GameManager.Instance.gameStage == GameStage.GameLoop
                && GameManager.Instance.AreGroupsAssigned
                && _currentColor != BallColor.None
                && _currentNumber != byte.MaxValue
                && ball.BallColor != _currentColor
                && ball.Number != _currentNumber
                && ball.Number != 8)
                GameManager.Instance.FoulPlayer();

            PocketBall(ball);
        }
    }

    private void PocketBall(Ball ball)
    {
        SetColorAndNumber(ball.BallColor, ball.Number);

        if (ball.Number == 8)
        {
            GameManager.Instance.pocketedEightBall = true;

            if (GameManager.Instance.gameStage != GameStage.GameLoop)
            {
                GameManager.Instance.FoulPlayer();
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.transform.position = new Vector3(0, 1, 0);
                return;
            }
        }

        if (ball.BallColor == BallColor.White)
        {
            GameManager.Instance.pocketedCueBall = true;

            GameManager.Instance.FoulPlayer();
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.transform.position = new Vector3(0, 1, 0);
            GameManager.Instance.allowCuePlacementX = true;
            GameManager.Instance.allowCuePlacementZ = true;
            return;
        }

        GameManager.Instance.RemoveBall(ball);

        if (ball.IsFullType == (GameManager.Instance.GetCurrentPlayerBallType() == BallType.Full))
        {
            if (ball.Number == 0)
            {
                var pockets = GameManager.Instance.allPockets.ToList();

                pockets.RemoveAll(x => x.gameObject.name == gameObject.name);

                for (int i = 0; i < 2; i++)
                {
                    var randomPocket = pockets[Random.Range(0, 5)];
                    randomPocket.SetColorAndNumber(_currentColor, randomPocket._currentNumber);
                }
            }
            else if (ball.Number == 16 && (ball.BallColor == BallColor.Red || ball.BallColor == BallColor.Green))
                GameManager.Instance.AddBallToGame(2, GameManager.Instance.GetCurrentPlayerBallType() == BallType.Half);
            else if (ball.Number == 12)
                SetColorAndNumber(BallColor.White, 0);
            else if (ball.Number == 10 && (ball.BallColor == BallColor.Yellow || ball.BallColor == BallColor.Green))
                GameManager.Instance.FoulPlayer(true);
            else if (ball.Number == 7 && (ball.BallColor == BallColor.Red || ball.BallColor == BallColor.Blue))
            {
                UIManager.Instance.MakeGroupChangeChoice();
                _groupChoiceCoroutine = StartCoroutine(WaitForGroupChoice());
            }
        }
    }

    private Coroutine _groupChoiceCoroutine;
    private IEnumerator WaitForGroupChoice()
    {
        while (true)
            yield return new WaitUntil(() =>
            {
                var task = UIManager.Instance.WaitForGroupChangeChoice();
                if (task.IsCompleted)
                {
                    if (task.Result)
                        GameManager.Instance.SwitchGroups();

                    StopCoroutine(_groupChoiceCoroutine);
                    return true;
                }

                return false;
            });
    }

    private void SetColorAndNumber(BallColor color, byte number)
    {
        switch (color)
        {
            case BallColor.Red:
                segmentDisplay.OffColor = new Color(.25f, 0, 0);
                segmentDisplay.OnColor = new Color(1, 0, 0);
                break;
            case BallColor.Blue:
                segmentDisplay.OffColor = new Color(0, 0, .25f);
                segmentDisplay.OnColor = new Color(0, 0, 1);
                break;
            case BallColor.Green:
                segmentDisplay.OffColor = new Color(0, .25f, 0);
                segmentDisplay.OnColor = new Color(0, 1, 0);
                break;
            case BallColor.Yellow:
                segmentDisplay.OffColor = new Color(.25f, .25f, 0);
                segmentDisplay.OnColor = new Color(1, 1, 0);
                break;
            case BallColor.White:
                segmentDisplay.OffColor = new Color(.25f, 0, 0);
                segmentDisplay.OnColor = new Color(1, 0, 0);
                segmentDisplay.SetText("");

                _currentColor = BallColor.None;
                _currentNumber = byte.MaxValue;
                return;
        }

        segmentDisplay.SetText("0000000" + number.ToString("00"));

        _currentColor = color;
        _currentNumber = number;
    }

    public void ClearText()
    {
        segmentDisplay.OffColor = new Color(.25f, 0, 0);
        segmentDisplay.OnColor = new Color(1, 0, 0);
        segmentDisplay.SetText("");
    }

    public bool CanColorBePocketed(BallColor color)
    {
        if (_currentColor == color)
            return true;

        if (_currentColor == BallColor.White || _currentColor == BallColor.Black || _currentColor == BallColor.None)
            return true;

        return false;
    }
}
