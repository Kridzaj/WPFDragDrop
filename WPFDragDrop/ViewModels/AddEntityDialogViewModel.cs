using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModelEditor.ViewModels
{
    public class AddEntityDialogViewModel : DialogViewModelBase
    {
        private string _entityName = "";

        public string EntityName
        {
            get { return _entityName; }
            set
            {
                _entityName = value;
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public string[] ExistingValues { get; set; }

        public AddEntityDialogViewModel()
        {
            this.Title = "Add new Entity";
        }

        protected override bool ConfirmCheck()
        {
            return _entityName?.Length > 0 && !ExistingValues.Contains(_entityName);
        }

        protected override void OnConfirm()
        {
            DialogParameters dp = new DialogParameters();
            dp.Add(nameof(EntityName), EntityName);
            dp.Add(nameof(X), X);
            dp.Add(nameof(Y), X);
            DialogResult dr = new DialogResult(ButtonResult.OK, dp);
            RaiseRequestClose(dr);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            ExistingValues = parameters.GetValue<string[]>(nameof(ExistingValues));
            OnPropertyChanged(nameof(ExistingValues));
            X = parameters.GetValue<int>(nameof(X));
            Y = parameters.GetValue<int>(nameof(Y));
        }

    }
}
