using Prism.Services.Dialogs;
using System.Linq;

namespace DomainModelEditor.ViewModels
{
    public class AddAttributeDialogViewModel : DialogViewModelBase
    {
        public EntityViewModel Entity { get; set; }
        public string[] ExistingValues { get; set; }
        private string _attrName = "";

        public string AttributeName
        {
            get { return _attrName; }
            set
            {
                _attrName = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _dataType = "";

        public string DataType
        {
            get { return _dataType; }
            set
            {
                _dataType = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public AddAttributeDialogViewModel()
        {
            this.Title = "Add new Attribute";
            this.ExistingValues = new string[0];
        }

        protected override bool ConfirmCheck()
        {
            bool exist = ExistingValues.Contains(_attrName);
            return _dataType?.Length > 0 && _attrName?.Length > 0 && !exist;
        }

        protected override void OnConfirm()
        {
            DialogParameters dp = new DialogParameters();
            dp.Add(nameof(AttributeName), AttributeName);
            dp.Add(nameof(DataType), DataType);
            dp.Add(nameof(Entity), Entity);
            DialogResult dr = new DialogResult(ButtonResult.OK, dp);
            RaiseRequestClose(dr);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            ExistingValues = parameters.GetValue<string[]>(nameof(ExistingValues));
            OnPropertyChanged(nameof(ExistingValues));
            Entity = parameters.GetValue<EntityViewModel>(nameof(Entity));
            OnPropertyChanged(nameof(Entity));
        }

    }
}
