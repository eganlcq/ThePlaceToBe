using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject {
	[TestClass]
	public class RestServiceTest {

		[TestMethod]
		public void IsConnexionAvailable_WithGoodPseudoAndPassword_ReturnsCurrentUser() {

			// Arrange
			var dic = new Dictionary<string, string> {
				{ "pseudo", "Octofen" },
				{ "pswd", "jej"}
			};

			// Act
			User currentUser = RestService.Request<User>(dic, "userConnexion").Result[0];

			// Assert
			Assert.AreEqual("Octofen", currentUser.Pseudo);
			Assert.AreEqual("Egan", currentUser.Prenom);
			Assert.AreEqual("Lecocq", currentUser.Nom);
		}

		[TestMethod]
		public void CanWeGetTheListOfBeers_ReturnsTheListOfBeers() {

			// Arrange
			var dic = new Dictionary<string, string>();
			
			// Act
			List<Beer> listBiere = RestService.Request<Beer>(dic, "selectBeer").Result;

			// Assert
			Assert.AreNotEqual(0, listBiere.Count);
		}

		[TestMethod]
		public void CanWeGetOneBeer_WithIdOfTheBeer_ReturnsOneBeer() {

			// Arrange
			var dic = new Dictionary<string, string> {

				{"idBiere", "1" }
			};

			// Act
			Beer beer = RestService.Request<Beer>(dic, "selectOneBeer").Result[0];

			// Assert
			Assert.AreEqual("Ambrasse-Temps", beer.Nombiere);
		}
	}
}
