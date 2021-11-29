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
    public class DragBehavior : Behavior<UserControl>
    {
        private bool isMouseClicked = false;

        public static readonly DependencyProperty DragObjectProperty = DependencyProperty.Register(
        "DragObject", typeof(ContentPresenter), typeof(DragBehavior), new PropertyMetadata(default(ContentPresenter)));

        public ContentPresenter DragObject
        {
            get { return (ContentPresenter)GetValue(DragObjectProperty); }
            set { SetValue(DragObjectProperty, value); }
        }

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
        "Offset", typeof(Point), typeof(DragBehavior), new PropertyMetadata(default(Point)));

        public Point Offset
        {
            get { return (Point)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isMouseClicked = false;
            this.DragObject = sender as ContentPresenter;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        }

    }
}
