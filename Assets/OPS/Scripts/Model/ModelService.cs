using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

namespace OPS.Model
{

	public class ModelService<T> : IInitializable
	{
		//private Dictionary<int, T> CacheRecords {get {return cacheRecords;}}
		Dictionary<int, T> cacheRecords = new Dictionary<int, T>();

		void IInitializable.Initialize()
		{
		}

        public Dictionary<int, T> Regist(Dictionary<int, T> records)
        {
			var refRecords = new Dictionary<int, T>();
			foreach (var record in records)
			{
				if(!cacheRecords.ContainsKey(record.Key))
				{
					cacheRecords[record.Key] = record.Value;
				}
				refRecords[record.Key] = cacheRecords[record.Key];
			}
			return refRecords;
        }
	}

}