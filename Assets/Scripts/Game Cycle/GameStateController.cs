using System.Collections.Generic;

namespace ShootEmUp
{
    public sealed class GameStateController
    {
        public List<T> ChangeState<T>(List<T> stateListeners, bool hasGameStarted) where T : IGameListener
        {
            List<T> checkedStateListeners = new();
            
            for (int i = 0; i < stateListeners.Count; i++)
            {
                if (!hasGameStarted)
                {
                    if (stateListeners[i] is IGameRunner)
                        checkedStateListeners.Add(stateListeners[i]);
                }
                else
                    checkedStateListeners.Add(stateListeners[i]);
            }
            
            return checkedStateListeners;
        }
    }
}