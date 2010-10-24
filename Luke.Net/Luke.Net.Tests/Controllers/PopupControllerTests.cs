//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Luke.Net.Contollers;
//using Magellan.Framework;
//using Luke.Net.Models;

//namespace Luke.Net.Tests.Controllers
//{
//    [TestClass]
//    public class PopupControllerTests
//    {
//        [TestMethod]
//        public void PopupControllerTests_OpenIndex_Returns_Default_View()
//        {
//            //Arrange
//            var controller = new PopupController();
            
//            //Act
//            var result = (PageResult)controller.OpenIndex();

//            //Assert
//            Assert.AreEqual("OpenIndex", result.ViewName);
//            Assert.IsInstanceOfType(result.Model, typeof(IndexModel));
//        }
//    }
//}
