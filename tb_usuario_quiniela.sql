USE cob_bvirtual
go

IF OBJECT_ID('dbo.tb_usuario_quiniela') IS NOT NULL
BEGIN
    DROP TABLE dbo.tb_usuario_quiniela
    IF OBJECT_ID('dbo.tb_usuario_quiniela') IS NOT NULL
        PRINT '<<< FAILED DROPPING TABLE dbo.tb_usuario_quiniela >>>'
    ELSE
        PRINT '<<< DROPPED TABLE dbo.tb_usuario_quiniela >>>'
END
go

CREATE TABLE dbo.tb_usuario_quiniela
(
    tuq_usuario     varchar(30),
    tuq_pass        varchar(50)
)
LOCK DATAROWS
go
IF OBJECT_ID('dbo.tb_usuario_quiniela') IS NOT NULL
    PRINT '<<< CREATED TABLE dbo.tb_usuario_quiniela >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE dbo.tb_usuario_quiniela >>>'
go