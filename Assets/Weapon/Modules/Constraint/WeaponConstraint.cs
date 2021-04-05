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
	public class WeaponConstraint : Weapon.Module
	{
		public List<IInterface> List { get; protected set; }
		public interface IInterface
        {
			bool Constraint { get; }
		}

        public bool Active
        {
            get
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (List[i].Constraint)
                        return true;
                }

                return false;
            }
        }

        public override void Configure()
        {
            base.Configure();

            List = new List<IInterface>();

            var selection = Weapon.Behaviours.Where(x => x is IInterface).Cast<IInterface>();

            List.AddRange(selection);
        }
    }
}