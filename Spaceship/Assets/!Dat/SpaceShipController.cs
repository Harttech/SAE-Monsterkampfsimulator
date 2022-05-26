using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipController : SerializedMonoBehaviour
{
    [TitleGroup("Thrusters")]
    public Dictionary<ControlActions, ThrusterConfig[]> setups;

    [Title("Control")]
    public bool playerControlled;
    [Title("Control")]
    public bool flightAssistOn;

    private Rigidbody _rigidbody;
    private Player _player;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (!playerControlled)
            return;

        if (_player.GetButtonDown((int)ControlActions.ToggleAssisst))
        {
            flightAssistOn = !flightAssistOn;
            _rigidbody.drag = flightAssistOn ? 3f : 0f;
        }

        ProcessPlayerInput(ControlActions.MoveX);
        ProcessPlayerInput(ControlActions.MoveY);
        ProcessPlayerInput(ControlActions.MoveZ);
        ProcessPlayerInput(ControlActions.RotX);
        ProcessPlayerInput(ControlActions.RotY);
        ProcessPlayerInput(ControlActions.RotZ);
    }

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
    }

    private void ProcessThrusterActivity(ThrusterConfig config, bool isActive)
    {
        if (isActive && config.currentStrength >= config.maxStrength)
            return;

        if (!isActive && config.currentStrength <= 0)
            return;

        if (isActive)
        {
            var increase = config.accelerationPerSecond * Time.deltaTime;
            config.currentStrength += increase;

            if (config.currentStrength > config.maxStrength)
                config.currentStrength = config.maxStrength;
            
            config.thruster.thrusterLength += increase;

            _rigidbody.AddForceAtPosition(-config.thruster.transform.forward, config.thruster.transform.position, ForceMode.Force);
        }
        else
        {
            var decrease = config.accelerationPerSecond * Time.deltaTime;
            config.currentStrength -= decrease;

            config.thruster.thrusterLength -= decrease;

            if (config.currentStrength < 0)
                config.currentStrength = 0;
        }


        if (config.thruster.thrusterLength < 0)
            config.thruster.thrusterLength = 0;
    }
}

[Serializable]
public class ThrusterConfig
{
    [BoxGroup("Thruster Configuration")]
    [TitleGroup("Thruster Configuration/Thruster Prefab")]
    public Thruster thruster;

    [TitleGroup("Thruster Configuration/Strength")]
    public float maxStrength = 5;
    [ReadOnly, TitleGroup("Thruster Configuration/Strength")]
    public float currentStrength;

    [TitleGroup("Thruster Configuration/Speed")]
    public float accelerationPerSecond = .2f;
    [TitleGroup("Thruster Configuration/Speed")]
    public bool negative;
}

public enum ControlActions
{
    MoveX = 13,
    MoveY,
    MoveZ,
    RotX,
    RotY,
    RotZ,
    ToggleAssisst
}