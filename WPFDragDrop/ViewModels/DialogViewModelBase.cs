using Prism.Commands;
using Prism.Services.Dialogs;
using System;

namespace DomainModelEditor.ViewModels
{
    public class DialogViewModelBase : ViewModelBase, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public string Title { get; set; }

        public DialogViewModelBase()
        {
            AddCommand = new DelegateCommand(Confirm, CanConfirm);
            CancelCommand = new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            DialogResult dr = new DialogResult(ButtonResult.Cancel);
            RaiseRequestClose(dr);
        }

        private bool CanConfirm()
        {
            return ConfirmCheck();
        }

        protected virtual bool ConfirmCheck()
        {
            return true;
        }

        protected void Confirm()
        {
            OnConfirm();
        }

        protected virtual void OnConfirm()
        {
            DialogResult dr = new DialogResult(ButtonResult.OK);
            RaiseRequestClose(dr);
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
