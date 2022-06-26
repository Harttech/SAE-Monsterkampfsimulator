using System.Threading.Tasks;
using Rewired;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    public bool isMenuOpen;

    public Behaviour[] menuElements;

    public TextMeshProUGUI player1Text;
    public Image player1Icon;
    public TextMeshProUGUI player1Group;

    public TextMeshProUGUI player2Text;
    public Image player2Icon;
    public TextMeshProUGUI player2Group;

    public Image playerFoulChoicePanel;

    public Image playerGroupChangeChoicePanel;

    public TextMeshProUGUI winnerText;

    public Image howToPlayPanel;

    private bool _canToggleMenu = true;

    private int _howToChildIndex;
    private bool _howToPlayOpen;

    [ReadOnly]
    public BreakFoulChoice breakFoulChoice;

    public void NewGame()
    {
        HideMenu();
        GameManager.Instance.CleanUp();
        GameManager.Instance.NewGame();
    }

    public void QuitGame() => Application.Quit();

    public void HideMenu()
    {
        menuElements?.ForEach(x => x.gameObject.SetActive(false));
        isMenuOpen = false;
    }

    public void ShowMenu()
    {
        if (isMenuOpen)
            return;

        menuElements?.ForEach(x => x.gameObject.SetActive(true));
        isMenuOpen = true;
    }

    public void ShowHowToPlay()
    {
        _howToPlayOpen = true;
        howToPlayPanel.gameObject.SetActive(true);
    }

    public void MakeBreakFoulChoice()
    {
        _canToggleMenu = false;
        isMenuOpen = true;
        playerFoulChoicePanel.gameObject.SetActive(true);
        breakFoulChoice = BreakFoulChoice.NotChosen;
    }

    public void MakeBreakFoulChoice(bool breakAgain)
    {
        isMenuOpen = false;
        _canToggleMenu = true;
        playerFoulChoicePanel.gameObject.SetActive(false);
        breakFoulChoice = breakAgain ? BreakFoulChoice.BreakAgain : BreakFoulChoice.SkipTurn;
    }

    private TaskCompletionSource<bool> _groupChangeTaskCompletionSource;
    public void MakeGroupChangeChoice(bool changeGroups)
    {
        _groupChangeTaskCompletionSource?.SetResult(changeGroups);
    }

    public void MakeGroupChangeChoice()
    {
        isMenuOpen = true;
        _canToggleMenu = false;
        playerGroupChangeChoicePanel.gameObject.SetActive(true);

        _groupChangeTaskCompletionSource = new TaskCompletionSource<bool>();
    }

    public async Task<bool> WaitForGroupChangeChoice()
    {
        await _groupChangeTaskCompletionSource.Task;

        playerGroupChangeChoicePanel.gameObject.SetActive(false);
        isMenuOpen = false;
        _canToggleMenu = true;

        return _groupChangeTaskCompletionSource.Task.Result;
    }

    private void Start()
    {
        _instance = this;
    }

    private void Update()
    {
        if (!_canToggleMenu)
            return;

        var player = ReInput.players.GetPlayer(0);

        if (player.GetButtonDown(4))
        {
            if (_howToPlayOpen)
            {
                _howToPlayOpen = false;
                howToPlayPanel.gameObject.SetActive(false);
                return;
            }

            if (isMenuOpen)
                HideMenu();
            else
                ShowMenu();
        }

        if (!_howToPlayOpen)
            return;

        if (player.GetButtonDown(5))
        {
            howToPlayPanel.transform.GetChild(_howToChildIndex).gameObject.SetActive(false);

            _howToChildIndex++;
            if (_howToChildIndex > howToPlayPanel.transform.childCount - 1)
                _howToChildIndex = 0;

            howToPlayPanel.transform.GetChild(_howToChildIndex).gameObject.SetActive(true);
        }
        else if (player.GetNegativeButtonDown(5))
        {
            howToPlayPanel.transform.GetChild(_howToChildIndex).gameObject.SetActive(false);

            _howToChildIndex--;
            if (_howToChildIndex < 0)
                _howToChildIndex = howToPlayPanel.transform.childCount - 1;

            howToPlayPanel.transform.GetChild(_howToChildIndex).gameObject.SetActive(true);
        }
    }

    public void ShowWinnerText(byte number)
    {
        winnerText.enabled = true;
        winnerText.text = $"Player {number} won!";
    }

    public void HideWinnerText()
    {
        winnerText.enabled = false;
    }
}