using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Vetproject.Data.Repository;
using Vetproject.Model;

namespace Vetproject.Tests.Controllers
{
    [TestFixture]
    public class MedicalRecordControllerTests
    {
        [Test]
        public void GetMedicalRecord_ReturnsNotFound_WhenRecordNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IMedicalRecordRepository>();
            mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((MedicalRecord)null);
            var controller = new MedicalRecordController(mockRepository.Object);

            // Act
            var result = controller.GetMedicalRecord(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            Assert.AreEqual("Medical record not found.", ((NotFoundObjectResult)result.Result).Value);
        }

        [Test]
        public void GetAllMedicalRecords_ReturnsRecordsOrderedByCreatedAtDesc()
        {
            // Arrange
            var medicalRecords = new List<MedicalRecord>
            {
                new MedicalRecord { Id = 1, CreatedAt = DateTime.Now.AddHours(-1) },
                new MedicalRecord { Id = 2, CreatedAt = DateTime.Now.AddHours(-2) }
            };
            var mockRepository = new Mock<IMedicalRecordRepository>();
            mockRepository.Setup(repo => repo.GetAll()).Returns(medicalRecords);
            var controller = new MedicalRecordController(mockRepository.Object);

            // Act
            var result = controller.GetAllMedicalRecords();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            var returnedRecords = (IEnumerable<MedicalRecord>)okResult.Value;
            Assert.AreEqual(medicalRecords.OrderByDescending(r => r.CreatedAt), returnedRecords);
        }

        [Test]
        public void SaveMedicalRecord_ReturnsOk_WhenRecordSavedSuccessfully()
        {
            // Arrange
            var mockRepository = new Mock<IMedicalRecordRepository>();
            var controller = new MedicalRecordController(mockRepository.Object);
            var newRecord = new MedicalRecord();

            // Act
            var result = controller.SaveMedicalRecord(newRecord);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual("Medical Record saved successfully.", ((OkObjectResult)result).Value);
            mockRepository.Verify(repo => repo.Add(newRecord), Times.Once);
        }

        [Test]
        public void UpdateMedicalRecord_ReturnsNotFound_WhenRecordNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IMedicalRecordRepository>();
            mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((MedicalRecord)null);
            var controller = new MedicalRecordController(mockRepository.Object);

            // Act
            var result = controller.UpdateMedicalRecord(1, new MedicalRecord());

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual("Medical record not found.", ((NotFoundObjectResult)result).Value);
        }

        [Test]
        public void DeleteMedicalRecord_ReturnsNotFound_WhenRecordNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IMedicalRecordRepository>();
            mockRepository.Setup(repo => repo.Get(It.IsAny<int>())).Returns((MedicalRecord)null);
            var controller = new MedicalRecordController(mockRepository.Object);

            // Act
            var result = controller.DeleteMedicalRecord(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual("Medical record not found.", ((NotFoundObjectResult)result).Value);
        }

        [Test]
        public void SearchMedicalRecords_ReturnsBadRequest_WhenSearchTermIsEmpty()
        {
            // Arrange
            var mockRepository = new Mock<IMedicalRecordRepository>();
            var controller = new MedicalRecordController(mockRepository.Object);

            // Act
            var result = controller.SearchMedicalRecords("");

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            Assert.AreEqual("Search term cannot be empty.", ((BadRequestObjectResult)result.Result).Value);
        }

        // More test cases for edge cases, error handling, and input validation can be added similarly
    }
}
