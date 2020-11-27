using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOH.Core
{
	public class Parameters 
	{
		private Dictionary<string, int> intData;
		private Dictionary<string, bool> boolData;
		private Dictionary<string, float> floatData;
		private Dictionary<string, string> stringData;
		private Dictionary<string, object> objectListData;

		public Parameters() 
		{
			this.intData = new Dictionary<string, int>();
			this.boolData = new Dictionary<string, bool>();
			this.floatData = new Dictionary<string, float>();
			this.stringData = new Dictionary<string, string>();
			this.objectListData = new Dictionary<string, object>();
		}

		public void PutExtra(string paramName, bool value) 
		{
			this.boolData.Add(paramName,value);
		}

		public void PutExtra(string paramName, int value) 
		{
			if(this.intData.ContainsKey(paramName)) 
			{
				this.intData[paramName] = value;
			} 
			else 
			{
				this.intData.Add(paramName, value);
			}
		}

		public void PutExtra(string paramName, float value) 
		{
			this.floatData.Add(paramName,value);
		}

		public void PutExtra(string paramName, string value) 
		{
			if(this.stringData.ContainsKey(paramName)) 
			{
				this.stringData[paramName] = value;
			} else 
			{
				this.stringData.Add(paramName, value);
			}
		}

		public void PutObjectExtra(string paramName, object value) 
		{
			this.objectListData.Add(paramName, value);
		}

		public int GetIntExtra(string paramName, int defaultValue) 
		{
			if(this.intData.ContainsKey(paramName)) 
			{
				return this.intData[paramName];
			}
			else 
			{
				return defaultValue;
			}
		}

		public bool GetBoolExtra(string paramName, bool defaultValue) 
		{
			if(this.boolData.ContainsKey(paramName)) 
			{
				return this.boolData[paramName];
			}
			else 
			{
				return defaultValue;
			}
		}

		public float GetFloatExtra(string paramName, float defaultValue) 
		{
			if(this.floatData.ContainsKey(paramName)) 
			{
				return this.floatData[paramName];
			}
			else 
			{
				return defaultValue;
			}
		}

		public string GetStringExtra(string paramName, string defaultValue) 
		{
			if(this.stringData.ContainsKey(paramName)) 
			{
				return this.stringData[paramName];
			}
			else 
			{
				return defaultValue;
			}
		}

		public object GetObjectExtra(string paramName) 
		{
			if(this.objectListData.ContainsKey(paramName)) 
			{
				return this.objectListData[paramName];
			}
			else 
			{
				return null;
			}
		}
	}
}
