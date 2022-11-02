using Managers.Game;
using UnityEngine;

namespace Entities.Item
{
    public class ItemTag : MonoBehaviour
    {
        private float SpawnDuration => GameManager.Instance.spawnDuration;

        private void OnDisable()
        {
            Invoke(nameof(Spawn), SpawnDuration);
        }

        void Spawn()
        {
            gameObject.SetActive(true);
        }
    }
}