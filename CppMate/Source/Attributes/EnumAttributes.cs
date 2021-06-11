using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CppMate.Source.Attributes
{
	public static class EnumAttributes
	{
		public static T Get<T>(Enum enumValue) where T : Attribute
		{
			return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<T>() as T;
		}
	}
}
