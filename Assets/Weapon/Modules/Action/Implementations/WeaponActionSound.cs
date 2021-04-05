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
	public class WeaponActionSound : Weapon.Module
	{
		[SerializeField]
		AudioClip clip;

        public override void Init()
        {
            base.Init();

            Weapon.Action.OnPerfom += Action;
        }

        void Action()
        {
            Weapon.Audio.Source.PlayOneShot(clip);
        }
    }
}