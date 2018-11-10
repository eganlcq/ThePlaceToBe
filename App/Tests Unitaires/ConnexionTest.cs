using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject {
	[TestClass]
	public class ConnexionTest {

		[TestMethod]
		public void CanBeConnected_WithGoodPseudoAndGoodPassword_ReturnsTrue() {
			// Arrange
			string pseudo = "Octofen";
			string password = "jej";

			// Act
			bool check = Process.CheckConnexion(pseudo, password);

			// Assert
			Assert.IsTrue(check);
		}

		[TestMethod]
		public void CanBeConnected_WithBadPseudoBadPassword_ReturnsFalse() {
			// Arrange
			string pseudo = "FauxPseudo";
			string password = "FauxMdp";

			// Act
			bool check = Process.CheckConnexion(pseudo, password);

			// Assert
			Assert.IsFalse(check);
		}

		[TestMethod]
		public void CanBeConnected_WithGoodPseudoAndBadPassword_ReturnsFalse() {
			// Arrange
			string pseudo = "Octofen";
			string password = "FauxMdp";

			// Act
			bool check = Process.CheckConnexion(pseudo, password);

			// Assert
			Assert.IsFalse(check);
		}

		[TestMethod]
		public void CanBeConnected_WithBadPseudoAndGoodPassword_ReturnsFalse() {
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
