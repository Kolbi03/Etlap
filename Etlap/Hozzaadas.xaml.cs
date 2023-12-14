using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for Hozzaadas.xaml
    /// </summary>
    public partial class Hozzaadas : Window
    {
        private FoodService service;
        public Hozzaadas(FoodService foodService)
        {
            InitializeComponent();
            this.service = foodService;
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Etel etel = createEtel();
				if (this.service.Create(etel))
				{
					MessageBox.Show("Sikeres felvétel!");
					textBoxNev.Text = "";
					textBoxLeiras.Text = "";
					radioEloetel.IsChecked = false;
					radioFoetel.IsChecked = false;
					radioDesszert.IsChecked = false;
					textBoxAr.Text = "";
				}
				else
				{
					MessageBox.Show("Hiba történt a felvétel során!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private Etel createEtel()
		{
			string name = textBoxNev.Text.Trim();
			string description = textBoxLeiras.Text.Trim();
			string category = "";
			string priceString = textBoxAr.Text.Trim();

			if ((bool)radioEloetel.IsChecked)
			{
				category = "előétel";
			}
			else if ((bool)radioFoetel.IsChecked)
			{
				category = "főétel";
			}
			else if ((bool)radioDesszert.IsChecked)
			{
				category = "desszert";
			}

			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("A név nem lehet üres!");
			}
			if (string.IsNullOrEmpty(description))
			{
				throw new Exception("A leírás nem lehet üres!");
			}
			if (!int.TryParse(priceString, out int price))
			{
				throw new Exception("Az ár csak szám lehet!");
			}
			if(price < 0)
			{
				throw new Exception("Az ár csak pozitív szám lehet!");
			}
			if (!(bool)radioEloetel.IsChecked && !(bool)radioFoetel.IsChecked && !(bool)radioDesszert.IsChecked)
			{
				throw new Exception("A kategória kiválasztása kötelező!");
			}

			Etel etel = new Etel();
			etel.Name = name;
			etel.Description = description;
			etel.Price = price;
			etel.Category = category;

			return etel;

		}
    }
}
