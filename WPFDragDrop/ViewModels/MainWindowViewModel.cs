using DomainModelEditor.Views;
using WPFDragDrop.DataAccess;
using WPFDragDrop.Entities;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DomainModelEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDialogService _dialogService;

        public DelegateCommand AddEntityCommand { get; private set; }
        public DelegateCommand ClearSelectionCommand { get; set; }
        public DelegateCommand SavePositionCommand { get; set; }
        public DelegateCommand AddAttributeCommand { get; private set; }
        private IRepository<Entity> _entityRepo;
        private IRepository<EntityAttribute> _entityAttrRepo;
        #region PublicProps

        public ObservableCollection<EntityViewModel> Items { get; set; }

        /// <summary>
        /// Gets true if ViewModel has and Entity selected
        /// </summary> 
        public bool HasSelection
        {
            get { return _selectedEntity != null; }
        }

        private EntityViewModel _selectedEntity;

        /// <summary>
        /// Selected entity on screen
        /// </summary>
        public EntityViewModel SelectedEntity
        {
            get { return _selectedEntity; }
            set
            {
                _selectedEntity = value;
                foreach (var item in Items)
                {
                    item.FontWeight = FontWeights.Normal;
                }
                if (_selectedEntity != null)
                {
                    _selectedEntity.FontWeight = FontWeights.Bold;
                }
                OnPropertyChanged(nameof(HasSelection));
                OnPropertyChanged(nameof(SelectedEntity));
                AddAttributeCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        public MainWindowViewModel(IDialogService dialogService, IRepository<Entity> entityRepo, IRepository<EntityAttribute> entityAttrRepo)
        {
            _dialogService = dialogService;
            Items = new ObservableCollection<EntityViewModel>();
            _entityRepo = entityRepo;
            _entityAttrRepo = entityAttrRepo;

            AddEntityCommand = new DelegateCommand(AddEntity);
            ClearSelectionCommand = new DelegateCommand(ClearSelection);
            SavePositionCommand = new DelegateCommand(SavePosition);
            AddAttributeCommand = new DelegateCommand(AddAttribute, CanAddAttribute);

            LoadData();
        }

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<EntityViewModel>();

            AddEntityCommand = new DelegateCommand(AddEntity);
            ClearSelectionCommand = new DelegateCommand(ClearSelection);
            SavePositionCommand = new DelegateCommand(SavePosition);
            AddAttributeCommand = new DelegateCommand(AddAttribute, CanAddAttribute);
            LoadData();
        }


        #region Private

        private void ClearSelection()
        {
            this.SelectedEntity = null;
            AddAttributeCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Shows add attribute dialog
        /// </summary>
        private void AddAttribute()
        {
            string[] existing = new string[_selectedEntity.Model.Attributes.Count];
            existing = _selectedEntity.Model.Attributes.Select(c => c.Name).ToArray();

            DialogParameters prms = new DialogParameters();
            prms.Add("ExistingValues", existing);
            prms.Add("Entity", _selectedEntity);
            _dialogService.ShowDialog(nameof(AddAttributeDialog), prms,
                r =>
                {
                    if (r.Result == ButtonResult.OK)
                    {
                        var name = r.Parameters.GetValue<string>("AttributeName");
                        var dataType = r.Parameters.GetValue<string>("DataType");
                        AddAttributeToEntity(_selectedEntity, name, dataType);
                    }
                }
                );
        }

        private bool CanAddAttribute()
        {
            return _selectedEntity != null;
        }




        #endregion

        /// <summary>
        /// Load data from DB
        /// </summary>
        public void LoadData()
        {
            Items.Clear();
            try
            {

                var entities = _entityRepo.QueryInclude(nameof(Entity.Attributes));
                foreach (var item in entities)
                {
                    EntityViewModel evm = new EntityViewModel(item);
                    evm.CoordinatesChanged += Evm_CoordinatesChanged;
                    Items.Add(evm);
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error loading data");
                throw;
            }

        }

        private void Evm_CoordinatesChanged(object sender, EventArgs e)
        {
            SavePosition();
        }



        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="name">Entity name</param>
        /// <param name="x">X coordinate for positioning on screen</param>
        /// <param name="y">Y coordinate for positioning on screen</param>
        public void AddEntity(string name, double x, double y)
        {
            try
            {

                Entity e = new Entity
                {
                    Name = name,
                    X = x,
                    Y = y
                };

                e = _entityRepo.Create(e);
                Items.Add(new EntityViewModel(e));
                OnPropertyChanged(nameof(Items));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving entity data");
                throw;
            }
            logger.Info($"Created entity {name}");
        }

        /// <summary>
        /// Shows add entity dialog
        /// </summary>
        private void AddEntity()
        {
            string[] existing = new string[Items.Count];
            existing = Items.Select(c => c.Model.Name).ToArray();

            int x = 10; int y = 10;
            if (Items.Count > 0)
            {
                x += (int)Items.Average(c => c.Model.X);
                y += (int)Items.Average(c => c.Model.Y);
            }

            DialogParameters prms = new DialogParameters();
            prms.Add("ExistingValues", existing);
            prms.Add("X", x);
            prms.Add("Y", y);
            _dialogService.ShowDialog(nameof(AddEntityDialog), prms,
                r =>
                {
                    if (r.Result == ButtonResult.OK)
                    {
                        var name = r.Parameters.GetValue<string>("EntityName");
                        var Xcoord = r.Parameters.GetValue<int>("X");
                        var Ycoor = r.Parameters.GetValue<int>("Y");
                        AddEntity(name, Xcoord, Ycoor);
                    }
                }
                );
        }

        /// <summary>
        /// Saves current item's position to DB
        /// </summary>
        public void SavePosition()
        {
            if (SelectedEntity == null)
            {
                return;
            }
            SavePosition(SelectedEntity.Model, SelectedEntity.NewX, SelectedEntity.NewY);
        }

        /// <summary>
        /// Save current position of control in DB
        /// </summary>
        /// <param name="ent">Entity</param>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        public void SavePosition(Entity ent, double x, double y)
        {
            try
            {

                ent.X = x;
                ent.Y = y;
                _entityRepo.Update(ent);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving position data");
                throw;
            }
        }

        /// <summary>
        /// Adds and attribute for selected entity
        /// </summary>
        /// <param name="entityVM">EntityViewModel</param>
        /// <param name="name">Name of new attribute</param>
        /// <param name="dataType">DataType of new attribute</param>
        /// <param name="order">Order number</param>
        /// <returns></returns>
        public Entity AddAttributeToEntity(EntityViewModel entityVM, string name, string dataType)
        {
            var entity = entityVM.Model;
            try
            {

                EntityAttribute attribute = new EntityAttribute
                {
                    Name = name,
                    EntityId = entity.ID,
                    DataType = dataType
                };
                attribute = _entityAttrRepo.Create(attribute);

                entity.Attributes.Add(attribute);
                Items.Remove(entityVM);
                Items.Add(entityVM);
                logger.Info($"Created attribute {name} entity {entityVM.Model.Name}");

                return entity;

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error saving entity attribute data");
                throw;
            }
        }
    }
}
