using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.Ui.Menu.Common
{
    [RequireComponent(typeof(Button))]
    internal sealed class BackButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private MenuGroup[] _groupsToHide;
        [SerializeField]
        private MenuGroup[] _groupsToShow;

        private void OnValidate() =>
            _button ??= GetComponent<Button>();

        private void Awake() =>
            _button.onClick.AddListener(OnBackButton);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnBackButton);

        private void OnBackButton()
        {
            foreach (MenuGroup group in _groupsToHide)
                group.Hide().Forget();
            
            foreach (MenuGroup group in _groupsToShow)
                group.Show().Forget();
        }
    }
}