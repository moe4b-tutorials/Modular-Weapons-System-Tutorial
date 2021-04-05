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
	public class WeaponRaycastAction : Weapon.Module
	{
        [SerializeField]
        Transform point;

        [SerializeField]
        float range = 400f;

        [SerializeField]
        LayerMask mask = Physics.DefaultRaycastLayers;

        public override void Init()
        {
            base.Init();

            Weapon.Action.OnPerfom += Action;
        }

        void Action()
        {
            if (Physics.Raycast(point.position, point.forward, out var hit, range, mask))
            {
                Debug.Log(hit.transform);
            }
        }
    }
}