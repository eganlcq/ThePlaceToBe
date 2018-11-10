using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject
{
    [TestClass]
    public class AchievementPageTest
    {
        [TestMethod]
        public void RecieveUser_WithAnId_returnUserNotNUll()
        {
            // Arrange
            string idUser = "1";

            // Act
            List<User> listWithOneUser = Process.GetUser(idUser);

            // Assert
            Assert.IsNotNull(listWithOneUser);
        }

        [TestMethod]
        public void RecieveUser_WithAnId_returnPseudoOfUser()
        {

            // Arrange
            string idUser = "1";

            // Act
            List<User> listWithOneUser = Process.GetUser(idUser);

            // Assert
            Assert.AreEqual("JEJ", listWithOneUser[0].Pseudo);
        }
    }
}
