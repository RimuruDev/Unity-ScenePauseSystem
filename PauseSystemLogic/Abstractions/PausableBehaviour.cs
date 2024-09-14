using UnityEngine;
using Object = UnityEngine.Object;

namespace AbyssMoth.ScenePauseSystem
{
    public abstract class PausableBehaviour : MonoBehaviour, IPausable
    {
        public bool IsPaused { get; private protected set; }

        private protected PauseRequest ReadLastPauseRequest { get; private set; }
        private protected PauseRequest ReadLastResumeRequest { get; private set; }

        private protected Object ReadLastPauseSender { get; private set; }
        private protected Object ReadLastResumeSender { get; private set; }

        private void FixedUpdate()
        {
            if (IsPaused)
                return;

            OnFixedUpdate();
        }

        private void Update()
        {
            if (IsPaused)
                return;

            OnUpdate();
        }

        private void LateUpdate()
        {
            if (IsPaused)
                return;

            OnLateUpdate();
        }

        private protected virtual void OnFixedUpdate()
        {
        }

        private protected virtual void OnUpdate()
        {
        }

        private protected virtual void OnLateUpdate()
        {
        }

        public virtual void Pause(in PauseRequest request, Object sender = null)
        {
            IsPaused = true;
            ReadLastPauseRequest = request;
            ReadLastPauseSender = sender;
        }

        public virtual void Resume(in PauseRequest request, Object sender = null)
        {
            IsPaused = false;
            ReadLastResumeRequest = request;
            ReadLastResumeSender = sender;
        }
    }
}