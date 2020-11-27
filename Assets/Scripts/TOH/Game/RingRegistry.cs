using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOH.Core;

namespace TOH.Game
{
	// Simple registry object for keeping track of "Ring" objects
	public class RingRegistry : MonoBehaviour 
	{
		private Dictionary<int, Ring> registry;

		void Awake()
		{
			registry = new Dictionary<int, Ring>();
			GameSystems.Register(typeof(RingRegistry), this);
		}

		#region Ring registry methods
		public void Register(Ring ring)
		{
			int id = ring.gameObject.GetInstanceID();
			if (!registry.ContainsKey(id))
			{
				registry.Add(id, ring);
			}
		}

		public Ring Get(int id)
		{
			if (registry.ContainsKey(id))
			{
				return registry[id];
			}

			return null;
		}
		#endregion // Ring registry methods
	}
}
