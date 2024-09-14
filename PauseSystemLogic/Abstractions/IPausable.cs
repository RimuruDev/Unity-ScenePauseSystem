using UnityEngine;

namespace AbyssMoth.ScenePauseSystem
{
    public interface IPausable
    {
        public bool IsPaused { get; }

        public void Pause(in PauseRequest request, Object sender = null);

        public void Resume(in PauseRequest request, Object sender = null);
    }
}