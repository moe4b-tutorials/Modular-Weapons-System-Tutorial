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
	public class ControllerStateElementStepModifier : ControllerStateElement.Module
	{
		[SerializeField]
        float target = 1f;
        public float Target => target;

        float Modifier() => target * Element.Weight;

        public override void Init()
        {
            base.Init();

            var target = State.Modules.Depend<ControllerStateStepModifier>();

            target.Scale.Add(Modifier);
        }
    }
}