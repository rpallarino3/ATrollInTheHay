﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ATrollInTheHay.Common;
using ATrollInTheHay.Common.Enumerations;
using ATrollInTheHay.Common.GameObjects;
using ATrollInTheHay.Common.GameObjects.Characters;
using ATrollInTheHay.Common.GameObjects.Phantom;
using ATrollInTheHay.Common.GameObjects.Weapons;
using ATrollInTheHay.ResourceManagement;
using ATrollInTheHay.Input;
using ATrollInTheHay.Graphics;

namespace ATrollInTheHay.Logic
{
    public class LogicHandler
    {
        private RoamLogicHandler _roamLogicHandler;
        private MenuLogicHandler _menuLogicHandler;

        private ResourceManager _resourceManager;
        private InputHandler _inputHandler;

        private GameStates _gameState;
        private GameStates _queuedUpState;
        private bool _paused;
        private Camera _camera;

        private PlayerCharacter _player;
        private Fader _fader;
        private Spinner _spinner;

        public LogicHandler(ResourceManager resourceManager, InputHandler inputHandler)
        {
            _resourceManager = resourceManager;
            _inputHandler = inputHandler;

            _gameState = GameStates.StartMenu;
            _queuedUpState = _gameState;

            var cameraImageList = new List<int>() { -1 };
            _camera = new Camera(RegionNames.Unknown, cameraImageList, Layer.FrontMidground, new Vector2(0, 0), 0);

            _fader = new Fader();

            var playerImageList = new List<int>() { 0 };
            _player = new PlayerCharacter(RegionNames.Unknown, playerImageList, Layer.MidMidground, new Vector2(0, 0), 1);
            _roamLogicHandler = new RoamLogicHandler(_resourceManager, _inputHandler, _camera, _player, _fader);
            _menuLogicHandler = new MenuLogicHandler(_resourceManager, _inputHandler, _fader);

            _spinner = new Spinner(_resourceManager, _fader);

            _player.AddWeaponToAvailable(new TestBluntWeapon(RegionNames.Unknown, Layer.FrontMidground, new Vector2(0, 0)), 0);
        }

        public void UpdateGameLogic()
        {
            // i guess fading holds a higher priority than spinning because you technically want to fade to spin
            if (_fader.Fading)
            {
                _fader.ContinueFade(ref _queuedUpState);
                return;
            }

            if (_spinner.Spinning)
            {
                _spinner.ContinueSpin(ref _queuedUpState);
                return;
            }

            _gameState = _queuedUpState;

            if (_fader.QueuedFadeIn)
            {
                _fader.FadeIn();
                // clear the inputs, i know this is kind of ghetto
                _inputHandler.ClearInputs();
            }

            if (_gameState == GameStates.StartMenu)
            {
                _player.Region = RegionNames.Test1;
                _camera.Region = RegionNames.Test1;
                _player.Position = new Vector2(100, 1800);
                _camera.Position = new Vector2(650, 1660);
                _player.UpdateWeaponAnchorPositions();
                _player.EquipStartingWeapon(0);

                _resourceManager.LoadAssets(new RegionLoadMessage()
                {
                    RegionsToLoad = new List<RegionNames>() { RegionNames.Test1, RegionNames.Test2 },
                    RegionsToUnload = new List<RegionNames>()
                });

                _fader.FadeOut();
                _spinner.BeginSpin(GameStates.Roam, RegionNames.Test1);
            }
            else if (_gameState == GameStates.ExitMenu)
            {
                _menuLogicHandler.UpdateExitMenuLogic(ref _queuedUpState, ref _paused);
            }
            else if (_gameState == GameStates.Menu)
            {
                _menuLogicHandler.UpdatePauseMenuLogic(ref _queuedUpState, ref _paused);
            }
            else if (_gameState == GameStates.Roam)
            {
                lock (_resourceManager.Loading.Sync)
                {
                    _roamLogicHandler.UpdateRoamLogic(ref _queuedUpState, ref _paused);

                    if (_paused)
                    {
                        _menuLogicHandler.EnterMenu();
                    }
                }
            }
        }

        public RoamLogicHandler RoamLogicHandler
        {
            get { return _roamLogicHandler; }
        }

        public GameStates GameState
        {
            get { return _gameState; }
        }

        public PlayerCharacter Player
        {
            get { return _player; }
        }

        public Camera Camera
        {
            get { return _camera; }
        }

        public Color DrawColor
        {
            get { return _fader.DrawColor; }
        }
    }
}
