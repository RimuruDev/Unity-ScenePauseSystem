using UnityEngine;
using AbyssMoth.ScenePauseSystem;

namespace Plugins.Unity_ScenePauseSystem.Example
{
    public class MainMenuUISettings : MonoBehaviour
    {
        [SerializeField] private PauseSystem pauseSystem;

        private void Update()
        {
            ReadUserInput();
        }

        private void ReadUserInput()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (pauseSystem.IsPaused)
                    pauseSystem.ResumeGame(new PauseRequest { IsPerformSettings = true }, this);
                else
                    pauseSystem.PauseGame(new PauseRequest { IsPerformSettings = true }, this);
            }
        }
    }
}