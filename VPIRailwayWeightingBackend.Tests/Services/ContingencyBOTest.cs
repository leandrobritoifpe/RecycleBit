using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HarpiaCommon.Models.Request;
using HarpiaCommon.Models.Response;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Response;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class ContingencyBOTest {

        [TestMethod]
        public void GetSimulatedValuesSuccess() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            string loadingPoint = "SI01";
            string produto = "FERRO";
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetLastCompositions(loadingPoint, produto)).Returns(weighingDataList);
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            SimulatedValues simulatedData = contingencyBO.GetSimulatedValues(loadingPoint, produto);
            Assert.IsTrue(simulatedData.AverageTare == 21.723);
            Assert.IsTrue(simulatedData.AverageGross == 128.077);
        }

        [TestMethod]
        public void GetSimulatedValuesFailure() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            string loadingPoint = "SI01";
            string produto = "FERRO";
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_TARE_WEIGHT = 21.695,
                DUAE_GROSS_WEIGHT = 129.716,
            };
            DUAET_REVIWED_WEIGHING weighingData2 = new DUAET_REVIWED_WEIGHING() {
                DUAE_TARE_WEIGHT = 21.475,
                DUAE_GROSS_WEIGHT = 127.253,
            };
            DUAET_REVIWED_WEIGHING weighingData3 = new DUAET_REVIWED_WEIGHING() {
                DUAE_TARE_WEIGHT = 21.999,
                DUAE_GROSS_WEIGHT = 127.262,
            };
            List<DUAET_REVIWED_WEIGHING> weighingDataList = new List<DUAET_REVIWED_WEIGHING>() {
                weighingData1,weighingData2,weighingData3
            };
            mockWeighingConsolidatedDao.Setup(dao => dao.GetLastCompositions(loadingPoint, produto)).Throws(new Exception());
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            SimulatedValues simulatedData = contingencyBO.GetSimulatedValues(loadingPoint, produto);
            Assert.IsTrue(simulatedData.AverageTare == null);
            Assert.IsTrue(simulatedData.AverageGross == null);
        }

        //[TestMethod]
        //public void GetWeightValuesSuccess() {
        //    Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
        //    Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
        //    Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
        //    Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
        //    string scaleTag = "SCALE_PIPOINT";
        //    string datetime = "2022-05-02T00:00:00-03:00";
        //    PiPointValueByDateTimeAndTypeRead piPointDto = new PiPointValueByDateTimeAndTypeRead() {
        //        DateTime = DateTime.Parse("2022-05-02T00:00:00-03:00"),
        //        PiPoints = new List<PiPointValueDto>() { new PiPointValueDto("SCALE_PIPOINT", "25") },
        //        ReadType = ValueReadTypeEnum.Stepped
        //    };
        //    HttpRequestMessage httpRequest = new HttpRequestMessage();
        //    httpRequest.SetConfiguration(new HttpConfiguration());
        //    HttpResponseMessage expectedResult = httpRequest.CreateResponse(HttpStatusCode.OK, piPointDto);
        //    mockVpiCoreBO.Setup(bo => bo.GetPiPointValueByDateTimeAndTypeRead(It.IsAny<PiPointValueByDateTimeAndTypeReadRequest>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResult.Content.ReadAsAsync<PiPointValueByDateTimeAndTypeRead>());
        //    IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
        //    double weightValue = contingencyBO.GetWeightValue(scaleTag, datetime);
        //    Assert.IsTrue(weightValue == 25);
        //}

        //[TestMethod]
        //public void GetWeightValuesFailureInvalidTag() {
        //    Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
        //    Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
        //    Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
        //    Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
        //    string scaleTag = "SCALE_PIPOINT";
        //    string datetime = "2022-05-02T00:00:00-03:00";
        //    PiPointValueDto piPoint = new PiPointValueDto() {
        //        Error = new Error("The specified path was not found. If more details are needed, please contact your PI Web API administrator for help in enabling debug mode.")
        //    };
        //    PiPointValueByDateTimeAndTypeRead piPointDto = new PiPointValueByDateTimeAndTypeRead() {
        //        PiPoints = new List<PiPointValueDto>() { piPoint }
        //    };
        //    HttpRequestMessage httpRequest = new HttpRequestMessage();
        //    httpRequest.SetConfiguration(new HttpConfiguration());
        //    HttpResponseMessage expectedResult = httpRequest.CreateResponse(HttpStatusCode.OK, piPointDto);
        //    mockVpiCoreBO.Setup(bo => bo.GetPiPointValueByDateTimeAndTypeRead(It.IsAny<PiPointValueByDateTimeAndTypeReadRequest>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResult.Content.ReadAsAsync<PiPointValueByDateTimeAndTypeRead>());
        //    IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
        //    double weightValue = contingencyBO.GetWeightValue(scaleTag, datetime);
        //    Assert.IsTrue(weightValue == 0);
        //}

        //[TestMethod]
        //public void GetFlowTagValueSuccess() {
        //    Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
        //    Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
        //    Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
        //    Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
        //    string scaleTag = "SCALE_PIPOINT";
        //    string startDatetime = "2022-05-02T00:00:00-03:00";
        //    string endDatetime = "2022-05-03T00:00:00-03:00";
        //    CalculatePiPointValueByDateRangeAndTypeCalc summaryDtoMax = new CalculatePiPointValueByDateRangeAndTypeCalc() {
        //        PipointName = "SCALE_PIPOINT",
        //        CalculatedValue = "100"
        //    };
        //    CalculatePiPointValueByDateRangeAndTypeCalc summaryDtoMin = new CalculatePiPointValueByDateRangeAndTypeCalc() {
        //        PipointName = "SCALE_PIPOINT",
        //        CalculatedValue = "10"
        //    };
        //    HttpRequestMessage httpRequest = new HttpRequestMessage();
        //    httpRequest.SetConfiguration(new HttpConfiguration());
        //    HttpResponseMessage expectedResultMax = httpRequest.CreateResponse(HttpStatusCode.OK, summaryDtoMax);
        //    HttpResponseMessage expectedResultMin = httpRequest.CreateResponse(HttpStatusCode.OK, summaryDtoMin);
        //    mockVpiCoreBO.SetupSequence(dao => dao.CalculatePipointValueByDateRangeAndTypeCalc(It.IsAny<CalculatePiPointValueByDateRangeAndTypeCalcRequest>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expectedResultMax.Content.ReadAsAsync<CalculatePiPointValueByDateRangeAndTypeCalc>()).Returns(expectedResultMin.Content.ReadAsAsync<CalculatePiPointValueByDateRangeAndTypeCalc>());
        //    IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
        //    FlowTagValue flowTagValue = contingencyBO.GetFlowTagValue(scaleTag, startDatetime, endDatetime);
        //    Assert.IsTrue(flowTagValue.Max == 100);
        //    Assert.IsTrue(flowTagValue.Min == 10);
        //}

        [TestMethod]
        public void GetFlowTagValueFailure() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            string scaleTag = "SCALE_PIPOINT";
            string startDatetime = "2022-05-02T00:00:00-03:00";
            string endDatetime = "2022-05-03T00:00:00-03:00";
            CalculatePiPointValueByDateRangeAndTypeCalc summaryDtoMax = new CalculatePiPointValueByDateRangeAndTypeCalc() {
                PipointName = "SCALE_PIPOINT",
                CalculatedValue = "100.56"
            };
            CalculatePiPointValueByDateRangeAndTypeCalc summaryDtoMin = new CalculatePiPointValueByDateRangeAndTypeCalc() {
                PipointName = "SCALE_PIPOINT",
                CalculatedValue = "10.32"
            };
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.SetConfiguration(new HttpConfiguration());
            HttpResponseMessage expectedResultMax = httpRequest.CreateResponse(HttpStatusCode.OK, summaryDtoMax);
            HttpResponseMessage expectedResultMin = httpRequest.CreateResponse(HttpStatusCode.OK, summaryDtoMin);
            mockVpiCoreBO.SetupSequence(bo => bo.CalculatePipointValueByDateRangeAndTypeCalc(It.IsAny<CalculatePiPointValueByDateRangeAndTypeCalcRequest>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception()).Throws(new Exception());
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            FlowTagValue flowTagValue = contingencyBO.GetFlowTagValue(scaleTag, startDatetime, endDatetime);
            Assert.IsTrue(flowTagValue.Max == null);
            Assert.IsTrue(flowTagValue.Min == null);
        }

        [TestMethod]
        public void GetValidCompValuesSuccess() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            string loadingPoint = "SI01";
            DateTime startDatetime = new DateTime(2022, 6, 1, 12, 0, 0);
            DateTime endDatetime = new DateTime(2022, 6, 2, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_REGISTER_TIME = new DateTime(2022, 6, 1, 12, 0, 0),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 6, 1, 12, 0, 1),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 6, 1, 12, 0, 2),
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
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidCompValues(loadingPoint, startDatetime, endDatetime)).Returns(weighingDataList);
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            List<ValidWeighingData> validWeighingDataList = contingencyBO.GetValidCompValues(loadingPoint, startDatetime, endDatetime);
            Assert.IsTrue(validWeighingDataList.Count == 3);
            Assert.IsTrue(validWeighingDataList[0].RegisterTime == "01/06/22 12:00:00");
            Assert.IsTrue(validWeighingDataList[0].CarNumber == 1);
            Assert.IsTrue(validWeighingDataList[0].TareWeight == 21.695);
            Assert.IsTrue(validWeighingDataList[0].GrossWeight == 129.716);
            Assert.IsTrue(validWeighingDataList[0].NetWeight == 108.021);
            Assert.IsTrue(validWeighingDataList[0].AxisNumber == 1);
            Assert.IsTrue(validWeighingDataList[0].LeftWheelTare == 0);
            Assert.IsTrue(validWeighingDataList[0].RightWheelTare == 0);
            Assert.IsTrue(validWeighingDataList[0].LeftWheelGross == 0);
            Assert.IsTrue(validWeighingDataList[0].RightWheelGross == 0);
            Assert.IsTrue(validWeighingDataList[0].LeftWheelNet == 0);
            Assert.IsTrue(validWeighingDataList[0].RightWheelNet == 0);
            Assert.IsTrue(validWeighingDataList[0].AxisTare == 0);
            Assert.IsTrue(validWeighingDataList[0].AxisGross == 0);
            Assert.IsTrue(validWeighingDataList[0].AxisNet == 0);
            Assert.IsTrue(validWeighingDataList[0].TruckTare == 0);
            Assert.IsTrue(validWeighingDataList[0].TruckGross == 0);
            Assert.IsTrue(validWeighingDataList[0].TruckNet == 0);
            Assert.IsTrue(validWeighingDataList[0].ProductId == "FERRO");
            Assert.IsTrue(validWeighingDataList[0].DataType == "NORMAL");
            Assert.IsTrue(validWeighingDataList[0].OperatorId == "OPERADOR");
            Assert.IsTrue(validWeighingDataList[0].LotID == "");
            Assert.IsTrue(validWeighingDataList[0].DataWeightingSource == "Pesagem Normal");
            Assert.IsTrue(validWeighingDataList[0].Justification == "");
            Assert.IsTrue(validWeighingDataList[1].RegisterTime == "01/06/22 12:00:01");
            Assert.IsTrue(validWeighingDataList[1].CarNumber == 2);
            Assert.IsTrue(validWeighingDataList[1].TareWeight == 21.475);
            Assert.IsTrue(validWeighingDataList[1].GrossWeight == 127.253);
            Assert.IsTrue(validWeighingDataList[1].NetWeight == 105.778);
            Assert.IsTrue(validWeighingDataList[2].RegisterTime == "01/06/22 12:00:02");
            Assert.IsTrue(validWeighingDataList[2].CarNumber == 3);
            Assert.IsTrue(validWeighingDataList[2].TareWeight == 21.999);
            Assert.IsTrue(validWeighingDataList[2].GrossWeight == 127.262);
            Assert.IsTrue(validWeighingDataList[2].NetWeight == 105.263);
        }

        [TestMethod]
        public void GetValidCompValuesFailure() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            string loadingPoint = "SI01";
            DateTime startDatetime = new DateTime(2022, 6, 1, 12, 0, 0);
            DateTime endDatetime = new DateTime(2022, 6, 2, 12, 0, 0);
            DUAET_REVIWED_WEIGHING weighingData1 = new DUAET_REVIWED_WEIGHING() {
                DUAE_REGISTER_TIME = new DateTime(2022, 6, 1, 12, 0, 0),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 6, 1, 12, 0, 1),
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
                DUAE_REGISTER_TIME = new DateTime(2022, 6, 1, 12, 0, 2),
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
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidCompValues(loadingPoint, startDatetime, endDatetime)).Throws(new Exception());
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            List<ValidWeighingData> validWeighingDataList = contingencyBO.GetValidCompValues(loadingPoint, startDatetime, endDatetime);
            Assert.IsTrue(validWeighingDataList.Count == 0);
        }

        [TestMethod]
        public void GetContingencyTagsListSuccess() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            DUAHT_WEIGHING_CONFIG loadingPointData1 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = "TAG_TESTE_1"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData2 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = "TAG_TESTE_2"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData3 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = string.Empty
            };
            DUAHT_WEIGHING_CONFIG loadingPointData4 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = null
            };
            List<DUAHT_WEIGHING_CONFIG> loadingPointDataList = new List<DUAHT_WEIGHING_CONFIG>() {
                loadingPointData1,loadingPointData2,loadingPointData3,loadingPointData4
            };
            mockLoadingPointDao.Setup(dao => dao.GetAllLoadingPoints()).Returns(loadingPointDataList);
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            List<string> contingencyTagsList = contingencyBO.GetContingencyTagsList();
            Assert.IsTrue(contingencyTagsList.Count == 2);
            Assert.IsTrue(contingencyTagsList[0] == "TAG_TESTE_1");
            Assert.IsTrue(contingencyTagsList[1] == "TAG_TESTE_2");
        }

        [TestMethod]
        public void GetContingencyTagsListFailure() {
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            Mock<IHarpiaLoggerBO> mockVpiLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IHarpiaPICoreBO> mockVpiCoreBO = new Mock<IHarpiaPICoreBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            DUAHT_WEIGHING_CONFIG loadingPointData1 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = "TAG_TESTE_1"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData2 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = "TAG_TESTE_2"
            };
            DUAHT_WEIGHING_CONFIG loadingPointData3 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = string.Empty
            };
            DUAHT_WEIGHING_CONFIG loadingPointData4 = new DUAHT_WEIGHING_CONFIG() {
                DUAH_CONTINGENCY_FLOW_TAG = null
            };
            List<DUAHT_WEIGHING_CONFIG> loadingPointDataList = new List<DUAHT_WEIGHING_CONFIG>() {
                loadingPointData1,loadingPointData2,loadingPointData3,loadingPointData4
            };
            mockLoadingPointDao.Setup(dao => dao.GetAllLoadingPoints()).Throws(new Exception());
            IContingencyBO contingencyBO = new ContingencyBOImpl(mockVpiLoggerBO.Object, mockVpiCoreBO.Object, mockWeighingConsolidatedDao.Object, mockLoadingPointDao.Object);
            List<string> contingencyTagsList = contingencyBO.GetContingencyTagsList();
            Assert.IsTrue(contingencyTagsList.Count == 0);
        }
    }
}