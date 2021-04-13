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
    public class ControllerState : FirstPersonController.Module
    {
        public ControllerStateHeight Height { get; protected set; }
        public ControllerStateRadius Radius { get; protected set; }

        public ControllerStateTransition Transition { get; protected set; }
        public ControllerStateSets Sets { get; protected set; }

        public List<ControllerStateElement> Elements { get; protected set; }

        public Modules<ControllerState> Modules { get; protected set; }
        public class Module : FirstPersonController.Behaviour, IModule<ControllerState>
        {
            public ControllerState State { get; protected set; }
            public virtual void Set(ControllerState value) => State = value;

            public FirstPersonController Controller => State.Controller;
        }

        public override void Set(FirstPersonController value)
        {
            base.Set(value);

            Modules = new Modules<ControllerState>(this);
            Modules.Register(Controller.Behaviours);

            Height = Modules.Depend<ControllerStateHeight>();
            Radius = Modules.Depend<ControllerStateRadius>();

            Transition = Modules.Depend<ControllerStateTransition>();
            Sets = Modules.Depend<ControllerStateSets>();

            Elements = Modules.FindAll<ControllerStateElement>();

            Modules.Set();
        }

        public event Action OnOperate;
        public virtual void Operate()
        {
            OnOperate?.Invoke();
        }
    }
}