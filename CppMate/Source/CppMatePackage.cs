using CppMate.Source;
using CppMate.Source.Config;
using CppMate.Source.ToolWindows;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Flavor;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using Task = System.Threading.Tasks.Task;

namespace CppMate
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the
	/// IVsPackage interface and uses the registration attributes defined in the framework to
	/// register itself and its components with the shell. These attributes tell the pkgdef creation
	/// utility what data to put into .pkgdef file.
	/// </para>
	/// <para>
	/// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
	/// </para>
	/// </remarks>
	/// 
	[ProvideAutoLoad(VSConstants.UICONTEXT.VCProject_string, PackageAutoLoadFlags.BackgroundLoad)]
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(CppMatePackage.PackageGuidString)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[ProvideToolWindow(typeof(CppMateToolWindow), Style = VsDockStyle.AlwaysFloat)]
	public sealed class CppMatePackage : AsyncPackage
	{
		/// <summary>
		/// CppMatePackage GUID string.
		/// </summary>
		public const string PackageGuidString = "969c8e63-189f-4be0-a8e0-e10bce3cdcef";

		public static EnvDTE80.DTE2 GetDTE2()
		{
			return GetGlobalService(typeof(DTE)) as EnvDTE80.DTE2;
		}

		#region Package Members

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		/// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
		/// <param name="progress">A provider for progress updates.</param>
		/// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			// When initialized asynchronously, the current thread may be a background thread at this point.
			// Do any initialization that requires the UI thread after switching to the UI thread.
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

			if (!ValidateProjectType())
			{
				MessageBox.Show("[!WARNING!] C++ Mate will not run correctly for this project type.");
				return;
			}

			// Create instance of manager
			CppMateManager manager = CppMateManager.Instance;

			// Read & load config
			string solutionDir = GetSolutionDirectory();
			if (solutionDir == null)
			{
				MessageBox.Show("[!ERROR!] C++ Mate could not get the solution.");
				return;
			}

			INIConfig config = manager.LoadINIConfig(solutionDir);
			if (config == null)
			{
				// Need to create new configuration
				CppMateInit winInit = new CppMateInit();
				winInit.ShowDialog();
			}

			// Initialize the app
			await CppMateToolWindowCommand.InitializeAsync(this);
			await AddFolderCmd.InitializeAsync(this);
		    await CppMate.Source.Commands.Tool.AddSourceFile.InitializeAsync(this);
		}

		private List<string> GetProjectTypes()
		{
			try
			{
				ThreadHelper.ThrowIfNotOnUIThread();
			}
			catch (Exception e) { return null; }

			List<string> types = new List<string>();

			EnvDTE.DTE dte;
			dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));

			EnvDTE.Project project;
			project = dte.Solution.Projects.Item(1);

			IVsSolution solution;
			solution = (IVsSolution)Package.GetGlobalService(typeof(SVsSolution));

			IVsHierarchy hierarchy;
			solution.GetProjectOfUniqueName(project.UniqueName, out hierarchy);

			IVsAggregatableProjectCorrected AP;
			AP = hierarchy as IVsAggregatableProjectCorrected;

			string projTypeGuids;
			int res = AP.GetAggregateProjectTypeGuids(out projTypeGuids);

			types = projTypeGuids.Split(';').Where(str => !string.IsNullOrWhiteSpace(str) && !string.IsNullOrEmpty(str)).ToList();

			return types;
		}

		private bool ValidateProjectType()
		{
			List<string> projectTypes = GetProjectTypes();
			if (projectTypes.Contains(ProjectTypes.CPP))
			{
				return true;
			}

			return false;
		}

		private string GetSolutionDirectory()
		{
			try
			{
				ThreadHelper.ThrowIfNotOnUIThread();
			}
			catch (Exception e) { return null; }

			EnvDTE.DTE dte;
			dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));

			return Path.GetDirectoryName(dte.Solution.FullName);
		}

		#endregion
	}
}
