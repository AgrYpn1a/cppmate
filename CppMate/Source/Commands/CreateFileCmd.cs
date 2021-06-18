using CppMate.Source.Commands.Tool;
using CppMate.Source.ToolWindows;
using CppMate.Source.ViewModel;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.VCProjectEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CppMate.Source.Commands
{
	static class Templates
	{
		public const string CPP_SOURCE_TEMPLATE =
@"#include ""$ClassName.h""\n
$ClassName::$ClassName()
{\n
}\n";

		public const string HPP_SOURCE_TEMPLATE =
@"#pragma once\n
class $ClassName
{
public:
    $ClassName();\n
};\n";
	}

	class CreateFileCmd : ICommand
	{
		public event EventHandler CanExecuteChanged;

		private CppMateMain m_Win;
		public CreateFileCmd(CppMateMain win)
		{
			m_Win = win;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
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
				if (File.Exists(m_Win.VM.HeaderFilePath) || File.Exists(m_Win.VM.SourceFilePath))
				{
					MessageBox.Show("Files with the same name already exist!");
					return;
				}

				string hppTemplate = Templates.HPP_SOURCE_TEMPLATE;
				hppTemplate = hppTemplate.Replace("\\n", Environment.NewLine);
				hppTemplate = hppTemplate.Replace("$ClassName", m_Win.VM.ClassName);

				string cppTemplate = Templates.CPP_SOURCE_TEMPLATE;
				cppTemplate = cppTemplate.Replace("\\n", Environment.NewLine);
				cppTemplate = cppTemplate.Replace("$ClassName", m_Win.VM.ClassName);

				File.WriteAllText(m_Win.VM.HeaderFilePath, hppTemplate);
				filter.AddFile(m_Win.VM.HeaderFilePath);


				if (!m_Win.VM.HeaderOnly)
				{
					File.WriteAllText(m_Win.VM.SourceFilePath, cppTemplate);
					filter.AddFile(m_Win.VM.SourceFilePath);
				}

				VCProject project = (VCProject)filter.project;
				project.Save();

				m_Win.Close();

				VsShellUtilities.OpenDocument(AddSourceFile.Instance.Package, m_Win.VM.SourceFilePath);
				VsShellUtilities.OpenDocument(AddSourceFile.Instance.Package, m_Win.VM.HeaderFilePath);
			}
			else
			{
				MessageBox.Show("Error: Should never happen!");
			}
		}
	}
}
