using UnityEngine;

namespace Client
{
    public class FPSRateController : MonoBehaviour
    {
        private const int FPS_RATE = 60;

        private void Awake()
        {
            Application.targetFrameRate = FPS_RATE;
        }
    }
}