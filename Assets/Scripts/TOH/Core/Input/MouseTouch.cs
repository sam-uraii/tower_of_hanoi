using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TOH.Core
{
	
	public class MouseTouch : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public delegate void OnPointerEventDataHandler(PointerEventData pEventData);
		public OnPointerEventDataHandler onBeginDragEvent;
		public OnPointerEventDataHandler onDragEvent;
		public OnPointerEventDataHandler onEndDragEvent;
		public OnPointerEventDataHandler onPointerClickEvent;
		public OnPointerEventDataHandler onPointerDownEvent;
		public OnPointerEventDataHandler onPointerEnterEvent;
		public OnPointerEventDataHandler onPointerExitEvent;
		public OnPointerEventDataHandler onPointerUpEvent;
		
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (onBeginDragEvent != null)
				onBeginDragEvent(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (onDragEvent != null)
				onDragEvent(eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			if (onEndDragEvent != null)
				onEndDragEvent(eventData);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (onPointerClickEvent != null)
				onPointerClickEvent(eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (onPointerDownEvent != null)
				onPointerDownEvent(eventData);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (onPointerEnterEvent != null)
				onPointerEnterEvent(eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (onPointerExitEvent != null)
				onPointerExitEvent(eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (onPointerUpEvent != null)
				onPointerUpEvent(eventData);
		}
	}
}
