using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OPS.Model
{

	public interface IDataModel
	{
        Dictionary<string, object> Record {get;}
    }

}