using System;
using System.Collections;
using Actors;
using Actors.Player;
using Models;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public enum SceneName
    {
        Main, InGame
    }
    
    public enum GameLoop
    {
        None, Update, Coroutine
    }
    
    public class GameController : BaseMonoBehaviour
    {
        //- static
        private static GameController _instance;
        public static GameParameters Parameters => _instance.parameters;

        public static Camera MainCamera => _instance._playerController != null ? _instance._playerController.MainCamera : null;
        
        //- Serialized fields
        [BoxGroup("Parameters")][SerializeField][Required][Expandable] 
        private GameParameters parameters;
        
        [BoxGroup("Internal References")] [SerializeField]
        private GameLoop gameLoop;
        
        [BoxGroup("Internal References")][SerializeField][Required]
        private Transform systemsHolder;
        
        //- private
        private PlayerController _playerController;
        private SystemController _systemController;
        private bool _isRun;
        
        protected override void ReleaseReferences()
        {
            PauseGame();
            _isRun = false;
            systemsHolder = null;
            _systemController = null;
            _playerController = null;
            parameters = null;
        }

        private void Awake()
        {
            if(_instance != null) return;
            _instance = this;
            _isRun = false;
        }

        private void Start()
        {
            //- Subscribe to events
            GameSystem.OnStart += AssignPlayer;
            
            //- Entry point
            AssignSystems();
            StartGame();
        }

        private void AssignPlayer(GameSystem inPlayer, Type inType)
        {
            if(inType != typeof(PlayerController)) return;
            GameSystem.OnStart -= AssignPlayer;
            _playerController = inPlayer as PlayerController;
        }

        private void AssignSystems()
        {
            _systemController = new SystemController();
            foreach (Transform systemTr in systemsHolder)
            {
                var system = systemTr.GetComponent<GameSystem>();
                if (system == null) continue;
                system.Initialize();
                _systemController.AddSystem(system);
            }
        }

        private void Update()
        {
            if(gameLoop != GameLoop.Update || !_isRun) return;
            _systemController.Tick(Time.deltaTime);
        }
        
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone) yield return null;
            GC.Collect();
        }
        
        public static void LoadScene(SceneName sceneName)
        {
            _instance.StartCoroutine(_instance.LoadSceneAsync(sceneName.ToString()));
        }
        
        public static T GetSystem<T>() where T : GameSystem
        {
            return _instance._systemController.GetSystem<T>();
        }

        [Button("Start Game")]
        private void StartGame()
        {
            _isRun = true;
            _systemController.Start();
        }
        
        [Button("Pause Game")]
        private void PauseGame()
        {
            _isRun = false;
            _systemController.Pause();
        }
        
        [Button("Reset Game")]
        private void ResetGame()
        {
            _isRun = false;
            _systemController.Reset();
        }
        
    }
}