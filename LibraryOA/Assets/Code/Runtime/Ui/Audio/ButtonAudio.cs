using Code.Runtime.Logic.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Runtime.Ui.Audio
{
    [RequireComponent(typeof(Button))]
    internal sealed class ButtonAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _buttonClickSound;
        [SerializeField]
        private Button _button;
        
        private AudioPlayer _audioPlayer;

        private void OnValidate() =>
            _button ??= GetComponent<Button>();

        [Inject]
        private void Construct(AudioPlayer audioPlayer) =>
            _audioPlayer = audioPlayer;

        private void Awake() =>
            _button.onClick.AddListener(OnClick);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OnClick);

        private void OnClick() =>
            _audioPlayer.PlaySfx(_buttonClickSound);
    }
}