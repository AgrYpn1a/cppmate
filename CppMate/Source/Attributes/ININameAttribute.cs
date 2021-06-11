using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppMate.Source.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class ININameAttribute : Attribute
	{
		public string Name { get; private set; }

		public ININameAttribute(string name)
		{
			Name = name;
		}
	}
}
