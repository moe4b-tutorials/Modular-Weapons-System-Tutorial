using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace FPC
{
	public class ControllerGamepadInput : ControllerInput
	{
        protected override void Process()
        {
            base.Process();

            Move = new Vector2()
            {
                x = Input.GetAxis("Gamepad Left Stick X"),
                y = Input.GetAxis("Gamepad Left Stick Y")
            };

            Look.SetRawValue(Input.GetAxis("Gamepad Right Stick X"), Input.GetAxis("Gamepad Right Stick Y"));

            Jump = Input.GetKey(KeyCode.JoystickButton0);

            Sprint = Input.GetKey(KeyCode.JoystickButton8) ? 1f : 0f;

            ChangeStance = Input.GetKey(KeyCode.JoystickButton1);

            Lean = GetAxis(KeyCode.JoystickButton5, KeyCode.JoystickButton4);
        }
    }
}