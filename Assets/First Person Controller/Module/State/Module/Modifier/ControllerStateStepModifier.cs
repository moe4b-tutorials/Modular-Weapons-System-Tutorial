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
	public class ControllerStateStepModifier : ControllerState.Module
	{
        public float Value { get; protected set; }

        public Modifier.Additive Scale { get; protected set; }

        float Modifier()
        {
            if (Controller.IsGrounded)
                Value = Scale.Value;

            return Value;
        }

        public override void Configure()
        {
            base.Configure();

            Scale = new Modifier.Additive();
        }

        public override void Init()
        {
            base.Init();

            Controller.Step.Scale.Add(Modifier);
        }
    }
}