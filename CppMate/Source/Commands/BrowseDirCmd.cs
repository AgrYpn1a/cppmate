using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace CppMate.Source.Commands
{
	class BrowseDirCmd : ICommand
	{
		public event EventHandler CanExecuteChanged;
		public Action<string> OnDirChosen;

		public bool CanExecute(object parameter) => true;

		public void Execute(object parameter)
		{
			using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
			{
				System.Windows.Forms.DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					OnDirChosen?.Invoke(dialog.SelectedPath);
				}
			}
		}
	}
}
