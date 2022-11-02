using Commons;
using Commons.Enums;
using Managers.GUI;
using TigerForge;
using UnityEngine;

namespace Managers.Game
{
    [CreateAssetMenu(fileName = "GameFlow", menuName = "MyBehaviors/GameFlow")]
    public class GameFlow : ScriptableObject
    {
        public void LoseGame()
        {
            GameManager.Instance.canPlay = false;
            GUIManager.Instance.ShowUI(UIType.Lose);
        }

        public void WinGame()
        {
            GameManager.Instance.canPlay = false;
            GUIManager.Instance.ShowUI(UIType.Win);
        }

        public void ReplayGame()
        {
            GameManager.Instance.canPlay = true;
            EventManager.EmitEvent(MyEvent.ReplayGame);
            GUIManager.Instance.ShowUI(UIType.None);
        }
    }
}