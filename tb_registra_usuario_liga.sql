USE cob_bvirtual
go

IF OBJECT_ID('dbo.tb_registra_usuario_liga') IS NOT NULL
BEGIN
    DROP TABLE dbo.tb_registra_usuario_liga
    IF OBJECT_ID('dbo.tb_registra_usuario_liga') IS NOT NULL
        PRINT '<<< FAILED DROPPING TABLE dbo.tb_registra_usuario_liga >>>'
    ELSE
        PRINT '<<< DROPPED TABLE dbo.tb_registra_usuario_liga >>>'
END
go

CREATE TABLE dbo.tb_registra_usuario_liga
(
    trul_nombre         varchar(30)     null,
    trul_apellido       varchar(30)     null,
    trul_nombre_grupo   varchar(50)     null,
    trul_nombre_liga    varchar(100),
    trul_puntaje        int
)
LOCK DATAROWS
go
IF OBJECT_ID('dbo.tb_registra_usuario_liga') IS NOT NULL
    PRINT '<<< CREATED TABLE dbo.tb_registra_usuario_liga >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE dbo.tb_registra_usuario_liga >>>'
go