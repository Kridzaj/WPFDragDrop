using DomainModelEditor.ViewModels;
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
using System.Windows.Shapes;

namespace DomainModelEditor.Views
{
    /// <summary>
    /// Interaction logic for AddAttributeDialog.xaml
    /// </summary>
    public partial class AddAttributeDialog : UserControl
    {

        public AddAttributeDialog()
        {
            InitializeComponent();
        }

        public AddAttributeDialog(AddAttributeDialogViewModel vm)
        {
            DataContext = vm;
            InitializeComponent();
        }

    }
}
