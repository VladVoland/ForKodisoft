using System;
using NUnit.Framework;
using System.Collections.Generic;
using BLL;
using BLL.FeedRead;
using BLL.Operations;
using BLL.Entities;
using DAL;
using Moq;
using AutoMapper;

namespace FuncTests
{
    [TestFixture]
    public class UnitTest1
    {
        private UserOperations UserO;
        private ContentOperations ContO;
        Mock<IUnitOfWork> mockContainer;
        Mock<KodiDB> mockModel;
        Mock<GenericRepository<DBFeed>> mockFeeds;
        Mock<GenericRepository<DBContentCollection>> mockContentCollections;
        Mock<GenericRepository<DBUser>> mockUsers;

        public void ResetData()
        {
            mockContainer = new Mock<IUnitOfWork>();
            mockModel = new Mock<KodiDB>();

            mockFeeds = new Mock<GenericRepository<DBFeed>>(mockModel.Object);
            mockContentCollections = new Mock<GenericRepository<DBContentCollection>>(mockModel.Object);
            mockUsers = new Mock<GenericRepository<DBUser>>(mockModel.Object);
            
            UserO = new UserOperations(mockContainer.Object);
            ContO = new ContentOperations(mockContainer.Object);

            mockContainer.Setup(a => a.Feeds).Returns(mockFeeds.Object);
            mockContainer.Setup(a => a.Users).Returns(mockUsers.Object);
            mockContainer.Setup(a => a.ContentCollections).Returns(mockContentCollections.Object);
        }

        [Test]
        public void GetCollections()
        {
            ResetData();
            List<DBContentCollection> collections = new List<DBContentCollection>();
            DBContentCollection collection1 = new DBContentCollection();
            DBContentCollection collection2 = new DBContentCollection();
            collections.Add(collection1);
            collections.Add(collection2);

            mockContentCollections.Setup(a => a.Get()).Returns(collections);

            List<ContentCollection> result = (List<ContentCollection>)ContO.GetCollections();

            Assert.AreEqual(collections.Count, result.Count);
        }

        [Test]
        public void AddFeedToCollection()
        {
            ResetData();
            DBContentCollection contentCollection = new DBContentCollection();
            Feed feed = new Feed();

            var id = 1;
            contentCollection.ContentCollectionId = id;
            feed.URL = "http";

            mockContentCollections.Setup(a => a.FindById(id)).Returns(contentCollection);

            bool result = ContO.AddFeedToCollection(id, feed);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void GetUsers()
        {
            ResetData();
            List<DBUser> users = new List<DBUser>();
            DBUser user1 = new DBUser();
            DBUser user2 = new DBUser();
            users.Add(user1);
            users.Add(user2);

            mockUsers.Setup(a => a.Get()).Returns(users);

            var result = UserO.GetUsers();

            Assert.AreEqual(users.Count, result.Count);
        }

        [Test]
        public void CheckUserByLogin()
        {
            ResetData();
            List<DBUser> users = new List<DBUser>();
            DBUser user1 = new DBUser();
            DBUser user2 = new DBUser();
            string login = "LOGIN";
            user1.Login = login;
            users.Add(user1);
            users.Add(user2);
            mockUsers.Setup(a => a.Get()).Returns(users);

            var result = UserO.CheckUser(login);

            Assert.AreEqual(true, result);
        }


        [Test]
        public void CheckUserByLoginAndPassword()
        {
            ResetData();
            BLLAutoMapper.Initialize();
            List<DBUser> users = new List<DBUser>();
            DBUser user1 = new DBUser();
            DBUser user2 = new DBUser();

            string login = "LOGIN";
            string password = "PASSWORD";
            user1.Login = login;
            user1.Password = password;
            user1.UserId = 1;
            users.Add(user1);
            users.Add(user2);
            mockUsers.Setup(a => a.Get()).Returns(users);
            int expected = Mapper.Map<DBUser, User>(user1).UserId;

            var result = UserO.CheckUser(login, password);

            Assert.AreEqual(expected, result);
        }


        [Test]
        public void UserAuthentication()
        {
            ResetData();
            List<DBUser> users = new List<DBUser>();
            DBUser user1 = new DBUser();
            DBUser user2 = new DBUser();

            string name = "name";
            string surname = "surname";
            string patr = "patr";

            user1.Name = name;
            user1.Surname = surname;
            user1.Patronymic = patr;
            users.Add(user1);
            users.Add(user2);
            mockUsers.Setup(a => a.Get()).Returns(users);

            var result = UserO.UserAuthentication(name, surname, patr);

            Assert.AreEqual(true, result);
        }

        /*[Test]
        public void TestChangeBet()
        {
            BLL_AutoMapper.Initialize();
            ResetData();
            int lot_id = 1;
            int user_id = 1;
            int bet = 100;
            DB_Lot lot1 = new DB_Lot();
            DB_User user1 = new DB_User();
            lot1.LotId = lot_id;
            lot1.Step = 0;
            user1.UserId = user_id;

            mockUsers.Setup(a => a.FindById(user_id)).Returns(user1);
            mockLots.Setup(a => a.FindById(lot_id)).Returns(lot1);

            var result = LotO.ChangeBet(bet, user_id, lot_id);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void TestGetCategories()
        {
            ResetData();
            List<DB_Category> categories = new List<DB_Category>();
            DB_Category catg1 = new DB_Category();
            DB_Category catg2 = new DB_Category();
            categories.Add(catg1);
            categories.Add(catg2);

            mockCategories.Setup(a => a.Get()).Returns(categories);
            List<Category> expected = new List<Category>();
            expected = Mapper.Map<List<DB_Category>, List<Category>>(categories);

            var result = CatgO.GetCategories();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetSubcategories()
        {
            ResetData();
            List<DB_Subcategory> subcategories = new List<DB_Subcategory>();
            DB_Subcategory subcatg1 = new DB_Subcategory();
            DB_Subcategory subcatg2 = new DB_Subcategory();
            subcategories.Add(subcatg1);
            subcategories.Add(subcatg2);

            mockSubcategories.Setup(a => a.GetWithInclude(s => s.Category)).Returns(subcategories);
            List<Subcategory> expected = new List<Subcategory>();
            expected = Mapper.Map<List<DB_Subcategory>, List<Subcategory>>(subcategories);

            var result = SubcO.GetSubcategories();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetSubcategoriesByCateg()
        {
            ResetData();
            List<DB_Subcategory> subcategories = new List<DB_Subcategory>();
            DB_Subcategory subcatg1 = new DB_Subcategory();
            DB_Subcategory subcatg2 = new DB_Subcategory();
            subcatg1.Category = new DB_Category();
            subcatg2.Category = new DB_Category();
            string c_name = "New category";
            string sc_name = "New subcategory";
            subcatg1.Name = sc_name;
            subcatg1.Category.Name = c_name;
            subcategories.Add(subcatg1);
            subcategories.Add(subcatg2);

            mockSubcategories.Setup(a => a.GetWithInclude(s => s.Category)).Returns(subcategories);
            List<Subcategory> expected = new List<Subcategory>();
            expected.Add(Mapper.Map<DB_Subcategory, Subcategory>(subcatg1));

            var result = SubcO.GetSubcategoriesByCateg(c_name);

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetUnconfirmedLots()
        {
            ResetData();
            List<DB_Lot> lots = new List<DB_Lot>();
            DB_Lot lot1 = new DB_Lot();
            DB_Lot lot2 = new DB_Lot();
            lots.Add(lot1);
            lots.Add(lot2);

            mockLots.Setup(a => a.GetWithInclude(l => (l.Category), l => (l.Owner))).Returns(lots);
            List<Lot> expected = new List<Lot>();
            expected = Mapper.Map<List<DB_Lot>, List<Lot>>(lots);

            var result = LotO.GetUnconfirmedLots();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetСonfirmedLots()
        {
            ResetData();
            List<DB_Lot> lots = new List<DB_Lot>();
            DB_Lot lot1 = new DB_Lot();
            DB_Lot lot2 = new DB_Lot();
            lot1.StartDate = DateTime.Now;
            lot2.StartDate = DateTime.Now;
            lot1.EndDate = DateTime.Now.AddDays(1);
            lot2.EndDate = DateTime.Now.AddDays(1);
            lots.Add(lot1);
            lots.Add(lot2);

            mockLots.Setup(a => a.GetWithInclude(l => (l.Category), l => (l.Owner))).Returns(lots);
            List<Lot> expected = new List<Lot>();
            expected = Mapper.Map<List<DB_Lot>, List<Lot>>(lots);

            var result = LotO.GetСonfirmedLots();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }

        [Test]
        public void TestGetEndedLots()
        {
            ResetData();
            List<DB_Lot> lots = new List<DB_Lot>();
            DB_Lot lot1 = new DB_Lot();
            DB_Lot lot2 = new DB_Lot();
            lot1.EndDate = DateTime.Now;
            lot2.EndDate = DateTime.Now;
            lots.Add(lot1);
            lots.Add(lot2);

            mockLots.Setup(a => a.GetWithInclude(l => (l.Category), l => (l.Owner))).Returns(lots);
            List<Lot> expected = new List<Lot>();
            expected = Mapper.Map<List<DB_Lot>, List<Lot>>(lots);

            var result = LotO.GetEndedLots();

            Assert.AreEqual(expected.Capacity, result.Capacity);
        }
        */
    }
}
