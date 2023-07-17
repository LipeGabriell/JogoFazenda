using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "Data/Weapon Stats")]
    public class WeaponStats : ScriptableObject
    {
        public string weaponName;
        public Sprite weaponSprite;
        public int weaponDamage;
    }
}