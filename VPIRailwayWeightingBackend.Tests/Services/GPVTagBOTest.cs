using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class GPVTagBOTest {

        [TestMethod]
        public void GetLocationSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();

            string weighingLocation = "Silo 01";
            string locationPointCode = "";
            DUAHT_WEIGHING_CONFIG loadingPointData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 01",
                DUAH_LOAD_POINT_CODE = "SI01",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS"
            };
            List<string> locationAccessRoles = new List<string>() { "SI01" };

            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLocation(weighingLocation)).Returns(loadingPointData);
            mockUsersDao.Setup(dao => dao.getUserLocationRoles(It.IsAny<string>())).Returns(locationAccessRoles);
            IGPVTagBO gpvTagBO = new GPVTagBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockUsersDao.Object, mockDestinationDao.Object);

            List<LoadingParameters> locationInformationList = gpvTagBO.GetLocation("testUser", weighingLocation, locationPointCode);
            Assert.IsTrue(locationInformationList.Count == 1);
            Assert.IsTrue(locationInformationList[0].LoadingPointCode == "SI01");
            Assert.IsTrue(locationInformationList[0].WeighingLocation == "Silo 01");
            Assert.IsTrue(locationInformationList[0].Overload == "YES");
            Assert.IsTrue(locationInformationList[0].DataSource == "WS");
        }

        [TestMethod]
        public void GetLocationSuccessEmpty() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();

            string weighingLocation = "";
            string locationPointCode = "";

            DUAHT_WEIGHING_CONFIG loadingPointData1 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 01",
                DUAH_LOAD_POINT_CODE = "SI01",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData2 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 02",
                DUAH_LOAD_POINT_CODE = "SI02",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData3 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 03",
                DUAH_LOAD_POINT_CODE = "SI03",
                DUAH_OVERLOAD = "NO",
                DUAH_DATA_SOURCE = "WS"
            };
            List<DUAHT_WEIGHING_CONFIG> loadingPointDataList = new List<DUAHT_WEIGHING_CONFIG>() {
                loadingPointData1,loadingPointData2,loadingPointData3
            };
            List<string> locationAccessRoles = new List<string>() { "SI01", "SI02", "SI03" };

            mockLoadingPointDao.Setup(dao => dao.GetAllLoadingPoints()).Returns(loadingPointDataList);
            mockUsersDao.Setup(dao => dao.getUserLocationRoles(It.IsAny<string>())).Returns(locationAccessRoles);
            IGPVTagBO gpvTagBO = new GPVTagBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockUsersDao.Object, mockDestinationDao.Object);

            List<LoadingParameters> locationInformationList = gpvTagBO.GetLocation("testUser", weighingLocation, locationPointCode);
            Assert.IsTrue(locationInformationList.Count == 3);
            Assert.IsTrue(locationInformationList[0].LoadingPointCode == "SI01");
            Assert.IsTrue(locationInformationList[0].WeighingLocation == "Silo 01");
            Assert.IsTrue(locationInformationList[0].Overload == "YES");
            Assert.IsTrue(locationInformationList[0].DataSource == "WS");
            Assert.IsTrue(locationInformationList[1].LoadingPointCode == "SI02");
            Assert.IsTrue(locationInformationList[1].WeighingLocation == "Silo 02");
            Assert.IsTrue(locationInformationList[1].Overload == "YES");
            Assert.IsTrue(locationInformationList[1].DataSource == "WS");
            Assert.IsTrue(locationInformationList[2].LoadingPointCode == "SI03");
            Assert.IsTrue(locationInformationList[2].WeighingLocation == "Silo 03");
            Assert.IsTrue(locationInformationList[2].Overload == "NO");
            Assert.IsTrue(locationInformationList[2].DataSource == "WS");
        }

        [TestMethod]
        public void GetLocationFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "Test";


            string weighingLocation = "";
            string locationPointCode = "";

            DUAHT_WEIGHING_CONFIG loadingPointData1 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 01",
                DUAH_LOAD_POINT_CODE = "SI01",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData2 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 02",
                DUAH_LOAD_POINT_CODE = "SI02",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData3 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 03",
                DUAH_LOAD_POINT_CODE = "SI03",
                DUAH_OVERLOAD = "NO",
                DUAH_DATA_SOURCE = "WS"
            };
            List<DUAHT_WEIGHING_CONFIG> loadingPointDataList = new List<DUAHT_WEIGHING_CONFIG>() {
                loadingPointData1,loadingPointData2,loadingPointData3
            };
            List<string> locationAccessRoles = new List<string>() { "SI01" };

            mockLoadingPointDao.Setup(dao => dao.GetAllLoadingPoints()).Throws(new Exception());
            mockUsersDao.Setup(dao => dao.getUserLocationRoles(It.IsAny<string>())).Returns(locationAccessRoles);
            IGPVTagBO gpvTagBO = new GPVTagBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockUsersDao.Object, mockDestinationDao.Object);

            List<LoadingParameters> locationInformationList = gpvTagBO.GetLocation("testUser", weighingLocation, locationPointCode);
            Assert.IsTrue(locationInformationList.Count == 0);
        }
    }
}