
using WPFDragDrop.DataAccess;
using WPFDragDrop.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace InterviewAssessmentTests
{
    [TestClass]
    public class EntityAttributeTests
    {
        [TestCleanup]
        public void CleanUp()
        {
            using (var db = new IADbContext())
            {
                var rep = new EFRepository<EntityAttribute>(db);
                var ent = rep.Query().Where(c => c.Name == "Test" && c.EntityId == 5).FirstOrDefault();
                if (ent != null)
                {
                    rep.Delete(ent);
                }

                var repAtt = new EFRepository<EntityAttribute>(db);
                var att = repAtt.Query().Where(c => c.Name == "TestAttribute" && c.EntityId == 1).FirstOrDefault();
                if (att != null)
                {
                    repAtt.Delete(att);
                }
            }
        }

        [TestMethod]
        public void EntityAttribute_InsertTest()
        {
            try
            {
                using (var db = new IADbContext())
                {
                    var _rep = new EFRepository<EntityAttribute>(db);
                    var newEnt = new EntityAttribute
                    {
                        Name = "Test",
                        DataType = "TypeTest",
                        EntityId = 5
                    };
                    var savedEnt = _rep.Create(newEnt);

                    Assert.AreEqual("Test", savedEnt.Name);
                    Assert.AreEqual("TypeTest", savedEnt.DataType);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        [TestMethod]
        public void EntityAttribute_UpdateTest()
        {
            try
            {
                using (var db = new IADbContext())
                {
                    var _rep = new EFRepository<EntityAttribute>(db);
                    var ent = _rep.GetById(2);
                    Assert.IsNotNull(ent, "Error Get by ID");

                    ent.Name = "AttrTest";
                    ent.DataType = "TypeTest";
                    _rep.Update(ent);

                    ent = _rep.GetById(2);
                    Assert.AreEqual("AttrTest", ent.Name, "Error updating Name");
                    Assert.AreEqual("TypeTest", ent.DataType, "Error updating DataType");
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }
    }
}
