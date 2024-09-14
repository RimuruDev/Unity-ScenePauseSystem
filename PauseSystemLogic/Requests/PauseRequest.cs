using System;

namespace AbyssMoth.ScenePauseSystem
{
    [Serializable]
    public class PauseRequest
    {
        #region Custom

        /// <summary>
        /// Click on pause button in settings panel
        /// </summary>
        public bool IsPerformSettings;

        /// <summary>
        /// Show win screen
        /// </summary>
        public bool IsVictory;

        /// <summary>
        /// Show defeat screen
        /// </summary>
        public bool IsDefeat;

        /// <summary>
        /// Show Ad :D
        /// </summary>
        public bool IsShowAd;

        #endregion

        #region MonoBehaviour

        /// <summary>
        /// Scene -> OnDestroy 
        /// </summary>
        public bool IsEndSceneLifeCycle;

        /// <summary>
        /// OnApplicationFocus
        /// </summary>
        public bool IsChangedApplicationFocus;

        /// <summary>
        /// OnApplicationPause
        /// </summary>
        public bool IsChangedOnApplicationPause;

        #endregion
    }
}