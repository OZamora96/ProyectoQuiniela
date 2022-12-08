USE cob_bvirtual
go

IF OBJECT_ID('dbo.tb_registra_quiniela') IS NOT NULL
BEGIN
    DROP TABLE dbo.tb_registra_quiniela
    IF OBJECT_ID('dbo.tb_registra_quiniela') IS NOT NULL
        PRINT '<<< FAILED DROPPING TABLE dbo.tb_registra_quiniela >>>'
    ELSE
        PRINT '<<< DROPPED TABLE dbo.tb_registra_quiniela >>>'
END
go

CREATE TABLE dbo.tb_registra_quiniela
(
    trq_nombre              varchar(30)     null,
    trq_apellido            varchar(30)     null,
    trq_grupo               varchar(30)     null,
    trq_nombre_liga         varchar(100),
    trq_equipo1             varchar(30),
    trq_puntaje1            int,
    trq_equipo2             varchar(30),
    trq_puntaje2            int
)
LOCK DATAROWS
go
IF OBJECT_ID('dbo.tb_registra_quiniela') IS NOT NULL
    PRINT '<<< CREATED TABLE dbo.tb_registra_quiniela >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE dbo.tb_registra_quiniela >>>'
go