#if UNITY_EDITOR
using AbyssMoth.ScenePauseSystem;
using UnityEngine;

namespace Plugins.Unity_ScenePauseSystem.Example
{
    /// <summary>
    /// Variant 2
    /// </summary>
    public class InfinityColorDebugLogWriter : MonoBehaviour, IPausable
    {
        public bool IsPaused { get; private set; }

        private void Update()
        {
            if (IsPaused)
                return;

            Debug.Log($"<color=yellow>Write color message!</color>");
        }

        public void Pause(in PauseRequest request, Object sender = null)
        {
            IsPaused = true;

            // Other handle
            // if (request.IsDefeat) { ... } 
            // ...
        }

        public void Resume(in PauseRequest request, Object sender = null)
        {
            IsPaused = false;

            // Other handle
            // if (request.IsShowAd) { ... } 
            // ...
        }
    }
}
#endif