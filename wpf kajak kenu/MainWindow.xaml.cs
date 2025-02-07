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

            using StreamReader sr = new StreamReader(@"..\..\..\src\kolcsonzes.txt", Encoding.UTF8);
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


        }
        private void Ellenoriz_Click(object sender, RoutedEventArgs e)
        {
            int aktOra = (int)OraComboBox.SelectedItem;
            int aktPerc = (int)PercComboBox.SelectedItem;

            var vizHajok = kajakok
                .Where(k => k.Viz(aktOra, aktPerc))
                .Select(k => new
                {
                    k.Nev,
                    k.Azonosito,
                    k.ElvitelOraja,
                    k.ElvitelPerce,
                    k.VisszahozatalOraja,
                    k.VisszahozatalPerc,
                    KolcsonzesHossza = k.KolcsonzesHossza()
                })
                .ToList();

            if (vizHajok.Count == 0)
            {
                MessageBox.Show("Nincs olyan hajó, ami a megadott időpontban vízen volt!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            VizDataGrid.ItemsSource = vizHajok;
        }





    }
}