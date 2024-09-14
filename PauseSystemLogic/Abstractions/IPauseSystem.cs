using UnityEngine;

namespace AbyssMoth.ScenePauseSystem
{
    public interface IPauseSystem
    {
        public void Subscribe(IPausable pausable);
        public void Unsubscribe(IPausable pausable);

        public void PauseGame(PauseRequest request, Object sender = null);
        public void ResumeGame(PauseRequest request, Object sender = null);
    }
}