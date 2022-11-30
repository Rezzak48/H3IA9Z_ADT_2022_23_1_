using H3IA9Z_ADT_2022_23_1_Logic;
using H3IA9Z_ADT_2022_23_1_Repository;
using Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Test
{
    [TestFixture]
    public class VisitorLogicTest
    {
        VisitorLogic VL;
        [SetUp]
        public void Init()
        {
            var MockVisitorRepository = new Mock<IVisitorRepository>();
            var MockReservationsRepository = new Mock<IReservationsRepository>();
            var vistors = new List<Visitor>()
            {
                new Visitor(){ Id =1,Address="Budapest1",Email="vis1@gmail.com",Name="vis1",PhoneNumber=11111111},
                new Visitor(){Id =2,Address="Budapest2",Email="vis2@gmail.com",Name="vis2",PhoneNumber=22222222},
                new Visitor(){Id =3,Address="Budapest3",Email="vis3@gmail.com",Name="vis3",PhoneNumber=33333333},
                new Visitor(){Id =4,Address="Budapest4",Email="vis4@gmail.com",Name="vis4",PhoneNumber=44444444},
                new Visitor(){Id =5,Address="Budapest5",Email="vis5@gmail.com",Name="vis5",PhoneNumber=55555555}
            }.AsQueryable();
            var Reservations = new List<Reservation>()
            {
                new Reservation(){Id = 1 , VisitorId=5,MovieId=4,DateTime=new DateTime(2022,12,21) },
                new Reservation(){Id = 2 , VisitorId=2,MovieId=5,DateTime=new DateTime(2022,12,22) },
                new Reservation(){Id = 3 , VisitorId=2,MovieId=2,DateTime=new DateTime(2022,12,23) },
                new Reservation(){Id = 4 , VisitorId=1,MovieId=3,DateTime=new DateTime(2022,12,24) },
                new Reservation(){Id = 5 , VisitorId=1,MovieId=1,DateTime=new DateTime(2022,12,25) }
            }.AsQueryable();
            MockVisitorRepository.Setup((t) => t.GetAll()).Returns(vistors);
            MockReservationsRepository.Setup((t) => t.GetAll()).Returns(Reservations);
            for (int i = 0; i < 5; i++)
            {
                MockVisitorRepository.Setup((t) => t.GetOne(i + 1)).Returns(vistors.ToList()[i]);
            }
            VL = new VisitorLogic(MockReservationsRepository.Object, MockVisitorRepository.Object);
        }

        [Test]
        public void AddNewVisitorTest_Throws()
        {
            Visitor vis = new Visitor() { Address = "budapest6", Email = "vis6@gmail.com", Name = null, PhoneNumber = 66666666 };
            //Arrange
            Assert.Throws<ArgumentException>(() => VL.AddNewVis(vis));
        }
        [Test]
        public void AddNewVisitorTest()
        {
            Visitor vis = new Visitor() { Address = "budapest6", Email = "vis6@gmail.com", Name = "vis6", PhoneNumber = 66666666 };
            Visitor vis6 = VL.AddNewVis(vis);
            Assert.That(vis6.Name, Is.EqualTo("vis6"));
        }

        [Test]
        public void DeleteVisitorTest_Throws()
        {
            Assert.Throws<ArgumentException>(() => VL.DeleteVisitor(100));
        }

        [Test]
        public void BestVisitor()
        {
            var result = VL.BestVisitor();
            var expected = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(1, 2), new KeyValuePair<int, int>(2, 2) };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void WorstVisitor()
        {
            var result = VL.WorstVisitor();
            var expected = new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(5, 1) };
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
