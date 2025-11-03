using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Dynamic;
using HarpiaCommon.Services.Interfaces;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Response;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class ConfigurationBOTest {

        [TestMethod]
        public void GetUserDataSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();
            string username = "Neymar";
            DUACT_USER userData = new DUACT_USER() {
                DUAC_TYPE = "NORMATIVO",
                DUAC_SEND_TO_SUPERVISORY = "YES",
                DUAC_SUPERVISORY_CODE = "SS"
            };

            mockUsersDao.Setup(dao => dao.GetActiveUserData(username)).Returns(userData);            
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            UserTypeData userTypeData = configurationBO.GetUserData(username);
            Assert.IsTrue(userTypeData.UserType == "NORMATIVO");
            Assert.IsTrue(userTypeData.SendSupervisory == "YES");
            Assert.IsTrue(userTypeData.SupervisoryCode == "SS");
        }

        [TestMethod]
        public void GetUserDataFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string username = "Neymar";
            mockUsersDao.Setup(dao => dao.GetActiveUserData(username)).Throws(new System.Exception());
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            UserTypeData userTypeData = configurationBO.GetUserData(username);
            Assert.IsTrue(userTypeData.UserType == "UNR");
            Assert.IsTrue(userTypeData.SendSupervisory == "NO");
            Assert.IsTrue(userTypeData.SupervisoryCode == "");
        }

        [TestMethod]
        public void GetConfigParamsSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();
            string username = "Neymar";
            DUACT_USER userData = new DUACT_USER() {
                DUAC_TYPE = "NORMATIVO",
                DUAC_SEND_TO_SUPERVISORY = "YES",
                DUAC_SUPERVISORY_CODE = "SS"
            };
            string location = "SI01";
            DUAHT_WEIGHING_CONFIG loadingPointConfigData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_DATA_SOURCE = "WS",
                DUAH_BATCH_ENABLE = "YES",
                DUAH_DISABLE_EXCEL_FILE = false,
                DUAH_TARE_WEIGHT_MAX = 24,
                DUAH_TARE_WEIGHT_MIN = 17,
                DUAH_HAS_TOLERANCE = "YES",
                DUAH_OPERATIONAL_UNIT = "SN",
                DUAH_WAGON_GROUP = "PESAGEM_WAGONS_2",
                DUAH_GROSS_WEIGHT_MAX = 100,
                DUAH_GROSS_WEIGHT_MIN = 10,
            };
            DUAFT_WAGON_PARAMETER wagonParameters1 = new DUAFT_WAGON_PARAMETER() {
                DUAF_WAGON_TYPE = "GDT",
                DUAF_DEFAULT_TARE = 20.5,
            };
            DUAFT_WAGON_PARAMETER wagonParameters2 = new DUAFT_WAGON_PARAMETER() {
                DUAF_WAGON_TYPE = "GDU",
                DUAF_DEFAULT_TARE = 22,
            };
            DUAFT_WAGON_PARAMETER wagonParameters3 = new DUAFT_WAGON_PARAMETER() {
                DUAF_WAGON_TYPE = "HAT",
                DUAF_DEFAULT_TARE = 22,
            };
            List<DUAFT_WAGON_PARAMETER> wagonParametersList = new List<DUAFT_WAGON_PARAMETER> {
                wagonParameters1,
                wagonParameters2,
                wagonParameters3
            };
            mockUsersDao.Setup(dao => dao.GetActiveUserData(username)).Returns(userData);
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLocation(location)).Returns(loadingPointConfigData);
            wagonParametersDao.Setup(dao => dao.GetWagonParametersByGroup("PESAGEM_WAGONS_2")).Returns(wagonParametersList);
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            ConfigurationParameters configurationParameters = configurationBO.GetConfigurationParameters(username, location);
            Assert.IsTrue(configurationParameters.UserType == "NORMATIVO");
            Assert.IsTrue(configurationParameters.SendSupervisory == false);
            Assert.IsTrue(configurationParameters.Batch == "YES");
            Assert.IsTrue(configurationParameters.DisableExcelFile == false);
            Assert.IsTrue(configurationParameters.MaxTare == 24);
            Assert.IsTrue(configurationParameters.MinTare == 17);
            Assert.IsTrue(configurationParameters.HasTolerance == "YES");
            Assert.IsTrue(configurationParameters.OperationalUnit == "SN");
            Assert.IsTrue(configurationParameters.OUDateFormat == "DD/MM/YY HH:MI:SS:T");
            Assert.IsTrue(configurationParameters.WagonDetails.Count == 3);
            Assert.IsTrue(configurationParameters.WagonDetails[0].WagonType == "GDT");
            Assert.IsTrue(configurationParameters.WagonDetails[0].WagonTare == 20.5);
            Assert.IsTrue(configurationParameters.WagonDetails[1].WagonType == "GDU");
            Assert.IsTrue(configurationParameters.WagonDetails[1].WagonTare == 22);
            Assert.IsTrue(configurationParameters.WagonDetails[2].WagonType == "HAT");
            Assert.IsTrue(configurationParameters.WagonDetails[2].WagonTare == 22);
        }

        [TestMethod]
        public void GetConfigParamsWagonFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string username = "Neymar";
            DUACT_USER userData = new DUACT_USER() {
                DUAC_TYPE = "NORMATIVO",
                DUAC_SEND_TO_SUPERVISORY = "YES",
                DUAC_SUPERVISORY_CODE = "SS"
            };
            string location = "SI01";
            DUAHT_WEIGHING_CONFIG loadingPointConfigData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_DATA_SOURCE = "WS",
                DUAH_BATCH_ENABLE = "YES",
                DUAH_DISABLE_EXCEL_FILE = false,
                DUAH_TARE_WEIGHT_MAX = 24,
                DUAH_TARE_WEIGHT_MIN = 17,
                DUAH_HAS_TOLERANCE = "YES",
                DUAH_OPERATIONAL_UNIT = "SN",
                DUAH_WAGON_GROUP = "PESAGEM_WAGONS_2",
                DUAH_GROSS_WEIGHT_MAX = 100,
                DUAH_GROSS_WEIGHT_MIN = 10,
            };
            mockUsersDao.Setup(dao => dao.GetActiveUserData(username)).Returns(userData);
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLocation(location)).Returns(loadingPointConfigData);
            wagonParametersDao.Setup(dao => dao.GetWagonParametersByGroup("PESAGEM_WAGONS_2")).Throws(new System.Exception());
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            ConfigurationParameters configurationParameters = configurationBO.GetConfigurationParameters(username, location);
            Assert.IsTrue(configurationParameters.UserType == "NORMATIVO");
            Assert.IsTrue(configurationParameters.SendSupervisory == false);
            Assert.IsTrue(configurationParameters.Batch == "YES");
            Assert.IsTrue(configurationParameters.DisableExcelFile == false);
            Assert.IsTrue(configurationParameters.MaxTare == 24);
            Assert.IsTrue(configurationParameters.MinTare == 17);
            Assert.IsTrue(configurationParameters.HasTolerance == "YES");
            Assert.IsTrue(configurationParameters.OperationalUnit == "SN");
            Assert.IsTrue(configurationParameters.OUDateFormat == "DD/MM/YY HH:MI:SS:T");
            Assert.IsTrue(configurationParameters.WagonDetails.Count == 0);
        }

        [TestMethod]
        public void GetConfigParamsLoadingPointFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string username = "Neymar";
            DUACT_USER userData = new DUACT_USER() {
                DUAC_TYPE = "NORMATIVO",
                DUAC_SEND_TO_SUPERVISORY = "YES",
                DUAC_SUPERVISORY_CODE = "SS"
            };
            string location = "SI01";
            mockUsersDao.Setup(dao => dao.GetActiveUserData(username)).Returns(userData);
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLocation(location)).Throws(new System.Exception());
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            ConfigurationParameters configurationParameters = configurationBO.GetConfigurationParameters(username, location);
            Assert.IsTrue(configurationParameters.UserType == "UNR");
            Assert.IsTrue(configurationParameters.SendSupervisory == false);
            Assert.IsTrue(configurationParameters.Batch == null);
            Assert.IsTrue(configurationParameters.DisableExcelFile == null);
            Assert.IsTrue(configurationParameters.MaxTare == 0);
            Assert.IsTrue(configurationParameters.MinTare == 0);
            Assert.IsTrue(configurationParameters.HasTolerance == null);
            Assert.IsTrue(configurationParameters.OperationalUnit == null);
            Assert.IsTrue(configurationParameters.OUDateFormat == null);
            Assert.IsTrue(configurationParameters.WagonDetails == null);
        }

        [TestMethod]
        public void GetUserRolesSuccess() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();
            string username = "Neymar";
            DUACT_USER userData = new DUACT_USER() {
                DUAC_ROLE = "UsuarioPesagemEdicao,SS_PF_NORMATIVO,UsuariosPesagem"
            };
            mockUsersDao.Setup(dao => dao.GetUserData(username)).Returns(userData);
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            List<string> userRolesList = configurationBO.GetUserRoles(username);
            Assert.IsTrue(userRolesList.Count == 3);
            Assert.IsTrue(userRolesList[0] == "UsuarioPesagemEdicao");
            Assert.IsTrue(userRolesList[1] == "SS_PF_NORMATIVO");
            Assert.IsTrue(userRolesList[2] == "UsuariosPesagem");
        }

        [TestMethod]
        public void GetUserRolesFailure() {
            Mock<IHarpiaLoggerBO> mockLoggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<IUsersDao> mockUsersDao = new Mock<IUsersDao>();
            Mock<ISupervisorsDao> mockSupervisorDao = new Mock<ISupervisorsDao>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IWagonParametersDao> wagonParametersDao = new Mock<IWagonParametersDao>();
            Mock<IGPVTagBO> gpvTagBO = new Mock<IGPVTagBO>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string username = "Neymar";
            mockUsersDao.Setup(dao => dao.GetUserData(username)).Throws(new System.Exception());
            IConfigurationBO configurationBO = new ConfigurationBOImpl(mockLoggerBO.Object, mockUsersDao.Object, mockSupervisorDao.Object, mockLoadingPointDao.Object, wagonParametersDao.Object, gpvTagBO.Object);
            List<string> userRolesList = configurationBO.GetUserRoles(username);
            Assert.IsTrue(userRolesList.Count == 0);
        }
    }
}