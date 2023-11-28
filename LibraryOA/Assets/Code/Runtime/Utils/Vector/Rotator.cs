using System.Collections.Generic;
using Code.Runtime.Data;
using UnityEngine;

namespace Code.Runtime.Utils.Vector
{
    public sealed class Rotator
    {
        private readonly float _length;
        private readonly int _count;
        private readonly float _intervalDegree;
        private readonly Vector3 _axis;
        private readonly List<Line> _resultCache;
        private readonly IReadOnlyList<Quaternion> _rotationsCache;

        public Rotator(float length, int count, float intervalDegree, Vector3 axis)
        {
            _length = length;
            _count = count;
            _intervalDegree = intervalDegree;
            _axis = axis;
            _resultCache = new List<Line>(count);
            _rotationsCache = CreateVectorsRotation();
        }

        public IReadOnlyList<Line> CreateVectorsRotated(Vector3 start, Vector3 initialDirection)
        {
            _resultCache.Clear();
            for(int i = 0; i < _count; i++)
            {
                Vector3 end = _rotationsCache[i] * initialDirection * _length + start;
                _resultCache.Add(new Line(start, end));
            }

            return _resultCache;
        }

        private List<Quaternion> CreateVectorsRotation()
        {
            List<Quaternion> result = new(_count);
            
            float currentDegree = 0;
            for(int i = 0; i < _count; i++)
            {
                result.Add(Quaternion.AngleAxis(currentDegree, _axis));
                if(i == 0)
                {
                    currentDegree = _intervalDegree;
                    continue;
                }

                currentDegree = -currentDegree;
                if(i % 2 == 0)
                    currentDegree += _intervalDegree;
            }

            return result;
        }
    }
}