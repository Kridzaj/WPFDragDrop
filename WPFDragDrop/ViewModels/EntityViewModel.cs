using WPFDragDrop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DomainModelEditor.ViewModels
{
    public class EntityViewModel : ViewModelBase
    {

        public event EventHandler CoordinatesChanged;

        public EntityViewModel()
        {
            Model = new Entity();
            FontWeight = FontWeights.Normal;
             
        }

        protected void OnCoordinatesChanged(EventArgs e)
        {
            EventHandler handler = CoordinatesChanged;
            handler?.Invoke(this, e);
        }

        public EntityViewModel(Entity e)
        {
            Model = e;
            FontWeight = FontWeights.Normal;
        }
        public Entity Model { get; set; }

        public int? ID
        {
            get
            {
                return Model?.ID;
            }
        }

        private double _x;

        public double NewX
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
               
            }
        }

        private double _y;

        public double NewY
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnCoordinatesChanged(null);
            }
        }

        private FontWeight _fontWeight;
        public FontWeight FontWeight
        {
            get
            {
                return _fontWeight;
            }
            set
            {
                _fontWeight = value;
                OnPropertyChanged(nameof(FontWeight));
            }
        }
    }
}
