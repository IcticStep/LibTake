using UnityEngine.UI;

namespace Code.PlayModeTests.Extensions
{
    internal static class ButtonExtensions
    {
        public static void SimulateClick(this Button button) =>
            button.onClick.Invoke();
    }
}