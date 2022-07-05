/// <summary>
/// What's the current stage of the game
/// </summary>
public enum GameStage
{
    /// <summary>
    /// The game hasn't started.
    /// </summary>
    NoGame,
    /// <summary>
    /// Before the cue ball has been shot for a break
    /// </summary>
    BeforeBreak,
    /// <summary>
    /// After the cue ball has been shot for a break
    /// </summary>
    AfterBreak,
    /// <summary>
    /// After the break is done, all balls have stopped moving, the Game Loop begins.
    /// </summary>
    GameLoop
}

/// <summary>
/// A choice the 2nd player can make for an invalid break of the 1st player
/// </summary>
public enum BreakFoulChoice
{
    /// <summary>
    /// The 2nd player has yet to choose
    /// </summary>
    NotChosen,
    /// <summary>
    /// The 1st player has to break again
    /// </summary>
    BreakAgain,
    /// <summary>
    /// The 1st player has to skip their next turn
    /// </summary>
    SkipTurn
}

/// <summary>
/// The group of a ball
/// </summary>
public enum BallType
{
    /// <summary>
    /// No group set
    /// </summary>
    None,
    /// <summary>
    /// Full colors
    /// </summary>
    Full,
    /// <summary>
    /// Half colors
    /// </summary>
    Half
}

/// <summary>
/// A ball's color.
/// </summary>
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