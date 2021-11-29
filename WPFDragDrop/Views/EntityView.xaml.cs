using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace DomainModelEditor.Views
{
    /// <summary>
    /// Interaction logic for EntityView.xaml
    /// </summary>
    public partial class EntityView : UserControl
    {
        public EntityView()
        {
            InitializeComponent();
        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
           nameof(Y),
           typeof(double),
           typeof(EntityView),
           new PropertyMetadata(default(double)) );

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
          nameof(X),
          typeof(double),
          typeof(EntityView),
          new PropertyMetadata(default(double)));

      
    }
}
