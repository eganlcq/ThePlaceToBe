using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;
using ThePlaceToBe.Views.ConnexionPage;
using ThePlaceToBe.Views.InscriptionPage;

namespace TDDProject
{
    [TestClass]
    public class TestConstructors
    {
        [TestMethod]
        public void Test_Classes()
        {
            Beer beerObj = new Beer();
            Bar barObj = new Bar();
            Check checkObj = new Check();
            RestService restServiceObj = new RestService();
            Succes succesObj = new Succes();
            User userObj = new User();


            Assert.IsNotNull(beerObj);
            Assert.IsNotNull(barObj);
            Assert.IsNotNull(checkObj);
            Assert.IsNotNull(restServiceObj);
            Assert.IsNotNull(succesObj);
            Assert.IsNotNull(userObj);

            
        }

    }
}
