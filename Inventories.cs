using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArkReplay
{
    /// <summary>
    /// A singleton-like class for <see cref="InventoryManager"/>s.
    /// <desc>
    /// Provides consistent hashes for inventories across replay boundaries that
    /// have static positions (I.E. do not move during normal game execution).
    /// </desc>
    /// </summary>
    public static class Inventories
    {
        /// <summary>
        /// Gets the party inventory manager.
        /// </summary>
        public static InventoryManager PartyInventory
        { get => global::PartyInventory.InvenM; }

        private static List<GameObject> _invens = new List<GameObject>();

        /// <summary>
        /// Gets an inventory manager.
        /// </summary>
        /// <param name="hash">The hash of the inventory manager.</param>
        /// <returns>The inventory manager, or null if it doesn't exist.</returns>
        public static InventoryManager Get(int hash)
        {
            if (hash >= 0 && hash < _invens.Count)
                return _invens[hash]?.GetComponent<InventoryManager>();
            else
                return null;
        }

        internal static int Add(GameObject inventory)
        {
            for (int i = 0; i < _invens.Count; i++)
            {
                if (_invens[i] == null)
                {
                    _invens[i] = inventory;
                    return i;
                }
            }

            // if no empty space was found, append it
            _invens.Add(inventory);

            return _invens.Count - 1;
        }

        internal static void Remove(GameObject inventory)
        {
            var hash = inventory.GetComponent<InventoryHash>();
            if (hash == null)
                throw new ArgumentException("game object does not have inventory hash");

            _invens[hash.Hash] = null;
        }
    }
}