using CppMate.Source.Attributes;
using CppMate.Source.Config;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace CppMate.Source
{
	public class CppMateManager
	{
		#region Singleton Implementation
		private static CppMateManager m_Instance = null;
		private static readonly object m_Lock = new object();

		public static CppMateManager Instance
		{
			get
			{
				lock (m_Lock)
				{
					if (m_Instance == null)
					{
						m_Instance = new CppMateManager();
					}

					return m_Instance;
				}
			}
		}
		#endregion

		public INIConfig Config { get; private set; }
		private string m_SolutionDir;

		CppMateManager()
		{
		}

		public INIConfig LoadINIConfig(string dir)
		{
			m_SolutionDir = dir;

			string line = null;
			INIConfig config = new INIConfig();

			try
			{
				var fileReader = new StreamReader(Path.Combine(dir, "cppmate.ini"));

				while ((line = fileReader.ReadLine()) != null)
				{
					HandleConfigLine(ref config, line);
				}

				fileReader.Close();
			}
			catch (Exception e)
			{
				return null;
			}

			// Set the config
			return Config = config;
		}

		public void CreateINIConfig()
		{
			if (Config == null)
			{
				Config = new INIConfig();
			}
		}

		public void SaveINIConfig()
		{
			try
			{
				var fileWriter = new StreamWriter(Path.Combine(m_SolutionDir, "cppmate.ini"));

				{ // Source Directory
					string configLine = $"{AttributesHelper.Get<ININameAttribute>(typeof(INIConfig).GetMember(nameof(Config.SourceDirectory))[0]).Name} = {Config.SourceDirectory}";
					fileWriter.WriteLine(configLine);
				}

				{ // Another config line

				}

				fileWriter.Close();
			}
			catch (Exception e)
			{
				MessageBox.Show("Failed to create ini config.", "Error");
			}
		}

		private void HandleConfigLine(ref INIConfig config, string line)
		{
			string attSourceDirectoryName = AttributesHelper.Get<ININameAttribute>(typeof(INIConfig).GetMember(nameof(Config.SourceDirectory))[0]).Name;
			if (line.Contains(attSourceDirectoryName))
			{
				string value = ParseStringValue(line);
				if (value != null)
				{
					config.SourceDirectory = value;
				}

				return;
			}
		}

		private string GetConfigLineString(object obj)
		{
			return AttributesHelper.Get<ININameAttribute>(obj.GetType()).Name;
		}

		private string ParseStringValue(string line)
		{
			try
			{
				string noWSLine = String.Concat(line.Where(c => !Char.IsWhiteSpace(c)));
				string value = noWSLine.Split('=')[1];

				return value;
			}
			catch (Exception e)
			{
				MessageBox.Show("[!ERROR!] Invalid configuration file format.");
				return null;
			}
		}
	}
}
