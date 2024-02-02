using System;
using UnityEngine;

namespace Code.Runtime.Data
{
    public static class Vector2Extensions
    {
        public static Vector2 WithX(this Vector2 vector2, float x)
        {
            vector2.x = x;
            return vector2;
        }
        
        public static Vector2 WithY(this Vector2 vector2, float y)
        {
            vector2.y = y;
            return vector2;
        }
        
        public static Vector2 WithZ(this Vector2 vector2, float z)
        {
            vector2.y = z;
            return vector2;
        }

        public static Vector2 ToVector2(this Direction2d direction) =>
            direction switch
            {
                Direction2d.Left => Vector2.left,
                Direction2d.Up => Vector2.up,
                Direction2d.Down => Vector2.down,
                Direction2d.Right => Vector2.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
    }
}