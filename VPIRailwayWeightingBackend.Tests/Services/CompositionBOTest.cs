using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Models.Request;
using RecycleBitBackEnd.Models.Response;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class CompositionBOTest {

        [TestMethod]
        public void GetUnsentCompSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ICompositionDao> mockCompositionDao = new Mock<ICompositionDao>();
            Mock<ISimmSoftDao> mockSimmSoftDao = new Mock<ISimmSoftDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            string operationalUnit = "SI01";
            DUAAT_COMPOSITION compositionTableData1 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M021",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            DUAAT_COMPOSITION compositionTableData2 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M021",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            DUAAT_COMPOSITION compositionTableData3 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M021",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            DUAAT_COMPOSITION compositionTableData4 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M024",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            List<DUAAT_COMPOSITION> compositionTableDataList = new List<DUAAT_COMPOSITION> {
                compositionTableData1,
                compositionTableData2,
                compositionTableData3,
                compositionTableData4
            };
            mockCompositionDao.Setup(dao => dao.GetUnsetCompositions(operationalUnit)).Returns(compositionTableDataList);
            ICompositionBO compositionBO = new CompositionBOImpl(mockLoggerBO.Object, mockCompositionDao.Object, mockSimmSoftDao.Object, mockSiloDao.Object);
            List<UnsentCompositionData> compositionDataList = compositionBO.GetUnsentComp(operationalUnit);
            Assert.IsTrue(compositionDataList.Count == 2);
            Assert.IsTrue(compositionDataList[0].OperationalUnit == "SI01");
            Assert.IsTrue(compositionDataList[0].TrainCode == "M021");
            Assert.IsTrue(compositionDataList[1].OperationalUnit == "SI01");
            Assert.IsTrue(compositionDataList[1].TrainCode == "M024");
        }

        [TestMethod]
        public void GetUnsentCompFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ICompositionDao> mockCompositionDao = new Mock<ICompositionDao>();
            Mock<ISimmSoftDao> mockSimmSoftDao = new Mock<ISimmSoftDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string operationalUnit = "SI01";
            DUAAT_COMPOSITION compositionTableData1 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M021",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            DUAAT_COMPOSITION compositionTableData2 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M021",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            DUAAT_COMPOSITION compositionTableData3 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M021",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            DUAAT_COMPOSITION compositionTableData4 = new DUAAT_COMPOSITION {
                DUAA_TRAIN_CODE = "M024",
                DUAA_OPERATIONAL_UNIT = "SI01",
            };
            List<DUAAT_COMPOSITION> compositionTableDataList = new List<DUAAT_COMPOSITION> {
                compositionTableData1,
                compositionTableData2,
                compositionTableData3,
                compositionTableData4
            };
            mockCompositionDao.Setup(dao => dao.GetUnsetCompositions(operationalUnit)).Throws(new System.Exception());
            ICompositionBO compositionBO = new CompositionBOImpl(mockLoggerBO.Object, mockCompositionDao.Object, mockSimmSoftDao.Object, mockSiloDao.Object);
            List<UnsentCompositionData> compositionDataList = compositionBO.GetUnsentComp(operationalUnit);
            Assert.IsTrue(compositionDataList.Count == 0);
        }

        [TestMethod]
        public void SendCompToSimmSoftSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ICompositionDao> mockCompositionDao = new Mock<ICompositionDao>();
            Mock<ISimmSoftDao> mockSimmSoftDao = new Mock<ISimmSoftDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            SendCompositionToSimmSoftRequest sendCompositionToSimmSoftRequest = new SendCompositionToSimmSoftRequest {
                OperationalUnit = "SI01",
                TrainCode = "M021",
                Silo = "SILO1"
            };
            DUAAT_COMPOSITION compositionTableData1 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 1,
                DUAA_CAR_SERIAL_NUMBER = "1705563",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            DUAAT_COMPOSITION compositionTableData2 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 2,
                DUAA_CAR_SERIAL_NUMBER = "1705564",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            DUAAT_COMPOSITION compositionTableData3 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 3,
                DUAA_CAR_SERIAL_NUMBER = "1705565",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            List<DUAAT_COMPOSITION> compositionTableDataList = new List<DUAAT_COMPOSITION> {
                compositionTableData1,
                compositionTableData2,
                compositionTableData3,
            };
            DUAJT_LOAD_POINT siloTableData = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO1",
                DUAJ_ENDPOINT = "http://172.19.204.18/WebService.svc?WSDL",
            };
            string simmSoftXMLResponse = "<RESPONSE>\r\n<SUCESSO>teste</SUCESSO>\r\n<SUCESSO>teste2</SUCESSO>\r\n<SUCESSO>teste3</SUCESSO>\r\n</RESPONSE>";
            mockCompositionDao.Setup(dao => dao.GetUnsentCompositionByTrainCodeAndOpUnit(sendCompositionToSimmSoftRequest.TrainCode, sendCompositionToSimmSoftRequest.OperationalUnit)).Returns(compositionTableDataList);
            mockSimmSoftDao.Setup(dao => dao.SendXMLToSimmSoft(It.IsAny<string>(), It.IsAny<string>())).Returns(simmSoftXMLResponse);
            mockSiloDao.Setup(dao => dao.GetSiloByCode("SILO1")).Returns(siloTableData);
            ICompositionBO compositionBO = new CompositionBOImpl(mockLoggerBO.Object, mockCompositionDao.Object, mockSimmSoftDao.Object, mockSiloDao.Object);
            SimmSoftSentStatus simmSoftSentStatus = compositionBO.SendCompToSimmSoft(sendCompositionToSimmSoftRequest);
            Assert.IsTrue(simmSoftSentStatus.OperationalUnit == "SI01");
            Assert.IsTrue(simmSoftSentStatus.Status == "OK");
        }

        [TestMethod]
        public void SendCompToSimmSoftError() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ICompositionDao> mockCompositionDao = new Mock<ICompositionDao>();
            Mock<ISimmSoftDao> mockSimmSoftDao = new Mock<ISimmSoftDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            SendCompositionToSimmSoftRequest sendCompositionToSimmSoftRequest = new SendCompositionToSimmSoftRequest {
                OperationalUnit = "SI01",
                TrainCode = "M021",
                Silo = "SILO1"
            };
            DUAAT_COMPOSITION compositionTableData1 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 1,
                DUAA_CAR_SERIAL_NUMBER = "1705563",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            DUAAT_COMPOSITION compositionTableData2 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 2,
                DUAA_CAR_SERIAL_NUMBER = "1705564",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            DUAAT_COMPOSITION compositionTableData3 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 3,
                DUAA_CAR_SERIAL_NUMBER = "1705565",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            List<DUAAT_COMPOSITION> compositionTableDataList = new List<DUAAT_COMPOSITION> {
                compositionTableData1,
                compositionTableData2,
                compositionTableData3,
            };
            DUAJT_LOAD_POINT siloTableData = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO1",
                DUAJ_ENDPOINT = "http://172.19.204.18/WebService.svc?WSDL",
            };
            string simmSoftXMLResponse = "<RESPONSE>\r\n<ERRO>teste</ERRO>\r\n<ERRO>teste2</ERRO>\r\n<ERRO>teste3</ERRO>\r\n</RESPONSE>";
            mockCompositionDao.Setup(dao => dao.GetUnsentCompositionByTrainCodeAndOpUnit(sendCompositionToSimmSoftRequest.TrainCode, sendCompositionToSimmSoftRequest.OperationalUnit)).Returns(compositionTableDataList);
            mockSimmSoftDao.Setup(dao => dao.SendXMLToSimmSoft(It.IsAny<string>(), "http://172.19.204.18/WebService.svc?WSDL")).Returns(simmSoftXMLResponse);
            mockSiloDao.Setup(dao => dao.GetSiloByCode("SILO1")).Returns(siloTableData);
            ICompositionBO compositionBO = new CompositionBOImpl(mockLoggerBO.Object, mockCompositionDao.Object, mockSimmSoftDao.Object, mockSiloDao.Object);
            SimmSoftSentStatus simmSoftSentStatus = compositionBO.SendCompToSimmSoft(sendCompositionToSimmSoftRequest);
            Assert.IsTrue(simmSoftSentStatus.OperationalUnit == "SI01");
            Assert.IsTrue(simmSoftSentStatus.Status == "NOK");
        }

        [TestMethod]
        public void SendCompToSimmSoftFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ICompositionDao> mockCompositionDao = new Mock<ICompositionDao>();
            Mock<ISimmSoftDao> mockSimmSoftDao = new Mock<ISimmSoftDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            SendCompositionToSimmSoftRequest sendCompositionToSimmSoftRequest = new SendCompositionToSimmSoftRequest {
                OperationalUnit = "SI01",
                TrainCode = "M021",
                Silo = "SILO1"
            };
            DUAAT_COMPOSITION compositionTableData1 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 1,
                DUAA_CAR_SERIAL_NUMBER = "1705563",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            DUAAT_COMPOSITION compositionTableData2 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 2,
                DUAA_CAR_SERIAL_NUMBER = "1705564",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            DUAAT_COMPOSITION compositionTableData3 = new DUAAT_COMPOSITION {
                DUAA_COMPOSITION_ID = 1,
                DUAA_LOAD_POINT = "SILO1",
                DUAA_OPERATIONAL_UNIT = "SI01",
                DUAA_COMPOSITION_GPV_ID = "333159",
                DUAA_TRAIN_CODE = "M021",
                DUAA_COMPOSITION_VALID = "A",
                DUAA_STATUS_TRAIN = 0,
                DUAA_WAGON_MAX_LOAD = 0,
                DUAA_CAR_NUMBER = 3,
                DUAA_CAR_SERIAL_NUMBER = "1705565",
                DUAA_CAR_TYPE = "GDU",
                DUAA_TARE_WEIGHT = 22,
                DUAA_STATUS_SEND = "NO",
                DUAA_DATE = new System.DateTime(2022, 6, 24, 15, 0, 0)
            };
            List<DUAAT_COMPOSITION> compositionTableDataList = new List<DUAAT_COMPOSITION> {
                compositionTableData1,
                compositionTableData2,
                compositionTableData3,
            };
            DUAJT_LOAD_POINT siloTableData = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO1",
                DUAJ_ENDPOINT = "http://172.19.204.18/WebService.svc?WSDL",
            };
            mockCompositionDao.Setup(dao => dao.GetUnsentCompositionByTrainCodeAndOpUnit(sendCompositionToSimmSoftRequest.TrainCode, sendCompositionToSimmSoftRequest.OperationalUnit)).Returns(compositionTableDataList);
            mockSimmSoftDao.Setup(dao => dao.SendXMLToSimmSoft(It.IsAny<string>(), "http://172.19.204.18/WebService.svc?WSDL")).Throws(new System.Exception());
            mockSiloDao.Setup(dao => dao.GetSiloByCode("SILO1")).Returns(siloTableData);
            ICompositionBO compositionBO = new CompositionBOImpl(mockLoggerBO.Object, mockCompositionDao.Object, mockSimmSoftDao.Object, mockSiloDao.Object);
            SimmSoftSentStatus simmSoftSentStatus = compositionBO.SendCompToSimmSoft(sendCompositionToSimmSoftRequest);
            Assert.IsTrue(simmSoftSentStatus.OperationalUnit == "SI01");
            Assert.IsTrue(simmSoftSentStatus.Status == "NOK");
        }
    }
}