using System.Collections;
using System.Collections.Generic;
using OPS.Model;

namespace OPS.ViewModel
{

	public class MaterialOptionListViewModel
	{
		public List<UserMaterialOption> option;

		public MaterialOptionListViewModel()
		{
			option = UserMaterialOption.AllOptions();
		}

	}

}
