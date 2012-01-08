using RS.EmploymentProfiler.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Objects;
using Moq;


namespace RS.EmploymentProfiler.Test
{
    /// <summary>
    ///This is a test class for PersonRepositoryTest and is intended
    ///to contain all PersonRepositoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PersonRepositoryTest
    {
        private Mock<PersonRepository> mockRepository;
        private EmploymentProfilerDB context;

        [TestInitialize]
        public void Initialize()
        {
            mockRepository = new Mock<PersonRepository>();           
            context = new EmploymentProfilerDB();
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Dispose();           
        }       

        [TestMethod]
        public void AddPerson()
        {
            var person = Person.CreatePerson(-1, "Test First", "Test Last", 21);

            // Act            
            PersonRepository target = new PersonRepository(context);
            var result = target.Add(person);

            // Assert
            Assert.AreEqual(person, result);
        }

        [TestMethod]
        public void AddPersonMock()
        {
            var person = Person.CreatePerson(-1, "Test First", "Test Last", 21);

            // Act
            var result = mockRepository.Object.Add(person);           

            // Assert
            Assert.AreEqual(person, result);
        }

       /*
        /// <summary>
        ///A test for GetById
        ///</summary>
        [TestMethod()]
        public void GetByIdTest()
        {
            ObjectContext context = null; // TODO: Initialize to an appropriate value
            PersonRepository target = new PersonRepository(context); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            Person expected = null; // TODO: Initialize to an appropriate value
            Person actual;
            actual = target.GetById(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        */
    }
}
