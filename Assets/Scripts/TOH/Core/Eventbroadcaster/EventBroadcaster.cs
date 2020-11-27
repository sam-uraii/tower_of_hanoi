using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOH.Core
{
	// A stripped-down version of a custom event-handling system that we use in one of our games
	// By: Lee
	public class EventBroadcaster 
	{
		private static Dictionary<string, ObserverList> eventObservers;

		static EventBroadcaster() 
		{
			eventObservers = new Dictionary<string, ObserverList>();
		}

		/// <summary>
		/// Adds an observer to listen to specified by notification name
		/// </summary>
		public static void AddObserver(string notificationName, System.Action<Parameters> action) 
		{
			//if there is already an existing key, add the listener to the observer list
			if(eventObservers.ContainsKey(notificationName)) 
			{
				ObserverList eventObserver = eventObservers[notificationName];
				eventObserver.AddObserver(action);
			}
			//create a new instance of an observer list
			else 
			{
				ObserverList eventObserver = new ObserverList();
				eventObserver.AddObserver(action);
				eventObservers.Add(notificationName,eventObserver);
			}
		}

		public static void AddObserver(string notificationName, System.Action action) 
		{
			//if there is already an existing key, add the listener to the observer list
			if(eventObservers.ContainsKey(notificationName)) 
			{
				ObserverList eventObserver = eventObservers[notificationName];
				eventObserver.AddObserver(action);
			}
			//create a new instance of an observer list
			else 
			{
				ObserverList eventObserver = new ObserverList();
				eventObserver.AddObserver(action);
				eventObservers.Add(notificationName,eventObserver);
			}
		}

		/// <summary>
		/// Removes the action at observer specified by notification name
		/// </summary>
		/// <param name="notificationName">Notification name.</param>
		/// <param name="action">Action.</param>
		public static void RemoveActionAtObserver(string notificationName, System.Action action) 
		{
			if(eventObservers.ContainsKey(notificationName)) 
			{
				ObserverList eventObserver = eventObservers[notificationName];
				eventObserver.RemoveObserver(action);
			}
		}

		/// <summary>
		/// Removes the action at observer specified by notification name
		/// </summary>
		/// <param name="notificationName">Notification name.</param>
		/// <param name="action">Action.</param>
		public static void RemoveActionAtObserver(string notificationName, System.Action<Parameters> action) 
		{
			if(eventObservers.ContainsKey(notificationName))
			{
				ObserverList eventObserver = eventObservers[notificationName];
				eventObserver.RemoveObserver(action);
			}
		}


		/// <summary>
		/// Removes all observers.
		/// </summary>
		public static void RemoveAllObservers() 
		{
			foreach(ObserverList eventObserver in eventObservers.Values) 
			{
				eventObserver.RemoveAllObservers();
			}
			eventObservers.Clear();
		}

		/// <summary>
		/// Posts an event specified by name that does not require any parameters. 
		/// Observers associated with this event will be called.
		/// </summary>
		public static void PostEvent(string notificationName) 
		{
			if(eventObservers.ContainsKey(notificationName)) 
			{
				ObserverList eventObserver = eventObservers[notificationName];
				eventObserver.NotifyObservers();
			}
		}

		/// <summary>
		/// Posts an event specified by name that requires parameters. Observers associated with this event will be called.
		/// Requires the parameters class to be passed.
		/// </summary>
		public static void PostEvent(string notificationName, Parameters parameters) 
		{
			if(eventObservers.ContainsKey(notificationName)) 
			{
				ObserverList eventObserver = eventObservers[notificationName];
				eventObserver.NotifyObservers(parameters);
			}
		}
	}
}
