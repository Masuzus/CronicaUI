using UnityEngine;
using UnityEngine.EventSystems;
using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

namespace Cronica.UI
{
    public class BtnAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerClickHandler
    {
        [LabelText("is trigger by holding"), ToggleLeft]
        public bool needHolding = false;

        [ShowIf(nameof(needHolding))] public float holdingTime = 1.0f;

        private bool _isHolding = false;
        private float _curHolding = 0.0f;
        public Action<float> OnHoldingUpdate;
        public Action OnHoldingInterrupt;

        public Action OnBtnClick;
        public Action OnBtnDown;
        public Action OnBtnUp;
        public Action OnBtnExit;

        private Sprite _sprite;
        [SerializeField] private Image _img;
        public Image image => _img;

        private Vector3 _original_scale;
        protected bool interactable;

        protected virtual void Awake()
        {
            if (!_img)
                _img = GetComponent<Image>();
            _original_scale = transform.localScale;
            interactable = true;
        }

        void Start()
        {
            _isHolding = false;
            _curHolding = 0.0f;
        }

        void Update()
        {
            if (needHolding)
            {
                if (_isHolding)
                {
                    _curHolding += Time.deltaTime;
                    if (_curHolding >= holdingTime)
                    {
                        OnHoldingUpdate?.Invoke(Mathf.Clamp01(_curHolding / holdingTime));
                        if (needHolding)
                            InterruptHolding();
                        OnBtnClick?.Invoke();
                    }
                    else
                    {
                        OnHoldingUpdate?.Invoke(Mathf.Clamp01(_curHolding / holdingTime));
                    }
                }
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;
            //  如果非长按按钮，则直接回调Click
            if (!needHolding)
            {
                if (OnBtnClick != null)
                {
                    OnBtnClick();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!interactable)
                return;
            transform.DOScale(_original_scale * 1.1f, 0.1f);
            if (OnBtnDown != null)
            {
                OnBtnDown();
            }

            if (needHolding && !_isHolding)
            {
                _isHolding = true;
                _curHolding = 0.0f;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!interactable)
                return;
            transform.DOScale(_original_scale, 0.1f);
            if (OnBtnUp != null)
            {
                OnBtnUp();
            }

            if (needHolding)
                InterruptHolding();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!interactable)
                return;
            if (OnBtnExit != null)
            {
                OnBtnExit();
            }

            if (needHolding)
                InterruptHolding(true);
        }

        //  中断长按
        private void InterruptHolding(bool isTween = false)
        {
            _isHolding = false;
            _curHolding = 0.0f;
            //  恢复缩放
            if (isTween)
                transform.DOScale(_original_scale, 0.1f);
            else
                transform.localScale = _original_scale;
            //  中断回调
            OnHoldingInterrupt?.Invoke();
        }

        //  禁止交互
        public void Interactable(bool isInteractable)
        {
            if (null != _img)
                _img.raycastTarget = isInteractable;
            interactable = isInteractable;
        }
    }
}