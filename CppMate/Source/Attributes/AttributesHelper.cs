using System;
using System.Reflection;

namespace CppMate.Source.Attributes
{
	public static class AttributesHelper
	{
		public static T Get<T>(MemberInfo mi) where T : Attribute
		{
			return (T)Attribute.GetCustomAttribute(mi, typeof(T));
		}

	}
}
