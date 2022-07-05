using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Leguar.SegmentDisplay;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    /// <summary>
    /// The visual indicator which number and color the pocket currently has.
    /// </summary>
    [SerializeField]
    private SegmentDisplay segmentDisplay;

    /// <summary>
    /// The pocket's current color.
    /// </summary>
    private BallColor _currentColor = BallColor.None;
    /// <summary>
    /// The pocket's current number.
    /// </summary>
    private byte _currentNumber = byte.MaxValue;

    /// <summary>
    /// The pocket's current color.
    /// </summary>
    public BallColor CurrentColor => _currentColor;
    /// <summary>
    /// The pocket's current number.
    /// </summary>
    public byte CurrentNumber => _currentNumber;

    void Start()
    {
        // The segment display contains more numbers than needed. But they cannot be configured on the component. So a manual edit is necessary.

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
            // Check whether the pocketed ball results in a foul or not.

            if (GameManager.Instance.gameStage == GameStage.GameLoop
                && GameManager.Instance.AreGroupsAssigned
                && _currentColor != BallColor.None
                && _currentNumber != byte.MaxValue
                && ball.BallColor != _currentColor
                && ball.Number != _currentNumber
                && ball.Number != 8)
                GameManager.Instance.FoulPlayer();

            // More extensive handling for a pocketed ball.
            PocketBall(ball);
        }
    }

    /// <summary>
    /// Handles game logic for when a ball was pocketed
    /// </summary>
    /// <param name="ball">The ball that was pocketed</param>
    private void PocketBall(Ball ball)
    {
        SetColorAndNumber(ball.BallColor, ball.Number);

        // Checking whether the 8-ball was pocketed
        if (ball.Number == 8)
        {
            GameManager.Instance.pocketedEightBall = true;

            // During a break, pocketing the 8-ball is merely a foul.
            if (GameManager.Instance.gameStage != GameStage.GameLoop)
            {
                GameManager.Instance.FoulPlayer();
                ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ball.transform.position = new Vector3(0, 1, 0);
                return;
            }
        }

        // Checking whether the cue ball was pocketed, which results in a foul.
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

        // Remove the ball from the table.
        GameManager.Instance.RemoveBall(ball);

        if (ball.IsFullType == (GameManager.Instance.GetCurrentPlayerBallType() == BallType.Full))
        {
            // Processing the "special balls"
            if (ball.Number == 0) // Setting two additional, random pockets to the same color.
            {
                var pockets = GameManager.Instance.allPockets.ToList();

                pockets.RemoveAll(x => x.gameObject.name == gameObject.name);

                for (int i = 0; i < 2; i++)
                {
                    var randomPocket = pockets[Random.Range(0, 5)];
                    randomPocket.SetColorAndNumber(_currentColor, randomPocket._currentNumber);
                }
            }
            else if (ball.Number == 16 && (ball.BallColor == BallColor.Red || ball.BallColor == BallColor.Green)) // Adding 2 balls of the opposite player's group.
                GameManager.Instance.AddBallToGame(2, GameManager.Instance.GetCurrentPlayerBallType() == BallType.Half);
            else if (ball.Number == 12) // Reset the pocket's color and number.
                SetColorAndNumber(BallColor.White, 0);
            else if (ball.Number == 10 && (ball.BallColor == BallColor.Yellow || ball.BallColor == BallColor.Green)) // Make the opposite player skip a turn.
                GameManager.Instance.FoulPlayer(true);
            else if (ball.Number == 7 && (ball.BallColor == BallColor.Red || ball.BallColor == BallColor.Blue)) // Prompt the player to switch groups, if desired.
            {
                UIManager.Instance.MakeGroupChangeChoice();
                _groupChoiceCoroutine = StartCoroutine(WaitForGroupChoice());
            }
        }
    }

    /// <summary>
    /// The coroutine where the player's choice is awaited.
    /// </summary>
    private Coroutine _groupChoiceCoroutine;
    /// <summary>
    /// The coroutine where the player's choice is awaited.
    /// </summary>
    private IEnumerator WaitForGroupChoice()
    {
        while (true)
            yield return new WaitUntil(() =>
            {
                var task = UIManager.Instance.WaitForGroupChangeChoice();
                if (task.IsCompleted)
                {
                    if (task.Result) // The player chose to switch groups.
                        GameManager.Instance.SwitchGroups();

                    StopCoroutine(_groupChoiceCoroutine);
                    return true;
                }

                return false;
            });
    }

    /// <summary>
    /// Sets the pocket's color and number. The color white will reset the pocket.
    /// </summary>
    /// <param name="color">The new color</param>
    /// <param name="number">The new number</param>
    public void SetColorAndNumber(BallColor color, byte number)
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
                ClearText();

                _currentColor = BallColor.None;
                _currentNumber = byte.MaxValue;
                return;
        }

        // The first batches of 0s is to "skip" the manually removed children of the segment display.
        segmentDisplay.SetText("0000000" + number.ToString("00"));

        _currentColor = color;
        _currentNumber = number;
    }

    /// <summary>
    /// Reset the segment display.
    /// </summary>
    public void ClearText()
    {
        segmentDisplay.OffColor = new Color(.25f, 0, 0);
        segmentDisplay.OnColor = new Color(1, 0, 0);
        segmentDisplay.SetText("");
    }

    /// <summary>
    /// Whether or not the color <see cref="color"/> can be pocketed without resulting in a foul.
    /// </summary>
    /// <param name="color">The color to be pocketed.</param>
    /// <returns>Whether or not the color <see cref="color"/> can be pocketed without resulting in a foul.</returns>
    public bool CanColorBePocketed(BallColor color)
    {
        if (_currentColor == color)
            return true;

        if (_currentColor == BallColor.White || _currentColor == BallColor.Black || _currentColor == BallColor.None)
            return true;

        return false;
    }
}
