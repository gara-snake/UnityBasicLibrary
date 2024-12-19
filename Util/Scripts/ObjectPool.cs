using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Util
{
	public class ObjectPool<T> where T : MonoBehaviour, IPoolable
	{
		private List<T> objects = new List<T>();
		private T prefab;

		public ObjectPool(T prefab, int initialSize)
		{
			this.prefab = prefab;
			for (int i = 0; i < initialSize; i++)
			{
				AddObjectToPool();
			}
		}

		public T GetObjectFromPool()
		{
			if (objects.Count == 0)
			{
				AddObjectToPool();
			}

			foreach (T obj in objects)
			{
				if (!obj.IsActive())
				{
					obj.WakeUp();
					return obj;
				}
			}

			var ret = AddObjectToPool();
			ret.WakeUp();

			return ret;
		}

		public void ReturnObjectToPool(T obj)
		{
			obj.Extinguish();
			objects.Add(obj);
		}

		public void ExtinguishAll()
		{
			foreach (T obj in objects)
			{
				obj.Extinguish();
			}
		}

		private T AddObjectToPool()
		{
			var newObj = GameObject.Instantiate(prefab);

			objects.Add(newObj);
			newObj.Extinguish();

			return newObj;
		}
	}
}