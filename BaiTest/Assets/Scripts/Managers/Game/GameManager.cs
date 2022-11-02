using Commons;
using Sirenix.OdinInspector;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace Managers.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Game Data")] public bool canPlay;
        public float playerMoveSpeed = 6f;
        public int maxCollectableAmount = 3;
        public int itemRequiredAmountToWin = 1;
        public float spawnDuration = 5f;

        [Header("Game Flow Configs")] public UnityEvent loseGame;
        public UnityEvent winGame;
        public UnityEvent replayGame;

        [Header("Game Enviroment Configs")] public BoxCollider movableZoneBox;
        public Bounds movableZone;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            InitGame();
        }

        void InitGame()
        {
            canPlay = true;
            movableZone = movableZoneBox.bounds;
            EventManager.StartListening(MyEvent.LoseGame, LoseGame);
            EventManager.StartListening(MyEvent.WinGame, WinGame);
        }
        

        void WinGame()
        {
            winGame?.Invoke();
        }

        void LoseGame()
        {
            loseGame?.Invoke();
        }

        private void OnDestroy()
        {
            EventManager.StopAll();
            EventManager.DisposeAll();
        }
    }
}