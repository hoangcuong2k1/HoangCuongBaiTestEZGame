using System.Linq;
using Commons;
using Entities.Item;
using Managers.Game;
using TigerForge;
using UnityEngine;

namespace Managers.GUI.SubModules
{
    public class ItemContainer : MonoBehaviour
    {
        [Header("GUIs")] public ItemView itemViewPrefab;
        public Transform container;
        private ItemView[] _itemViews;

        [Header("GameObj In Scene")] public ItemTag itemTag;


        private int MaxCollectableAmount => GameManager.Instance.maxCollectableAmount;
        private int ItemRequiredAmountToWin => GameManager.Instance.itemRequiredAmountToWin;
        private int _currentActiveAmount;

        private void Awake()
        {
            EventManager.StartListening(MyEvent.CollectionNewItem, CollectNewItem);
            EventManager.StartListening(MyEvent.ReplayGame, InitItemList);
            EventManager.StartListening(MyEvent.ReachGoal, ReachGoal);
        }

        private void Start()
        {
            InitItemList();
        }

        void ReachGoal()
        {
            if (_currentActiveAmount < ItemRequiredAmountToWin) return;
            _itemViews.Where(e=>e.gameObject.activeSelf).FirstOrDefault()?.gameObject.SetActive(false);
            EventManager.EmitEvent(MyEvent.WinGame);
        }

        void InitItemList()
        {
            ClearAllItem();
            itemTag.gameObject.SetActive(true);

            _currentActiveAmount = 0;
            _itemViews = new ItemView[MaxCollectableAmount];

            for (int i = 0; i < MaxCollectableAmount; i++)
            {
                ItemView itemView = Instantiate(itemViewPrefab, container);
                _itemViews[i] = itemView;
            }
        }

        void CollectNewItem()
        {
            _currentActiveAmount++;
            if (_currentActiveAmount > MaxCollectableAmount) return;
            _itemViews[_currentActiveAmount - 1].gameObject.SetActive(true);
            itemTag.gameObject.SetActive(false);
        }

        void ClearAllItem()
        {
            if (_itemViews == null || _itemViews.Length < 1) return;
            for (int i = 0; i < _itemViews.Length; i++) _itemViews[i].gameObject.SetActive(false);
        }
    }
}