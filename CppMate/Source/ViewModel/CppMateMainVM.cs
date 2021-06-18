using CppMate.Source.Commands;
using CppMate.Source.ToolWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CppMate.Source.ViewModel
{
	public class CppMateMainVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand CMD_Cancel { get; private set; }
		public ICommand CMD_Create { get; private set; }

		private Visibility m_CppVisible;
		public Visibility CppVisible
		{
			get => m_CppVisible;
			set
			{
				m_CppVisible = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CppVisible)));
			}
		}

		private bool m_HeaderOnly;
		public bool HeaderOnly
		{
			get => m_HeaderOnly;
			set
			{
				m_HeaderOnly = value;
				if (value)
				{
					CppVisible = Visibility.Collapsed;
				}
				else
				{
					CppVisible = Visibility.Visible;
				}

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeaderOnly)));
			}
		}

		private string m_CurrentSelectedFolder;
		public string CurrentSelectedFolder
		{
			get => m_CurrentSelectedFolder;
			set
			{
				m_CurrentSelectedFolder = value;
				SourceFilePath = Path.Combine(value, ClassName + ".cpp");
				HeaderFilePath = Path.Combine(value, ClassName + ".h");
			}
		}

		private string m_SourceFilePath;
		public string SourceFilePath
		{
			get => m_SourceFilePath;
			set
			{
				m_SourceFilePath = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SourceFilePath)));
			}
		}

		private string m_HeaderFilePath;
		public string HeaderFilePath
		{
			get => m_HeaderFilePath; set
			{
				m_HeaderFilePath = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeaderFilePath)));
			}
		}

		private string m_ClassName = "MyNewCppClass";
		public string ClassName
		{
			get => m_ClassName;
			set
			{
				m_ClassName = value;
				if (UseLowerCase)
				{
					SourceFilePath = Path.Combine(CurrentSelectedFolder, ClassName.ToLower() + ".cpp");
					HeaderFilePath = Path.Combine(CurrentSelectedFolder, ClassName.ToLower() + ".h");
				}
				else
				{
					SourceFilePath = Path.Combine(CurrentSelectedFolder, ClassName + ".cpp");
					HeaderFilePath = Path.Combine(CurrentSelectedFolder, ClassName + ".h");
				}
			}
		}

		private bool m_UseLowerCase;
		public bool UseLowerCase
		{
			get => m_UseLowerCase;
			set
			{
				m_UseLowerCase = value;
				ClassName = ClassName; // Refresh
			}
		}


		private CppMateMain m_Win;
		public CppMateMainVM(CppMateMain win)
		{
			m_Win = win;
			CMD_Cancel = new CancelAndCloseCmd(m_Win);
			CMD_Create = new CreateFileCmd(m_Win);
		}
	}
}
