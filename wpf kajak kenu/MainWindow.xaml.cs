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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kajak> kajakok = new();
        public MainWindow()
        {
            InitializeComponent();

            using StreamReader sr = new StreamReader(
                path: @"..\..\..\src\kolcsonzes.txt",
                encoding: Encoding.UTF8);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                kajakok.Add(new Kajak(sr.ReadLine()));
            }
            sr.Close();


            KajakDataGrid.ItemsSource = kajakok;
            
         
        }
        private void EllenorzesGomb_Click(object sender, RoutedEventArgs e)
        {
            // Feltételezve, hogy van két TextBox az órához és perchez (OraTextBox, PercTextBox)
            if (int.TryParse(OraTextBox.Text, out int ora) && int.TryParse(PercTextBox.Text, out int perc))
            {
                List<string> vizenLevok = new List<string>();

                foreach (var kajak in kajakok)
                {
                    if (kajak.Viz(ora, perc))
                    {
                        vizenLevok.Add(kajak.Nev);
                    }
                }

                if (vizenLevok.Count > 0)
                {
                    MessageBox.Show("Vizen lévő hajók:\n" + string.Join("\n", vizenLevok));
                }
                else
                {
                    MessageBox.Show("Ebben az időpontban egy hajó sincs vízen.");
                }
            }
            else
            {
                MessageBox.Show("Hibás időpont formátum! Kérlek, adj meg egy érvényes órát és percet.");
            }
        }




    }
}