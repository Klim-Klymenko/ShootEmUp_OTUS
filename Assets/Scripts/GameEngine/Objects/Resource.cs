using UnityEngine;

namespace GameEngine
{
    public sealed class Resource : MonoBehaviour
    {
        public string ID
        {
            get => id;
            set => id = value;
        }

        public int Amount
        {
            get => amount;
            set => amount = value;
        }

        [SerializeField]
        private string id;

        [SerializeField]
        private int amount;
    }
}