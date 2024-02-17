using TMPro;
using UnityEngine;

namespace Code.Runtime.Ui.Menu
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    internal sealed class ApplicationVersionText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private void OnValidate() =>
            _text ??= GetComponent<TextMeshProUGUI>();
        
        private void Start() =>
            _text.text = $"v{Application.version}";
    }
}