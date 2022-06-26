using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipController : SerializedMonoBehaviour
{
    [TitleGroup("Thrusters")]
    public Dictionary<ControlActions, ThrusterConfig[]> setups;
    
    [TitleGroup("Control")]
    public bool playerControlled; // If the player is currently controlling this ship
    [TitleGroup("Control")]
    public bool flightAssistOn; // Auto stabilization system. Not yet implemented.

    [FormerlySerializedAs("camera")]
    [TitleGroup("Camera")]
    [SerializeField]
    private Camera _camera;

    private Rigidbody _rigidbody;
    private Player _player;

    [ReadOnly, TitleGroup("Debug")]
    public Vector3 currentVelocity; // Debugging

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = ReInput.players.GetPlayer(0);

        foreach (var setup in setups)
        {
            foreach (var thrusterConfig in setup.Value)
            {
                if (thrusterConfig.thruster != null)
                    thrusterConfig.thruster.thrusterLength = 0;
            }
        }
    }

    void Update()
    {
        if (_player.GetButtonDown((int)ControlActions.SwitchShip))
        {
            //Toggle between the fighter and corvette. Won't work if there are 3 or more ships.
            playerControlled = !playerControlled;
            _camera.enabled = playerControlled;
        }

        if (!playerControlled)
            return;

        if (_player.GetButtonDown((int)ControlActions.ToggleAssisst))
        {
            // Meant for auto breaks of the ship to stabilize in space. Ran out of time, though.
            flightAssistOn = !flightAssistOn;
        }

        ProcessPlayerInput(ControlActions.MoveX);
        ProcessPlayerInput(ControlActions.MoveY);
        ProcessPlayerInput(ControlActions.MoveZ);
        ProcessPlayerInput(ControlActions.RotX);
        ProcessPlayerInput(ControlActions.RotY);
        ProcessPlayerInput(ControlActions.RotZ);
    }

    /// <summary>
    /// Checks whether the keys of the <paramref name="action"/> was pressed and steers the thrusters accordingly.
    /// </summary>
    /// <param name="action">The keys of this action will be evaluated.</param>
    private void ProcessPlayerInput(ControlActions action)
    {
        if (!setups.ContainsKey(action))
            return;

        // Calling loop twice, as to not check if the button is pressed in each iteration

        // Positive
        if (_player.GetButton((int)action))
        {
            foreach (var config in setups[action].Where(config => !config.negative))
                ProcessThrusterActivity(config, true);
        }
        else
        {
            foreach (var config in setups[action].Where(config => !config.negative))
                ProcessThrusterActivity(config, false);
        }

        // Negative
        if (_player.GetNegativeButton((int)action))
        {
            foreach (var config in setups[action].Where(config => config.negative))
                ProcessThrusterActivity(config, true);
        }
        else
        {
            foreach (var config in setups[action].Where(config => config.negative))
                ProcessThrusterActivity(config, false);
        }

        currentVelocity = _rigidbody.velocity;
    }

    /// <summary>
    /// Increases thruster output, if currentStrength has not reached maxStrength, or decreases (visual) output if the thruster is no longer active.
    /// </summary>
    /// <param name="config"></param>
    /// <param name="isActive"></param>
    private void ProcessThrusterActivity(ThrusterConfig config, bool isActive)
    {
        if (config.thruster == null)
            return;

        // Can't boost more than the thruster is capable of. Gotta buy better thrusters.
        if (isActive && config.currentStrength >= config.maxStrength)
            return;

        // Can't go into negative.
        if (!isActive && config.currentStrength <= 0)
            return;

        if (isActive) // Thruster key is pressed (either positive or negative)
        {
            var increase = config.accelerationPerSecond * Time.deltaTime;
            config.currentStrength += increase;
            
            if (config.currentStrength > config.maxStrength)
                config.currentStrength = config.maxStrength;
            
            config.thruster.thrusterLength += increase; // Same thruster can be active "multiple" times, so the output can overlap. But only increase length, if thruster is actually able to boost further.

            /*
             * Technically, force should be applied even if the thrusters reached the max output. They are still active and pushing, just not able to push "harder" than they already do.
             * But that would, in theory, allow the ship to reach an infinitely high velocity. Ships are rarely handled that way in games, however. They usually have a max speed for balance reasons.
             *
             * Therefore, force is only applied as long as the thrusters have not reached their max output.
             */
            _rigidbody.AddForceAtPosition(-config.thruster.transform.forward * config.thruster.globalScale, config.thruster.transform.position, ForceMode.Force);
        }
        else // No thruster key is pressed (neither positive nor negative)
        {
            // More visual than functional. Slowly turns off the thrusters. Does NOT slow down the ship. Only stops accelerating. Thrusters that are directed in the opposite direction are required for this.
            // Could be used for fuel, however. Since the ship has reached a velocity, the thrusters are not needed anymore and fuel can be conserved.

            var decrease = config.accelerationPerSecond * Time.deltaTime;
            config.currentStrength -= decrease;

            config.thruster.thrusterLength -= decrease; // Same thruster can be active "multiple" times, so the output can overlap.

            if (config.currentStrength < 0)
                config.currentStrength = 0;
        }


        if (config.thruster.thrusterLength < 0)
            config.thruster.thrusterLength = 0;
    }
}

/// <summary>
/// Configuration of a single thruster
/// </summary>
[Serializable]
public class ThrusterConfig
{
    [BoxGroup("Thruster Configuration")]
    [TitleGroup("Thruster Configuration/Thruster Prefab")]
    public Thruster thruster; // The physical thruster object. Location determines where the force is applied. Its globalScale attribute acts as a multiplier for the applied force.

    [TitleGroup("Thruster Configuration/Strength")]
    public float maxStrength = 5; // How much force can be applied before the thruster stops boosting further. Determines how much speed output the thruster is capable of. The higher the number, the faster the ship can travel. 
    [ReadOnly, TitleGroup("Thruster Configuration/Strength")]
    public float currentStrength; // How much force is currently applied.

    [TitleGroup("Thruster Configuration/Speed")]
    public float accelerationPerSecond = .2f; // How much force per second the thruster is able to boost. The higher this number, the faster the thruster reaches its max speed output. It does NOT determine the max speed of the ship.
    [TitleGroup("Thruster Configuration/Speed")]
    public bool negative; // Whether this thruster is used to steer the ship into the negative of an axis.
}

/// <summary>
/// Enumeration of actions set up in ReWired
/// </summary>
public enum ControlActions
{
    MoveX = 13,
    MoveY,
    MoveZ,
    RotX,
    RotY,
    RotZ,
    ToggleAssisst,
    SwitchShip
}