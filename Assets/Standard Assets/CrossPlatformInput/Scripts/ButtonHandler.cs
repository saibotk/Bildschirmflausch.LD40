using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class ButtonHandler : MonoBehaviour
    {

        public string Name;

		public Sprite NormalImage;

		public Sprite ActionImage;

        void OnEnable()
        {

        }

        public void SetDownState()
        {
            CrossPlatformInputManager.SetButtonDown(Name);
			GetComponent<UnityEngine.UI.Image> ().sprite = ActionImage;
        }


        public void SetUpState()
        {
            CrossPlatformInputManager.SetButtonUp(Name);
			GetComponent<UnityEngine.UI.Image> ().sprite = NormalImage;
        }


        public void SetAxisPositiveState()
        {
            CrossPlatformInputManager.SetAxisPositive(Name);
        }


        public void SetAxisNeutralState()
        {
            CrossPlatformInputManager.SetAxisZero(Name);
        }


        public void SetAxisNegativeState()
        {
            CrossPlatformInputManager.SetAxisNegative(Name);
        }

        public void Update()
        {

        }
    }
}
