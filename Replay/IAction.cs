using System.Collections.Generic;

namespace ArkReplay.Replay
{
    /// <summary>
    /// An action performed by the player.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Replays the action.
        /// </summary>
        public void Replay();

        /// <summary>
        /// This is called every update to check if the action is ready.
        /// </summary>
        /// <returns>
        /// A boolean, <code>false</code> if the action is still waiting for
        /// something to be available.
        /// </returns>
        public bool Ready();
    }
}