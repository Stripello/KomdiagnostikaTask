using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookCathalog.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCathalog.Dal.Models.Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void CreateCopyTest_Succed()
        {
            //Arrange
            var original = new Book()
            {
                Title = "a",
                Author = "b",
                Year = 1,
                About = "c",
                Isbn = "123456789X",
                Guid = "d",
                FrontPage = new byte[] { 1, 2, 3 },
                Id = 3
            };

            //Act
            var copy = original.CreateCopy();

            //Assert
            Assert.IsTrue(original.Equals(copy));
        }

        [TestMethod()]
        public void EqalityTest_Equal()
        {
            //Arrange
            var original = new Book()
            {
                Title = "a",
                Author = "b",
                Year = 1,
                About = "c",
                Isbn = "123456789X",
                Guid = "d",
                FrontPage = new byte[] { 1, 2, 3 },
                Id = 3
            };

            //Act
            var changedCopy = original.CreateCopy();
            changedCopy.Id = 4;

            //Assert
            Assert.IsTrue(original.Equals(changedCopy));
        }


        [TestMethod()]
        [DataRow ("X","b",1,"c","123456789X","d",new byte[] { 1,2,3}, 3)]
        [DataRow("a", "X", 1, "c", "123456789X", "d", new byte[] { 1, 2, 3 }, 3)]
        [DataRow("a", "b", 2, "c", "123456789X", "d", new byte[] { 1, 2, 3 }, 3)]
        [DataRow("a", "b", 1, "X", "123456789X", "d", new byte[] { 1, 2, 3 }, 3)]
        [DataRow("a", "b", 1, "c", "123456789", "d", new byte[] { 1, 2, 3 }, 3)]
        [DataRow("a", "b", 1, "c", "123456789X", "X", new byte[] { 1, 2, 3 }, 3)]
        [DataRow("a", "b", 1, "c", "123456789X", "d", new byte[] { 1, 2, 0 }, 3)]
        public void EqalityTest_NotEqual(string title, string author, int year, string about, string isbn, string guid, byte[] frontPage,int id)
        {
            //Arrange
            var original = new Book()
            {
                Title = "a",
                Author = "b",
                Year = 1,
                About = "c",
                Isbn = "123456789X",
                Guid = "d",
                FrontPage = new byte[] { 1, 2, 3 },
                Id = id
            };

            //Act
            var changedCopy = new Book()
            {
                Title = title,
                Author = author,
                Year = year,
                About = about,
                Isbn = isbn,
                Guid = guid,
                FrontPage = frontPage,
                Id = id
            };


            //Assert
            Assert.IsFalse(original.Equals(changedCopy));
        }

        [TestMethod()]
        [DataRow("X", "b", 1, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3 , true)]
        [DataRow(null, "b", 1, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", null, 1, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", "b", -1, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", "b", 40000, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", "b", 1, null, "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, true)]
        [DataRow("X", "b", 1, "c", "1234567891", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", "b", 1, "c", "123456789", "d235362c-31b4-410f-8a94-f6f43a719cce", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", "b", 1, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cc", new byte[] { 1, 2, 3 }, 3, false)]
        [DataRow("X", "b", 1, "c", "123456789X", "d235362c-31b4-410f-8a94-f6f43a719cce", null, 3, true)]
        public void IsValid_VariousAnswers(string title, string author, int year, string about, string isbn, string guid, byte[] frontPage, int id, bool expected)
        {
            //Arrange
            var original = new Book()
            {
                Title = title,
                Author = author,
                Year = year,
                About = about,
                Isbn = isbn,
                Guid = guid,
                FrontPage = frontPage,
                Id = id
            };

            //Act
            var actual = original.IsValid();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}