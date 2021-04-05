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

namespace Default
{
	public class WeaponAction : Weapon.Module
	{
        [SerializeField]
        float minInput = 0.1f;

        public IProcessor Processor { get; protected set; }
        public interface IProcessor : Weapon.IProcessor
        {
            float Action { get; }
        }

        public WeaponConstraint Constraint => Weapon.Constraint;

        public override void Init()
        {
            base.Init();

            Processor = Weapon.GetProcessor<IProcessor>();

            Weapon.OnProcess += Process;
        }

        void Process()
        {
            if (Processor.Action > minInput)
            {
                if (Constraint.Active == false)
                {
                    Perform();
                }
            }
        }

        public event Action OnPerfom;
        void Perform()
        {
            OnPerfom?.Invoke();
        }
    }
}