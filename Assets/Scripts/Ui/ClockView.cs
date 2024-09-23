using TMPro;
using UnityEngine;

namespace Ui
{
    public class ClockView : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI Time { get; private set; }
    }
}