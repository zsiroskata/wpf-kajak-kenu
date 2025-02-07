using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_kajak_kenu
{
    public partial class MainWindow : Window
    {
        List<Kajak> kajakok = new();
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new StreamReader(
                path: @"..\..\..\src\kolcsonzes.txt",
                Encoding.UTF8);

            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                kajakok.Add(new Kajak(sr.ReadLine()));
            }
            sr.Close();

            VizDataGrid.ItemsSource = kajakok;

            for (int i = 0; i < 24; i++)
                OraComboBox.Items.Add(i);

            for (int i = 0; i < 60; i++)
                PercComboBox.Items.Add(i);

            OraComboBox.SelectedIndex = 0;
            PercComboBox.SelectedIndex = 0;

            foreach (var item in kajakok)
            {
                felorasok.Items.Add(item.ToString());
            }

        }
        private void Ellenoriz_Click(object sender, RoutedEventArgs e)
        {
            int aktOra = (int)OraComboBox.SelectedItem;
            int aktPerc = (int)PercComboBox.SelectedItem;

            var vizHajok = kajakok.Where(x => x.Viz(aktOra, aktPerc))
                .Select(x => new
                {
                    x.Nev,
                    x.Azonosito,
                    x.ElvitelOraja,
                    x.ElvitelPerce,
                    x.VisszahozatalOraja,
                    x.VisszahozatalPerc,
                    KolcsonzesHossza = x.Kolcsonzes()
                }).ToList();

            if (vizHajok.Count == 0)
            {
                MessageBox.Show("Nincs olyan hajó, ami a megadott időpontban vízen volt!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            VizDataGrid.ItemsSource = vizHajok;
        }

        private void BevetelButton_Click(object sender, RoutedEventArgs e)
        {
            BevetelLabel.Content = $"Bevétel: {NapiBevetel()} Ft";
        }

        private int NapiBevetel()
        {
            int osszeg = 0;
            foreach (var kajak in kajakok)
            {
                osszeg += kajak.FelOrakSzama() * 1500;
            }
            return osszeg;
        }

        private void UjHajo(Kajak ujHajo)
        {
            if (ujHajo.ErvenyesHajo(kajakok))
            {
                MessageBox.Show("Ez a hajó érvényes és már szerepel az adatbázisban!", "Érvényes hajó", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ez a hajó nem létezik az adatbázisban!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}