using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace CppMate
{
	[Guid("03182f7d-1337-447b-90b3-00c96d8f9cf9")]
	public class CppMateToolWindow : ToolWindowPane
	{
		public CppMateToolWindow() : base(null)
		{
			Caption = "CppMateToolWindow";

			// This is the user control hosted by the tool window; Note that, even if this class implements IDisposable,
			// we are not calling Dispose on this object. This is because ToolWindowPane calls Dispose on
			// the object returned by the Content property.
			Content = new CppMateToolWindowControl();
		}
	}
}
