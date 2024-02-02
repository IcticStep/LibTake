using Code.Runtime.StaticData.GlobalGoals;
using UnityEngine;
using UnityEngine.Playables;

namespace Code.Runtime.Logic.GlobalGoals
{
    [RequireComponent(typeof(PlayableDirector))]
    public sealed class GlobalGoalDirector : MonoBehaviour
    {
        [SerializeField]
        private PlayableDirector _director;
        [field: SerializeField]
        public GlobalGoal GlobalGoal { get; private set; }

        private void OnValidate() =>
            _director ??= GetComponent<PlayableDirector>();
        
        public void PlayFinishCutscene() =>
            _director.Play();
    }
}