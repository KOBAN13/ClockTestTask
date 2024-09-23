using TMPro;
using UnityEngine;

namespace Ui
{
    public class ClockView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI Time { get; private set; }
        [field: SerializeField] public RectTransform Hours { get; private set; }
        [field: SerializeField] public RectTransform Minutes { get; private set; }
        [field: SerializeField] public RectTransform Seconds { get; private set; }
    }
}