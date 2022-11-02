using System;
using Commons.Enums;
using HoangCuongCore.Utils.Dictionary;
using UnityEngine;

namespace Managers.GUI
{
    [Serializable]
    public class UIDict : SerializableDictionary<UIType, GameObject>
    {
    }

    public class GUIManager : MonoBehaviour
    {
        public static GUIManager Instance;
        public UIDict uiDict;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        public void ShowUI(UIType uiType)
        {
            foreach (var ui in uiDict)
            {
                bool sameKey = uiType == ui.Key;
                ui.Value.gameObject.SetActive(sameKey);
            }
        }
    }
}