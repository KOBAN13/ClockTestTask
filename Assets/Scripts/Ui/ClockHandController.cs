using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class ClockHandController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private bool isDragging = false;
        private Vector3 initialPosition;
        private float initialAngle;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            initialPosition = transform.position;
            initialAngle = GetCurrentAngle();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            
            var direction = eventData.position - new Vector2(Screen.width / 2, Screen.height / 2);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
            UpdateClockTime();
        }

        private float GetCurrentAngle()
        {
            return transform.rotation.eulerAngles.z;
        }

        private void UpdateClockTime()
        {
            var angle = GetCurrentAngle();
            var minutes = (angle + 360) % 360 / 6;
            var hours = (angle + 360) % 360 / 30;

            Debug.Log($"Часы: {Mathf.Floor(hours)}, Минуты: {Mathf.Floor(minutes)}");
        }
    }
}