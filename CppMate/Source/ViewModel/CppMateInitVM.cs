using CppMate.Source.Commands;
using CppMate.Source.ToolWindows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CppMate.Source.ViewModel
{
	public class CppMateInitVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private string m_SourceDirPath;
		public string SourceDirPath
		{
			get => m_SourceDirPath;
			set
			{
				m_SourceDirPath = value;
				OnPropertyChanged(nameof(SourceDirPath));
			}
		}

		private CppMateInit m_View;

		public ICommand CMD_SaveAndRun { get; private set; }
		public ICommand CMD_BrowseSourceDir { get; private set; }

		public CppMateInitVM(CppMateInit view)
		{
			m_View = view;

			SourceDirPath = "C:\\";
			CMD_SaveAndRun = new SaveAndRunCmd(this);

			CMD_BrowseSourceDir = new BrowseDirCmd();
			((BrowseDirCmd)CMD_BrowseSourceDir).OnDirChosen = (dir) => { SourceDirPath = dir; };
		}

		public void Close()
		{
			m_View.Close();
		}
	}
}
