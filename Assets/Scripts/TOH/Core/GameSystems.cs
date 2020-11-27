using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOH.Core
{
	// Simple service registry that uses the service-locator 
	// pattern to save and serve game objects
	// By: Lee
	public class GameSystems 
	{
		// Object mapping
		private static IDictionary<System.Type, object> registry;
		
		static GameSystems()
		{
			registry = new Dictionary<System.Type, object>();
		}

		#region GameSystems methods
		public static void Dispose()
		{
			if (registry != null) registry.Clear();
			registry = null;
		}

		public static bool Register(System.Type type, object objectReference, bool overwriteExisting = false)
		{
			if (registry.ContainsKey(type))
			{
				if (overwriteExisting)
				{
					registry.Remove(type);
				}
				else
				{
					Debug.Log ("<color=red>GameSystems | Object of type \'" + objectReference.GetType() + "\' already registered</color>");
					return false;
				}
			}
			
			registry.Add(type, objectReference);
			return true;
		}

		public static T GetService<T> ()
		{
			try
			{
				return (T)(registry[typeof(T)]);
			}
			catch(KeyNotFoundException)
			{
				Debug.Log ("<color=red>ObjectRegistry | Object of type \'" + typeof(T) + "\' not registered</color>");
				return default(T);
			}
		}
		#endregion
	}
}
