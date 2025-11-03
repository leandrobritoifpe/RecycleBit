using HarpiaCommon.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using RecycleBitBackEnd.Dao.Interfaces;
using RecycleBitBackEnd.Models;
using RecycleBitBackEnd.Models.Dto;
using RecycleBitBackEnd.Services;
using RecycleBitBackEnd.Services.Interfaces;
using RecycleBitBackEnd.Util;

namespace RecycleBitBackEnd.Tests.Services {

    [TestClass]
    public class GlobalParametersBOTest {

        [TestMethod]
        public void GetLoadingParametersSuccess() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            string loadingPointCode = "SI01";
            DUAHT_WEIGHING_CONFIG loadingPointData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_TARE_WEIGHT_MAX = 24.0,
                DUAH_TARE_WEIGHT_MIN = 18.0,
                DUAH_GROSS_WEIGHT_MAX = 131.0,
                DUAH_GROSS_WEIGHT_MIN = 7.0,
                DUAH_ACCEPT_ABNORMAL = false,
                DUAH_WEIGHING_ADJUSTMENT = "1",
                DUAH_ADJUSTMENT_PERCENT = 1,
                DUAH_LOAD_POINT_CODE = "SI01"
            };
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(loadingPointCode)).Returns(loadingPointData);
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            LoadingParameters loadingParameters = globalParametersBO.GetLoadingParameters(loadingPointCode);
            Assert.IsTrue(loadingParameters.TareWeightMax == 24.0);
            Assert.IsTrue(loadingParameters.TareWeightMin == 18.0);
            Assert.IsTrue(loadingParameters.GrossWeightMax == 131.0);
            Assert.IsTrue(loadingParameters.GrossWeightMin == 7.0);
            Assert.IsTrue(loadingParameters.DefaultTare == 0.0);
            Assert.IsTrue(loadingParameters.AcceptAbnormal == false);
            Assert.IsTrue(loadingParameters.WeighingAdjustment == "1");
            Assert.IsTrue(loadingParameters.PercentAdjustment == 1.0);
            Assert.IsTrue(loadingParameters.LoadingPointCode == "SI01");
        }

        [TestMethod]
        public void GetLoadingParametersFailure() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();

            ApplicationParameters.Params = new ExpandoObject();
            ApplicationParameters.Params.ApplicationName = "FakeAppName";

            string loadingPointCode = "SI01";
            DUAHT_WEIGHING_CONFIG loadingPointData = new DUAHT_WEIGHING_CONFIG() {
                DUAH_TARE_WEIGHT_MAX = 24.0,
                DUAH_TARE_WEIGHT_MIN = 18.0,
                DUAH_GROSS_WEIGHT_MAX = 131.0,
                DUAH_GROSS_WEIGHT_MIN = 7.0,
                DUAH_ACCEPT_ABNORMAL = false,
                DUAH_WEIGHING_ADJUSTMENT = "1",
                DUAH_ADJUSTMENT_PERCENT = 1,
                DUAH_LOAD_POINT_CODE = "SI01"
            };
            mockLoadingPointDao.Setup(dao => dao.GetLoadingPointByLoadingPointCode(loadingPointCode)).Throws(new Exception());
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            LoadingParameters loadingParameters = globalParametersBO.GetLoadingParameters(loadingPointCode);
            Assert.IsTrue(loadingParameters.TareWeightMax == 0);
            Assert.IsTrue(loadingParameters.TareWeightMin == 0);
            Assert.IsTrue(loadingParameters.GrossWeightMax == null);
            Assert.IsTrue(loadingParameters.GrossWeightMin == null);
            Assert.IsTrue(loadingParameters.DefaultTare == null);
            Assert.IsTrue(loadingParameters.AcceptAbnormal == false);
            Assert.IsTrue(loadingParameters.WeighingAdjustment == null);
            Assert.IsTrue(loadingParameters.PercentAdjustment == null);
            Assert.IsTrue(loadingParameters.LoadingPointCode == null);
        }

        [TestMethod]
        public void UpdateLoadingPointConfigSuccess() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            LoadingParameters loadingPointData = new LoadingParameters() {
                TareWeightMax = 24.0,
                TareWeightMin = 18.0,
                GrossWeightMax = 131.0,
                GrossWeightMin = 7.0,
                AcceptAbnormal = false,
                WeighingAdjustment = "1",
                PercentAdjustment = 1,
                LoadingPointCode = "SI01"
            };

            DUACT_USER user = new DUACT_USER() {
                DUAC_USER = "testUser",
                DUAC_ACTIVE = "YES",
                DUAC_WEIGHING_USER = true
            };

            string daoReturnMessage = "OK";
            mockLoadingPointDao.Setup(dao => dao.UpdateLoadingPointConfig(loadingPointData, user)).Returns(daoReturnMessage);
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.UpdateLoadingPointConfig(loadingPointData, user);
            Assert.IsTrue(returnMessage == "OK");
        }

        [TestMethod]
        public void UpdateLoadingPointConfigFailure() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            LoadingParameters loadingPointData = new LoadingParameters() {
                TareWeightMax = 24.0,
                TareWeightMin = 18.0,
                GrossWeightMax = 131.0,
                GrossWeightMin = 7.0,
                AcceptAbnormal = false,
                WeighingAdjustment = "1",
                PercentAdjustment = 1,
                LoadingPointCode = "SI01"
            };

            DUACT_USER user = new DUACT_USER() {
                DUAC_USER = "testUser",
                DUAC_ACTIVE = "YES",
                DUAC_WEIGHING_USER = true
            };

            mockLoadingPointDao.Setup(dao => dao.UpdateLoadingPointConfig(loadingPointData, user)).Throws(new Exception());
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.UpdateLoadingPointConfig(loadingPointData, user);
            Assert.IsTrue(returnMessage == "NOK");
        }

        [TestMethod]
        public void GetToleranceRulesSuccess() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            string loadingPointCode = "SI01";
            DUAIT_TOLERANCE toleranceRule1 = new DUAIT_TOLERANCE() {
                DUAI_RULE_ENABLE = true,
                DUAI_GROSS_WEIGHT_TOL = 1,
                DUAI_MAX_CAR_IN_RULE = 1,
                DUAI_LOAD_POINT_CODE = "SI01"
            };
            DUAIT_TOLERANCE toleranceRule2 = new DUAIT_TOLERANCE() {
                DUAI_RULE_ENABLE = true,
                DUAI_GROSS_WEIGHT_TOL = 2,
                DUAI_MAX_CAR_IN_RULE = 2,
                DUAI_LOAD_POINT_CODE = "SI01"
            };
            DUAIT_TOLERANCE toleranceRule3 = new DUAIT_TOLERANCE() {
                DUAI_RULE_ENABLE = true,
                DUAI_GROSS_WEIGHT_TOL = 3,
                DUAI_MAX_CAR_IN_RULE = 3,
                DUAI_LOAD_POINT_CODE = "SI01"
            };
            List<DUAIT_TOLERANCE> toleranceRulesDataList = new List<DUAIT_TOLERANCE>() {
                toleranceRule1, toleranceRule2, toleranceRule3
            };
            mockToleranceRulesDao.Setup(dao => dao.GetRulesByLocation(loadingPointCode)).Returns(toleranceRulesDataList);
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            List<ToleranceRule> toleranceRulesList = globalParametersBO.GetToleranceRules(loadingPointCode);
            Assert.IsTrue(toleranceRulesList.Count == 3);
            Assert.IsTrue(toleranceRulesList[0].RuleEnabled == "YES");
            Assert.IsTrue(toleranceRulesList[0].GrossWeightTolerance == 1);
            Assert.IsTrue(toleranceRulesList[0].MaxCarsInRule == 1);
            Assert.IsTrue(toleranceRulesList[1].RuleEnabled == "YES");
            Assert.IsTrue(toleranceRulesList[1].GrossWeightTolerance == 2);
            Assert.IsTrue(toleranceRulesList[1].MaxCarsInRule == 2);
            Assert.IsTrue(toleranceRulesList[2].RuleEnabled == "YES");
            Assert.IsTrue(toleranceRulesList[2].GrossWeightTolerance == 3);
            Assert.IsTrue(toleranceRulesList[2].MaxCarsInRule == 3);
        }

        [TestMethod]
        public void GetToleranceRulesFailure() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            string loadingPointCode = "SI01";
            DUAIT_TOLERANCE toleranceRule1 = new DUAIT_TOLERANCE() {
                DUAI_RULE_ENABLE = true,
                DUAI_GROSS_WEIGHT_TOL = 1,
                DUAI_MAX_CAR_IN_RULE = 1,
                DUAI_LOAD_POINT_CODE = "SI01"
            };
            DUAIT_TOLERANCE toleranceRule2 = new DUAIT_TOLERANCE() {
                DUAI_RULE_ENABLE = true,
                DUAI_GROSS_WEIGHT_TOL = 2,
                DUAI_MAX_CAR_IN_RULE = 2,
                DUAI_LOAD_POINT_CODE = "SI01"
            };
            DUAIT_TOLERANCE toleranceRule3 = new DUAIT_TOLERANCE() {
                DUAI_RULE_ENABLE = true,
                DUAI_GROSS_WEIGHT_TOL = 3,
                DUAI_MAX_CAR_IN_RULE = 3,
                DUAI_LOAD_POINT_CODE = "SI01"
            };
            List<DUAIT_TOLERANCE> toleranceRulesDataList = new List<DUAIT_TOLERANCE>() {
                toleranceRule1, toleranceRule2, toleranceRule3
            };
            mockToleranceRulesDao.Setup(dao => dao.GetRulesByLocation(loadingPointCode)).Throws(new Exception());
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            List<ToleranceRule> toleranceRulesList = globalParametersBO.GetToleranceRules(loadingPointCode);
            Assert.IsTrue(toleranceRulesList.Count == 0);
        }

        [TestMethod]
        public void CreateToleranceRuleSuccess() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "YES",
                GrossWeightTolerance = 1,
                MaxCarsInRule = 1,
                LoadingPointCode = "SI01",
            };
            mockToleranceRulesDao.Setup(dao => dao.CreateToleranceRule(toleranceRule));
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.CreateToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == "OK");
        }

        [TestMethod]
        public void CreateToleranceRuleParameterInvalid1() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "YES",
                GrossWeightTolerance = 0,
                MaxCarsInRule = 1,
                LoadingPointCode = "SI01",
            };
            mockToleranceRulesDao.Setup(dao => dao.CreateToleranceRule(toleranceRule));
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.CreateToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == " Valor Inválido para o peso bruto (deve ser maior do que zero). ");
        }

        [TestMethod]
        public void CreateToleranceRuleParameterInvalid2() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "YES",
                GrossWeightTolerance = 0,
                MaxCarsInRule = 0,
                LoadingPointCode = "SI01",
            };
            mockToleranceRulesDao.Setup(dao => dao.CreateToleranceRule(toleranceRule));
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.CreateToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == " Valor Inválido para o número de carros na regra (deve ser maior do que zero).  Valor Inválido para o peso bruto (deve ser maior do que zero). ");
        }

        [TestMethod]
        public void CreateToleranceRuleParameterInvalid3() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "Test",
                GrossWeightTolerance = 0,
                MaxCarsInRule = 0,
                LoadingPointCode = "SI01",
            };
            mockToleranceRulesDao.Setup(dao => dao.CreateToleranceRule(toleranceRule));
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.CreateToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == "Valor para o parâmetro \"Regra Habilitada\" inválido.  Valor Inválido para o número de carros na regra (deve ser maior do que zero).  Valor Inválido para o peso bruto (deve ser maior do que zero). ");
        }

        [TestMethod]
        public void CreateToleranceRuleFailure() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "YES",
                GrossWeightTolerance = 1,
                MaxCarsInRule = 1,
                LoadingPointCode = "SI01",
            };
            mockToleranceRulesDao.Setup(dao => dao.CreateToleranceRule(toleranceRule)).Throws(new Exception("Erro Teste"));
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.CreateToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == "Erro durante criação da regra de tolerância: Erro Teste");
        }

        [TestMethod]
        public void DeleteToleranceRuleSuccess() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "YES",
                GrossWeightTolerance = 1,
                MaxCarsInRule = 1,
                LoadingPointCode = "SI01",
            };
            string daoReturnMessage = "OK";
            mockToleranceRulesDao.Setup(dao => dao.DeleteToleranceRule(toleranceRule)).Returns(daoReturnMessage);
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.DeleteToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == "OK");
        }

        [TestMethod]
        public void DeleteToleranceRuleFailure() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            ToleranceRule toleranceRule = new ToleranceRule() {
                RuleEnabled = "YES",
                GrossWeightTolerance = 1,
                MaxCarsInRule = 1,
                LoadingPointCode = "SI01",
            };
            mockToleranceRulesDao.Setup(dao => dao.DeleteToleranceRule(toleranceRule)).Throws(new Exception("Erro Teste"));
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            string returnMessage = globalParametersBO.DeleteToleranceRule(toleranceRule);
            Assert.IsTrue(returnMessage == "Erro durante criação da regra de tolerância: Erro Teste");
        }

        [TestMethod]
        public void GetSiloCodesSuccess() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            DUAJT_LOAD_POINT siloTableData1 = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO1",
                DUAJ_ENDPOINT = "http://172.19.204.18/WebService.svc?WSDL",
            };
            DUAJT_LOAD_POINT siloTableData2 = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO2",
                DUAJ_ENDPOINT = "http://172.19.204.12/WebService.svc?WSDL",
            };
            List<DUAJT_LOAD_POINT> siloTableDataList = new List<DUAJT_LOAD_POINT>() {
                siloTableData1,siloTableData2
            };
            mockSiloDao.Setup(dao => dao.GetAllSilos()).Returns(siloTableDataList);
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            List<SiloDTO> siloCodes = globalParametersBO.GetSiloCodes();
            Assert.IsTrue(siloCodes.Count == 2);
            Assert.IsTrue(siloCodes[0].SiloCode == "SILO1");
            Assert.IsTrue(siloCodes[1].SiloCode == "SILO2");
        }

        [TestMethod]
        public void GetSiloCodesFailure() {
            Mock<IHarpiaLoggerBO> mockloggerBO = new Mock<IHarpiaLoggerBO>();
            Mock<ILoadingPointDao> mockLoadingPointDao = new Mock<ILoadingPointDao>();
            Mock<IToleranceRulesDao> mockToleranceRulesDao = new Mock<IToleranceRulesDao>();
            Mock<ISiloDao> mockSiloDao = new Mock<ISiloDao>();
            DUAJT_LOAD_POINT siloTableData1 = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO1",
                DUAJ_ENDPOINT = "http://172.19.204.18/WebService.svc?WSDL",
            };
            DUAJT_LOAD_POINT siloTableData2 = new DUAJT_LOAD_POINT() {
                DUAJ_LOAD_POINT_CODE = "SILO2",
                DUAJ_ENDPOINT = "http://172.19.204.12/WebService.svc?WSDL",
            };
            List<DUAJT_LOAD_POINT> siloTableDataList = new List<DUAJT_LOAD_POINT>() {
                siloTableData1,siloTableData2
            };
            mockSiloDao.Setup(dao => dao.GetAllSilos()).Throws(new Exception());
            IGlobalParametersBO globalParametersBO = new GlobalParametersBOImpl(mockloggerBO.Object, mockLoadingPointDao.Object, mockToleranceRulesDao.Object, mockSiloDao.Object);
            List<SiloDTO> siloCodes = globalParametersBO.GetSiloCodes();
            Assert.IsTrue(siloCodes.Count == 0);
        }
    }
}