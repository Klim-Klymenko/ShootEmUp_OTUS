using UnityEngine;

namespace GameEngine
{
    //Нельзя менять!
    public sealed class Unit : MonoBehaviour
    {
        public string Type => type;

        public int HitPoints
        {
            get => hitPoints;
            set => hitPoints = value;
        }

        public Vector3 Position => transform.position;
   
        public Vector3 Rotation => transform.eulerAngles;

        [SerializeField]
        private string type;
        
        [SerializeField]
        private int hitPoints;

        private void Reset()
        {
            type = name;
            hitPoints = 10;
        }
    }
}