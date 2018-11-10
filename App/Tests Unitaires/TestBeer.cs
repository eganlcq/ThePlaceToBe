using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThePlaceToBe.Data;

namespace TDDProject
{
    [TestClass]
    public class TestBeer
    {

        [TestMethod]
        public void Test_Get_One_Beer_With_Id_Parameter()
        {
            RestService.dic = new Dictionary<string, string> {

                {"idBiere", "1" }
            };
            Beer beerObjGetDataFromWebService = RestService.Request<Beer>(RestService.dic, "selectOneBeer").Result[0];

            Assert.IsNotNull(beerObjGetDataFromWebService);

        }
        [TestMethod]
        public void Test_Get_list_Beer()
        {
            RestService.dic = new Dictionary<string, string>();
            List<Beer> listBiere = RestService.Request<Beer>(RestService.dic, "selectBeer").Result;

            Assert.IsNotNull(listBiere);
            Assert.AreEqual(57, listBiere.Count);

        }
    }
}
