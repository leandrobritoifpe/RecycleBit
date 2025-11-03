using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Models.Response;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util.Enums;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class GPVBOTest {
        
        /*[TestMethod]
        public void GetWeighingDispatchValidationSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string loadingPoint = "SI01";
            DateTime startDateTime = new DateTime(2022, 8, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 8, 2, 0, 0, 0);
            string compositionId = "C37C38-120722";
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 2,
                DUAE_CAR_TYPE = 2,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 3,
                DUAE_CAR_TYPE = 3,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            string status = "OK";
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns(weighingDataList);
            mockGpvmDispatchDao.Setup(dao => dao.GetGPVMDispatchStatus(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(status);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            List<DispatchData> gpvmDispatchDataList = gpvBO.GetWeighingDispatchValidation(loadingPoint, startDateTime, endDateTime, compositionId);
            Assert.IsTrue(gpvmDispatchDataList.Count == 3);
            Assert.IsTrue(gpvmDispatchDataList[0].Status == "OK");
            Assert.IsTrue(gpvmDispatchDataList[0].LastCarNumber == 1);
            Assert.IsTrue(gpvmDispatchDataList[0].InitialCarNumber == 1);
            Assert.IsTrue(gpvmDispatchDataList[0].RegistryTime == new DateTime(2022, 8, 1, 0, 0, 0));
            Assert.IsTrue(gpvmDispatchDataList[1].Status == "OK");
            Assert.IsTrue(gpvmDispatchDataList[1].LastCarNumber == 2);
            Assert.IsTrue(gpvmDispatchDataList[1].InitialCarNumber == 1);
            Assert.IsTrue(gpvmDispatchDataList[1].RegistryTime == new DateTime(2022, 8, 1, 0, 0, 0));
            Assert.IsTrue(gpvmDispatchDataList[2].Status == "OK");
            Assert.IsTrue(gpvmDispatchDataList[2].LastCarNumber == 3);
            Assert.IsTrue(gpvmDispatchDataList[2].InitialCarNumber == 1);
            Assert.IsTrue(gpvmDispatchDataList[2].RegistryTime == new DateTime(2022, 8, 1, 0, 0, 0));
        }*/

        /// <summary>
        /// /
        /// </summary>


        [TestMethod]
        public void GetWeighingDispatchValidationFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            DateTime startDateTime = new DateTime(2022, 8, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 8, 2, 0, 0, 0);
            string compositionId = "C37C38-120722";
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 2,
                DUAE_CAR_TYPE = 2,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 3,
                DUAE_CAR_TYPE = 3,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            string status = "OK";
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Throws(new Exception());
            mockGpvmDispatchDao.Setup(dao => dao.GetGPVMDispatchStatus(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(status);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            List<DispatchData> gpvmDispatchDataList = gpvBO.GetWeighingDispatchValidation(loadingPoint, startDateTime, endDateTime, compositionId);
            Assert.IsTrue(gpvmDispatchDataList.Count == 0);
        }
        /*
        [TestMethod]
        public void SendRailwayWeighingSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string loadingPoint = "SI01";
            DateTime startDateTime = new DateTime(2022,8,1,0,0,0);
            DateTime endDateTime = new DateTime(2022,8,2,0,0,0);
            string compositionId = "C37C38-120722";
            string lotId = string.Empty;
            string operatorId = "OPERADOR";
            DUAHT_WEIGHING_CONFIG loadingPointData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 01",
                DUAH_OPERATIONAL_UNIT = "SN",
                DUAH_LOAD_POINT_CODE = "SI01",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS",
                DUAH_DESTINATION_HOST_WS = "http://172.19.204.18/WebService.svc?WSDL",
                DUAH_ACCEPT_ABNORMAL = false,
            };
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,1),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 2,
                DUAE_CAR_TYPE = 2,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,2),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 3,
                DUAE_CAR_TYPE = 3,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            string status = "OK";
            string tibcoXMLResponse = "<RESPONSE xmlns:ns0=\"http://test.com\">\r\n<ns0:Status>OK</ns0:Status>\r\n</RESPONSE>";
            SendWeighingRequest weighingRequest = new SendWeighingRequest(startDateTime,endDateTime,loadingPoint,compositionId,operatorId,lotId);
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(),It.IsAny<DateTime>(),It.IsAny<DateTime>(),It.IsAny<string>())).Returns(weighingDataList);
            mockGpvmDispatchDao.Setup(dao => dao.GetGPVMDispatchStatus(It.IsAny<DateTime>(),It.IsAny<DateTime>())).Returns(status);
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(It.IsAny<string>())).Returns(loadingPointData);
            mockTibcoDao.Setup(dao => dao.SendXMLTibco(It.IsAny<string>())).Returns(tibcoXMLResponse);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            gpvBO.SendRailwayWeighing(weighingRequest);
            mockGpvmDispatchDao.Verify(mock => mock.SaveNewGPVMDispatchData(It.Is<DUAKT_LOG_GPVM>(param => param.DUAK_EXECUTION_STATUS == "OK" &&
                                                                                                                param.DUAK_LOAD_POINT_CODE == "SI01")),Times.Once());
        }*/
        /*
        [TestMethod]
        public void SendRailwayWeighingFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string loadingPoint = "SI01";
            DateTime startDateTime = new DateTime(2022,8,1,0,0,0);
            DateTime endDateTime = new DateTime(2022,8,2,0,0,0);
            string compositionId = "C37C38-120722";
            string lotId = string.Empty;
            string operatorId = "OPERADOR";
            DUAHT_WEIGHING_CONFIG loadingPointData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 01",
                DUAH_OPERATIONAL_UNIT = "SN",
                DUAH_LOAD_POINT_CODE = "SI01",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS",
                DUAH_DESTINATION_HOST_WS = "http://172.19.204.18/WebService.svc?WSDL",
                DUAH_ACCEPT_ABNORMAL = false,
            };
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,1),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 2,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,2),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 3,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData4 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022,8,1,0,0,2),
                DUAE_REGISTRY_TIME = new DateTime(2022,8,1,0,0,0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 4,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3,weighingData4
            };
            string status = "OK";
            string tibcoXMLResponse = "<RESPONSE xmlns:ns0=\"http://test.com\">\r\n" +
                                      "<ns0:Status>NOK</ns0:Status>\r\n" +
                                      "<ns0:ExceptionDescription>Erro Teste</ns0:ExceptionDescription>\r\n" +
                                      "<ns0:ExceptionCode>666</ns0:ExceptionCode>\r\n" +
                                      "<ns0:ExceptionType>F</ns0:ExceptionType>\r\n" +
                                      "</RESPONSE>";
            SendWeighingRequest weighingRequest = new SendWeighingRequest(startDateTime,endDateTime,loadingPoint,compositionId,operatorId,lotId);
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(),It.IsAny<DateTime>(),It.IsAny<DateTime>(),It.IsAny<string>())).Returns(weighingDataList);
            mockGpvmDispatchDao.Setup(dao => dao.GetGPVMDispatchStatus(It.IsAny<DateTime>(),It.IsAny<DateTime>())).Returns(status);
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(It.IsAny<string>())).Returns(loadingPointData);
            mockTibcoDao.Setup(dao => dao.SendXMLTibco(It.IsAny<string>())).Returns(tibcoXMLResponse);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            gpvBO.SendRailwayWeighing(weighingRequest);
            mockGpvmDispatchDao.Verify(mock => mock.SaveNewGPVMDispatchData(It.Is<DUAKT_LOG_GPVM>(param => param.DUAK_EXECUTION_STATUS == "NOK" &&
                                                                                                                param.DUAK_ERROR_CODE == "666" &&
                                                                                                                param.DUAK_ERROR_TYPE == "F" &&
                                                                                                                param.DUAK_RETURN_MESSAGE == "Erro Teste")),Times.Once());
        }
        */

        [TestMethod]
        public void SendRailwayWeighingError() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            DateTime startDateTime = new DateTime(2022, 8, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 8, 2, 0, 0, 0);
            string compositionId = "C37C38-120722";
            string lotId = string.Empty;
            string operatorId = "OPERADOR";
            DUAHT_WEIGHING_CONFIG loadingPointData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_WEIGHING_LOCATION = "Silo 01",
                DUAH_OPERATIONAL_UNIT = "SN",
                DUAH_LOAD_POINT_CODE = "SI01",
                DUAH_OVERLOAD = "YES",
                DUAH_DATA_SOURCE = "WS",
                DUAH_DESTINATION_HOST_WS = "http://172.19.204.18/WebService.svc?WSDL",
                DUAH_ACCEPT_ABNORMAL = false,
            };
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 2,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 3,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData4 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 4,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3,weighingData4
            };
            string status = "OK";
            string tibcoXMLResponse = "<RESPONSE xmlns:ns0=\"http://test.com\">\r\n" +
                                      "<ns0:Status>NOK</ns0:Status>\r\n" +
                                      "<ns0:ExceptionDescription>Erro Teste</ns0:ExceptionDescription>\r\n" +
                                      "<ns0:ExceptionCode>666</ns0:ExceptionCode>\r\n" +
                                      "<ns0:ExceptionType>F</ns0:ExceptionType>\r\n" +
                                      "</RESPONSE>";
            SendWeighingRequest weighingRequest = new SendWeighingRequest(startDateTime, endDateTime, loadingPoint, compositionId, operatorId, lotId);
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Throws(new Exception());
            mockGpvmDispatchDao.Setup(dao => dao.GetGPVMDispatchStatus(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(status);
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(It.IsAny<string>())).Returns(loadingPointData);
            mockTibcoDao.Setup(dao => dao.SendAPIManagement(It.IsAny<string>(), It.IsAny<SendDestination>())).Returns(tibcoXMLResponse);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            gpvBO.SendRailwayWeighing(weighingRequest);
            mockGpvmDispatchDao.Verify(mock => mock.SaveNewGPVMDispatchData(It.IsAny<DUAKT_LOG_SEND_WEIGHING>()), Times.Never());
        }

        [TestMethod]
        public void GetLastGPVMDispatchSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string messageID = "MessageIdSNSI01";
            DUAKT_LOG_SEND_WEIGHING dispatchStatus = new DUAKT_LOG_SEND_WEIGHING() {
                DUAK_LOAD_POINT_CODE = "SI01",
                DUAK_MESSAGE_ID = "MessageIdSNSI01",
                DUAK_SEND_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAK_RETURN_TIME = new DateTime(2022, 8, 1, 0, 1, 0),
                DUAK_EXECUTION_STATUS = "OK",
                DUAK_REQUEST_SUCCESS_TOTAL = 1,
                DUAK_REQUEST_DURATION = 60,
                DUAK_REQUEST_ERROR_TOTAL = 0,
                DUAK_MESSAGE_SIZE = 1000,
            };
            mockGpvmDispatchDao.Setup(dao => dao.GetLastGPVMDispatchEntry(messageID)).Returns(dispatchStatus);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            GPVMDispatchStatus lastDispatchStatus = gpvBO.GetLastGPVMDispatch(messageID);
            Assert.IsTrue(lastDispatchStatus.TrendTime == "01/08/22 00:01:00");
            Assert.IsTrue(lastDispatchStatus.SendTime == "01/08/22 00:00:00");
            Assert.IsTrue(lastDispatchStatus.ReturnTime == "01/08/22 00:01:00");
            Assert.IsTrue(lastDispatchStatus.Status == "OK");
            Assert.IsTrue(lastDispatchStatus.ReturnMessage == null);
            Assert.IsTrue(lastDispatchStatus.ErrorCode == null);
            Assert.IsTrue(lastDispatchStatus.ErrorType == null);
        }

        [TestMethod]
        public void GetLastGPVMDispatchFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string messageID = "MessageIdSNSI01";
            DUAKT_LOG_SEND_WEIGHING dispatchStatus = new DUAKT_LOG_SEND_WEIGHING() {
                DUAK_LOAD_POINT_CODE = "SI01",
                DUAK_MESSAGE_ID = "MessageIdSNSI01",
                DUAK_SEND_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAK_RETURN_TIME = new DateTime(2022, 8, 1, 0, 1, 0),
                DUAK_EXECUTION_STATUS = "NOK",
                DUAK_RETURN_MESSAGE = "Mensagem de erro",
                DUAK_ERROR_CODE = "666",
                DUAK_ERROR_TYPE = "F",
                DUAK_REQUEST_SUCCESS_TOTAL = 1,
                DUAK_REQUEST_DURATION = 60,
                DUAK_REQUEST_ERROR_TOTAL = 0,
                DUAK_MESSAGE_SIZE = 1000,
            };
            mockGpvmDispatchDao.Setup(dao => dao.GetLastGPVMDispatchEntry(messageID)).Returns(dispatchStatus);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            GPVMDispatchStatus lastDispatchStatus = gpvBO.GetLastGPVMDispatch(messageID);
            Assert.IsTrue(lastDispatchStatus.TrendTime == "01/08/22 00:01:00");
            Assert.IsTrue(lastDispatchStatus.SendTime == "01/08/22 00:00:00");
            Assert.IsTrue(lastDispatchStatus.ReturnTime == "01/08/22 00:01:00");
            Assert.IsTrue(lastDispatchStatus.Status == "NOK");
            Assert.IsTrue(lastDispatchStatus.ReturnMessage == "Mensagem de erro");
            Assert.IsTrue(lastDispatchStatus.ErrorCode == "666");
            Assert.IsTrue(lastDispatchStatus.ErrorType == "F");
        }

        [TestMethod]
        public void GetLastGPVMDispatchError() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string messageID = "MessageIdSNSI01";
            DUAKT_LOG_SEND_WEIGHING dispatchStatus = new DUAKT_LOG_SEND_WEIGHING() {
                DUAK_LOAD_POINT_CODE = "SI01",
                DUAK_MESSAGE_ID = "MessageIdSNSI01",
                DUAK_SEND_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAK_RETURN_TIME = new DateTime(2022, 8, 1, 0, 1, 0),
                DUAK_EXECUTION_STATUS = "NOK",
                DUAK_RETURN_MESSAGE = "Mensagem de erro",
                DUAK_ERROR_CODE = "666",
                DUAK_ERROR_TYPE = "F",
                DUAK_REQUEST_SUCCESS_TOTAL = 1,
                DUAK_REQUEST_DURATION = 60,
                DUAK_REQUEST_ERROR_TOTAL = 0,
                DUAK_MESSAGE_SIZE = 1000,
            };
            mockGpvmDispatchDao.Setup(dao => dao.GetLastGPVMDispatchEntry(messageID)).Throws(new Exception());
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            GPVMDispatchStatus lastDispatchStatus = gpvBO.GetLastGPVMDispatch(messageID);
            Assert.IsTrue(lastDispatchStatus.TrendTime == null);
            Assert.IsTrue(lastDispatchStatus.SendTime == null);
            Assert.IsTrue(lastDispatchStatus.ReturnTime == null);
            Assert.IsTrue(lastDispatchStatus.Status == null);
            Assert.IsTrue(lastDispatchStatus.ReturnMessage == null);
            Assert.IsTrue(lastDispatchStatus.ErrorCode == null);
            Assert.IsTrue(lastDispatchStatus.ErrorType == null);
        }

        [TestMethod]
        public void GetCompositionCountSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string loadingPoint = "SI01";
            DateTime startTime = new DateTime(2022, 8, 1, 0, 0, 0);
            DateTime endTime = new DateTime(2022, 8, 2, 0, 0, 0);
            string compId = "C37C38-120722";
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 2,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 3,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData4 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 4,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3,weighingData4
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns(weighingDataList);
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            int compCount = gpvBO.GetCompositionCount(loadingPoint, startTime, endTime, compId);
            Assert.IsTrue(compCount == 1);
        }

        [TestMethod]
        public void GetCompositionCountError() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            string loadingPoint = "SI01";
            DateTime startTime = new DateTime(2022, 8, 1, 0, 0, 0);
            DateTime endTime = new DateTime(2022, 8, 2, 0, 0, 0);
            string compId = "C37C38-120722";
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
                DUAE_NET_WEIGHT = 108.021,
                DUAE_AXIS_NUMBER = 1,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
                DUAE_NET_WEIGHT = 105.778,
                DUAE_AXIS_NUMBER = 2,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 3,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            DUAET_REVIWED_WEIGHING weighingData4 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 8, 1, 0, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 8, 1, 0, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
                DUAE_CAR_TYPE = 1,
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
                DUAE_NET_WEIGHT = 105.263,
                DUAE_AXIS_NUMBER = 4,
                DUAE_LEFT_WHEEL_TARE = 0,
                DUAE_RIGHT_WHEEL_TARE = 0,
                DUAE_LEFT_WHEEL_GROSS = 0,
                DUAE_RIGHT_WHEEL_GROSS = 0,
                DUAE_LEFT_WHEEL_NET = 0,
                DUAE_RIGHT_WHEEL_NET = 0,
                DUAE_AXIS_TARE = 0,
                DUAE_AXIS_GROSS = 0,
                DUAE_AXIS_NET = 0,
                DUAE_TRUCK_TARE = 0,
                DUAE_TRUCK_GROSS = 0,
                DUAE_TRUCK_NET = 0,
                DUAE_DATA_TYPE = "NORMAL",
                DUAE_ID_PRODUCT = "FERRO",
                DUAE_OPERATOR_ID = "OPERADOR",
                DUAE_INTER_WHEIGHT = null,
                DUAE_LOAD_REMOVER = null,
                DUAE_SPEED = 0,
                DUAE_SPEED_EXCEEDED = string.Empty,
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
                DUAE_OPERATOR_WEIGHT = "1507447",
                DUAE_OVERLOAD_STATUS = "NO",
                DUAE_OVERLOAD_WEIGHT = null,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3,weighingData4
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Throws(new Exception());
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            int compCount = gpvBO.GetCompositionCount(loadingPoint, startTime, endTime, compId);
            Assert.IsTrue(compCount == 0);
        }

        /*[TestMethod]
        public void PostConsolidatedWeighingSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            AxisData axis1 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            AxisData axis2 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            AxisData axis3 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            AxisData axis4 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            List<AxisData> axisDataList = new List<AxisData>() { axis1,axis2,axis3,axis4 };
            TruckData truckData1 = new TruckData() {
                TruckTare = 0,
                TruckGross = 0,
                TruckNet = 0,
            };
            TruckData truckData2 = new TruckData() {
                TruckTare = 0,
                TruckGross = 0,
                TruckNet = 0,
            };
            List<TruckData> truckDataList = new List<TruckData>() { truckData1,truckData2 };
            PostConsolidatedWeighingRequest newConsolidatedWeighing = new PostConsolidatedWeighingRequest() {
                AxisDataList = axisDataList,
                TruckDataList = truckDataList,
                LoadingPoint = "SI01",
                CarNumber = 1,
                CompositionId = "C37C38-120722",
                RegisterTime = new DateTime(2022,8,1,0,0,0),
                CarType = 1,
                Product = "FERRO",
                OperatorId = "OPERADOR",
                User = "User",
                DataType = "NORMAL",
                DataSource = "WS",
                Status = "Good",
                Justification = "",
                LastRegistryValid = "YES",
                LotId = null,
                TareWeight = 21.695,
                GrossWeight = 129.716,
                NetWeight = 108.021,
            };
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            InsertStatus insertStatus = gpvBO.PostConsolidatedWeighing(newConsolidatedWeighing);
            mockWeighingConsolidatedDao.Verify(mock => mock.InsertConsolidatedValues(It.IsAny<DUAET_REVIWED_WEIGHING>()),Times.Exactly(4));
            Assert.IsTrue(insertStatus.Status == "OK");
            Assert.IsTrue(insertStatus.Description == "Dados gravados com sucesso.");
        }*/
        
        /*[TestMethod]
        public void PostConsolidatedWeighingFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IAPIManagement> mockTibcoDao = new Mock<IAPIManagement>();
            Mock<IDestinationDao> mockDestinationDao = new Mock<IDestinationDao>();
            Mock<IGPVMDispatchDao> mockGpvmDispatchDao = new Mock<IGPVMDispatchDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonBO> mockWagonBO = new Mock<IWagonBO>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();

            AxisData axis1 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            AxisData axis2 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            AxisData axis3 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            AxisData axis4 = new AxisData() {
                LeftWheelTare = 0,
                LeftWheelGross = 0,
                LeftWheelNet = 0,
                RightWheelTare = 0,
                RightWheelGross = 0,
                RightWheelNet = 0,
                AxisTare = 0,
                AxisGross = 0,
                AxisNet = 0,
                Speed = 0,
                SpeedExceeded = "N",
            };
            List<AxisData> axisDataList = new List<AxisData>() { axis1,axis2,axis3,axis4 };
            TruckData truckData1 = new TruckData() {
                TruckTare = 0,
                TruckGross = 0,
                TruckNet = 0,
            };
            TruckData truckData2 = new TruckData() {
                TruckTare = 0,
                TruckGross = 0,
                TruckNet = 0,
            };
            List<TruckData> truckDataList = new List<TruckData>() { truckData1,truckData2 };
            PostConsolidatedWeighingRequest newConsolidatedWeighing = new PostConsolidatedWeighingRequest() {
                AxisDataList = axisDataList,
                TruckDataList = truckDataList,
                LoadingPoint = "SI01",
                CarNumber = 1,
                CompositionId = "C37C38-120722",
                RegisterTime = new DateTime(2022,8,1,0,0,0),
                CarType = 1,
                Product = "FERRO",
                OperatorId = "OPERADOR",
                User = "User",
                DataType = "NORMAL",
                DataSource = "WS",
                Status = "Good",
                Justification = "",
                LastRegistryValid = "YES",
                LotId = null,
                TareWeight = 21.695,
                GrossWeight = 129.716,
                NetWeight = 108.021,
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.InsertConsolidatedValues(It.IsAny<DUAET_REVIWED_WEIGHING>())).Throws(new Exception("Teste erro"));
            IGPVBO gpvBO = new GPVBOImpl(mockLoggerBO.Object, mockTibcoDao.Object, mockDestinationDao.Object, mockGpvmDispatchDao.Object, mockLoadingPointDao.Object, mockWagonBO.Object, mockWeighingConsolidatedDao.Object);
            InsertStatus insertStatus = gpvBO.PostConsolidatedWeighing(newConsolidatedWeighing);
            mockWeighingConsolidatedDao.Verify(mock => mock.InsertConsolidatedValues(It.IsAny<DUAET_REVIWED_WEIGHING>()),Times.Once());
            Assert.IsTrue(insertStatus.Status == "ERROR");
            Assert.IsTrue(insertStatus.Description == "Teste erro");
        }*/
    }
}