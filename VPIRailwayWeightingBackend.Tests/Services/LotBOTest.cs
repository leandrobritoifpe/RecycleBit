using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class LotBOTest {

        [TestMethod]
        public void GetUnassociatedLotsByOperationalUnitSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            mockLotDao.Setup(dao => dao.GetUnassociatedLotsByOperationalUnit(loadingPoint,true)).Returns(lotTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            List<Lot> unassociatedLotsList = lotBO.GetLot(loadingPoint);
            Assert.IsTrue(unassociatedLotsList.Count == 3);
            Assert.IsTrue(unassociatedLotsList[0].LotID == "20225203498");
            Assert.IsTrue(unassociatedLotsList[0].OperationalUnit == "SN");
            Assert.IsTrue(unassociatedLotsList[0].Product == "SFCK");
            Assert.IsTrue(unassociatedLotsList[0].WagonsQuantity == 330);
            Assert.IsTrue(unassociatedLotsList[0].LoadingPoint == "SI01");
            Assert.IsTrue(unassociatedLotsList[1].LotID == "20225203497");
            Assert.IsTrue(unassociatedLotsList[1].OperationalUnit == "SN");
            Assert.IsTrue(unassociatedLotsList[1].Product == "SFLS");
            Assert.IsTrue(unassociatedLotsList[1].WagonsQuantity == 330);
            Assert.IsTrue(unassociatedLotsList[1].LoadingPoint == "SI01");
            Assert.IsTrue(unassociatedLotsList[2].LotID == "20225203494");
            Assert.IsTrue(unassociatedLotsList[2].OperationalUnit == "SN");
            Assert.IsTrue(unassociatedLotsList[2].Product == "SFCK");
            Assert.IsTrue(unassociatedLotsList[2].WagonsQuantity == 330);
            Assert.IsTrue(unassociatedLotsList[2].LoadingPoint == "SI01");
        }

        [TestMethod]
        public void GetUnassociatedLotsByOperationalUnitFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            mockLotDao.Setup(dao => dao.GetUnassociatedLotsByOperationalUnit(loadingPoint, true)).Throws(new Exception());
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            List<Lot> unassociatedLotsList = lotBO.GetLot(loadingPoint);
            Assert.IsTrue(unassociatedLotsList.Count == 0);
        }

        [TestMethod]
        public void GetAssociatedLotsSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            int minCarNumber = 0;
            int maxCarNumber = 330;
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData1 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203498",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData2 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData3 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 0,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData4 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 1,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData5 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 2,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData6 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 329,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData7 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 328,
                DUAE_LOAD_POINT_CODE = "SI01",
            };
            List<DUAET_REVIWED_WEIGHING> consolidatedWeighingTableDataList = new List<DUAET_REVIWED_WEIGHING> {
                consolidatedWeighingTableData1,consolidatedWeighingTableData2,consolidatedWeighingTableData3,consolidatedWeighingTableData4,
                consolidatedWeighingTableData5,consolidatedWeighingTableData6,consolidatedWeighingTableData7
            };
            mockLotDao.Setup(dao => dao.GetUnassociatedLotsByOperationalUnit(loadingPoint, false)).Returns(lotTableDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidValuesLotAss(loadingPoint, minCarNumber, maxCarNumber)).Returns(consolidatedWeighingTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            List<Lot> associatedLotsList = lotBO.GetAssociatedLots(loadingPoint, minCarNumber, maxCarNumber);
            Assert.IsTrue(associatedLotsList.Count == 2);
            Assert.IsTrue(associatedLotsList[0].LotID == "20225203498");
            Assert.IsTrue(associatedLotsList[0].OperationalUnit == "SN");
            Assert.IsTrue(associatedLotsList[0].Product == "SFCK");
            Assert.IsTrue(associatedLotsList[0].WagonsQuantity == 330);
            Assert.IsTrue(associatedLotsList[0].LoadingPoint == "SI01");
            Assert.IsTrue(associatedLotsList[1].LotID == "20225203497");
            Assert.IsTrue(associatedLotsList[1].OperationalUnit == "SN");
            Assert.IsTrue(associatedLotsList[1].Product == "SFLS");
            Assert.IsTrue(associatedLotsList[1].WagonsQuantity == 330);
            Assert.IsTrue(associatedLotsList[1].LoadingPoint == "SI01");
        }

        [TestMethod]
        public void GetAssociatedLotsFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            int minCarNumber = 0;
            int maxCarNumber = 330;
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData1 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203498",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120718"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData2 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData3 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 0,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData4 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 1,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData5 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 2,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData6 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 329,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData7 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 328,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            List<DUAET_REVIWED_WEIGHING> consolidatedWeighingTableDataList = new List<DUAET_REVIWED_WEIGHING> {
                consolidatedWeighingTableData1,consolidatedWeighingTableData2,consolidatedWeighingTableData3,consolidatedWeighingTableData4,
                consolidatedWeighingTableData5,consolidatedWeighingTableData6,consolidatedWeighingTableData7
            };
            mockLotDao.Setup(dao => dao.GetUnassociatedLotsByOperationalUnit(loadingPoint, false)).Throws(new Exception());
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidValuesLotAss(loadingPoint, minCarNumber, maxCarNumber)).Returns(consolidatedWeighingTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            List<Lot> associatedLotsList = lotBO.GetAssociatedLots(loadingPoint, minCarNumber, maxCarNumber);
            Assert.IsTrue(associatedLotsList.Count == 0);
        }

        [TestMethod]
        public void GetLotsAssociatedToCompSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            int minCarNumber = 0;
            int maxCarNumber = 330;
            string compositionId = "C37C38-120722";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData1 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203498",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120718"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData2 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData3 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 0,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData4 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 1,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData5 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 2,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData6 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 329,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData7 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 328,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            List<DUAET_REVIWED_WEIGHING> consolidatedWeighingTableDataList = new List<DUAET_REVIWED_WEIGHING> {
                consolidatedWeighingTableData1,consolidatedWeighingTableData2,consolidatedWeighingTableData3,consolidatedWeighingTableData4,
                consolidatedWeighingTableData5,consolidatedWeighingTableData6,consolidatedWeighingTableData7
            };
            mockLotDao.Setup(dao => dao.GetLotsByLoadingPoint(loadingPoint)).Returns(lotTableDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidValuesLotAss(loadingPoint, minCarNumber, maxCarNumber)).Returns(consolidatedWeighingTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            List<int> associatedCarsList = lotBO.GetLotsAssociatedToComp(loadingPoint, minCarNumber, maxCarNumber, compositionId);
            Assert.IsTrue(associatedCarsList.Count == 3);
            Assert.IsTrue(associatedCarsList[0] == 328);
            Assert.IsTrue(associatedCarsList[1] == 329);
            Assert.IsTrue(associatedCarsList[2] == 330);
        }

        [TestMethod]
        public void GetLotsAssociatedToCompFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            int minCarNumber = 0;
            int maxCarNumber = 330;
            string compositionId = "C37C38-120722";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData1 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203498",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120718"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData2 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData3 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 0,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData4 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 1,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData5 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 2,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData6 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 329,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData7 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 328,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722"
            };
            List<DUAET_REVIWED_WEIGHING> consolidatedWeighingTableDataList = new List<DUAET_REVIWED_WEIGHING> {
                consolidatedWeighingTableData1,consolidatedWeighingTableData2,consolidatedWeighingTableData3,consolidatedWeighingTableData4,
                consolidatedWeighingTableData5,consolidatedWeighingTableData6,consolidatedWeighingTableData7
            };
            mockLotDao.Setup(dao => dao.GetLotsByLoadingPoint(loadingPoint)).Throws(new Exception());
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidValuesLotAss(loadingPoint, minCarNumber, maxCarNumber)).Returns(consolidatedWeighingTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            List<int> associatedCarsList = lotBO.GetLotsAssociatedToComp(loadingPoint, minCarNumber, maxCarNumber, compositionId);
            Assert.IsTrue(associatedCarsList.Count == 0);
        }

        [TestMethod]
        public void GetNumberOfLotsSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            mockLotDao.Setup(dao => dao.GetLotsByLoadingPoint(loadingPoint)).Returns(lotTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            int numberOfCars = lotBO.GetNumberOfLots(loadingPoint);
            Assert.IsTrue(numberOfCars == 3);
        }

        [TestMethod]
        public void GetNumberOfLotsFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            mockLotDao.Setup(dao => dao.GetLotsByLoadingPoint(loadingPoint)).Throws(new Exception());
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            int numberOfCars = lotBO.GetNumberOfLots(loadingPoint);
            Assert.IsTrue(numberOfCars == 0);
        }

        [TestMethod]
        public void GetNumberOfAxisSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            int minCarNumber = 0;
            int maxCarNumber = 330;
            string compositionId = "C37C38-120722";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData1 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203498",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120718",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData2 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData3 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 0,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData4 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 1,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData5 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 2,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData6 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 329,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData7 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 328,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_AXIS_NUMBER = 1,
            };
            List<DUAET_REVIWED_WEIGHING> consolidatedWeighingTableDataList = new List<DUAET_REVIWED_WEIGHING> {
                consolidatedWeighingTableData1,consolidatedWeighingTableData2,consolidatedWeighingTableData3,consolidatedWeighingTableData4,
                consolidatedWeighingTableData5,consolidatedWeighingTableData6,consolidatedWeighingTableData7
            };
            mockLotDao.Setup(dao => dao.GetAssociatedLotsByLoadingPoint(loadingPoint)).Returns(lotTableDataList);
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidValuesLotAss(loadingPoint, minCarNumber, maxCarNumber)).Returns(consolidatedWeighingTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            int numberOfAxis = lotBO.GetNumberOfAxis(loadingPoint, minCarNumber, maxCarNumber, compositionId);
            Assert.IsTrue(numberOfAxis == 3);
        }

        [TestMethod]
        public void GetNumberOfAxisFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILotDao> mockLotDao = new Mock<ILotDao>();
            Mock<IWeighingConsolidatedGpvmDao> mockWeighingConsolidatedDao = new Mock<IWeighingConsolidatedGpvmDao>();
            string loadingPoint = "SI01";
            int minCarNumber = 0;
            int maxCarNumber = 330;
            string compositionId = "C37C38-120722";
            DUABT_LOT lotTableData1 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203498",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData2 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203497",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFLS",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            DUABT_LOT lotTableData3 = new DUABT_LOT {
                DUAB_LOT_GPV_ID = "20225203494",
                DUAB_OPERATIONAL_UNIT = "SN",
                DUAB_PRODUCT_CODE = "SFCK",
                DUAB_EXPECTED_WAGONS_QUANTITY = 330,
                DUAB_LOAD_POINT = "SI01",
            };
            List<DUABT_LOT> lotTableDataList = new List<DUABT_LOT> {
                lotTableData1,lotTableData2,lotTableData3
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData1 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203498",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120718",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData2 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 330,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData3 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 0,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData4 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 1,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData5 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "",
                DUAE_ID_PRODUCT = "SFCK",
                DUAE_CAR_NUMBER = 2,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120715",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData6 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 329,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_AXIS_NUMBER = 1,
            };
            DUAET_REVIWED_WEIGHING consolidatedWeighingTableData7 = new DUAET_REVIWED_WEIGHING {
                DUAE_LOT_GPV_ID = "20225203497",
                DUAE_ID_PRODUCT = "SFLS",
                DUAE_CAR_NUMBER = 328,
                DUAE_LOAD_POINT_CODE = "SI01",
                DUAE_ID_COMPOSITION = "C37C38-120722",
                DUAE_AXIS_NUMBER = 1,
            };
            List<DUAET_REVIWED_WEIGHING> consolidatedWeighingTableDataList = new List<DUAET_REVIWED_WEIGHING> {
                consolidatedWeighingTableData1,consolidatedWeighingTableData2,consolidatedWeighingTableData3,consolidatedWeighingTableData4,
                consolidatedWeighingTableData5,consolidatedWeighingTableData6,consolidatedWeighingTableData7
            };
            mockLotDao.Setup(dao => dao.GetAssociatedLotsByLoadingPoint(loadingPoint)).Throws(new Exception());
            mockWeighingConsolidatedDao.Setup(dao => dao.GetValidValuesLotAss(loadingPoint, minCarNumber, maxCarNumber)).Returns(consolidatedWeighingTableDataList);
            ILotBO lotBO = new LotBOImpl(mockLoggerBO.Object, mockLotDao.Object, mockWeighingConsolidatedDao.Object);
            int numberOfAxis = lotBO.GetNumberOfAxis(loadingPoint, minCarNumber, maxCarNumber, compositionId);
            Assert.IsTrue(numberOfAxis == 0);
        }
    }
}