using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.PlayModeTests.Extensions
{
    internal static class ButtonExtensions
    {
        public static void SimulateClick(this Button button)
        {
            EventSystem findObjectOfType = Object.FindObjectOfType<EventSystem>();
            ExecuteEvents.Execute(button.gameObject, new BaseEventData(findObjectOfType), ExecuteEvents.submitHandler);
        }
    }
}