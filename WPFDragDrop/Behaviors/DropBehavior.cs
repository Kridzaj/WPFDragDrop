using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DomainModelEditor.Behaviors
{
   public class DropBehavior : Behavior<Canvas>
    {

        public static readonly DependencyProperty DragObjectProperty = DependencyProperty.Register(
        "DragObject", typeof(ContentPresenter), typeof(DropBehavior), new PropertyMetadata(default(ContentPresenter)));

        public ContentPresenter DragObject
        {
            get { return (ContentPresenter)GetValue(DragObjectProperty); }
            set { SetValue(DragObjectProperty, value); }
        }

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
        "Offset", typeof(Point), typeof(DropBehavior), new PropertyMetadata(default(Point)));

        public Point Offset
        {
            get { return (Point)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewMouseMove += AssociatedObject_PreviewMouseMove;
            this.AssociatedObject.PreviewMouseUp += AssociatedObject_PreviewMouseUp;
        }

        private void AssociatedObject_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}
