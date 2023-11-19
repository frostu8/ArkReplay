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
    }
}