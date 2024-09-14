#if UNITY_EDITOR
using UnityEngine;
using AbyssMoth.ScenePauseSystem;

namespace Plugins.Unity_ScenePauseSystem.Example
{
    /// <summary>
    /// Variant 1
    /// </summary>
    public class InfinityDebugLogWriter : PausableBehaviour
    {
        private protected override void OnUpdate()
        {
            Debug.Log($"Write message! Pause Sender: [{ReadLastPauseSender?.GetType().Name}] | Resume Sender: [{ReadLastResumeSender?.GetType().Name}]");
        }
    }
}
#endif