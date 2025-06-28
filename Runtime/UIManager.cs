using System.Collections.Generic;
using Cronica.Core.GameUtils;
using Cronica.Core.ResourceManager;
using UnityEngine;

namespace Cronica.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public Camera UICamera;
        public Transform downLayer;
        public Transform gameLayer;
        public Transform topLayer;

        public enum Layer
        {
            Down,
            Game,
            Top
        }

        public Dictionary<UIConst.UIID, UIBase> DicUiPanels { get; set; }

        public void Awake()
        {
            transform.position = new Vector3(1000, 1000, 1000);

            DicUiPanels = new Dictionary<UIConst.UIID, UIBase>();
        }

        public void CloseAllUI()
        {
            DicUiPanels.Clear();
            downLayer.RemoveAllChild();
            gameLayer.RemoveAllChild();
            topLayer.RemoveAllChild();
        }

        /// <summary>
        /// 加载UI，只需要返回加载后的gameObject
        /// </summary>
        /// <param name="iUIIDDef"></param>
        /// <param name="UIBase"></param>
        /// <returns></returns>
        public GameObject OpenUI(UIConst.UIID iUIID, Layer layer = Layer.Game)
        {
            bool bHasUI = DicUiPanels.TryGetValue(iUIID, out UIBase ui);
            if (bHasUI)
            {
                ui.Show();
                return ui.gameObject;
            }
            else
            {
                Transform uiLayer = null;
                switch (layer)
                {
                    case Layer.Down:
                        uiLayer = downLayer;
                        break;
                    case Layer.Game:
                        uiLayer = gameLayer;
                        break;
                    case Layer.Top:
                        uiLayer = topLayer;
                        break;
                }

                GameObject goUI = Instantiate(CronicaResourceManager.LoadAsset<GameObject>($"Prefab/UI/{iUIID}.prefab"), uiLayer);
                UIBase uiBase = goUI.GetComponent<UIBase>();
                uiBase.Show();
                DicUiPanels.Add(iUIID, uiBase);
                return goUI;
            }
        }

        public bool GetUI(UIConst.UIID iUIID, out UIBase ui)
        {
            bool bHasUI = DicUiPanels.TryGetValue(iUIID, out ui);
            return bHasUI;
        }

        public void CloseUI(UIConst.UIID iUIID, bool bAim)
        {
            bool bHasUI = DicUiPanels.TryGetValue(iUIID, out UIBase ui);
            if (bHasUI && ui.m_bShow == true)
            {
                ui.Hide();
            }
        }

        public bool CheckUIOpen(UIConst.UIID iUIID)
        {
            //查看DicUIList里面有没有当前id的uiBase
            if (DicUiPanels.TryGetValue(iUIID, out UIBase uiBase))
            {
                return uiBase.m_bShow;
            }
            else
            {
                return false;
            }
        }
    }
}