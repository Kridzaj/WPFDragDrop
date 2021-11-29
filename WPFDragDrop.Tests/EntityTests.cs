using System;
using System.Linq;
using DomainModelEditor;
using WPFDragDrop.DataAccess;
using WPFDragDrop.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewAssessmentTests
{
    [TestClass()]
    public class EntityTests
    {
        [TestCleanup]
        public void CleanUp()
        {
            using (var db = new IADbContext())
            {
                var rep = new EFRepository<Entity>(db);
                var ent = rep.Query().Where(c => c.Name == "Test").FirstOrDefault();
                if (ent != null)
                {
                    rep.Delete(ent);
                }

                ent = rep.Query().Where(c => c.ID == 2).FirstOrDefault();
                if (ent != null)
                {
                    ent.X = 100;
                    ent.Y = 100;
                    rep.Update(ent);
                }
            }
        }



        [TestMethod]
        public void Entity_InsertTest()
        {
            try
            {
                using (var db = new IADbContext())
                {
                    var rep = new EFRepository<Entity>(db);
                    var newEnt = new Entity
                    {
                        Name = "Test",
                        X = 100,
                        Y = 90
                    };
                    var savedEnt = rep.Create(newEnt);

                    Assert.AreEqual("Test", savedEnt.Name);
                    Assert.AreEqual(100, savedEnt.X);
                    Assert.AreEqual(90, savedEnt.Y);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        [TestMethod]
        public void Entity_UpdateTest()
        {
            try
            {
                using (var db = new IADbContext())
                {
                    var rep = new EFRepository<Entity>(db);
                    var ent = rep.GetById(2);
                    Assert.IsNotNull(ent, "Error Get by ID");

                    ent.X = 300;
                    ent.Y = 200;
                    rep.Update(ent);

                    ent = rep.GetById(2);
                    Assert.AreEqual(300, ent.X, "Error updating X coordinate");
                    Assert.AreEqual(200, ent.Y, "Error updating Y coordinate");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        [TestMethod]
        public void Entity_LoadRelatedTest()
        {
            try
            {
                using (var db = new IADbContext())
                {
                    var _rep = new EFRepository<Entity>(db);
                    var ent = _rep.GetByIdWithInclude(typeof(Entity).GetProperty("ID"), 2, "Attributes");
                    Assert.IsNotNull(ent, "Error Get by ID");

                    Assert.AreEqual(3, ent.Attributes.Count);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

    }
}