using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace OPS.Model
{

	public interface IDataModel
	{
        Dictionary<string, ReactiveProperty<object>> Record {get;}
    }

}