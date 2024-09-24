using System;
using Client;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Ui
{
    public class ClockHandController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private bool _isDragging = false;
        public event Action<Transform> IsEndDrag;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!_isDragging) return;
            
            var direction = eventData.position - new Vector2(Screen.width / 2, Screen.height / 2);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
            UpdateClockTime();
        }

        private void UpdateClockTime()
        {
            IsEndDrag?.Invoke(transform);
        }
    }
}