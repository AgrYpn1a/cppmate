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
	public class CppMateMainVM : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public ICommand CMD_Cancel { get; private set; }
		public ICommand CMD_Create { get; private set; }

		private CppMateMain m_Win;
		public CppMateMainVM(CppMateMain win)
		{
			m_Win = win;
			CMD_Cancel = new CancelAndCloseCmd(m_Win);
		}
	}
}
