using System;
using System.Linq;
using DomainModelEditor.ViewModels;
using WPFDragDrop.DataAccess;
using WPFDragDrop.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewAssessmentTests
{
    [TestClass]
    public class MainVMTest
    {
        [TestCleanup]
        public void CleanUp()
        {
            using (var db = new IADbContext())
            {
                var rep = new EFRepository<Entity>(db);
                var ent = rep.Query().Where(c => c.Name == "TestEntity").FirstOrDefault();
                if (ent != null)
                {
                    rep.Delete(ent);
                }
            }
        }

        [TestMethod]
        public void RepositionTest()
        {
            try
            {
                var vm = new MainWindowViewModel();
                vm.SelectedEntity = vm.Items.Where(c => c.ID == 1).First();
                vm.SelectedEntity.NewX = 100;
                vm.SelectedEntity.NewY = 200;

                vm.SavePosition();

                using (var db = new IADbContext())
                {
                    var _rep = new EFRepository<Entity>(db);
                    var ent = _rep.GetById(1);
                    Assert.AreEqual(100, ent.X, "X Position not updated");
                    Assert.AreEqual(200, ent.Y, "Y Position not updated");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void LoadEntitiesTest()
        {
            try
            {
                var vm = new MainWindowViewModel();
                Assert.AreEqual(5, vm.Items.Count, "Failed loading all entities");
                Assert.AreEqual(1, vm.Items.Where(c => c.ID == 4).First().Model.Attributes.Count, "Failed loading entity attributes");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void AddEntityTest()
        {
            try
            {
                var vm = new MainWindowViewModel();
                vm.AddEntity("TestEntity", 50, 80);

                using (var db = new IADbContext())
                {
                    var _rep = new EFRepository<Entity>(db);
                    var ent = _rep.Query().Where(c => c.Name == "TestEntity").FirstOrDefault();

                    Assert.IsNotNull(ent, "Failed creating entity");
                    Assert.AreEqual(50, ent.X, "X Position not saved");
                    Assert.AreEqual(80, ent.Y, "Y Position not saved");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void AddEntityAttributeTest()
        {
            try
            {
                var vm = new MainWindowViewModel();
                var evm = vm.Items.Where(c => c.ID == 1).First();
                Assert.IsNotNull(evm, "Failed fetching entity VM ");

                vm.AddAttributeToEntity(evm, "TestAttribute", "boolean");

                using (var db = new IADbContext())
                {
                    var _rep = new EFRepository<EntityAttribute>(db);
                    var ent = _rep.Query().Where(c => c.Name == "TestAttribute" && c.EntityId == 1).FirstOrDefault();

                    Assert.IsNotNull(ent, "Failed creating entity attribute");
                    Assert.AreEqual("boolean", ent.DataType, "Data Type not saved");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
