using DomainModelEditor.ViewModels;
using DomainModelEditor.Views;
using WPFDragDrop.DataAccess;
using WPFDragDrop.Entities;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DomainModelEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AddEntityDialog, AddEntityDialogViewModel>();
            containerRegistry.RegisterDialog<AddAttributeDialog, AddAttributeDialogViewModel>();
            containerRegistry.Register<DbContext>(() =>
            {
                //var options = // Configure your DbContextOptions here
                return new IADbContext();
            });
            containerRegistry.Register<IRepository<Entity>>(() =>
            {
               return new EntityRepository((DbContext)Container.Resolve(typeof(DbContext)));
            });
            containerRegistry.Register<IRepository<EntityAttribute>>(() =>
            {
                return new EntityAttributeRepository((DbContext)Container.Resolve(typeof(DbContext)));
            });
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }
    }
}
