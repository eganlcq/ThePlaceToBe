using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject
{
    [TestClass]
    public class TestConnexion
    {
        [TestMethod]
        public void CheckConnexion_WithGoodPseudoAndGoodPassword_ReturnTrue()
        {
            // Arrange
            string pseudo = "Octofen";
            string password = "jej";

            // Act
            bool check = Process.CheckConnexion(pseudo, password);

            // Assert
            Assert.IsTrue(check);
        }

        [TestMethod]
        public void CheckConnexion_WithBadPseudoBadPassword_ReturnFalse()
        {
            // Arrange
            string pseudo = "FauxPseudo";
            string password = "FauxMdp";

            // Act
            bool check = Process.CheckConnexion(pseudo, password);

            // Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void CheckConnexion_WithGoodPseudoAndBadPassword_ReturnFalse()
        {
            // Arrange
            string pseudo = "Octofen";
            string password = "FauxMdp";

            // Act
            bool check = Process.CheckConnexion(pseudo, password);

            // Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void CheckConnexion_WithBadPseudoAndGoodPassword_ReturnFalse()
        {
            // Arrange
            string pseudo = "FauxPseudo";
            string password = "jej";

            // Act
            bool check = Process.CheckConnexion(pseudo, password);

            // Assert
            Assert.IsFalse(check);
        }


    }
}
