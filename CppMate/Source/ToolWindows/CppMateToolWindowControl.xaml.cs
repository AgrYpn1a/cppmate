using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace CppMate
{
	public partial class CppMateToolWindowControl : UserControl
	{
		public CppMateToolWindowControl()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Button clicked");
		}
	}
}