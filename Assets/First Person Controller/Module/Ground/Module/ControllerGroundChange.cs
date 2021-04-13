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
	public class ControllerGroundChange : ControllerGround.Module
	{
        private ControllerGroundData oldData;

        public ControllerAirTravel AirTravel => Ground.AirTravel;

        public virtual void Set(ControllerGroundData data)
        {
            oldData = data;

            if (data.Connected == false) Leave();
        }

        public virtual void Process(ControllerGroundData newData)
        {
            if (oldData.Connected == false && newData.Connected == true) //Landed On Ground
                Land();
            else if (oldData.Connected == true && newData.Connected == false) //Left Ground
                Leave();

            oldData = newData;
        }

        public delegate void LeftGroundDelegate();
        public event LeftGroundDelegate OnLeave;
        protected virtual void Leave()
        {
            Debug.Log("Left Ground");

            AirTravel.Begin();

            OnLeave?.Invoke();
        }

        public delegate void LandingDelegate(ControllerAirTravel.Data travel);
        public event LandingDelegate OnLand;
        protected virtual void Land()
        {
            Debug.Log("Landed On Ground");

            var travel = AirTravel.End();

            OnLand?.Invoke(travel);
        }
    }
}