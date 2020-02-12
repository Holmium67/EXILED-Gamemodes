using System;
using System.Reflection;

namespace HandStand
{
	public static class ReflectionHelpers
	{
		public static BindingFlags Flags = BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic |
		                     BindingFlags.Static | BindingFlags.Public;
		
		public static void InvokeStaticMethod(this Type type, string methodName, object[] param)
		{
			MethodInfo info = type.GetMethod(methodName, Flags);
			info?.Invoke(null, param);
		}
	}
}