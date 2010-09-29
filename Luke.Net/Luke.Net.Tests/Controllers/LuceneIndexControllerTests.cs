using System.Linq;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using Luke.Net.Contollers;
using Luke.Net.Models;
using Magellan.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Luke.Net.Tests.Controllers
{
    [TestClass]
    public class LuceneIndexControllerTests
    {
        [TestMethod]
        public void LuceneIndexControllerTests_LoadIndexPath_Info_Loaded_From_Indexer()
        {
            //Arrange
            var controller = new LuceneIndexController();

            var dir = new RAMDirectory();

            var indexer = new Lucene.Net.Index.IndexWriter(dir, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
            var doc = new Document();
            doc.Add(new Field("StoredField", "This value is stored", Field.Store.YES, Field.Index.ANALYZED));
            indexer.AddDocument(doc);
            indexer.Commit();
            indexer.Close();

            var model = new ActiveIndexModel
            {
                Directory = dir
            };

            //Act
            
            var result = (PageResult)controller.LoadIndex(model);
            
            //Assert
            Assert.IsNotNull(result.Model);
            Assert.IsInstanceOfType(result.Model, typeof(LoadIndexModel));

            var indexModel = (LoadIndexModel)result.Model;
            Assert.AreEqual(1, indexModel.DocumentCount, "Only 1 document in index");
            Assert.AreEqual(1, indexModel.FieldCount, "Only 1 field was added to the document");
            Assert.AreEqual(2, indexModel.TermCount, "Only 2 terms in the index, StandardAnalyzer should remove stop words");
            Assert.AreEqual("Lucene 2.9", indexModel.IndexDetails.GenericName, "Index should be Lucene 2.9");
            Assert.AreEqual("StoredField", indexModel.Fields.First().Field, "This is the field in the index");
            indexModel.Terms.Select(x => x.Term).AssertContains(new[] { "value", "stored" });

            dir.Close();
        }
    }
}
