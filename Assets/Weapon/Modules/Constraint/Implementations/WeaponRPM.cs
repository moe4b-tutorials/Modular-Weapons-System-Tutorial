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
    public class WeaponRPM : Weapon.Module, WeaponConstraint.IInterface
    {
        [SerializeField]
        int value = 600;

        public float Delay => 60f / value;

        float timer = 0f;

        public bool Constraint => timer > 0f;

        public override void Init()
        {
            base.Init();

            Weapon.OnProcess += Process;
            Weapon.Action.OnPerfom += Action;
        }

        void Process()
        {
            timer = Mathf.MoveTowards(timer, 0f, Time.deltaTime);
        }

        void Action()
        {
            timer = Delay;
        }
    }
}