using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.EventSystems;

namespace TOH.Core
 {
   
    [RequireComponent(typeof(SpringJoint2D))]
    [RequireComponent(typeof(MouseTouch))]
    public class DragDropSpring : MonoBehaviour 
    {
        [SerializeField]
        private SpringJoint2D spring;

        [SerializeField]
        private MouseTouch mTouch;

        public UnityEvent onInputDown;
        public UnityEvent onInputUp;
    
        void Awake()
        {
            spring.connectedAnchor = transform.position;
            spring.enabled = false;
        }

        void Start()
        {
            mTouch.onPointerDownEvent += HandleOnInputDownEvent;
            mTouch.onDragEvent += HandleOnInputDragEvent;
            mTouch.onPointerUpEvent += HandleOnInputUpEvent;
        }
    
        void HandleOnInputDownEvent(PointerEventData eventData)
        {
            spring.enabled = true;
            SetConnectedAnchor(Camera.main.ScreenToWorldPoint (eventData.position));
            if (onInputDown != null)
                onInputDown.Invoke();
        }
    
        void HandleOnInputDragEvent(PointerEventData eventData)        
        {
            if (spring.enabled) 
            {
                SetConnectedAnchor(Camera.main.ScreenToWorldPoint (eventData.position));
            }
        }
        
        void HandleOnInputUpEvent(PointerEventData eventData)        
        {
            spring.enabled = false;
            if (onInputUp != null)
                onInputUp.Invoke();
        }

        public void Disable()
        {
            spring.enabled = false;
        }

        #region Helpers
        private void SetConnectedAnchor(Vector2 pos)
        {
            spring.connectedAnchor = pos;
        }
        #endregion // Helpers
    }
 }