using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class OperatorBOTest {

        [TestMethod]
        public void GetUserValidationSuccess() {
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            string username = "Neymar";
            bool validationReturn = true;
            mockUsersDao.Setup(dao => dao.ValidateWeighingUser(username)).Returns(validationReturn);
            IOperatorBO operatorBO = new OperatorBOImpl(mockUsersDao.Object);
            string userValidation = operatorBO.GetUserValidation(username);
            Assert.IsTrue(userValidation == "OK");
        }

        [TestMethod]
        public void GetUserValidationFailure() {
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            string username = "Neymar";
            bool validationReturn = false;
            mockUsersDao.Setup(dao => dao.ValidateWeighingUser(username)).Returns(validationReturn);
            IOperatorBO operatorBO = new OperatorBOImpl(mockUsersDao.Object);
            string userValidation = operatorBO.GetUserValidation(username);
            Assert.IsTrue(userValidation == "NOK");
        }
    }
}