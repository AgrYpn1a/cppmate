using System;
using System.Windows;
using System.Windows.Input;

namespace CppMate.Source.Commands
{
	class CancelAndCloseCmd : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter) => true;

		public void Execute(object parameter)
		{
			m_Win.Close();
		}

		private Window m_Win;

		public CancelAndCloseCmd(Window win)
		{
			m_Win = win;
		}
	}
}