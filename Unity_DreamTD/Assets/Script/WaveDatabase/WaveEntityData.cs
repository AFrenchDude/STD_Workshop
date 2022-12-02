//From Template
using UnityEngine;

	[System.Serializable]
	public class WaveEntityData
	{
		[SerializeField]
		private WaveEntity _waveEntityPrefab = null;

		[SerializeField]
		private NightmareData _nightmareData = null;

		public WaveEntity WaveEntityPrefab => _waveEntityPrefab;
		public NightmareData NightmareData => _nightmareData;
	}