using UnityEngine;

namespace ArkReplay
{
    /// <summary>
    /// Tracks inventory hashes. See <see cref="Inventories"/> for more info.
    /// </summary>
    public class InventoryHash : MonoBehaviour
    {
        public int Hash { get; private set; }

        private bool _initialized = false;

        public void Initialize()
        {
            // add self to inventories
            if (!_initialized)
            {
                Hash = Inventories.Add(gameObject);
                _initialized = true;
            }
        }

        void Awake()
        {
            Initialize();
        }

        void OnDestroy()
        {
            if (_initialized)
                Inventories.Remove(gameObject);
        }
    }
}