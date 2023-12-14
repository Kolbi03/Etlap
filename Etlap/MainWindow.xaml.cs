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

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public FoodService service;
		public MainWindow()
		{
			InitializeComponent();
			this.service = new FoodService();
			Read();
		}

		private void Read()
		{
			dataGridMenu.ItemsSource = this.service.GetAll();
		}

		private void buttonEmelesForint_Click(object sender, RoutedEventArgs e)
		{
			if (textBoxEmelesForint.Text.Trim() == "")
			{
				MessageBox.Show("Az érték megadása kötelező!");
				return;
			}

			Etel selectedEtel = dataGridMenu.SelectedItem as Etel;
			int forint = int.Parse(textBoxEmelesForint.Text.Trim());
			if (selectedEtel == null)
			{
				List<Etel> list = new List<Etel>();
				list = dataGridMenu.Items.OfType<Etel>().ToList();

				foreach(var item in list)
				{
					service.UpdateForint(item.Id, forint, item);
				}
			}
			else
			{
				service.UpdateForint(selectedEtel.Id, forint, selectedEtel);
			}

			textBoxEmelesForint.Text = "";
			Read();
		}

		private void addButton_Click(object sender, RoutedEventArgs e)
		{
			Hozzaadas hozzaadas = new Hozzaadas(service);
			hozzaadas.Closed += (_, _) => Read();
			hozzaadas.ShowDialog();
		}

		private void buttonEmelesSzazalek_Click(object sender, RoutedEventArgs e)
		{
			if (textBoxEmelesSzazalek.Text.Trim() == "")
			{
				MessageBox.Show("Az érték megadása kötelező!");
				return;
			}

			Etel selectedEtel = dataGridMenu.SelectedItem as Etel;
			int szazalek = int.Parse(textBoxEmelesSzazalek.Text.Trim());

			if(selectedEtel == null)
			{

				List<Etel> list = new List<Etel>();
				list = dataGridMenu.Items.OfType<Etel>().ToList();



				foreach (var item in list)
				{
					service.UpdateSzazalek(item.Id, szazalek, item);
				}
			}
			else
			{
				service.UpdateSzazalek(selectedEtel.Id, szazalek, selectedEtel);
			}

			textBoxEmelesSzazalek.Text = "";
			Read();


		}

		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			Etel selected = dataGridMenu.SelectedItem as Etel;
			if (selected == null)
			{
				MessageBox.Show("Nincs kiválasztva étel!");
				return;
			}

			MessageBoxResult result = MessageBox.Show($"Biztos törölni szeretnéd a következő ételt: {selected.Name}?",
				"Törlés", MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				if (this.service.Delete(selected.Id))
				{
					MessageBox.Show("A törlés sikeres!");
				}
				else
				{
					MessageBox.Show("A törlés sikertelen!");
				}
				Read();
			}
		}
	}
}
