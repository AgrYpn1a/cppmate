using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace CppMate
{
	internal sealed class CppMateToolWindowCommand
	{
		public static async Task InitializeAsync(AsyncPackage package)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

			IMenuCommandService commandService = await package.GetServiceAsync<IMenuCommandService, IMenuCommandService>();
			Assumes.Present(commandService);

			var cmdId = new CommandID(new Guid("f80f8d3c-bf68-4920-aade-1773c0cc052a"), 0x0100);
			var menuItem = new OleMenuCommand((s, e) => Execute(package), cmdId);

			commandService.AddCommand(menuItem);
		}

		private static void Execute(AsyncPackage package)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			ToolWindowPane window = package.FindToolWindow(typeof(CppMateToolWindow), 0, true);
			if ((null == window) || (null == window.Frame))
			{
				throw new NotSupportedException("Cannot create tool window");
			}

			IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
			Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
		}
	}
}
