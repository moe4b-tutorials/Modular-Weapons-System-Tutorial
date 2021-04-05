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
	public class Weapon : MonoBehaviour
	{
        public IBehaviour[] Behaviours { get; protected set; }
		public interface IBehaviour
        {
			//Like Awake
			void Configure();

			//Like Start
			void Init();
        }
        public class Behaviour : MonoBehaviour, IBehaviour
        {
            public virtual void Configure()
            {

            }

            public virtual void Init()
            {
                
            }
        }

        public WeaponAction Action { get; protected set; }
        public WeaponConstraint Constraint { get; protected set; }
        public WeaponAudio Audio { get; protected set; }

        public IModule[] Modules { get; protected set; }
        public interface IModule
        {
			void Set(Weapon reference);
        }
        public class Module : Behaviour, IModule
        {
            public Weapon Weapon { get; protected set; }
            public virtual void Set(Weapon reference)
            {
                Weapon = reference;
            }
        }

        public IOwner Owner { get; protected set; }
        public interface IOwner
        {
            IProcessor[] Processors { get; }
        }
        public void Setup(IOwner reference)
        {
            Owner = reference;

            Behaviours = GetComponentsInChildren<IBehaviour>(true);
            Modules = GetComponentsInChildren<IModule>(true);

            Action = Modules.First(x => x is WeaponAction) as WeaponAction;
            Constraint = Modules.First(x => x is WeaponConstraint) as WeaponConstraint;
            Audio = Modules.First(x => x is WeaponAudio) as WeaponAudio;

            Array.ForEach(Modules, x => x.Set(this));

            Array.ForEach(Behaviours, x => x.Configure());
            Array.ForEach(Behaviours, x => x.Init());
        }

        void Update()
        {
            Process();
        }

        public event Action OnProcess;
        void Process()
        {
            OnProcess?.Invoke();
        }

        public T GetProcessor<T>()
            where T : IProcessor
        {
            for (int i = 0; i < Owner.Processors.Length; i++)
                if (Owner.Processors[i] is T processor)
                    return processor;

            throw new Exception($"No Processor of Type {typeof(T)} Found");
        }
        public interface IProcessor
        {

        }
    }
}