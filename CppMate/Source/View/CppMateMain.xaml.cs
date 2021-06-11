using CppMate.Source.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CppMate.Source.ToolWindows
{
	/// <summary>
	/// Interaction logic for CppMateInit.xaml
	/// </summary>
	public partial class CppMateMain : Window
	{
		public CppMateMainVM VM { private set; get; }

		public CppMateMain()
		{
			InitializeComponent();

			VM = new CppMateMainVM(this);
			this.DataContext = VM;
		}
	}
}
