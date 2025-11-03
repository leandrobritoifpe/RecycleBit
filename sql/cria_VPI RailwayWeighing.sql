/*==============================================================*/
/* User: BDVPIPESAGEM                                           */
/*==============================================================*/
create schema BDVPIPESAGEM
go

/*==============================================================*/
/* User: PJVPIPESAGEM                                           */
/*==============================================================*/
create user PJVPIPESAGEM with default_schema = BDVPIPESAGEM
go

/*==============================================================*/
/* Role: AVPIPESAGEM                                            */
/*==============================================================*/
execute sp_addrole AVPIPESAGEM
go

/*==============================================================*/
/* Role: MVPIPESAGEM                                            */
/*==============================================================*/
execute sp_addrole MVPIPESAGEM
go

execute sp_addrolemember AVPIPESAGEM, MVPIPESAGEM
go

/*==============================================================*/
/* Role: CVPIPESAGEM                                            */
/*==============================================================*/
execute sp_addrole CVPIPESAGEM
go

execute sp_addrolemember CVPIPESAGEM, MVPIPESAGEM
go

/*==============================================================*/
/* Role: EVPIPESAGEM                                            */
/*==============================================================*/
execute sp_addrole EVPIPESAGEM
go

execute sp_addrolemember EVPIPESAGEM, MVPIPESAGEM
go

execute sp_addrolemember MVPIPESAGEM, PJVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAAT_COMPOSITION                                     */
/*==============================================================*/
create table BDVPIPESAGEM.DUAAT_COMPOSITION (
   DUAA_COMPOSITION_ID  int                  identity,
   DUAA_LOAD_POINT      varchar(80)          not null,
   DUAA_OPERATIONAL_UNIT varchar(80)          not null,
   DUAA_COMPOSITION_GPV_ID varchar(80)          not null,
   DUAA_TRAIN_CODE      varchar(80)          not null,
   DUAA_COMPOSITION_VALID varchar(5)           not null,
   DUAA_STATUS_TRAIN    int                  not null,
   DUAA_WAGON_MAX_LOAD  float                not null,
   DUAA_CAR_NUMBER      int                  not null,
   DUAA_CAR_SERIAL_NUMBER varchar(80)          not null,
   DUAA_CAR_TYPE        varchar(80)          not null,
   DUAA_TARE_WEIGHT     float                not null,
   DUAA_STATUS_SEND     varchar(10)          not null,
   DUAA_DATE            datetime2            not null,
   constraint DUAAI_PK primary key (DUAA_COMPOSITION_ID)
)
go

/*==============================================================*/
/* Index: DUAAI_01                                              */
/*==============================================================*/




create nonclustered index DUAAI_01 on BDVPIPESAGEM.DUAAT_COMPOSITION (DUAA_LOAD_POINT ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAAT_COMPOSITION to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAAT_COMPOSITION to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUABT_LOT                                             */
/*==============================================================*/
create table BDVPIPESAGEM.DUABT_LOT (
   DUAB_LOT_ID          int                  identity,
   DUAB_LOT_GPV_ID      varchar(100)         not null,
   DUAB_LOAD_POINT      varchar(80)          not null,
   DUAB_OPERATIONAL_UNIT varchar(80)          not null,
   DUAB_ASSOCIATED      varchar(5)           not null,
   DUAB_PRODUCT_CODE    varchar(80)          not null,
   DUAB_SITE_ID         varchar(80)          not null,
   DUAB_EXPECTED_WAGONS_QUANTITY int                  not null,
   DUAB_SITUATION       varchar(10)          not null,
   DUAB_EXPECTED_INITIAL_DATE datetime             not null,
   DUAB_REGISTRY_TIME   datetime             not null,
   constraint DUABI_PK primary key (DUAB_LOT_ID)
)
go

/*==============================================================*/
/* Index: DUABI_01                                              */
/*==============================================================*/




create nonclustered index DUABI_01 on BDVPIPESAGEM.DUABT_LOT (DUAB_LOAD_POINT ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUABT_LOT to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUABT_LOT to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUACT_USER                                            */
/*==============================================================*/
create table BDVPIPESAGEM.DUACT_USER (
   DUAC_USER_ID         int                  identity,
   DUAC_USER            varchar(80)          not null,
   DUAC_TYPE            varchar(80)          null,
   DUAC_ROLE            varchar(300)         null,
   DUAC_SEND_TO_SUPERVISORY varchar(20)          null,
   DUAC_SUPERVISORY_CODE varchar(20)          null,
   DUAC_ACTIVE          varchar(20)          null,
   DUAC_WEIGHING_USER   bit                  not null,
   constraint DUACI_PK primary key (DUAC_USER_ID)
)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUACT_USER to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUACT_USER to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUADT_WEIGHING                                        */
/*==============================================================*/
create table BDVPIPESAGEM.DUADT_WEIGHING (
   DUAD_WEIGHING_ID     int                  identity,
   DUAD_LOAD_POINT_CODE varchar(80)          not null,
   DUAD_OPERATIONAL_UNIT varchar(50)          not null,
   DUAD_REGISTER_TIME   datetime             not null,
   DUAD_REGISTRY_TIME   datetime             not null,
   DUAD_ID_COMPOSITION  varchar(80)          not null,
   DUAD_CAR_NUMBER      int                  not null,
   DUAD_AXIS_NUMBER     int                  not null,
   DUAD_CAR_TYPE        int                  null,
   DUAD_ID_PRODUCT      varchar(100)         null,
   DUAD_LOT_GPV_ID      varchar(100)         null,
   DUAD_RIGHT_WHEEL_TARE float                null,
   DUAD_LEFT_WHEEL_TARE float                null,
   DUAD_TRUCK_TARE      float                null,
   DUAD_AXIS_TARE       float                null,
   DUAD_TARE_WEIGHT     float                null,
   DUAD_RIGHT_WHEEL_GROSS float                null,
   DUAD_LEFT_WHEEL_GROSS float                null,
   DUAD_TRUCK_GROSS     float                null,
   DUAD_AXIS_GROSS      float                null,
   DUAD_GROSS_WEIGHT    float                null,
   DUAD_RIGHT_WHEEL_NET float                null,
   DUAD_LEFT_WHEEL_NET  float                null,
   DUAD_TRUCK_NET       float                null,
   DUAD_AXIS_NET        float                null,
   DUAD_NET_WEIGHT      float                null,
   DUAD_INTER_WHEIGHT   float                null,
   DUAD_LOAD_REMOVER    float                null,
   DUAD_SPEED           float                null,
   DUAD_SPEED_EXCEEDED  varchar(50)          null,
   DUAD_DATA_TYPE       varchar(80)          null,
   DUAD_DATA_WEIGHING_SOURCE varchar(150)         null,
   DUAD_CONCH           int                  null,
   DUAD_OVERLOAD_WEIGHT float                null,
   DUAD_GROSS_OVERLOAD  float                null,
   DUAD_OVERLOAD_STATUS varchar(4)           null,
   DUAD_JUSTIFICATION   varchar(600)         null,
   DUAD_OPERATOR_WEIGHT varchar(200)         null,
   DUAD_OPERATOR_ID     varchar(200)         null,
   DUAD_LAST_REGISTRY_VALID varchar(4)           not null,
   DUAD_QUALITY_STATUS  varchar(4)           not null,
   constraint DUADI_PK primary key (DUAD_WEIGHING_ID)
)
go

/*==============================================================*/
/* Index: DUADI_01                                              */
/*==============================================================*/




create nonclustered index DUADI_01 on BDVPIPESAGEM.DUADT_WEIGHING (DUAD_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUADT_WEIGHING to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUADT_WEIGHING to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAET_REVIWED_WEIGHING                                */
/*==============================================================*/
create table BDVPIPESAGEM.DUAET_REVIWED_WEIGHING (
   DUAE_REVIWED_WEIGHING_ID int                  identity,
   DUAE_LOAD_POINT_CODE varchar(80)          not null,
   DUAE_OPERATIONAL_UNIT varchar(50)          not null,
   DUAE_REGISTER_TIME   datetime             not null,
   DUAE_REGISTRY_TIME   datetime             not null,
   DUAE_ID_COMPOSITION  varchar(100)         not null,
   DUAE_CAR_NUMBER      int                  not null,
   DUAE_AXIS_NUMBER     int                  not null,
   DUAE_CAR_TYPE        int                  null,
   DUAE_ID_PRODUCT      varchar(100)         null,
   DUAE_LOT_GPV_ID      varchar(100)         null,
   DUAE_RIGHT_WHEEL_TARE float                null,
   DUAE_LEFT_WHEEL_TARE float                null,
   DUAE_TRUCK_TARE      float                null,
   DUAE_AXIS_TARE       float                null,
   DUAE_TARE_WEIGHT     float                null,
   DUAE_RIGHT_WHEEL_GROSS float                null,
   DUAE_LEFT_WHEEL_GROSS float                null,
   DUAE_TRUCK_GROSS     float                null,
   DUAE_AXIS_GROSS      float                null,
   DUAE_GROSS_WEIGHT    float                null,
   DUAE_RIGHT_WHEEL_NET float                null,
   DUAE_LEFT_WHEEL_NET  float                null,
   DUAE_TRUCK_NET       float                null,
   DUAE_AXIS_NET        float                null,
   DUAE_NET_WEIGHT      float                null,
   DUAE_INTER_WHEIGHT   float                null,
   DUAE_LOAD_REMOVER    float                null,
   DUAE_SPEED           float                null,
   DUAE_SPEED_EXCEEDED  varchar(50)          null,
   DUAE_DATA_TYPE       varchar(80)          null,
   DUAE_DATA_WEIGHING_SOURCE varchar(150)         null,
   DUAE_CONCH           int                  null,
   DUAE_OVERLOAD_WEIGHT float                null,
   DUAE_GROSS_OVERLOAD  float                null,
   DUAE_OVERLOAD_STATUS varchar(4)           null,
   DUAE_JUSTIFICATION   varchar(600)         null,
   DUAE_OPERATOR_WEIGHT varchar(200)         null,
   DUAE_OPERATOR_ID     varchar(200)         null,
   DUAE_LAST_REGISTRY_VALID varchar(4)           not null,
   DUAE_QUALITY_STATUS  varchar(4)           not null,
   constraint DUAEI_PK primary key (DUAE_REVIWED_WEIGHING_ID)
)
go

/*==============================================================*/
/* Index: DUAEI_01                                              */
/*==============================================================*/




create nonclustered index DUAEI_01 on BDVPIPESAGEM.DUAET_REVIWED_WEIGHING (DUAE_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAET_REVIWED_WEIGHING to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAET_REVIWED_WEIGHING to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAFT_WAGON_PARAMETER                                 */
/*==============================================================*/
create table BDVPIPESAGEM.DUAFT_WAGON_PARAMETER (
   DUAF_WAGON_PARAMETER_ID int                  identity,
   DUAF_WAGON_TYPE      varchar(80)          not null,
   DUAF_DEFAULT_TARE    float                not null,
   DUAF_WAGON_GROUP     varchar(20)          not null,
   constraint DUAFI_PK primary key (DUAF_WAGON_PARAMETER_ID)
)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAFT_WAGON_PARAMETER to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAFT_WAGON_PARAMETER to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAGT_JUSTIFICATION                                   */
/*==============================================================*/
create table BDVPIPESAGEM.DUAGT_JUSTIFICATION (
   DUAG_JUSTIFICATION_ID int                  identity,
   DUAG_DESCRIPTION     varchar(200)         not null,
   constraint DUAGI_PK primary key (DUAG_JUSTIFICATION_ID)
)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAGT_JUSTIFICATION to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAGT_JUSTIFICATION to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAHT_WEIGHING_CONFIG                                 */
/*==============================================================*/
create table BDVPIPESAGEM.DUAHT_WEIGHING_CONFIG (
   DUAH_WEIGHING_CONFIG_ID int                  identity,
   DUAH_LOAD_POINT_CODE varchar(80)          not null,
   DUAH_OPERATIONAL_UNIT varchar(50)          not null,
   DUAH_DATA_SOURCE     varchar(10)          not null,
   DUAH_WEIGHING_LOCATION varchar(50)          not null,
   DUAH_TARE_WEIGHT_MIN float                not null,
   DUAH_TARE_WEIGHT_MAX float                not null,
   DUAH_GROSS_WEIGHT_MIN float                null,
   DUAH_GROSS_WEIGHT_MAX float                null,
   DUAH_ACCEPT_ABNORMAL bit                  null,
   DUAH_WEIGHING_ADJUSTMENT varchar(5)           null,
   DUAH_ADJUSTMENT_PERCENT float                null,
   DUAH_GDU_LIMIT_GROSS float                null,
   DUAH_GDT_LIMIT_GROSS float                null,
   DUAH_HAT_LIMIT_GROSS float                null,
   DUAH_WAGON_TYPE_TOLERANCE int                  null,
   DUAH_TOLERANCE_LIMIT_GROSS float                null,
   DUAH_GDU_LIMIT_TARE  float                null,
   DUAH_GDT_LIMIT_TARE  float                null,
   DUAH_HAT_LIMIT_TARE  float                null,
   DUAH_TOLERANCE_LIMIT_TARE float                null,
   DUAH_OVERLOAD        varchar(5)           null,
   DUAH_BATCH_ENABLE    varchar(5)           null,
   DUAH_DISABLE_EXCEL_FILE bit                  null,
   DUAH_WAGON_GROUP     varchar(50)          null,
   DUAH_HAS_TOLERANCE   varchar(5)           null,
   DUAH_PROCESSING      varchar(3)           null,
   DUAH_WAGONS_DELAY_QTY int                  null,
   DUAH_DESTINATION_HOST_WS varchar(100)         null,
   DUAH_DESTINATION_PORT_WS int                  null,
   DUAH_WEB_SERVICE_NAME_WS varchar(50)          null,
   DUAH_CONTINGENCY_FLOW_TAG varchar(200)         null,
   DUAH_COMPANY         varchar(100)         not null,
   constraint DUAHI_PK primary key (DUAH_WEIGHING_CONFIG_ID)
)
go

/*==============================================================*/
/* Index: DUAHI_01                                              */
/*==============================================================*/




create nonclustered index DUAHI_01 on BDVPIPESAGEM.DUAHT_WEIGHING_CONFIG (DUAH_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAHT_WEIGHING_CONFIG to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAHT_WEIGHING_CONFIG to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAIT_TOLERANCE                                       */
/*==============================================================*/
create table BDVPIPESAGEM.DUAIT_TOLERANCE (
   DUAI_TOLERANCE_ID    int                  identity,
   DUAI_RULE_ENABLE     bit                  not null,
   DUAI_GROSS_WEIGHT_TOL float                not null,
   DUAI_MAX_CAR_IN_RULE int                  not null,
   DUAI_LOAD_POINT_CODE varchar(80)          not null,
   constraint DUAII_PK primary key (DUAI_TOLERANCE_ID)
)
go

/*==============================================================*/
/* Index: DUAII_01                                              */
/*==============================================================*/




create nonclustered index DUAII_01 on BDVPIPESAGEM.DUAIT_TOLERANCE (DUAI_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAIT_TOLERANCE to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAIT_TOLERANCE to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAJT_LOAD_POINT                                      */
/*==============================================================*/
create table BDVPIPESAGEM.DUAJT_LOAD_POINT (
   DUAJ_LOAD_POINT_ID   int                  identity,
   DUAJ_LOAD_POINT_CODE varchar(80)          not null,
   DUAJ_ENDPOINT        varchar(300)         not null,
   constraint DUAJI_PK primary key (DUAJ_LOAD_POINT_ID)
)
go

/*==============================================================*/
/* Index: DUAJI_01                                              */
/*==============================================================*/




create unique nonclustered index DUAJI_01 on BDVPIPESAGEM.DUAJT_LOAD_POINT (DUAJ_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAJT_LOAD_POINT to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAJT_LOAD_POINT to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUAKT_LOG_GPVM                                        */
/*==============================================================*/
create table BDVPIPESAGEM.DUAKT_LOG_GPVM (
   DUAK_LOG_GPVM_ID     int                  identity,
   DUAK_LOAD_POINT_CODE varchar(80)          not null,
   DUAK_MESSAGE_ID      varchar(50)          null,
   DUAK_SEND_TIME       datetime             null,
   DUAK_RETURN_TIME     datetime             null,
   DUAK_EXECUTION_STATUS varchar(10)          null,
   DUAK_RETURN_MESSAGE  varchar(800)         null,
   DUAK_ERROR_CODE      varchar(30)          null,
   DUAK_ERROR_TYPE      varchar(30)          null,
   DUAK_REQUEST_ERROR_TOTAL int                  not null,
   DUAK_REQUEST_SUCCESS_TOTAL int                  not null,
   DUAK_REQUEST_DURATION int                  not null,
   DUAK_MESSAGE_SIZE    int                  not null,
   constraint PK_DUAKT_LOG_GPVM primary key nonclustered (DUAK_LOG_GPVM_ID)
)
go

/*==============================================================*/
/* Index: DUAKI_PK                                              */
/*==============================================================*/




create unique clustered index DUAKI_PK on BDVPIPESAGEM.DUAKT_LOG_GPVM (DUAK_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUAKT_LOG_GPVM to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUAKT_LOG_GPVM to CVPIPESAGEM
go

/*==============================================================*/
/* Table: DUALT_OPC_CONFIG                                      */
/*==============================================================*/
create table BDVPIPESAGEM.DUALT_OPC_CONFIG (
   DUAL_OPC_CONFIG_ID   int                  identity,
   DUAL_CAR_TAG         varchar(50)          null,
   DUAL_CAR_TYPE_TAG    varchar(50)          null,
   DUAL_COMPOSITION_TAG varchar(50)          null,
   DUAL_TARE_WEIGHT_TAG varchar(50)          null,
   DUAL_GROSS_WEIGHT_TAG varchar(50)          null,
   DUAL_NET_WEIGHT_TAG  varchar(50)          null,
   DUAL_INTER_WEIGHT_TAG varchar(50)          null,
   DUAL_PRODUCT_TAG     varchar(50)          null,
   DUAL_LOAD_REMOVER_TAG varchar(50)          null,
   DUAL_OPERATOR_ID_TAG varchar(50)          null,
   DUAL_DEFAULT_TARE_WEIGHT float                null,
   DUAL_DEFAULT_GROSS_WEIGHT float                null,
   DUAL_DEFAULT_PRODUCT varchar(100)         null,
   DUAL_MAX_CAR_NUMBER  int                  null,
   DUAL_ADJUST_WAGON_STATUS varchar(5)           null,
   DUAL_TYPE_WAGON      int                  null,
   DUAL_TYPE_LOCOMOTIVE int                  null,
   DUAL_WAGON_MIN_TARE_WEIGHT float                null,
   DUAL_WAGON_MIN_GROSS_WEIGHT float                null,
   DUAL_COMPOSITION_ACRO char(1)              null,
   DUAL_LOAD_POINT_CODE varchar(80)          null,
   constraint DUALI_PK primary key (DUAL_OPC_CONFIG_ID)
)
go

/*==============================================================*/
/* Index: DUALI_01                                              */
/*==============================================================*/




create nonclustered index DUALI_01 on BDVPIPESAGEM.DUALT_OPC_CONFIG (DUAL_LOAD_POINT_CODE ASC)
go

grant INSERT,DELETE,UPDATE on BDVPIPESAGEM.DUALT_OPC_CONFIG to AVPIPESAGEM
go

grant SELECT on BDVPIPESAGEM.DUALT_OPC_CONFIG to CVPIPESAGEM
go
