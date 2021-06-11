using CppMate.Source.ViewModel;
using System.ComponentModel;
using System.Windows;

namespace CppMate.Source.ToolWindows
{
	/// <summary>
	/// Interaction logic for CppMateInit.xaml
	/// </summary>
	public partial class CppMateAddFolder : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public CppMateInitVM VM { private set; get; }

		private string m_FolderName;
		public string FolderName
		{
			get => m_FolderName;
			set
			{
				m_FolderName = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(m_FolderName)));
			}
		}

		public bool Confirmed { get; set; }

		public CppMateAddFolder()
		{
			InitializeComponent();
			this.DataContext = this;

			//VM = new CppMateInitVM();
			//this.DataContext = new CppMateInitVM();
		}

		public void BtnCreate(object sender, RoutedEventArgs e)
		{
			Confirmed = true;
			this.Close();
		}

		public void BtnCancel(object sender, RoutedEventArgs e)
		{
			Confirmed = false;
			this.Close();
		}
	}
}
