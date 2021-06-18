using EnvDTE;
using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CppMate.Source
{
	class CppMateVSHelper
	{
		public static string GetSelectedFolderPath()
		{
			EnvDTE80.DTE2 dte = CppMatePackage.GetDTE2();
			UIHierarchy uih = dte.ToolWindows.SolutionExplorer;
			Array selectedItems = (Array)uih.SelectedItems;

			VCFilter filter = null;

			foreach (UIHierarchyItem currItem in selectedItems)
			{
				ProjectItem prjItem = currItem.Object as ProjectItem;
				filter = prjItem.Object as VCFilter;
				break; // We want only first
			}

			if (filter != null)
			{
				string[] filters = filter.CanonicalName.Split('\\');
				string targetPath = Path.Combine(CppMateManager.Instance.Config.SourceDirectory, Path.Combine(filters));

				return targetPath;
			}

			return string.Empty;
		}
	}
}
