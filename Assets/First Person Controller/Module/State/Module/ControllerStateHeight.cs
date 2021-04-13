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
	public class ControllerStateHeight : ControllerState.Module
	{
        public float Value
        {
            get => Controller.Height;
            protected set => Controller.Height = value;
        }

        public Modifier.Additive Modifier { get; protected set; }

        public override void Configure()
        {
            base.Configure();

            Modifier = new Modifier.Additive();
        }

        public override void Init()
        {
            base.Init();

            Controller.OnProcess += Process;
        }
        
        void Process()
        {
            var delta = Modifier.Value - Value;

            Value += delta;

            OnDelta?.Invoke(delta);
        }

        public event DeltaDelegate OnDelta;
        public delegate void DeltaDelegate(float delta);
    }
}