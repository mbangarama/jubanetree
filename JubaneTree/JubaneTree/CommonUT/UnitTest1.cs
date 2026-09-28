using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace CommonUT
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string actual = PersonJSONFactory.MakeJSON(new Person
            {
                MemberID = 42,
                Title = "Chief",
                Name = "Nick",
                Surname = "Hubbard",
                Telephone = "123456",
                Mobile = "7890",
                Email = "nick@anotherurl.com",
                Address = "An Address",
                Job = "A Job",
                Company = "Company",
                Country = "Country",
                Image = "Image",
                HasChildren = true,
                FamilyCodeID = 99,
                IsAlive = false
            });
            string expected = "{\"MemberID\":42,\"Title\":\"Chief\"," +
                "\"Name\":\"Nick\",\"Surname\":\"Hubbard\",\"Telephone\":\"123456\"," +
                "\"Mobile\":\"7890\",\"Email\":\"nick@anotherurl.com\"," +
                "\"Address\":\"An Address\",\"Job\":\"A Job\",\"Company\":\"Company\"," +
                "\"Country\":\"Country\",\"Image\":\"Image\",\"HasChildren\":\"true\"," +
                "\"FamilyCodeID\":99,\"IsAlive\":\"false\"}";
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethod2()
        {
            string actual = PersonJSONFactory.MakeJSON(new Person
            {
                MemberID = 0,
                Title = "",
                Name = "",
                Surname = "",
                Telephone = "",
                Mobile = "",
                Email = "",
                Address = "",
                Job = "",
                Company = "",
                Country = "",
                Image = "",
                HasChildren = false,
                FamilyCodeID = 0,
                IsAlive = false
            });

            string expected = "{\"MemberID\":0,\"Title\":\"\"," +
                "\"Name\":\"\",\"Surname\":\"\",\"Telephone\":\"\"," +
                "\"Mobile\":\"\",\"Email\":\"\"," +
                "\"Address\":\"\",\"Job\":\"\",\"Company\":\"\"," +
                "\"Country\":\"\",\"Image\":\"\",\"HasChildren\":\"false\"," +
                "\"FamilyCodeID\":0,\"IsAlive\":\"false\"}";

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethod3()
        {
            var p1 = new Person
            {
                MemberID = 0,
                Title = "",
                Name = "",
                Surname = "",
                Telephone = "",
                Mobile = "",
                Email = "",
                Address = "",
                Job = "",
                Company = "",
                Country = "",
                Image = "",
                HasChildren = false,
                FamilyCodeID = 0,
                IsAlive = false
            };

            var p2 = new Person
            {
                MemberID = 0,
                Title = "",
                Name = "",
                Surname = "",
                Telephone = "",
                Mobile = "",
                Email = "",
                Address = "",
                Job = "",
                Company = "",
                Country = "",
                Image = "",
                HasChildren = false,
                FamilyCodeID = 0,
                IsAlive = false
            };

            var actual = PersonJSONFactoryList.MakeJSONList(new List<Person> { p1, p2 });

            string expectedInner = "{\"MemberID\":0,\"Title\":\"\"," +
                "\"Name\":\"\",\"Surname\":\"\",\"Telephone\":\"\"," +
                "\"Mobile\":\"\",\"Email\":\"\"," +
                "\"Address\":\"\",\"Job\":\"\",\"Company\":\"\"," +
                "\"Country\":\"\",\"Image\":\"\",\"HasChildren\":\"false\"," +
                "\"FamilyCodeID\":0,\"IsAlive\":\"false\"}";
            string expected = "[" + expectedInner + "," + expectedInner + "]";

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TestMethod_json()
        {
            var peoples = new List<Person>();

            foreach (var person in peoples)
            {
                if (person.HasChildren)
                {
                    var children = new List<Person>() {
                        new Person {
                            Name = "",

                        }

                    };
                }
            }
        }

        public List<Person> Test(List<Person> people)
        {
            foreach (var person in people)
            {
                person.Name = "";
                if (person.HasChildren)
                {
                    //var children = null; //(from x in people where x.MemberID == person.MemberID select x).ToList<Person>;
                    Test(null);
                }
            }

            var ret = new List<Person>();

            return ret;
        }
    }
}
