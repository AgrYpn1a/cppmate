using CppMate.Source.ViewModel;
using System;
using System.Windows.Input;

namespace CppMate.Source.Commands
{
	public class SaveAndRunCmd : ICommand
	{
		private CppMateInitVM m_ViewModel;

		public SaveAndRunCmd(CppMateInitVM vm)
		{
			m_ViewModel = vm;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			return m_ViewModel.SourceDirPath != string.Empty;
		}

		public void Execute(object parameter)
		{
			CppMateManager.Instance.CreateINIConfig();
			CppMateManager.Instance.Config.SourceDirectory = m_ViewModel.SourceDirPath;
			CppMateManager.Instance.SaveINIConfig();

			m_ViewModel.Close();
		}
	}
}
