using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject {
	[TestClass]
	public class InscriptionTest {

		[TestMethod]
		public void CanBeSubscribed_WithBlankInputs_ReturnsFalse() {

			// Arrange
			string firstNameUser = "";
			string nameUser = "";
			string pseudoUser = "";
			string emailUser = "";
			string pswdUser = "";
			string confirmPswdUser = "";
			string birthDate = "";

			// Act
			bool result = Process.CheckInfo("", firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithNullInputs_ReturnsFalse() {

			// Arrange
			string firstNameUser = null;
			string nameUser = null;
			string pseudoUser = null;
			string emailUser = null;
			string pswdUser = null;
			string confirmPswdUser = null;
			string birthDate = null;

			// Act
			bool result = Process.CheckInfo(null, firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithPasswordsNotTheSame_ReturnFalse() {

			// Arrange
			string pswdUser = "password";
			string confirmPswdUser = "other password";

			// Act
			bool result = Process.CheckSamePswd(pswdUser, confirmPswdUser);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithAlreadyExistingPseudo_ReturnsFalse() {

			// Arrange
			string pseudoUser = "Octofen";
			
			// Act
			bool result = Process.CheckPseudo(pseudoUser);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithAlreadyExistingEmail_ReturnsFalse() {

			// Arrange
			string emailUser = "e.lecocq@hotmail.com";

			// Act
			bool result = Process.CheckMail(emailUser);

			// Assert
			Assert.IsFalse(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithAllInputsFilled_ReturnsTrue() {

			// Arrange
			string firstNameUser = "firstNameUser";
			string nameUser = "nameUser";
			string pseudoUser = "pseudoUser";
			string emailUser = "emailUser";
			string pswdUser = "pswdUser";
			string confirmPswdUser = "pswdUser";
			string birthDate = "01/01/1980";

			// Act
			bool result = Process.CheckInfo("", firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithSamePasswords_ReturnTrue() {

			// Arrange
			string pswdUser = "password";
			string confirmPswdUser = "password";

			// Act
			bool result = Process.CheckSamePswd(pswdUser, confirmPswdUser);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithNotExistingPseudo_ReturnsTrue() {

			// Arrange
			string pseudoUser = "Not existing pseudo";

			// Act
			bool result = Process.CheckPseudo(pseudoUser);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void CanBeSubscribed_WithNotExistingEmail_ReturnsTrue() {

			// Arrange
			string emailUser = "Not existing email";

			// Act
			bool result = Process.CheckMail(emailUser);

			// Assert
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void WhatWillContainsErrorLabel_WithBlankInputs_ReturnsThatYouMustFillAllTheBlanks() {

			// Arrange
			string firstNameUser = "";
			string nameUser = "";
			string pseudoUser = "";
			string emailUser = "";
			string pswdUser = "";
			string confirmPswdUser = "";
			string birthDate = "";

			// Act
			string result = Process.VerifyInscription(firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.AreEqual("Veuillez remplir tous les champs s'il vous plaît", result);
		}

		[TestMethod]
		public void WhatWillContainsErrorLabel_WithPasswordsNotTheSame_ReturnsThatPasswordsAreNotTheSame() {

			// Arrange
			string firstNameUser = "test";
			string nameUser = "test";
			string pseudoUser = "test";
			string emailUser = "test";
			string pswdUser = "password";
			string confirmPswdUser = "not the same password";
			string birthDate = "test";

			// Act
			string result = Process.VerifyInscription(firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.AreEqual("Les mots de passe ne correspondent pas.", result);
		}

		[TestMethod]
		public void WhatWillContainsErrorLabel_WithAlreadyExistingPseudo_ReturnsThatPseudoAlreadyExists() {

			// Arrange
			string firstNameUser = "test";
			string nameUser = "test";
			string pseudoUser = "Octofen";
			string emailUser = "test";
			string pswdUser = "password";
			string confirmPswdUser = "password";
			string birthDate = "test";

			// Act
			string result = Process.VerifyInscription(firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.AreEqual("Le pseudo existe déjà.", result);
		}

		[TestMethod]
		public void WhatWillContainsErrorLabel_WithAlreadyExistingEmail_ReturnsThatEmailAlreadyExists() {

			// Arrange
			string firstNameUser = "test";
			string nameUser = "test";
			string pseudoUser = "test";
			string emailUser = "e.lecocq@hotmail.com";
			string pswdUser = "password";
			string confirmPswdUser = "password";
			string birthDate = "test";

			// Act
			string result = Process.VerifyInscription(firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.AreEqual("Le mail existe déjà.", result);
		}

		[TestMethod]
		public void WhatWillAppend_WithCorrectInscription_ReturnsOK() {

			// Arrange
			string firstNameUser = "correct first name";
			string nameUser = "correct name";
			string pseudoUser = "correct pseudo";
			string emailUser = "correctPseudo@correct.com";
			string pswdUser = "correct password";
			string confirmPswdUser = "correct password";
			string birthDate = "correct birth Date";

			// Act
			string result = Process.VerifyInscription(firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate);

			// Assert
			Assert.AreEqual("OK", result);
		}
	}
}
