using Microsoft.Testing.Platform.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OblOPGBirkTrophy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblOPGBirkTrophy.Tests {
    [TestClass()]
    public class TrophyTests {

        [TestMethod()]
        public void TrophyCompTest()
        {
            //Opretter en ny konkurrence som burde leve op til kravene. Laver med 3 tegn, da det er det mindste der er tilladt.
            Trophy t = new Trophy(1, "abc", 2021);
            Assert.AreEqual("abc", t.Competition);

            //ser også om Id er ens.
            Assert.AreEqual(1, t.Id);

            //laver det cases der burde give fejl
            //først en der er null.

            Assert.ThrowsException<ArgumentNullException>(() => t.Competition = null);
            // så en der er under 2 tegn. Vælger 2 da det er lige på grænsen.
            Assert.ThrowsException<ArgumentException>(() => t.Competition = "ab");
        }
        [TestMethod]
        public void TrophyYearTest()
        {
            //tester igen for year. Først en der burde virke dato før 1970 og efter 2015.
            //vælger 2015 da det er lige på grænsen.
            Trophy t = new Trophy(1, "abc", 2025);

            Assert.AreEqual(2025, t.Year);
            //tester for den anden ende:
            t.Year = 1970;
            Assert.AreEqual(1970, t.Year);

            //vælger 1969 da den er lige før den nedre grænse
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => t.Year = 1969);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => t.Year = 2026);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            string tToString = new Trophy(1, "abc", 2021).ToString();
            Assert.AreEqual("Id: 1, Competition: abc, Year: 2021", tToString);
        }
    }
}
