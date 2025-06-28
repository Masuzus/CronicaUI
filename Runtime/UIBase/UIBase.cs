/*
 * Time: 2022.3.23
 * Author:Masuzus
 */

using System;
using UnityEngine;

namespace Cronica.UI
{
    public class UIBase : MonoBehaviour
    {
        [HideInInspector] public GameObject m_goGameObject = null; //

        [HideInInspector] public GameObject m_goMask = null; //Mask遮罩
        [HideInInspector] public GameObject m_goBase = null; //Base界面

        [HideInInspector] public GameObject m_goLeft = null; //左
        [HideInInspector] public GameObject m_goUp = null; //上
        [HideInInspector] public GameObject m_goRight = null; //右
        [HideInInspector] public GameObject m_goDown = null; //下
        [HideInInspector] public GameObject m_goMid = null; //中间

        [HideInInspector] public Canvas m_canvas = null; //Canvas
        [HideInInspector] public RectTransform m_rectTransform = null; //rectTransform

        [HideInInspector] public Animation m_animUI = null;

        [HideInInspector] public bool m_bShow;

        private Action m_hideAction;

        public virtual void Awake()
        {
            Transform tfMask = transform.Find("Mask");
            Transform tfBase = transform.Find("Base");

            Transform tfLeft = tfBase.Find("Left");
            Transform tfUp = tfBase.Find("Up");
            Transform tfRight = tfBase.Find("Right");
            Transform tfDown = tfBase.Find("Down");
            Transform tfMid = tfBase.Find("Mid");


            GameObject goMask = null;
            GameObject goBase = null;

            GameObject goLeft = null;
            GameObject goUp = null;
            GameObject goRight = null;
            GameObject goDown = null;
            GameObject goMid = null;

            if (tfMask != null)
            {
                goMask = tfMask.gameObject;
            }

            if (tfBase != null)
            {
                goBase = tfBase.gameObject;
            }

            if (tfLeft != null)
            {
                goLeft = tfLeft.gameObject;
            }

            if (tfUp != null)
            {
                goUp = tfUp.gameObject;
            }

            if (tfRight != null)
            {
                goRight = tfRight.gameObject;
            }

            if (tfDown != null)
            {
                goDown = tfDown.gameObject;
            }

            if (tfMid != null)
            {
                goMid = tfMid.gameObject;
            }

            m_rectTransform = transform.GetComponent<RectTransform>();

            SetHierarchy(goMask, goBase, goLeft, goUp, goRight, goDown, goMid);

            m_animUI = transform.GetComponent<Animation>();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            m_bShow = true;
        }

        public virtual void Hide()
        {
            m_bShow = false;
            if (m_hideAction != null)
            {
                m_hideAction();
                m_hideAction = null;
            }

            gameObject.SetActive(false);
        }

        public void SetHideAction(Action hideAction)
        {
            m_hideAction = hideAction;
        }

        private void SetHierarchy(GameObject goMask, GameObject goBase, GameObject goLeft, GameObject goUp, GameObject goRight, GameObject goDown,
            GameObject goMid)
        {
            this.m_goMask = goMask;
            this.m_goBase = goBase;
            this.m_goLeft = goLeft;
            this.m_goUp = goUp;
            this.m_goRight = goRight;
            this.m_goDown = goDown;
            this.m_goMid = goMid;
        }

        /// <summary>
        /// UI销毁
        /// </summary>
        public void Destroy()
        {
            Destroy(this);
        }

        /// <summary>
        /// UI是否开启
        /// </summary>
        /// <returns></returns>
        public bool IsActiveSelf()
        {
            return gameObject.activeSelf;
        }

        public bool IsActiveInHierarchy()
        {
            return gameObject.activeInHierarchy;
        }
    }
}