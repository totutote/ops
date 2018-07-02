using System.Collections;
using System.Collections.Generic;
using OPS.Model;

namespace OPS.ViewModel
{

	public class OptionListViewModel
	{
		public List<MasterOption> option;

		public OptionListViewModel()
		{
			option = MasterOption.AllOptions();
		}

	}

}
