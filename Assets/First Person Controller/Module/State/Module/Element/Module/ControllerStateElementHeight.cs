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
	public class ControllerStateElementHeight : ControllerStateElement.Module
	{
        [SerializeField]
        float target = 2f;
        public float Target => target;

        float Modifier() => target * Element.Weight;

        public override void Init()
        {
            base.Init();

            State.Height.Modifier.Add(Modifier);
        }
    }
}