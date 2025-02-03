using System.Collections.Generic;
using UnityEngine;

namespace Snake.Gara.Unity.Basic.Library.Sound
{

	[CreateAssetMenu(fileName = "SoundData", menuName = "UBL/SoundData", order = 1)]
	public class SoundData : ScriptableObject, ISEAssets
	{
		public SECell[] SeClips;

		public List<SECell> GetSEs()
		{
			return new List<SECell>(SeClips);
		}
	}

}