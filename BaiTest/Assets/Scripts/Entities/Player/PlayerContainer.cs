using Commons;
using Entities.Enemy;
using Entities.Item;
using Managers.Game;
using TigerForge;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerContainer : MonoBehaviour
    {
        private GameManager _gameManager => GameManager.Instance;
        private float MoveSpeed => _gameManager.playerMoveSpeed;
        private bool CanPlay => _gameManager.canPlay;
        private Bounds MovableZone => _gameManager.movableZone;

        private Vector3 _originPos;

        private void Awake()
        {
            _originPos = gameObject.transform.position;
            EventManager.StartListening(MyEvent.ReplayGame, ReSpawnPos);
        }

        void ReSpawnPos()
        {
            gameObject.transform.position = _originPos;
        }

        private void Update()
        {
            if (!CanPlay) return;

            Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (moveDir == Vector3.zero) return;

            Move(moveDir);
        }

        void Move(Vector3 moveDir)
        {
            Vector3 currentPos = gameObject.transform.position;

            transform.Translate(moveDir * Time.deltaTime * MoveSpeed);

            Vector3 newPos = gameObject.transform.position;

            if (!MovableZone.Contains(newPos)) gameObject.transform.position = currentPos;
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyHandle(other);
            ItemHandle(other);
            GoalHandle(other);
        }

        void EnemyHandle(Collider other)
        {
            bool collisionWithEnemy = other.gameObject.GetComponent<EnemyTag>();
            if (collisionWithEnemy) EventManager.EmitEvent(MyEvent.LoseGame);
        }

        void ItemHandle(Collider other)
        {
            bool collisionWithItem = other.gameObject.GetComponent<ItemTag>();
            if (collisionWithItem) EventManager.EmitEvent(MyEvent.CollectionNewItem);
        }

        void GoalHandle(Collider other)
        {
            bool collisionWithItem = other.gameObject.GetComponent<GoalTag>();
            if (collisionWithItem) EventManager.EmitEvent(MyEvent.ReachGoal);
        }
    }
}