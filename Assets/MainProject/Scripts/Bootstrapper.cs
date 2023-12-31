﻿using UnityEngine;


public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private JoystickController _joystickController;
    [SerializeField] private TargetSystem _targetSystem;
    [SerializeField] private ButtonAlphaChanger _buttonManager;
    [SerializeField] private Player _player;
    private void Awake()
    {
        _joystickController.Init();
        _targetSystem.Init();
        _buttonManager.Init();
        _player.Init();
    }
}

