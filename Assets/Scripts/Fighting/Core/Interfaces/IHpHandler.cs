using UnityEngine;

namespace Fighting.Core
{
    public interface IHpHandler
    {
        public float Hp { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        /// <returns>true if dead</returns>
        public bool HandleDamage(float damage);
    }
}