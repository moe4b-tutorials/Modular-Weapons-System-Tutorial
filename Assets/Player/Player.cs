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
    public class Player : MonoBehaviour, Weapon.IOwner, WeaponAction.IProcessor
    {
        [SerializeField]
        Weapon weapon;

        public Weapon.IProcessor[] Processors { get; protected set; }

        public float Action => Input.GetKey(KeyCode.Mouse0) ? 1f : 0f;

        void Start()
        {
            Processors = GetComponentsInChildren<Weapon.IProcessor>(true);

            weapon.Setup(this);
        }
    }
}