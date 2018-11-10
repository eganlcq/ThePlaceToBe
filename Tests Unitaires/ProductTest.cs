using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject
{
    [TestClass]
    public class TestProductPage
    {
        [TestMethod]
        public void GetBeerTypes_ReturnsListOfStringNotNull()
        {
            // Arrange

            // Act
            List<string> check = Process.GetBeerTypes();

            // Assert
            Assert.IsNotNull(check);
        }
    }
}
