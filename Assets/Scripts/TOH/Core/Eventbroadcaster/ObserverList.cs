using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOH.Core
{
	// Stripped-down version of a custom list for handling event observers
	// By: Lee
	public class ObserverList 
	{
		private List<System.Action<Parameters>> eventListeners;	
		private List<System.Action> eventListenersNoParams; 

		public ObserverList() 
		{
			this.eventListeners = new List<System.Action<Parameters>>();
			this.eventListenersNoParams = new List<System.Action>();
		}

		public void AddObserver(System.Action<Parameters> action) 
		{
			this.eventListeners.Add(action);
		}

		public void AddObserver(System.Action action) 
		{
			this.eventListenersNoParams.Add(action);
		}

		public void RemoveObserver(System.Action<Parameters> action)
		{
			if(this.eventListeners.Contains(action)) 
			{
				this.eventListeners.Remove(action);
			}
		}

		public void RemoveObserver(System.Action action) 
		{
			if(this.eventListenersNoParams.Contains(action)) 
			{
				this.eventListenersNoParams.Remove(action);
			}
		}

		public void RemoveAllObservers() 
		{
			this.eventListeners.Clear();
			this.eventListenersNoParams.Clear();
		}

		public void NotifyObservers(Parameters parameters) 
		{
			for(int i = 0; i < this.eventListeners.Count; i++) 
			{
				System.Action<Parameters> action = this.eventListeners[i];
				action(parameters);
			}
		}

		public void NotifyObservers() 
		{
			for(int i = 0; i < this.eventListenersNoParams.Count; i++) 
			{
				System.Action action = this.eventListenersNoParams[i];
				action();
			}
		}
	}
}
