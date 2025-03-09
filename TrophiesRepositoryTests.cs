using Microsoft.VisualStudio.TestTools.UnitTesting;
using OblOPGBirkTrophy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OblOPGBirkTrophy.Tests {
    [TestClass()]
    public class TrophiesRepositoryTests {
        private static TrophiesRepository _rep;
        [TestInitialize]
        public void Initialize()
        {
            _rep = new TrophiesRepository();
            _rep.AddTrophy(new Trophy(1, "ccc", 2021));
            _rep.AddTrophy(new Trophy(2, "bbb", 2025));
            _rep.AddTrophy(new Trophy(3, "ghi", 1988));
            _rep.AddTrophy(new Trophy(4, "zzz", 2024));
            _rep.AddTrophy(new Trophy(5, "mno", 2022));
        }
        [TestMethod()]
        public void AddTest()
        {
            //Tjekker i gettest at der var 5 objekter på den originale liste.

            //tilføjer et nyt objekt til listen.
            //selvom id er sat til 1, så burde den automatisk blive sat til 6.

            Trophy t = new Trophy(1, "TestTrophy", 1988);
            Trophy AddedTrophy = _rep.AddTrophy(t);
            Assert.AreEqual(t, _rep.GetTrophyById(6));
            Assert.AreEqual(6, AddedTrophy.Id);
            Assert.AreEqual("TestTrophy", AddedTrophy.Competition);
            Assert.AreEqual(1988, AddedTrophy.Year);


            //tjekker om listen nu er på 6 objekter.
            List<Trophy> newList = _rep.Get(null, null, null).ToList();
            Assert.AreEqual(6, newList.Count());

            //null exception test.
            
            Assert.ThrowsException<ArgumentNullException>(() => _rep.AddTrophy(null));



        }
        [TestMethod()]
        public void GetById()
        {
            //tjekker først en hvor den finder den korrekte trophy.
            Trophy t = _rep.GetTrophyById(3);
            Assert.AreEqual(3, t.Id);
            Assert.AreEqual("ghi", t.Competition);
            Assert.AreEqual(1988, t.Year);


            //tjekker om den laver en null exception, hvis trophy ikke findes.
            Assert.ThrowsException<ArgumentException>(() => _rep.GetTrophyById(999));
        }
        [TestMethod()]
        public void GetTest()
        {
            //henter listen og tjekker om den er på 5 objekter.
            List<Trophy> initList = _rep.Get(null, null, null).ToList();
            Assert.AreEqual(5, initList.Count());
            //ser om den øverste er "bbb".
            Assert.AreEqual("ccc", initList.FirstOrDefault().Competition);
            //ser om den nederste er "mno".
            Assert.AreEqual("mno", initList.LastOrDefault().Competition);

            //tester om den kan finde en liste med trophies før 2020, der burde kun være en.
            List<Trophy> beforeList = _rep.Get(2020, null, null).ToList();
            Assert.AreEqual(1, beforeList.Count());
            //ser om competition passer.
            Assert.AreEqual("ghi", beforeList.FirstOrDefault().Competition);

            //tester om den kan finde en liste med trophies efter 2024, der burde være 1.
            List<Trophy> afterList = _rep.Get(null, 2024, null).ToList();
            Assert.AreEqual(1, afterList.Count());
            //ser om competition passer.
            Assert.AreEqual("bbb", afterList.FirstOrDefault().Competition);

            //tester om den kan finde mellem 2021 og 2024. Der burde være 1.
            List<Trophy> betweenList = _rep.Get(2024, 2021, null).ToList();
            Assert.AreEqual(1, betweenList.Count());
            //ser om competition passer.
            Assert.AreEqual("mno", betweenList.FirstOrDefault().Competition);

            //sorterer listen så ældste år er først.
            List<Trophy> yearAscList = _rep.Get(null, null, "year_asc").ToList();
            //tester om den første nu er 1988.
            Assert.AreEqual(yearAscList.FirstOrDefault().Year, 1988);
            Assert.AreEqual(yearAscList.FirstOrDefault().Competition, "ghi");


            //tester om den kan lave den descending nu er nyeste år først.
            List<Trophy> yearDescList = _rep.Get(null, null, "year_desc").ToList();
            //ser om den første nu er 2025.
            Assert.AreEqual(yearDescList.FirstOrDefault().Year, 2025);

            //tester om den kan lave den kan competition name alfabetisk.
            List<Trophy> compAscListc = _rep.Get(null, null, "competition_asc").ToList();
            // ser om den første nu er "bbb".
            Assert.AreEqual(compAscListc.FirstOrDefault().Competition, "bbb");

            //tester om den kan lave den descending nu er den omvendt alfabetisk.
            List<Trophy> compDescList = _rep.Get(null, null, "competition_desc").ToList();
            //ser om den første nu er "zzz".
            Assert.AreEqual(compDescList.FirstOrDefault().Competition, "zzz");

        }

    }
}