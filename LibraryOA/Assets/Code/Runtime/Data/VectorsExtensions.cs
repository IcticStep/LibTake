using UnityEngine;

namespace Code.Runtime.Data
{
    internal static class VectorsExtensions
    {
        public static Vector3 WithX(this Vector3 vector3, float x)
        {
            vector3.x = x;
            return vector3;
        }
        
        public static Vector3 WithY(this Vector3 vector3, float y)
        {
            vector3.y = y;
            return vector3;
        }
        
        public static Vector3 WithZ(this Vector3 vector3, float z)
        {
            vector3.z = z;
            return vector3;
        }        
    }
}