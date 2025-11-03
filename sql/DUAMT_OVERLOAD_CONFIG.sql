/*==============================================================*/
/* Table: DUAMT_OPC_CONFIG                                      */
/*==============================================================*/
create table BDVPIPESAGEM.DUAMT_OVERLOAD_CONFIG (
   DUAM_OVERLOAD_CONFIG_ID   int                  identity,
   DUAM_RECORD_NAME         varchar(50)          null,
   DUAM_LOAD_POINT_CODE    varchar(50)          null,
   DUAM_OPERATION_UNIT varchar(50)          null,
   DUAM_BIN_NAME_TAG varchar(50)          null,
   DUAM_COMPOSITION_NAME_TAG varchar(50)          null,
   DUAM_DISTINY_WAGON_TAG  varchar(50)          null,
   DUAM_ORIGIN_WAGON_TAG varchar(50)          null,
   DUAM_WEIGTH_WAGON_MOVED_TAG   varchar(50)          null,
   DUAM_LAST_WEIGHINH_TAG varchar(50)          null,
   DUAM_BIN_NUMBER int(50)          null,
   DUAM_CRANE_LAST_USAGE DATETIME                null,
   constraint DUAMI_PK primary key (DUAM_OPC_CONFIG_ID)
)
go