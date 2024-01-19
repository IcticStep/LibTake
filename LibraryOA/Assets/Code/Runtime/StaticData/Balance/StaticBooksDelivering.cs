using UnityEngine;
using RangeInt = Code.Runtime.Data.RangeInt;

namespace Code.Runtime.StaticData.Balance
{
    [CreateAssetMenu(fileName = "Books delivering data", menuName = "Static data/Books delivering")]
    public class StaticBooksDelivering : ScriptableObject
    {
        [SerializeField]
        private int _daysScale;
        [SerializeField]
        [Range(0.01f, 1f)]
        private float _percentsShouldLeftInLibrary = 0.1f;
        [SerializeField]
        private AnimationCurve _curve;
        [SerializeField]
        private RangeInt _inLibraryLimit;

        public float PercentsShouldLeftInLibrary => _percentsShouldLeftInLibrary;
        public RangeInt InLibraryLimit => _inLibraryLimit;

        public AnimationCurve Curve => _curve;

        public int DaysScale => _daysScale;

        public int GetBooksShouldBeInLibraryForDay(int day)
        {
            float curveTime = (float)day / _daysScale;
            float curveValue = _curve.Evaluate(curveTime);

            return (int)Mathf.Lerp(_inLibraryLimit.Min, _inLibraryLimit.Max, curveValue);
        }
    }
}