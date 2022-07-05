using System.Threading.Tasks;
using Rewired;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    /// <summary>
    /// Whether or not a menu (or other interactive GUI element) is currently opened
    /// </summary>
    public bool isMenuOpen;

    /// <summary>
    /// The elements of the main menu.
    /// </summary>
    public Behaviour[] menuElements;

    /// <summary>
    /// The element for the text "Player 1"
    /// </summary>
    public TextMeshProUGUI player1Text;
    /// <summary>
    /// The icon for player 1, used to indicate that they have to skip a turn.
    /// </summary>
    public Image player1Icon;
    /// <summary>
    /// The text indicating player 1's group.
    /// </summary>
    public TextMeshProUGUI player1Group;

    /// <summary>
    /// The element for the text "Player 2"
    /// </summary>
    public TextMeshProUGUI player2Text;
    /// <summary>
    /// The icon for player 2, used to indicate that they have to skip a turn.
    /// </summary>
    public Image player2Icon;
    /// <summary>
    /// The text indicating player 2's group.
    /// </summary>
    public TextMeshProUGUI player2Group;

    /// <summary>
    /// The panel which contains the buttons for a choice in case of a break foul
    /// </summary>
    [SerializeField]
    private Image playerFoulChoicePanel;

    /// <summary>
    /// The pane which contains the buttons for a choice in case a player is allowed to switch groups.
    /// </summary>
    [SerializeField]
    private Image playerGroupChangeChoicePanel;

    /// <summary>
    /// The text indicating which player won.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI winnerText;

    /// <summary>
    /// The panel containing the controls and rules.
    /// </summary>
    [SerializeField]
    private Image howToPlayPanel;

    /// <summary>
    /// Whether or not the menu can currently be toggled with the ESC key.
    /// </summary>
    private bool _canToggleMenu = true;

    /// <summary>
    /// Index of the currently visible child of the tutorial panel.
    /// </summary>
    private int _howToChildIndex;
    /// <summary>
    /// Whether or not the tutorial panel is open.
    /// </summary>
    private bool _howToPlayOpen;

    /// <summary>
    /// A task to await the player's choice when prompted to switch groups or not.
    /// </summary>
    private TaskCompletionSource<bool> _groupChangeTaskCompletionSource;

    /// <summary>
    /// The choice which has been made by the 2nd player during a break foul.
    /// </summary>
    [ReadOnly]
    public BreakFoulChoice breakFoulChoice;

    /// <summary>
    /// New game has been clicked
    /// </summary>
    public void NewGame()
    {
        HideMenu();
        GameManager.Instance.CleanUp();
        GameManager.Instance.NewGame();
    }

    /// <summary>
    /// Quit Game has been clicked
    /// </summary>
    public void QuitGame() => Application.Quit();

    /// <summary>
    /// Hide the main menu
    /// </summary>
    private void HideMenu()
    {
        menuElements?.ForEach(x => x.gameObject.SetActive(false));
        isMenuOpen = false;
    }

    /// <summary>
    /// Show the main menu
    /// </summary>
    public void ShowMenu()
    {
        if (isMenuOpen)
            return;

        menuElements?.ForEach(x => x.gameObject.SetActive(true));
        isMenuOpen = true;
    }

    /// <summary>
    /// How to play has been clicked
    /// </summary>
    public void ShowHowToPlay()
    {
        _howToPlayOpen = true;
        howToPlayPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// Initiate the choice for the 2nd player during a break foul
    /// </summary>
    public void MakeBreakFoulChoice()
    {
        _canToggleMenu = false;
        isMenuOpen = true;
        playerFoulChoicePanel.gameObject.SetActive(true);
        breakFoulChoice = BreakFoulChoice.NotChosen;
    }

    /// <summary>
    /// A choice has been made by the 2nd player
    /// </summary>
    /// <param name="breakAgain">Whether or not the 1st player has to break again or not. If not, they have to skip a turn.</param>
    public void MakeBreakFoulChoice(bool breakAgain)
    {
        isMenuOpen = false;
        _canToggleMenu = true;
        playerFoulChoicePanel.gameObject.SetActive(false);
        breakFoulChoice = breakAgain ? BreakFoulChoice.BreakAgain : BreakFoulChoice.SkipTurn;
    }

    /// <summary>
    /// Initiate a choice for the current player whether or not they want to switch the groups.
    /// </summary>
    public void MakeGroupChangeChoice()
    {
        isMenuOpen = true;
        _canToggleMenu = false;
        playerGroupChangeChoicePanel.gameObject.SetActive(true);

        _groupChangeTaskCompletionSource = new TaskCompletionSource<bool>();
    }

    /// <summary>
    /// The player has made a choice to switch groups or not.
    /// </summary>
    /// <param name="changeGroups">Whether or not groups should be switched.</param>
    public void MakeGroupChangeChoice(bool changeGroups)
    {
        _groupChangeTaskCompletionSource?.SetResult(changeGroups);
    }

    /// <summary>
    /// Wait until the player has made a choice for whether or not groups should be switched.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> WaitForGroupChangeChoice()
    {
        await _groupChangeTaskCompletionSource.Task;

        playerGroupChangeChoicePanel.gameObject.SetActive(false);
        isMenuOpen = false;
        _canToggleMenu = true;

        return _groupChangeTaskCompletionSource.Task.Result;
    }

    // Set up Singleton
    private void Start()
    {
        _instance = this;
    }

    // Listen for key input to change menu states
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

    /// <summary>
    /// Display the text indicating the winner
    /// </summary>
    /// <param name="number">Which player has won.</param>
    public void ShowWinnerText(byte number)
    {
        winnerText.enabled = true;
        winnerText.text = $"Player {number} won!";
    }

    /// <summary>
    /// Hide the winner text.
    /// </summary>
    public void HideWinnerText()
    {
        winnerText.enabled = false;
    }
}