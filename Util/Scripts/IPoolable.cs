using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Util
{
	public interface IPoolable
	{
	    public void WakeUp();
	    public bool IsActive();
	    public void Extinguish();
	}
}