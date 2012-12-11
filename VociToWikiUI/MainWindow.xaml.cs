using System;
using System.Collections.Generic;
using System.IO;
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

namespace VociToWikiUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void openFile_Click(object sender, RoutedEventArgs e)
		{
			// Create OpenFileDialog
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".txt";
			dlg.Filter = "Text documents (.txt)|*.txt";

			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = dlg.ShowDialog();

			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				// Open document 
				string filename = dlg.FileName;

				FileNameTextBox.Text = filename;
			}
		}

		private void buttonCopyToClipboard_Click(object sender, RoutedEventArgs e)
		{
			string tableMarkup = this.Convert();
			if (tableMarkup != string.Empty)
			{
				Clipboard.SetText(tableMarkup);
			}
		}

		private string Convert()
		{
			string ergebnis = string.Empty;
			string[] trennzeichen = { "|", ";" };
			ergebnis += "{| {{table}}\n";

			ergebnis += "|-\n";
			ergebnis += "|'''Sprache1'''||'''Sprache2'''\n";

			if (!File.Exists(FileNameTextBox.Text))
			{
				MessageBox.Show("Dude that's wrong!");
				return "";
			}

			foreach (string row in streamToArray(FileNameTextBox.Text))
			{
				string rowRepaced = string.Empty;
				rowRepaced = row;
				foreach (string zeichen in trennzeichen)
				{
					rowRepaced = rowRepaced.Replace(zeichen, "||");
				}

				ergebnis += "|-\n";
				ergebnis += "|" + rowRepaced + "\n";
			}
			ergebnis += "|}\n";
			return ergebnis;
		}

		static List<string> streamToArray(string path)
		{
			List<string> worterListe = new List<string>();
			string line = string.Empty;
			StreamReader stream = new StreamReader(path);

			while ((line = stream.ReadLine()) != null)
			{
				worterListe.Add(line);
			}

			return worterListe;
		}
	}
}
