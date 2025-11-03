using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Response;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class JustificationBOTest {

        [TestMethod]
        public void GetJustificationsSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IJustificationDao> mockJustificationDao = new Mock<IJustificationDao>();
            DUAGT_JUSTIFICATION justificationTableData1 = new DUAGT_JUSTIFICATION {
                DUAG_JUSTIFICATION_ID = 2,
                DUAG_DESCRIPTION = "Falha na Balança de Tara",
            };
            DUAGT_JUSTIFICATION justificationTableData2 = new DUAGT_JUSTIFICATION {
                DUAG_JUSTIFICATION_ID = 3,
                DUAG_DESCRIPTION = "Falha na Balança de Bruto",
            };
            DUAGT_JUSTIFICATION justificationTableData3 = new DUAGT_JUSTIFICATION {
                DUAG_JUSTIFICATION_ID = 4,
                DUAG_DESCRIPTION = "Falha nas Balanças",
            };
            List<DUAGT_JUSTIFICATION> justificationTableDataList = new List<DUAGT_JUSTIFICATION> {
                justificationTableData1,
                justificationTableData2,
                justificationTableData3
            };
            mockJustificationDao.Setup(dao => dao.GetJustificationData()).Returns(justificationTableDataList);
            IJustificationBO justificationBO = new JustificationBOImpl(mockLoggerBO.Object, mockJustificationDao.Object);
            List<JustificationData> justificationDataList = justificationBO.GetJustifications();
            Assert.IsTrue(justificationDataList.Count == 3);
            Assert.IsTrue(justificationDataList[0].ID == 2);
            Assert.IsTrue(justificationDataList[0].JustificationDescription == "Falha na Balança de Tara");
            Assert.IsTrue(justificationDataList[1].ID == 3);
            Assert.IsTrue(justificationDataList[1].JustificationDescription == "Falha na Balança de Bruto");
            Assert.IsTrue(justificationDataList[2].ID == 4);
            Assert.IsTrue(justificationDataList[2].JustificationDescription == "Falha nas Balanças");
        }

        [TestMethod]
        public void GetJustificationsFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IJustificationDao> mockJustificationDao = new Mock<IJustificationDao>();
            DUAGT_JUSTIFICATION justificationTableData1 = new DUAGT_JUSTIFICATION {
                DUAG_JUSTIFICATION_ID = 2,
                DUAG_DESCRIPTION = "Falha na Balança de Tara",
            };
            DUAGT_JUSTIFICATION justificationTableData2 = new DUAGT_JUSTIFICATION {
                DUAG_JUSTIFICATION_ID = 3,
                DUAG_DESCRIPTION = "Falha na Balança de Bruto",
            };
            DUAGT_JUSTIFICATION justificationTableData3 = new DUAGT_JUSTIFICATION {
                DUAG_JUSTIFICATION_ID = 4,
                DUAG_DESCRIPTION = "Falha nas Balanças",
            };
            List<DUAGT_JUSTIFICATION> justificationTableDataList = new List<DUAGT_JUSTIFICATION> {
                justificationTableData1,
                justificationTableData2,
                justificationTableData3
            };
            mockJustificationDao.Setup(dao => dao.GetJustificationData()).Throws(new System.Exception());
            IJustificationBO justificationBO = new JustificationBOImpl(mockLoggerBO.Object, mockJustificationDao.Object);
            List<JustificationData> justificationDataList = justificationBO.GetJustifications();
            Assert.IsTrue(justificationDataList.Count == 0);
        }
    }
}