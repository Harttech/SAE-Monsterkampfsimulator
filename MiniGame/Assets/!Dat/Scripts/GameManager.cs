using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LineRenderer))]
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    [SerializeField, TitleGroup("Stick Prefab")]
    private GameObject cueStick;

    #region Spawn Positions

    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition1;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition2;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition3;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition4;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition5;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition6;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition7;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition8;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition9;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition10;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition11;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition12;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition13;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition14;
    [SerializeField, TitleGroup("Object setups"), FoldoutGroup("Object setups/Spawn Positions")]
    private Vector3 spawnPosition15;

    [SerializeField, FoldoutGroup("Spawn Positions (Cue)")]
    private Vector3 cueSpawnPosition;

    #endregion

    [SerializeField, FoldoutGroup("Ball Prefabs")]
    private GameObject cueBall;
    [SerializeField, FoldoutGroup("Ball Prefabs")]
    private GameObject eightBall;

    #region Red

    #region Red Full

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Full")]
    private GameObject redFull15;

    #endregion

    #region Red Half

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Red"), FoldoutGroup("Ball Prefabs/Balls/Red/Half")]
    private GameObject redHalf15;

    #endregion

    #endregion

    #region Blue

    #region Blue Full

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Full")]
    private GameObject blueFull15;

    #endregion

    #region Blue Half

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Blue"), FoldoutGroup("Ball Prefabs/Balls/Blue/Half")]
    private GameObject blueHalf15;

    #endregion

    #endregion

    #region Yellow

    #region Yellow Full

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Full")]
    private GameObject yellowFull15;

    #endregion

    #region Yellow Half

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Yellow"), FoldoutGroup("Ball Prefabs/Balls/Yellow/Half")]
    private GameObject yellowHalf15;

    #endregion

    #endregion

    #region Green

    #region Green Full

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Full")]
    private GameObject greenFull15;

    #endregion

    #region Green Half

    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf1;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf2;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf3;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf4;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf5;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf6;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf7;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf9;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf10;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf11;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf12;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf13;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf14;
    [SerializeField]
    [FoldoutGroup("Ball Prefabs"), TabGroup("Ball Prefabs/Balls", "Green"), FoldoutGroup("Ball Prefabs/Balls/Green/Half")]
    private GameObject greenHalf15;

    #endregion

    #endregion

    #region Pockets

    [SerializeField, FoldoutGroup("Pockets")]
    private GameObject pocketTl;
    [SerializeField, FoldoutGroup("Pockets")]
    private GameObject pocketTr;
    [SerializeField, FoldoutGroup("Pockets")]
    private GameObject pocketL;
    [SerializeField, FoldoutGroup("Pockets")]
    private GameObject pocketR;
    [SerializeField, FoldoutGroup("Pockets")]
    private GameObject pocketBl;
    [SerializeField, FoldoutGroup("Pockets")]
    private GameObject pocketBr;

    #endregion

    [TitleGroup("Cue ball settings")]
    public bool allowCuePlacementX;
    [TitleGroup("Cue ball settings")]
    public bool allowCuePlacementZ;
    [TitleGroup("Cue ball settings")]
    [SerializeField]
    private float cueBallForceMultiplier;

    private readonly List<GameObject> _spawnedBalls = new();
    private Queue<GameObject> _fullReserves = new();
    private Queue<GameObject> _halfReserves = new();

    private GameObject _spawnedCueBall;
    private GameObject _spawnedEightBall;
    private GameObject _spawnedCueStick;

    private GameObject _ballPreview;

    private LineRenderer _lineRenderer;

    private byte _playerTurn;
    private bool _waitingForPlayer;

    [TitleGroup("Game settings")]
    [ReadOnly]
    public GameStage gameStage;

    private BallType _player1BallType = BallType.None;
    private BallType _player2BallType = BallType.None;

    public bool AreGroupsAssigned => _player1BallType != BallType.None;

    [TitleGroup("Game settings")]
    [ReadOnly]
    public Dictionary<Guid, Ball> touchedWalls = new();
    [TitleGroup("Game settings")]
    [ReadOnly]
    public bool pocketedEightBall;
    [ReadOnly]
    public bool pocketedCueBall;

    private readonly float _predictionMaxDistance = MathF.Sqrt(.5f);

    private bool _player1Skip;
    private bool _player2Skip;

    private float _updateTimeout;

    private Ball _firstHitBall;
    private bool _pocketedBallOfOwnGroup;

    private Camera _mainCamera;

    public Pocket[] allPockets;

    private void Start()
    {
        allPockets = new[]
        {
            pocketBl.GetComponent<Pocket>(),
            pocketBr.GetComponent<Pocket>(),
            pocketTl.GetComponent<Pocket>(),
            pocketTr.GetComponent<Pocket>(),
            pocketL.GetComponent<Pocket>(),
            pocketR.GetComponent<Pocket>(),
        };

        _instance = this;

        _lineRenderer = GetComponent<LineRenderer>();

        _mainCamera = Camera.main;
        if (_mainCamera == null)
            return;

        _mainCamera.orthographic = true;
        _mainCamera.orthographicSize = 0.8f;
    }

    [Button]
    public void NewGame()
    {
        UIManager.Instance.HideWinnerText();
        UIManager.Instance.breakFoulChoice = BreakFoulChoice.NotChosen;
        _waitingForPlayer = true;
        _playerTurn = 0;

        var fullPool = new List<GameObject>
        {
            // Red Full
            redFull1,
            redFull2,
            redFull3,
            redFull4,
            redFull5,
            redFull6,
            redFull7,
            redFull9,
            redFull10,
            redFull11,
            redFull12,
            redFull13,
            redFull14,
            redFull15,
            // Blue Full
            blueFull1,
            blueFull2,
            blueFull3,
            blueFull4,
            blueFull5,
            blueFull6,
            blueFull7,
            blueFull9,
            blueFull10,
            blueFull11,
            blueFull12,
            blueFull13,
            blueFull14,
            blueFull15,
            // Yellow Full
            yellowFull1,
            yellowFull2,
            yellowFull3,
            yellowFull4,
            yellowFull5,
            yellowFull6,
            yellowFull7,
            yellowFull9,
            yellowFull10,
            yellowFull11,
            yellowFull12,
            yellowFull13,
            yellowFull14,
            yellowFull15,
            // Green Full
            greenFull1,
            greenFull2,
            greenFull3,
            greenFull4,
            greenFull5,
            greenFull6,
            greenFull7,
            greenFull9,
            greenFull10,
            greenFull11,
            greenFull12,
            greenFull13,
            greenFull14,
            greenFull15
        };
        var halfPool = new List<GameObject>
        {
            // Red Half
            redHalf1,
            redHalf2,
            redHalf3,
            redHalf4,
            redHalf5,
            redHalf6,
            redHalf7,
            redHalf9,
            redHalf10,
            redHalf11,
            redHalf12,
            redHalf13,
            redHalf14,
            redHalf15,
            // Blue Half
            blueHalf1,
            blueHalf2,
            blueHalf3,
            blueHalf4,
            blueHalf5,
            blueHalf6,
            blueHalf7,
            blueHalf9,
            blueHalf10,
            blueHalf11,
            blueHalf12,
            blueHalf13,
            blueHalf14,
            blueHalf15,
            // Yellow Half
            yellowHalf1,
            yellowHalf2,
            yellowHalf3,
            yellowHalf4,
            yellowHalf5,
            yellowHalf6,
            yellowHalf7,
            yellowHalf9,
            yellowHalf10,
            yellowHalf11,
            yellowHalf12,
            yellowHalf13,
            yellowHalf14,
            yellowHalf15,
            // Green Half
            greenHalf1,
            greenHalf2,
            greenHalf3,
            greenHalf4,
            greenHalf5,
            greenHalf6,
            greenHalf7,
            greenHalf9,
            greenHalf10,
            greenHalf11,
            greenHalf12,
            greenHalf13,
            greenHalf14,
            greenHalf15
        };

        void Shuffle(List<GameObject> arr)
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                var r = Random.Range(i, arr.Count);
                (arr[r], arr[i]) = (arr[i], arr[r]);
            }
        }

        Shuffle(fullPool);
        Shuffle(halfPool);

        _spawnedCueBall = Instantiate(cueBall, cueSpawnPosition, new Quaternion(0, 0, 0, 0));

        GameObject GetRandomBall(bool full)
        {
            var index = Random.Range(0, fullPool.Count);
            GameObject go;
            if (full)
            {
                go = fullPool[index];
                fullPool.Remove(go);
            }
            else
            {
                go = halfPool[index];
                halfPool.Remove(go);
            }

            return go;
        }

        GameObject SpawnBall(Vector3 spawnPosition, GameObject ball)
        {
            var spawnedBall = Instantiate(ball, spawnPosition, new Quaternion(0, 0, 0, 0));
            spawnedBall.transform.Rotate(-90, 0, -90);
            _spawnedBalls.Add(spawnedBall);
            return spawnedBall;
        }

        SpawnBall(spawnPosition1, GetRandomBall(true));
        SpawnBall(spawnPosition2, GetRandomBall(false));
        SpawnBall(spawnPosition3, GetRandomBall(true));
        SpawnBall(spawnPosition4, GetRandomBall(false));
        _spawnedEightBall = SpawnBall(spawnPosition5, eightBall);
        SpawnBall(spawnPosition6, GetRandomBall(true));
        SpawnBall(spawnPosition7, GetRandomBall(false));
        SpawnBall(spawnPosition8, GetRandomBall(true));
        SpawnBall(spawnPosition9, GetRandomBall(false));
        SpawnBall(spawnPosition10, GetRandomBall(true));
        SpawnBall(spawnPosition11, GetRandomBall(false));
        SpawnBall(spawnPosition12, GetRandomBall(true));
        SpawnBall(spawnPosition13, GetRandomBall(false));
        SpawnBall(spawnPosition14, GetRandomBall(true));
        SpawnBall(spawnPosition15, GetRandomBall(false));

        Shuffle(fullPool);
        Shuffle(halfPool);

        fullPool.ForEach(x =>
        {
            var go = SpawnBall(Vector3.zero, x);
            _spawnedBalls.Remove(go);
            go.GetComponent<Rigidbody>().isKinematic = true;
            go.GetComponent<Rigidbody>().detectCollisions = false;
            _fullReserves.Enqueue(go);
        });

        halfPool.ForEach(x =>
        {
            var go = SpawnBall(Vector3.zero, x);
            _spawnedBalls.Remove(go);
            go.GetComponent<Rigidbody>().isKinematic = true;
            go.GetComponent<Rigidbody>().detectCollisions = false;
            _halfReserves.Enqueue(go);
        });

        _spawnedCueStick = Instantiate(cueStick, Vector3.zero, Quaternion.Euler(Vector3.zero));
        SetStickActive(true);
        gameStage = GameStage.BeforeBreak;
    }

    [Button]
    public void CleanUp()
    {
        UIManager.Instance.HideWinnerText();
        gameStage = GameStage.NoGame;
        SetStickActive(false);
        _waitingForPlayer = false;

        _spawnedBalls.ToList().ForEach(x => RemoveBall(x.GetComponent<Ball>(), true));

        while (_fullReserves.Count > 0)
        {
            var go = _fullReserves.Dequeue();
            Destroy(go);
        }

        while (_halfReserves.Count > 0)
        {
            var go = _halfReserves.Dequeue();
            Destroy(go);
        }

        Destroy(_spawnedCueStick);
        Destroy(_spawnedCueBall);
        Destroy(_spawnedEightBall);

        _spawnedCueStick = null;
        _spawnedCueBall = null;
        _spawnedEightBall = null;

        FindObjectsOfType<Ball>().ForEach(x => Destroy(x.gameObject));

        pocketTl.GetComponent<Pocket>().ClearText();
        pocketTr.GetComponent<Pocket>().ClearText();
        pocketL.GetComponent<Pocket>().ClearText();
        pocketR.GetComponent<Pocket>().ClearText();
        pocketBl.GetComponent<Pocket>().ClearText();
        pocketBr.GetComponent<Pocket>().ClearText();

        touchedWalls.Clear();
        _firstHitBall = null;

        _player1BallType = BallType.None;
        _player2BallType = BallType.None;
        _player1Skip = false;
        _player2Skip = false;

        UIManager.Instance.player1Icon.gameObject.SetActive(false);
        UIManager.Instance.player2Icon.gameObject.SetActive(false);
        UIManager.Instance.player1Text.color = Color.white;
        UIManager.Instance.player2Text.color = Color.white;
        UIManager.Instance.player1Group.text = "";
        UIManager.Instance.player2Group.text = "";
        _playerTurn = 0;
    }

    private void Update()
    {
        if (_updateTimeout > 0)
        {
            _updateTimeout -= Time.deltaTime;
            return;
        }

        if (UIManager.Instance.isMenuOpen)
            return;

        if (gameStage == GameStage.NoGame)
            return;

        if (_spawnedBalls.Any(x => x.GetComponent<Rigidbody>().velocity != Vector3.zero) || _spawnedCueBall.GetComponent<Rigidbody>().velocity != Vector3.zero)
            return;

        if (_waitingForPlayer)
        {
            if (_playerTurn == 0 && _player1Skip)
            {
                _player1Skip = false;
                UIManager.Instance.player1Icon.gameObject.SetActive(false);
                _playerTurn = 1;
            }
            else if (_playerTurn == 1 && _player2Skip)
            {
                _player2Skip = false;
                UIManager.Instance.player2Icon.gameObject.SetActive(false);
                _playerTurn = 0;
            }

            _firstHitBall = null;
            _pocketedBallOfOwnGroup = false;
            pocketedEightBall = false;

            UIManager.Instance.player1Text.color = _playerTurn == 0 ? new Color(0f, .75f, 0f) : Color.white;
            UIManager.Instance.player2Text.color = _playerTurn == 1 ? new Color(0f, .75f, 0f) : Color.white;

            var currentGroup = GetCurrentPlayerBallType();
            if (currentGroup != BallType.None)
            {
                var spawnedBallsOfGroup = _spawnedBalls.Select(x => x.GetComponent<Ball>()).Where(x =>
                    (x.IsFullType && currentGroup == BallType.Full) ||
                    (!x.IsFullType && currentGroup == BallType.Half)).Where(x => x.Number != 8).ToArray();

                if (spawnedBallsOfGroup.Length > 0 && spawnedBallsOfGroup.All(x => allPockets.All(y => y.CanColorBePocketed(x.BallColor))))
                    AddBallOfColorToGame(allPockets.First(x => x.CurrentColor != BallColor.White && x.CurrentColor != BallColor.Black && x.CurrentColor != BallColor.None).CurrentColor, 1, currentGroup == BallType.Full);
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Ball ball))
                {
                    if (ball.BallColor == BallColor.White)
                        SetStickActive(false);

                    if (ball.BallColor == BallColor.White && ReInput.players.GetPlayer(0).GetButton(3))
                    {
                        ball.isDragged = true;
                        FreezeBalls();
                        return;
                    }

                    if (ball.BallColor != BallColor.White)
                        SetStickActive(true);

                    if (_ballPreview != null)
                    {
                        Destroy(_ballPreview);
                        _ballPreview = null;
                    }

                    _ballPreview = Instantiate(ball.gameObject);
                    _ballPreview.GetComponent<Rigidbody>().isKinematic = true;
                    _ballPreview.GetComponent<Rigidbody>().detectCollisions = false;
                    _ballPreview.GetComponent<SphereCollider>().enabled = false;
                    _ballPreview.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;

                    _ballPreview.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    _ballPreview.transform.SetPositionAndRotation(new Vector3(-1, 1, -0.5f), Quaternion.Euler(new Vector3(-180, 0, 0)));
                }
                else if (!_spawnedCueBall.GetComponent<Ball>().isDragged)
                {
                    SetStickActive(true);
                    if (_ballPreview != null)
                    {
                        Destroy(_ballPreview);
                        _ballPreview = null;
                    }
                }
            }

            if (_spawnedCueStick.activeSelf)
            {
                var mousePoint = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var cueStickPos = new Vector3(mousePoint.x, 1f, mousePoint.z);

                _spawnedCueStick.transform.position = cueStickPos;
                _spawnedCueStick.transform.LookAt(_spawnedCueBall.transform);

                var eulers = _spawnedCueStick.transform.rotation.eulerAngles;
                eulers.x = 0;
                _spawnedCueStick.transform.rotation = Quaternion.Euler(eulers);

                var cueBallPos = _spawnedCueBall.transform.position;

                cueStickPos.y = cueBallPos.y;
                var distance = cueBallPos - cueStickPos;
                distance *= 2;

                if (distance.sqrMagnitude >= .5f)
                {
                    distance = distance.normalized * _predictionMaxDistance;

                    var newCueStickPos = cueBallPos - (distance / 2);
                    newCueStickPos.y = 1f;
                    _spawnedCueStick.transform.position = newCueStickPos;

                    newCueStickPos.y = cueBallPos.y;
                    cueStickPos = newCueStickPos;
                }

                _lineRenderer.positionCount = 1;
                _lineRenderer.SetPosition(0, cueBallPos);

                var index = 1;

                var predictionRay = new Ray(cueBallPos, distance);

                _lineRenderer.endColor = Color.white;
                _lineRenderer.startColor = Color.white;

                while (Physics.Raycast(predictionRay, out var predictionHit, distance.magnitude))
                {
                    var predictionPoint = predictionHit.point;
                    predictionPoint.y = cueBallPos.y;

                    _lineRenderer.positionCount = index + 1;
                    _lineRenderer.SetPosition(index, predictionPoint);
                    index++;

                    var remainingDistance = distance.magnitude - predictionHit.distance;

                    if (remainingDistance <= 0)
                        break;

                    distance = Vector3.Reflect(distance, predictionHit.normal) * remainingDistance;

                    var hasHitBall = predictionHit.transform.gameObject.TryGetComponent(out Ball ball);

                    if (_lineRenderer.positionCount == 2)
                    {
                        if (hasHitBall)
                        {
                            var ownGroup = _playerTurn == 0 ? _player1BallType : _player2BallType;
                            if (ball.Number != 8 && ((ball.IsFullType && ownGroup == BallType.Half) || (!ball.IsFullType && ownGroup == BallType.Full)))
                            {
                                _lineRenderer.endColor = Color.red;
                                _lineRenderer.startColor = Color.red;
                            }
                            else
                            {
                                _lineRenderer.endColor = Color.white;
                                _lineRenderer.startColor = Color.white;
                            }


                        }
                        else
                        {
                            _lineRenderer.endColor = Color.white;
                            _lineRenderer.startColor = Color.white;
                        }
                    }

                    if (hasHitBall)
                    {
                        var ballPosition = ball.transform.position;
                        var ballDistance = (ballPosition - predictionPoint);

                        _lineRenderer.positionCount = index + 1;
                        _lineRenderer.SetPosition(index, ballPosition + ballDistance);
                        index++;

                        _lineRenderer.positionCount = index + 1;
                        _lineRenderer.SetPosition(index, predictionPoint);
                        index++;
                    }

                    predictionRay = new Ray(predictionPoint, distance);
                }

                _lineRenderer.positionCount = index + 1;
                _lineRenderer.SetPosition(index, predictionRay.origin + distance);

                if (ReInput.players.GetPlayer(0).GetButton(3))
                {
                    _waitingForPlayer = false;
                    SetStickActive(false);

                    var force = ((cueBallPos - cueStickPos) / _predictionMaxDistance) * cueBallForceMultiplier;
                    _spawnedCueBall.GetComponent<Ball>().Push(force);

                    allowCuePlacementX = false;
                    allowCuePlacementZ = false;

                    if (gameStage == GameStage.BeforeBreak)
                        gameStage = GameStage.AfterBreak;

                    // Sometimes the velocity isn't immediately updated on the next frame. Need this timeout to prevent the break choice buttons to appear too soon.
                    _updateTimeout = 1f;
                }
            }
        }
        else if (gameStage == GameStage.AfterBreak)
        {
            if (touchedWalls.Count < 4 || pocketedEightBall)
            {
                var breakChoice = UIManager.Instance.breakFoulChoice;
                if (breakChoice == BreakFoulChoice.NotChosen)
                    UIManager.Instance.MakeBreakFoulChoice();
                else if (breakChoice == BreakFoulChoice.BreakAgain)
                {
                    // Didn't have time to reset the balls that were already spawned...so new game it is.
                    CleanUp();
                    NewGame();
                }
                else if (breakChoice == BreakFoulChoice.SkipTurn)
                {
                    FoulPlayer();
                    gameStage = GameStage.GameLoop;
                    _waitingForPlayer = true;
                    _playerTurn = _playerTurn == 0 ? (byte)1 : (byte)0;
                    SetStickActive(true);
                }
            }
            else
                gameStage = GameStage.GameLoop;
        }
        else if (gameStage == GameStage.GameLoop)
        {
            if (pocketedEightBall)
            {
                var currentGroup = GetCurrentPlayerBallType();
                var ballsLeftOver = _spawnedBalls.Any(x => x.GetComponent<Ball>().IsFullType == (currentGroup == BallType.Full));

                CleanUp();

                if (ballsLeftOver || pocketedCueBall)
                    UIManager.Instance.ShowWinnerText((byte)(_playerTurn == 0 ? 2 : 1));
                else
                    UIManager.Instance.ShowWinnerText((byte)(_playerTurn + 1));

                UIManager.Instance.ShowMenu();
                return;
            }

            _waitingForPlayer = true;
            SetStickActive(true);

            var hadFoul = (_playerTurn == 0 && _player1Skip) || (_playerTurn == 1 && _player2Skip);

            if (_firstHitBall == null)
                FoulPlayer();
            else if (_pocketedBallOfOwnGroup && !hadFoul)
                return;

            _playerTurn = _playerTurn == 0 ? (byte)1 : (byte)0;
        }
    }

    public void RemoveBall(Ball ball, bool destroy = false)
    {
        var go = ball.gameObject;
        if (destroy)
        {
            _spawnedBalls.Remove(go);

            if (ball.IsFullType)
                _fullReserves = new Queue<GameObject>(_fullReserves.Where(x => x.gameObject != go));
            else
                _halfReserves = new Queue<GameObject>(_fullReserves.Where(x => x.gameObject != go));

            Destroy(go);
        }
        else
        {
            if (ball.Number != 8)
            {
                if (ball.IsFullType && !_fullReserves.Contains(go))
                    _fullReserves.Enqueue(go);
                else if (!_halfReserves.Contains(go))
                    _halfReserves.Enqueue(go);

                _spawnedBalls.Remove(go);
            }

            go.transform.position = Vector3.zero;
            var rb = go.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;

            if ((gameStage == GameStage.AfterBreak && !pocketedEightBall) || gameStage == GameStage.GameLoop)
            {
                if (ball.Number == 8)
                    return;

                if (_player1BallType == BallType.None)
                {
                    if (_playerTurn == 0)
                    {
                        _player1BallType = ball.IsFullType ? BallType.Full : BallType.Half;
                        _player2BallType = ball.IsFullType ? BallType.Half : BallType.Full;
                    }
                    else
                    {
                        _player1BallType = ball.IsFullType ? BallType.Half : BallType.Full;
                        _player2BallType = ball.IsFullType ? BallType.Full : BallType.Half;
                    }

                    UIManager.Instance.player1Group.text = $"({_player1BallType})";
                    UIManager.Instance.player2Group.text = $"({_player2BallType})";

                    _pocketedBallOfOwnGroup = true;
                }
                else
                {
                    if (ball.IsFullType)
                    {
                        if ((_playerTurn == 0 && _player1BallType == BallType.Half) || (_playerTurn == 1 && _player2BallType == BallType.Half))
                            FoulPlayer();
                        else
                            _pocketedBallOfOwnGroup = true;
                    }
                    else if (!ball.IsFullType)
                    {
                        if ((_playerTurn == 0 && _player1BallType == BallType.Full) || (_playerTurn == 1 && _player2BallType == BallType.Full))
                            FoulPlayer();
                        else
                            _pocketedBallOfOwnGroup = true;
                    }
                }
            }
        }
    }

    [Button]
    public void AddBallToGame(byte amount, bool fromFullReserve)
    {
        if (fromFullReserve)
        {
            if (_fullReserves.Count < amount)
                amount = (byte)_fullReserves.Count;
        }
        else
        {
            if (_halfReserves.Count < amount)
                amount = (byte)_halfReserves.Count;
        }

        for (int i = 0; i < amount; i++)
        {
            var ballGo = fromFullReserve ? _fullReserves.Dequeue() : _halfReserves.Dequeue();
            _spawnedBalls.Add(ballGo);
            ballGo.transform.position = new Vector3(Random.Range(-1.15f, 1.15f), 1, Random.Range(-.5f, .5f));

            var rb = ballGo.GetComponent<Rigidbody>();
            rb.detectCollisions = true;
            rb.isKinematic = false;
        }
    }

    public void AddBallOfColorToGame(BallColor color, byte amount, bool fromFullReserve)
    {
        if (fromFullReserve)
        {
            if (_fullReserves.Count < amount)
                amount = (byte)_fullReserves.Count;
        }
        else
        {
            if (_halfReserves.Count < amount)
                amount = (byte)_halfReserves.Count;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject ballGo;

            var counter = fromFullReserve ? _fullReserves.Count : _halfReserves.Count;
            while (true)
            {
                ballGo = fromFullReserve ? _fullReserves.Dequeue() : _halfReserves.Dequeue();
                if (ballGo.GetComponent<Ball>().BallColor == color)
                    break;

                if (fromFullReserve)
                    _fullReserves.Enqueue(ballGo);
                else
                    _halfReserves.Enqueue(ballGo);
                
                counter--;
                if (counter == 0)
                    return;
            }

            _spawnedBalls.Add(ballGo);
            ballGo.transform.position = new Vector3(Random.Range(-1.15f, 1.15f), 1, Random.Range(-.5f, .5f));

            var rb = ballGo.GetComponent<Rigidbody>();
            rb.detectCollisions = true;
            rb.isKinematic = false;
        }
    }

    public void SetStickActive(bool active)
    {
        if (_spawnedCueStick != null)
            _spawnedCueStick.SetActive(active);
        _lineRenderer.enabled = active;
    }

    public void FoulPlayer(bool foulNext = false)
    {
        ShakeCamera();

        if ((_playerTurn == 0 && !foulNext) || (_playerTurn == 1 && foulNext))
        {
            UIManager.Instance.player1Icon.gameObject.SetActive(true);
            _player1Skip = true;
        }
        else if ((_playerTurn == 1 && !foulNext) || (_playerTurn == 0 && foulNext))
        {
            UIManager.Instance.player2Icon.gameObject.SetActive(true);
            _player2Skip = true;
        }

        if (!foulNext)
        {
            allowCuePlacementX = true;
            allowCuePlacementZ = true;
        }
    }

    public void FreezeBalls()
    {
        _spawnedBalls.ForEach(x => SetRigidBodyKinematic(x, true));
        SetRigidBodyKinematic(_spawnedCueBall, true);
    }
    public void UnfreezeBalls()
    {
        _spawnedBalls.ForEach(x => SetRigidBodyKinematic(x, false));
        SetRigidBodyKinematic(_spawnedCueBall, false);
    }
    private void SetRigidBodyKinematic(GameObject go, bool frozen)
    {
        var rb = go.GetComponent<Rigidbody>();
        rb.isKinematic = frozen;
        rb.velocity = Vector3.zero;
    }

    public void ShakeCamera() => StartCoroutine(ShakeCameraCoroutine());
    private IEnumerator ShakeCameraCoroutine()
    {
        for (int i = 0; i < 10; i++)
        {
            Camera.main.transform.position = new Vector3(Random.Range(-.009f, 0.001f), 2.2f, Random.Range(-.009f, 0.001f));
            yield return new WaitForSeconds(0.01f);
        }

        Camera.main.transform.position = new Vector3(0, 2.2f, 0);
    }

    public void HandleCueBallHit(Ball otherBall)
    {
        if (_firstHitBall != null)
            return;

        _firstHitBall = otherBall;

        if (otherBall.BallColor == BallColor.Black)
            return;

        if (otherBall.IsFullType)
        {
            if ((_playerTurn == 0 && _player1BallType == BallType.Half) || (_playerTurn == 1 && _player2BallType == BallType.Half))
                FoulPlayer();
        }
        else
        {
            if ((_playerTurn == 0 && _player1BallType == BallType.Full) || (_playerTurn == 1 && _player2BallType == BallType.Full))
                FoulPlayer();
        }
    }

    public BallType GetCurrentPlayerBallType() => _playerTurn == 0 ? _player1BallType : _player2BallType;

    public void SwitchGroups()
    {
        (_player1BallType, _player2BallType) = (_player2BallType, _player1BallType);
        UIManager.Instance.player1Group.text = $"({_player1BallType})";
        UIManager.Instance.player2Group.text = $"({_player2BallType})";
    }
}
