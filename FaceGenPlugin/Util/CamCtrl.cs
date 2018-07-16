using System.Collections;
using UnityEngine;

namespace FaceGenPlugin.Util
{
    class CamCtrl : MonoBehaviour
    {
        public static bool windowdragflag = false;
        CamCtrl()
        {
            Illcam = Camera.main.GetComponent<IllusionCamera>();
        }

        void OnEnable()
        {
            StartCoroutine(CameraLock());
        }
        void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator CameraLock()
        {
            while (true)
            {
                yield return null;
                if (windowdragflag)
                {
                    Illcam.Lock = true;
                }
                else if (previouswindowdragflag)
                {
                    Illcam.Lock = false;
                }
                previouswindowdragflag = windowdragflag;
            }
        }
        bool previouswindowdragflag = false;
        internal IllusionCamera Illcam;
    }
}
