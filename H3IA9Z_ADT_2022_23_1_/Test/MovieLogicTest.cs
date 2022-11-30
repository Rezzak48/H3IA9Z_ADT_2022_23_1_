using H3IA9Z_ADT_2022_23_1_Logic;
using H3IA9Z_ADT_2022_23_1_Repository;
using Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    [TestFixture]
    public class MovieLogicTest
    {
        MovieLogic ML;
        [SetUp]
        public void Init()
        {
            var MockMovieRepository = new Mock<IMovieRepository>();
            var MockReservationsRepository = new Mock<IReservationsRepository>();
            var Movies = new List<Movie>()
            {
                new Movie(){Id=1,Name="movie1",Category="c1",Price=100,Duration=1 },
                new Movie(){Id=2,Name="movie2",Category="c2",Price=200,Duration=1 },
                new Movie(){Id=3,Name="movie3",Category="c3",Price=300,Duration=1 },
                new Movie(){Id=4,Name="movie4",Category="c4",Price=400,Duration=1 },
                new Movie(){Id=5,Name="movie5",Category="c5",Price=500,Duration=1 }
            }.AsQueryable();
            var Reservations = new List<Reservation>()
            {
                new Reservation(){Id = 1 , VisitorId=5,MovieId=4,DateTime=new DateTime(2022,12,11) },
                new Reservation(){Id = 2 , VisitorId=2,MovieId=5,DateTime=new DateTime(2022,12,12) },
                new Reservation(){Id = 3 , VisitorId=2,MovieId=2,DateTime=new DateTime(2022,12,13) },
                new Reservation(){Id = 4 , VisitorId=1,MovieId=3,DateTime=new DateTime(2022,12,19) },
                new Reservation(){Id = 5 , VisitorId=1,MovieId=1,DateTime=new DateTime(2022,12,20) }
            }.AsQueryable();
            MockMovieRepository.Setup((t) => t.GetAll()).Returns(Movies);
            MockReservationsRepository.Setup((t) => t.GetAll()).Returns(Reservations);
            for (int i = 0; i < 5; i++)
            {
                MockMovieRepository.Setup((t) => t.GetOne(i + 1)).Returns(Movies.ToList()[i]);
            }
            ML = new MovieLogic(MockMovieRepository.Object, MockReservationsRepository.Object);
        }
        [Test]
        public void AddNewMovieTest()
        {
            Movie mv = new Movie() { Name = "movie6", Duration = 1, Price = 600, Category = "c6" };
            //Act
            Movie movie6 = ML.AddNewMovie(mv);
            //Arrange
            Assert.That(movie6.Name, Is.EqualTo("movie6"));
        }
        public void GetMovieest()
        {
            var result = this.ML.GetMovie(1).Name;
            var expected = "movie1";
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void DeleteMovieTest_Throws()
        {
            Assert.Throws<ArgumentException>(() => ML.DeleteMovie(100));
        }
        [Test]
        public void GetMovieTest_Throws()
        {
            Assert.Throws<Exception>(() => ML.GetMovie(100));
        }

        [Test]
        public void MostPaidMovieTest()
        {
            var result = ML.MostSellMovie();
            var expected = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("movie5", 500) };
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void LessPaidMovieTest()
        {
            var result = ML.LessSellMovie();
            var expected = new List<KeyValuePair<string, int>>() { new KeyValuePair<string, int>("movie1", 100) };
            Assert.That(result, Is.EqualTo(expected));
        }
    }



}
