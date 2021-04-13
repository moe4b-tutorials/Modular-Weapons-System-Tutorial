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
	public class ControllerStateHeightAdjustment : ControllerState.Module
	{
        public override void Init()
        {
            base.Init();

            State.Height.OnDelta += Process;
        }

        public virtual void Process(float delta)
        {
            if (Controller.IsGrounded)
                Controller.transform.position += Controller.transform.up * delta / 2f;
        }
    }
}