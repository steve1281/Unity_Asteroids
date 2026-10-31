using UnityEngine;

namespace Asteroids
{
    public static class Layers
    {
        // Built in layers
        public const int Default = 0;
        public const int TransparentFX = 1;
        public const int IgnoreRaycast = 2;
        public const int Water = 4;
        public const int UI = 5;

        // Custom Layers
        public static readonly int Player = LayerMask.NameToLayer("Player");
        public static readonly int Enemy = LayerMask.NameToLayer("Enemy");
        public static readonly int Teleporter = LayerMask.NameToLayer("Teleporter");
    }
}
