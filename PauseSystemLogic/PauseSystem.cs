using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

#if NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif

namespace AbyssMoth.ScenePauseSystem
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public class PauseSystem : MonoBehaviour, IPauseSystem
    {
#if UNITY_EDITOR
        [SerializeField] private bool enableDebugLog;
#endif
        private List<IPausable> pausables = new();
        private bool isPaused = false;
        public bool IsPaused => isPaused;

#if NAUGHTY_ATTRIBUTES
        [ResizableTextArea, ReadOnly]
#else
        [TextArea(3, 20)]
#endif
#if UNITY_EDITOR
        [SerializeField]
        private string editorPausablesList;
#endif
        private void Awake()
        {
            // TODO: Add Custom Editor for bake (cache) all. For zero reflection in runtime.
            FindAllPausables();
        }

        private void OnDestroy()
        {
            if (isPaused)
                ResumeGame(new PauseRequest { IsEndSceneLifeCycle = true }, this);

            var pausablesCopy = pausables.ToList();

            foreach (var pausable in pausablesCopy)
            {
                Unsubscribe(pausable);
            }

            pausables.Clear();
            pausables = null;
        }

        public void Subscribe(IPausable pausable)
        {
            if (!pausables.Contains(pausable))
            {
                pausables?.Add(pausable);
#if UNITY_EDITOR
                UpdatePausablesListForEditor();
#endif
            }
        }

        public void Unsubscribe(IPausable pausable)
        {
            if (pausables == null || !Application.isPlaying)
                return;

            if (pausables.Contains(pausable))
            {
                pausables?.Remove(pausable);
#if UNITY_EDITOR
                UpdatePausablesListForEditor();
#endif
            }
        }

        public void PauseGame(PauseRequest request, Object sender = null)
        {
            if (isPaused)
                return;

            isPaused = true;

            foreach (var pausable in pausables)
            {
                pausable?.Pause(request, sender);
            }

            Log("Game Paused", sender);
        }

        public void ResumeGame(PauseRequest request, Object sender = null)
        {
            if (!isPaused)
                return;

            isPaused = false;

            foreach (var pausable in pausables)
            {
                pausable?.Resume(request, sender);
            }

            Log("Game Resumed", sender);
        }

        private void FindAllPausables()
        {
            var foundPausables = FindObjectsOfType<MonoBehaviour>(true).OfType<IPausable>().ToArray();
            foreach (var pausable in foundPausables)
            {
                Subscribe(pausable);
            }
        }

#if UNITY_EDITOR
        private void UpdatePausablesListForEditor()
        {
            editorPausablesList = "Pausables:\n";
            foreach (var pausable in pausables)
            {
                if (pausable != null)
                    editorPausablesList += pausable.GetType().Name + "\n";
            }
        }
#endif

        private void Log(string message, Object sender = null)
        {
#if UNITY_EDITOR
            if (!enableDebugLog)
                return;

            Debug.Log(message, sender);
#endif
        }
    }
}