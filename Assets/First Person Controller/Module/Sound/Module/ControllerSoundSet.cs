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

using UCollections;

namespace FPC
{
	public class ControllerSoundSet : ControllerSound.Module
	{
        [SerializeField]
        protected ControllerSoundSetTemplate fallback;
        public ControllerSoundSetTemplate Fallback { get { return fallback; } }

        public ControllerSoundSetTemplate Value { get; protected set; }

        [SerializeField]
        protected SubstanceSoundDictionary surfaces;
        public SubstanceSoundDictionary Surfaces { get { return surfaces; } }
        [Serializable]
        public class SubstanceSoundDictionary : UDictionary<Substance, ControllerSoundSetTemplate> { }

        public override void Configure()
        {
            base.Configure();

            Value = Fallback;
        }

        public override void Init()
        {
            base.Init();

            Controller.Ground.Detect.OnProcess += GroundDetectCallback;
        }

        void GroundDetectCallback(ControllerGroundData hit)
        {
            Value = Detect(hit.Collider);
        }

        protected virtual ControllerSoundSetTemplate Detect(Collider collider)
        {
            if (collider == null) return fallback;

            var surface = Surface.Get(collider);

            if (surface == null) return fallback;
            if (surface.Substance == null) return fallback;

            if (surfaces.TryGetValue(surface.Substance, out var set) == false) return fallback;

            return set;
        }
    }
}