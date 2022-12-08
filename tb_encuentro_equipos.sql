USE cob_bvirtual
go

IF OBJECT_ID('dbo.tb_encuentro_equipos') IS NOT NULL
BEGIN
    DROP TABLE dbo.tb_encuentro_equipos
    IF OBJECT_ID('dbo.tb_encuentro_equipos') IS NOT NULL
        PRINT '<<< FAILED DROPPING TABLE dbo.tb_encuentro_equipos >>>'
    ELSE
        PRINT '<<< DROPPED TABLE dbo.tb_encuentro_equipos >>>'
END
go

CREATE TABLE dbo.tb_encuentro_equipos
(
    tee_equipo1             varchar(30),
    tee_puntaje1            int,
    tee_equipo2             varchar(30),
    tee_puntaje2            int,
    tee_fecha_encuentro     datetime    null
)
LOCK DATAROWS
go
IF OBJECT_ID('dbo.tb_encuentro_equipos') IS NOT NULL
    PRINT '<<< CREATED TABLE dbo.tb_encuentro_equipos >>>'
ELSE
    PRINT '<<< FAILED CREATING TABLE dbo.tb_encuentro_equipos >>>'
go