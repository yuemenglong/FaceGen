using FaceGenPlugin.Util;
using FkAssistPlugin.Util;
using IllusionPlugin;
using UnityEngine;

namespace FaceGenPlugin 
{
    public class FaceGenPlugin : IEnhancedPlugin
    {
        public void OnApplicationStart()
        {
            Tracer.Log("OnApplicationStart");
        }

        public void OnApplicationQuit()
        {
            Tracer.Log("");
        }

        public void OnLevelWasLoaded(int level)
        {
            Tracer.Log("OnLevelWasLoaded, " + level);
        }

        public void OnLevelWasInitialized(int level)
        {
            Tracer.Log("OnLevelWasInitialized, " + level);
            if (level != 6)
                return;
            BaseMgr<FaceGen>.Install(new GameObject("FaceGen"));
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public string Name
        {
            get { return "FaceGenPlugin"; }
        }

        public string Version
        {
            get { return "1.0.0"; }
        }

        public void OnLateUpdate()
        {
        }

        public string[] Filter
        {
            get
            {
                return new[]
                {
                    "PlayHome64bit"
                };
            }
        }
    }
}