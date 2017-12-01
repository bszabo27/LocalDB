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

namespace WpfApplication1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            WpfApplication1.Database1DataSet database1DataSet = ((WpfApplication1.Database1DataSet)(this.FindResource("database1DataSet")));
            // Load data into the table DEPT. You can modify this code as needed.
            WpfApplication1.Database1DataSetTableAdapters.DEPTTableAdapter database1DataSetDEPTTableAdapter = new WpfApplication1.Database1DataSetTableAdapters.DEPTTableAdapter();
            database1DataSetDEPTTableAdapter.Fill(database1DataSet.DEPT);
            System.Windows.Data.CollectionViewSource dEPTViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dEPTViewSource")));
            dEPTViewSource.View.MoveCurrentToFirst();
        }
    }
}
