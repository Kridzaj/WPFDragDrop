using System.Windows.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using DomainModelEditor.Views;
using DomainModelEditor.ViewModels;

namespace DomainModelEditor.Behavior
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool isMouseClicked = false;

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseLeftButtonDown += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown);
            this.AssociatedObject.MouseLeftButtonUp += new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonUp);
            this.AssociatedObject.PreviewDragLeave += AssociatedObject_PreviewDragLeave;
        }

        private void AssociatedObject_PreviewDragLeave(object sender, DragEventArgs e)
        {
        }

        private ContentPresenter _dragObj = null;
        private Point _offset;
        private Canvas _mainCanvas;
        private EntityView ev;


        private void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _dragObj != null)
            {
                DataObject data = new DataObject();
                data.SetData(typeof(EntityViewModel), this.AssociatedObject.DataContext);
                System.Windows.DragDrop.DoDragDrop(_dragObj, data, DragDropEffects.Move);
            }
        }

        void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = true;
             ev = (EntityView)sender;            
        }

        void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isMouseClicked = false;
            _dragObj = null;
            _mainCanvas.ReleaseMouseCapture();
        }

        void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isMouseClicked)
            {
                //set the item's DataContext as the data to be transferred
                IDragable dragObject = this.AssociatedObject.DataContext as IDragable;
                if (dragObject != null)
                {
                    DataObject data = new DataObject();
                    data.SetData(dragObject.DataType, this.AssociatedObject.DataContext);
                    System.Windows.DragDrop.DoDragDrop(this.AssociatedObject, data, DragDropEffects.Move);
                }
            }
            isMouseClicked = false;
        }
    }
}
