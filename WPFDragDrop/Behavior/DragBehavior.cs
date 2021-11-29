using DomainModelEditor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace DomainModelEditor.Behavior
{
    public class LBDragBehavior : Behavior<UserControl>
    {
        protected bool isDragging;
        private Point clickPosition;
        private TranslateTransform originTT;

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDragging = false;
            var draggable = sender as EntityView;

           var _mainCanvas = (Canvas)draggable.GetSelfAndAncestors().Where(c => c.GetType() == typeof(Canvas)).First();
            var p = draggable.TranslatePoint(new Point(0, 0), _mainCanvas);
            draggable.X = p.X;
            draggable.Y = p.Y;

            draggable.ReleaseMouseCapture();

        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var draggableControl = sender as UIElement;
            originTT = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
            isDragging = true;
            clickPosition = e.GetPosition(Application.Current.MainWindow);
            draggableControl.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var draggableControl = sender as UIElement;
            if (isDragging && draggableControl != null)
            {
                Point currentPosition = e.GetPosition(Application.Current.MainWindow);
                var transform = draggableControl.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = originTT.X + (currentPosition.X - clickPosition.X);
                transform.Y = originTT.Y + (currentPosition.Y - clickPosition.Y);
               
                draggableControl.RenderTransform = new TranslateTransform(transform.X, transform.Y);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;

        }
    }
}
