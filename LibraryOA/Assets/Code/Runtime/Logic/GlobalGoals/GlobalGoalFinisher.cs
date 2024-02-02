using Code.Runtime.Logic.GlobalGoals.RocketStart;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.GlobalGoals
{
    internal sealed class GlobalGoalFinisher : MonoBehaviour
    {
        [SerializeField]
        private Rocket _rocket;
        
        public void Finish() =>
            FinishAsync()
                .Forget();

        private async UniTaskVoid FinishAsync()
        {
            await _rocket.LaunchAsync(); 
        }
    }
}