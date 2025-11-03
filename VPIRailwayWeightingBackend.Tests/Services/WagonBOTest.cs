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
    public class WagonBOTest {

        [TestMethod]
        public void GetValidWeightTypeSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();
            string loadingPoint = "SI01";
            DUAHT_WEIGHING_CONFIG loadingPointTableData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_GDU_LIMIT_TARE = 22.0,
                DUAH_GDU_LIMIT_GROSS = 131.0,
                DUAH_WAGON_TYPE_TOLERANCE = 24,
                DUAH_TOLERANCE_LIMIT_GROSS = 13.0,
                DUAH_TOLERANCE_LIMIT_TARE = 22.0,
                DUAH_GDT_LIMIT_GROSS = 13.0,
                DUAH_GDT_LIMIT_TARE = 21.0,
                DUAH_HAT_LIMIT_GROSS = 13,
                DUAH_HAT_LIMIT_TARE = 21.0,
                DUAH_TARE_WEIGHT_MAX = 24.0,
            };
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(loadingPoint)).Returns(loadingPointTableData);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            WagonParameters wagonParameters = wagonBO.GetValidWeightType(loadingPoint);
            Assert.IsTrue(wagonParameters.GDULimitTare == 22);
            Assert.IsTrue(wagonParameters.GDULimitGross == 131);
            Assert.IsTrue(wagonParameters.GDUTolerance == 24);
            Assert.IsTrue(wagonParameters.GDUToleranceLimitGross == 13);
            Assert.IsTrue(wagonParameters.GDUToleranceLimitTare == 22);
            Assert.IsTrue(wagonParameters.GDTLimitGross == 13);
            Assert.IsTrue(wagonParameters.GDTLimitTare == 21);
            Assert.IsTrue(wagonParameters.HATLimitGross == 13);
            Assert.IsTrue(wagonParameters.HATLimitTare == 21);
            Assert.IsTrue(wagonParameters.TareWeightMax == 24);
            Assert.IsTrue(wagonParameters.GDUType == "GDU");
            Assert.IsTrue(wagonParameters.GDUTareType == "GDU_TARE");
            Assert.IsTrue(wagonParameters.GDTType == "GDT");
            Assert.IsTrue(wagonParameters.GDTTareType == "GDT_TARE");
            Assert.IsTrue(wagonParameters.HATType == "HAT");
            Assert.IsTrue(wagonParameters.HATTareType == "HAT_TARE");
        }

        [TestMethod]
        public void GetValidWeightTypeFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            DUAHT_WEIGHING_CONFIG loadingPointTableData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_GDU_LIMIT_TARE = 22.0,
                DUAH_GDU_LIMIT_GROSS = 131.0,
                DUAH_WAGON_TYPE_TOLERANCE = 24,
                DUAH_TOLERANCE_LIMIT_GROSS = 13.0,
                DUAH_TOLERANCE_LIMIT_TARE = 22.0,
                DUAH_GDT_LIMIT_GROSS = 13.0,
                DUAH_GDT_LIMIT_TARE = 21.0,
                DUAH_HAT_LIMIT_GROSS = 13,
                DUAH_HAT_LIMIT_TARE = 21.0,
                DUAH_TARE_WEIGHT_MAX = 24.0,
            };
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(loadingPoint)).Throws(new Exception());
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            WagonParameters wagonParameters = wagonBO.GetValidWeightType(loadingPoint);
            Assert.IsTrue(wagonParameters.GDULimitGross == null);
            Assert.IsTrue(wagonParameters.GDULimitGross == null);
            Assert.IsTrue(wagonParameters.GDUTolerance == null);
            Assert.IsTrue(wagonParameters.GDUToleranceLimitGross == null);
            Assert.IsTrue(wagonParameters.GDUToleranceLimitTare == null);
            Assert.IsTrue(wagonParameters.GDTLimitGross == null);
            Assert.IsTrue(wagonParameters.GDTLimitTare == null);
            Assert.IsTrue(wagonParameters.HATLimitGross == null);
            Assert.IsTrue(wagonParameters.HATLimitTare == null);
            Assert.IsTrue(wagonParameters.TareWeightMax == 0);
            Assert.IsTrue(wagonParameters.GDUType == "GDU");
            Assert.IsTrue(wagonParameters.GDUTareType == "GDU_TARE");
            Assert.IsTrue(wagonParameters.GDTType == "GDT");
            Assert.IsTrue(wagonParameters.GDTTareType == "GDT_TARE");
            Assert.IsTrue(wagonParameters.HATType == "HAT");
            Assert.IsTrue(wagonParameters.HATTareType == "HAT_TARE");
        }

        [TestMethod]
        public void GetCompositionsSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            DateTime startDatetime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDatetime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUADT_WEIGHING weighingData1 = new DUADT_WEIGHING() {
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 1,
                DUAD_TARE_WEIGHT = 21.695,
                DUAD_GROSS_WEIGHT = 129.716,
                DUAD_NET_WEIGHT = 108.021,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
            };
            DUADT_WEIGHING weighingData2 = new DUADT_WEIGHING() {
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_ID_COMPOSITION = "L25M18-110722",
                DUAD_CAR_NUMBER = 2,
                DUAD_TARE_WEIGHT = 21.475,
                DUAD_GROSS_WEIGHT = 127.253,
                DUAD_NET_WEIGHT = 105.778,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
            };
            DUADT_WEIGHING weighingData3 = new DUADT_WEIGHING() {
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_ID_COMPOSITION = "L29M852-11072022",
                DUAD_CAR_NUMBER = 3,
                DUAD_TARE_WEIGHT = 21.999,
                DUAD_GROSS_WEIGHT = 127.262,
                DUAD_NET_WEIGHT = 105.263,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
            };
            List<DUADT_WEIGHING> weighingDataList = new List<DUADT_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetCompositions(loadingPoint, startDatetime, endDatetime)).Returns(weighingDataList);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<CompositionData> compositionDataList = wagonBO.GetCompositions(loadingPoint, startDatetime, endDatetime);
            Assert.IsTrue(compositionDataList.Count == 3);
            Assert.IsTrue(compositionDataList[0].CompositionId == "C37C38-120722");
            Assert.IsTrue(compositionDataList[0].TimestampFirstWagon == new DateTime(2022, 7, 1, 3, 0, 0));
            Assert.IsTrue(compositionDataList[1].CompositionId == "L25M18-110722");
            Assert.IsTrue(compositionDataList[1].TimestampFirstWagon == new DateTime(2022, 7, 1, 6, 0, 1));
            Assert.IsTrue(compositionDataList[2].CompositionId == "L29M852-11072022");
            Assert.IsTrue(compositionDataList[2].TimestampFirstWagon == new DateTime(2022, 7, 1, 9, 0, 2));
        }

        [TestMethod]
        public void GetCompositionsFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            DateTime startDatetime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDatetime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_CAR_NUMBER = 1,
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
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAE_ID_COMPOSITION = "L25M18-110722",
                DUAE_CAR_NUMBER = 2,
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
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAE_ID_COMPOSITION = "L29M852-11072022",
                DUAE_CAR_NUMBER = 3,
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
                DUAE_LOT_GPV_ID = string.Empty,
                DUAE_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAE_JUSTIFICATION = string.Empty,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetCompositions(loadingPoint, startDatetime, endDatetime)).Throws(new Exception());
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<CompositionData> compositionDataList = wagonBO.GetCompositions(loadingPoint, startDatetime, endDatetime);
            Assert.IsTrue(compositionDataList.Count == 0);
        }

        [TestMethod]
        public void GetWagonsOPCSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUADT_WEIGHING weighingData1 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 1,
                DUAD_CAR_TYPE = 1,
                DUAD_TARE_WEIGHT = 21.695,
                DUAD_GROSS_WEIGHT = 129.716,
                DUAD_NET_WEIGHT = 108.021,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData2 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 2,
                DUAD_CAR_TYPE = 2,
                DUAD_TARE_WEIGHT = 21.475,
                DUAD_GROSS_WEIGHT = 127.253,
                DUAD_NET_WEIGHT = 105.778,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData3 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 3,
                DUAD_CAR_TYPE = 3,
                DUAD_TARE_WEIGHT = 21.999,
                DUAD_GROSS_WEIGHT = 127.262,
                DUAD_NET_WEIGHT = 105.263,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            List<DUADT_WEIGHING> weighingDataList = new List<DUADT_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetWagons(loadingPoint, compositionId, startDateTime, endDateTime)).Returns(weighingDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.ValidateWeighingsExist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetWagonsOPC(loadingPoint, compositionId, startDateTime, endDateTime);
            Assert.IsTrue(wagonsDataList.Count == 3);
            Assert.IsTrue(wagonsDataList[0].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[0].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[0].RegisterTime == "01/07/22 03:00:00");
            Assert.IsTrue(wagonsDataList[0].RegistryTime == new DateTime(2022, 7, 1, 3, 0, 0));
            Assert.IsTrue(wagonsDataList[0].CarNumber == 1);
            Assert.IsTrue(wagonsDataList[0].AxisNumber == 1);
            Assert.IsTrue(wagonsDataList[0].CarType == "GDU");
            Assert.IsTrue(wagonsDataList[0].TareWeight == 21.695);
            Assert.IsTrue(wagonsDataList[0].GrossWeight == 129.716);
            Assert.IsTrue(wagonsDataList[0].NetWeight == 108.021);
            Assert.IsTrue(wagonsDataList[0].LeftWheelTare == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelTare == 0);
            Assert.IsTrue(wagonsDataList[0].LeftWheelGross == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelGross == 0);
            Assert.IsTrue(wagonsDataList[0].LeftWheelNet == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelNet == 0);
            Assert.IsTrue(wagonsDataList[0].AxisTare == 0);
            Assert.IsTrue(wagonsDataList[0].AxisGross == 0);
            Assert.IsTrue(wagonsDataList[0].AxisNet == 0);
            Assert.IsTrue(wagonsDataList[0].TruckTare == 0);
            Assert.IsTrue(wagonsDataList[0].TruckGross == 0);
            Assert.IsTrue(wagonsDataList[0].TruckNet == 0);
            Assert.IsTrue(wagonsDataList[0].ProductId == "FERRO");
            Assert.IsTrue(wagonsDataList[0].DataType == "NORMAL");
            Assert.IsTrue(wagonsDataList[0].LotId == "");
            Assert.IsTrue(wagonsDataList[0].DataWeightingSource == "Pesagem Normal");
            Assert.IsTrue(wagonsDataList[0].Justification == "");
            Assert.IsTrue(wagonsDataList[0].InterWeight == null);
            Assert.IsTrue(wagonsDataList[0].LoadRemover == null);
            Assert.IsTrue(wagonsDataList[0].Speed == 0);
            Assert.IsTrue(wagonsDataList[0].SpeedExceeded == "");
            Assert.IsTrue(wagonsDataList[0].OperatorWeight == "1507447");
            Assert.IsTrue(wagonsDataList[0].OperatorId == "OPERADOR");
            Assert.IsTrue(wagonsDataList[0].OverloadType == "NO");
            Assert.IsTrue(wagonsDataList[0].OverloadWeight == null);
            Assert.IsTrue(wagonsDataList[1].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[1].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[1].RegistryTime == new DateTime(2022, 7, 1, 6, 0, 1));
            Assert.IsTrue(wagonsDataList[1].RegisterTime == "01/07/22 06:00:01");
            Assert.IsTrue(wagonsDataList[1].TareWeight == 21.475);
            Assert.IsTrue(wagonsDataList[1].GrossWeight == 127.253);
            Assert.IsTrue(wagonsDataList[1].NetWeight == 105.778);
            Assert.IsTrue(wagonsDataList[2].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[2].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[2].RegisterTime == "01/07/22 09:00:02");
            Assert.IsTrue(wagonsDataList[2].RegistryTime == new DateTime(2022, 7, 1, 9, 0, 2));
            Assert.IsTrue(wagonsDataList[2].TareWeight == 21.999);
            Assert.IsTrue(wagonsDataList[2].GrossWeight == 127.262);
            Assert.IsTrue(wagonsDataList[2].NetWeight == 105.263);
        }

        [TestMethod]
        public void GetWagonsOPCNoData() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUADT_WEIGHING weighingData1 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 1,
                DUAD_CAR_TYPE = 1,
                DUAD_TARE_WEIGHT = 21.695,
                DUAD_GROSS_WEIGHT = 129.716,
                DUAD_NET_WEIGHT = 108.021,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData2 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 2,
                DUAD_CAR_TYPE = 2,
                DUAD_TARE_WEIGHT = 21.475,
                DUAD_GROSS_WEIGHT = 127.253,
                DUAD_NET_WEIGHT = 105.778,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData3 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 3,
                DUAD_CAR_TYPE = 3,
                DUAD_TARE_WEIGHT = 21.999,
                DUAD_GROSS_WEIGHT = 127.262,
                DUAD_NET_WEIGHT = 105.263,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            List<DUADT_WEIGHING> weighingDataList = new List<DUADT_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetWagons(loadingPoint, compositionId, startDateTime, endDateTime)).Returns(weighingDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.ValidateWeighingsExist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetWagonsOPC(loadingPoint, compositionId, startDateTime, endDateTime);
            Assert.IsTrue(wagonsDataList.Count == 0);
        }

        [TestMethod]
        public void GetWagonsOPCFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
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
            mockWeighingConsolidatedDao.Setup(dao => dao.GetWagons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Throws(new Exception());
            mockWeighingConsolidatedDao.Setup(dao => dao.ValidateWeighingsExist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetWagonsOPC(loadingPoint, compositionId, startDateTime, endDateTime);
            Assert.IsTrue(wagonsDataList.Count == 0);
        }

        [TestMethod]
        public void GetWagonsSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUADT_WEIGHING weighingData1 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 1,
                DUAD_CAR_TYPE = 1,
                DUAD_TARE_WEIGHT = 21.695,
                DUAD_GROSS_WEIGHT = 129.716,
                DUAD_NET_WEIGHT = 108.021,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData2 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 2,
                DUAD_CAR_TYPE = 2,
                DUAD_TARE_WEIGHT = 21.475,
                DUAD_GROSS_WEIGHT = 127.253,
                DUAD_NET_WEIGHT = 105.778,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData3 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 3,
                DUAD_CAR_TYPE = 3,
                DUAD_TARE_WEIGHT = 21.999,
                DUAD_GROSS_WEIGHT = 127.262,
                DUAD_NET_WEIGHT = 105.263,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            List<DUADT_WEIGHING> weighingDataList = new List<DUADT_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetWagons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(weighingDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.ValidateWeighingsExist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetWagons(loadingPoint, compositionId, startDateTime, endDateTime);
            Assert.IsTrue(wagonsDataList.Count == 3);
            Assert.IsTrue(wagonsDataList[0].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[0].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[0].RegisterTime == "01/07/22 03:00:00");
            Assert.IsTrue(wagonsDataList[0].RegistryTime == new DateTime(2022, 7, 1, 3, 0, 0));
            Assert.IsTrue(wagonsDataList[0].CarNumber == 1);
            Assert.IsTrue(wagonsDataList[0].AxisNumber == 1);
            Assert.IsTrue(wagonsDataList[0].CarType == "GDU");
            Assert.IsTrue(wagonsDataList[0].TareWeight == 21.695);
            Assert.IsTrue(wagonsDataList[0].GrossWeight == 129.716);
            Assert.IsTrue(wagonsDataList[0].NetWeight == 108.021);
            Assert.IsTrue(wagonsDataList[0].LeftWheelTare == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelTare == 0);
            Assert.IsTrue(wagonsDataList[0].LeftWheelGross == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelGross == 0);
            Assert.IsTrue(wagonsDataList[0].LeftWheelNet == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelNet == 0);
            Assert.IsTrue(wagonsDataList[0].AxisTare == 0);
            Assert.IsTrue(wagonsDataList[0].AxisGross == 0);
            Assert.IsTrue(wagonsDataList[0].AxisNet == 0);
            Assert.IsTrue(wagonsDataList[0].TruckTare == 0);
            Assert.IsTrue(wagonsDataList[0].TruckGross == 0);
            Assert.IsTrue(wagonsDataList[0].TruckNet == 0);
            Assert.IsTrue(wagonsDataList[0].ProductId == "FERRO");
            Assert.IsTrue(wagonsDataList[0].DataType == "NORMAL");
            Assert.IsTrue(wagonsDataList[0].LotId == "");
            Assert.IsTrue(wagonsDataList[0].DataWeightingSource == "Pesagem Normal");
            Assert.IsTrue(wagonsDataList[0].Justification == "");
            Assert.IsTrue(wagonsDataList[0].InterWeight == null);
            Assert.IsTrue(wagonsDataList[0].LoadRemover == null);
            Assert.IsTrue(wagonsDataList[0].Speed == 0);
            Assert.IsTrue(wagonsDataList[0].SpeedExceeded == "");
            Assert.IsTrue(wagonsDataList[0].OperatorWeight == "1507447");
            Assert.IsTrue(wagonsDataList[0].OperatorId == "OPERADOR");
            Assert.IsTrue(wagonsDataList[0].OverloadType == "NO");
            Assert.IsTrue(wagonsDataList[0].OverloadWeight == null);
            Assert.IsTrue(wagonsDataList[1].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[1].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[1].RegistryTime == new DateTime(2022, 7, 1, 6, 0, 1));
            Assert.IsTrue(wagonsDataList[1].RegisterTime == "01/07/22 06:00:01");
            Assert.IsTrue(wagonsDataList[1].TareWeight == 21.475);
            Assert.IsTrue(wagonsDataList[1].GrossWeight == 127.253);
            Assert.IsTrue(wagonsDataList[1].NetWeight == 105.778);
            Assert.IsTrue(wagonsDataList[2].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[2].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[2].RegisterTime == "01/07/22 09:00:02");
            Assert.IsTrue(wagonsDataList[2].RegistryTime == new DateTime(2022, 7, 1, 9, 0, 2));
            Assert.IsTrue(wagonsDataList[2].TareWeight == 21.999);
            Assert.IsTrue(wagonsDataList[2].GrossWeight == 127.262);
            Assert.IsTrue(wagonsDataList[2].NetWeight == 105.263);
        }

        [TestMethod]
        public void GetWagonsNoData() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUADT_WEIGHING weighingData1 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 1,
                DUAD_CAR_TYPE = 1,
                DUAD_TARE_WEIGHT = 21.695,
                DUAD_GROSS_WEIGHT = 129.716,
                DUAD_NET_WEIGHT = 108.021,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData2 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 2,
                DUAD_CAR_TYPE = 2,
                DUAD_TARE_WEIGHT = 21.475,
                DUAD_GROSS_WEIGHT = 127.253,
                DUAD_NET_WEIGHT = 105.778,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            DUADT_WEIGHING weighingData3 = new DUADT_WEIGHING() {
                DUAD_LOAD_POINT_CODE = "SI01",
                DUAD_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAD_ID_COMPOSITION = "C37C38-120722",
                DUAD_CAR_NUMBER = 3,
                DUAD_CAR_TYPE = 3,
                DUAD_TARE_WEIGHT = 21.999,
                DUAD_GROSS_WEIGHT = 127.262,
                DUAD_NET_WEIGHT = 105.263,
                DUAD_AXIS_NUMBER = 1,
                DUAD_LEFT_WHEEL_TARE = 0,
                DUAD_RIGHT_WHEEL_TARE = 0,
                DUAD_LEFT_WHEEL_GROSS = 0,
                DUAD_RIGHT_WHEEL_GROSS = 0,
                DUAD_LEFT_WHEEL_NET = 0,
                DUAD_RIGHT_WHEEL_NET = 0,
                DUAD_AXIS_TARE = 0,
                DUAD_AXIS_GROSS = 0,
                DUAD_AXIS_NET = 0,
                DUAD_TRUCK_TARE = 0,
                DUAD_TRUCK_GROSS = 0,
                DUAD_TRUCK_NET = 0,
                DUAD_DATA_TYPE = "NORMAL",
                DUAD_ID_PRODUCT = "FERRO",
                DUAD_OPERATOR_ID = "OPERADOR",
                DUAD_INTER_WHEIGHT = null,
                DUAD_LOAD_REMOVER = null,
                DUAD_SPEED = 0,
                DUAD_SPEED_EXCEEDED = string.Empty,
                DUAD_LOT_GPV_ID = string.Empty,
                DUAD_DATA_WEIGHING_SOURCE = "Pesagem Normal",
                DUAD_JUSTIFICATION = string.Empty,
                DUAD_OPERATOR_WEIGHT = "1507447",
                DUAD_OVERLOAD_STATUS = "NO",
                DUAD_OVERLOAD_WEIGHT = null,
            };
            List<DUADT_WEIGHING> weighingDataList = new List<DUADT_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetWagons(loadingPoint, compositionId, startDateTime, endDateTime)).Returns(weighingDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.ValidateWeighingsExist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(false);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetWagons(loadingPoint, compositionId, startDateTime, endDateTime);
            Assert.IsTrue(wagonsDataList.Count == 0);
        }

        [TestMethod]
        public void GetWagonsFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
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
            mockWeighingConsolidatedDao.Setup(dao => dao.GetWagons(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Throws(new Exception());
            mockWeighingConsolidatedDao.Setup(dao => dao.ValidateWeighingsExist(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetWagons(loadingPoint, compositionId, startDateTime, endDateTime);
            Assert.IsTrue(wagonsDataList.Count == 0);
        }

        [TestMethod]
        public void GetConsolidatedValuesSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
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
                DUANT_WEIGHING_DESTINATION = new DUANT_WEIGHING_DESTINATION() {
                    DUAN_WEIGHING_DESTINATION_CODE = "DEST01"
                }
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
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
                DUANT_WEIGHING_DESTINATION = new DUANT_WEIGHING_DESTINATION() {
                    DUAN_WEIGHING_DESTINATION_CODE = "DEST01"
                }
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
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
                DUANT_WEIGHING_DESTINATION = new DUANT_WEIGHING_DESTINATION() {
                    DUAN_WEIGHING_DESTINATION_CODE = "DEST01"
                }
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns(weighingDataList);
            mockWeighingConsolidatedMrsDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Returns(weighingDataList);
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetConsolidatedValues(loadingPoint, startDateTime, endDateTime, compositionId);
            Assert.IsTrue(wagonsDataList.Count == 3);
            Assert.IsTrue(wagonsDataList[0].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[0].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[0].RegisterTime == "01/07/22 03:00:00");
            Assert.IsTrue(wagonsDataList[0].RegistryTime == new DateTime(2022, 7, 1, 3, 0, 0));
            Assert.IsTrue(wagonsDataList[0].CarNumber == 1);
            Assert.IsTrue(wagonsDataList[0].AxisNumber == 1);
            Assert.IsTrue(wagonsDataList[0].CarType == "GDU");
            Assert.IsTrue(wagonsDataList[0].TareWeight == 21.695);
            Assert.IsTrue(wagonsDataList[0].GrossWeight == 129.716);
            Assert.IsTrue(wagonsDataList[0].NetWeight == 108.021);
            Assert.IsTrue(wagonsDataList[0].LeftWheelTare == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelTare == 0);
            Assert.IsTrue(wagonsDataList[0].LeftWheelGross == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelGross == 0);
            Assert.IsTrue(wagonsDataList[0].LeftWheelNet == 0);
            Assert.IsTrue(wagonsDataList[0].RightWheelNet == 0);
            Assert.IsTrue(wagonsDataList[0].AxisTare == 0);
            Assert.IsTrue(wagonsDataList[0].AxisGross == 0);
            Assert.IsTrue(wagonsDataList[0].AxisNet == 0);
            Assert.IsTrue(wagonsDataList[0].TruckTare == 0);
            Assert.IsTrue(wagonsDataList[0].TruckGross == 0);
            Assert.IsTrue(wagonsDataList[0].TruckNet == 0);
            Assert.IsTrue(wagonsDataList[0].ProductId == "FERRO");
            Assert.IsTrue(wagonsDataList[0].DataType == "NORMAL");
            Assert.IsTrue(wagonsDataList[0].LotId == "");
            Assert.IsTrue(wagonsDataList[0].DataWeightingSource == "Pesagem Normal");
            Assert.IsTrue(wagonsDataList[0].Justification == "");
            Assert.IsTrue(wagonsDataList[0].InterWeight == null);
            Assert.IsTrue(wagonsDataList[0].LoadRemover == null);
            Assert.IsTrue(wagonsDataList[0].Speed == 0);
            Assert.IsTrue(wagonsDataList[0].SpeedExceeded == "");
            Assert.IsTrue(wagonsDataList[0].OperatorWeight == "1507447");
            Assert.IsTrue(wagonsDataList[0].OperatorId == "OPERADOR");
            Assert.IsTrue(wagonsDataList[0].OverloadType == "NO");
            Assert.IsTrue(wagonsDataList[0].OverloadWeight == null);
            Assert.IsTrue(wagonsDataList[1].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[1].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[1].RegistryTime == new DateTime(2022, 7, 1, 6, 0, 1));
            Assert.IsTrue(wagonsDataList[1].RegisterTime == "01/07/22 06:00:01");
            Assert.IsTrue(wagonsDataList[1].TareWeight == 21.475);
            Assert.IsTrue(wagonsDataList[1].GrossWeight == 127.253);
            Assert.IsTrue(wagonsDataList[1].NetWeight == 105.778);
            Assert.IsTrue(wagonsDataList[2].LoadingPoint == "SI01");
            Assert.IsTrue(wagonsDataList[2].CompositionId == "C37C38-120722");
            Assert.IsTrue(wagonsDataList[2].RegisterTime == "01/07/22 09:00:02");
            Assert.IsTrue(wagonsDataList[2].RegistryTime == new DateTime(2022, 7, 1, 9, 0, 2));
            Assert.IsTrue(wagonsDataList[2].TareWeight == 21.999);
            Assert.IsTrue(wagonsDataList[2].GrossWeight == 127.262);
            Assert.IsTrue(wagonsDataList[2].NetWeight == 105.263);
        }

        [TestMethod]
        public void GetConsolidatedValuesFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IWeighingConsolidatedMrsDao> mockWeighingConsolidatedMrsDao = new Mock<IWeighingConsolidatedMrsDao>();
            Mock<IWagonParametersDao> mockWagonParametersDao = new Mock<IWagonParametersDao>();

            string loadingPoint = "SI01";
            string compositionId = "C37C38-120722";
            DateTime startDateTime = new DateTime(2022, 7, 1, 0, 0, 0);
            DateTime endDateTime = new DateTime(2022, 7, 1, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 3, 0, 0),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 6, 0, 1),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
                DUAE_REGISTRY_TIME = new DateTime(2022, 7, 1, 9, 0, 2),
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
            mockWeighingConsolidatedDao.Setup(dao => dao.GetConsolidatedValues(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>())).Throws(new Exception());
            IWagonBO wagonBO = new WagonBOImpl(mockLoggerBO.Object, mockLoadingPointDao.Object, mockWeighingConsolidatedDao.Object, mockWeighingConsolidatedMrsDao.Object, mockWagonParametersDao.Object);
            List<WagonData> wagonsDataList = wagonBO.GetConsolidatedValues(loadingPoint, startDateTime, endDateTime, compositionId);
            Assert.IsTrue(wagonsDataList.Count == 0);
        }
    }
}