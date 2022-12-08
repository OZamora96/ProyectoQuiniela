USE cob_bvirtual
go
IF OBJECT_ID('dbo.sp_quiniela_mundial') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.sp_quiniela_mundial
    IF OBJECT_ID('dbo.sp_quiniela_mundial') IS NOT NULL
        PRINT '<<< FAILED DROPPING PROCEDURE dbo.sp_quiniela_mundial >>>'
    ELSE
        PRINT '<<< DROPPED PROCEDURE dbo.sp_quiniela_mundial >>>'
END
go
create proc dbo.sp_quiniela_mundial
@i_operacion        char(3),
@i_equipo1          varchar(30) = null,
@i_equipo2          varchar(30) = null,
@i_puntaje1         int = 0,
@i_puntaje2         int = 0,
@i_nombre           varchar(30) = null,
@i_apellido         varchar(30) = null,
@i_nombre_liga      varchar(100) = null,
@i_grupo_liga       varchar(30) = null,
@i_puntaje_persona  int = 0,
@i_puntaje_grupo    int = 0,
@i_fecha_encuentro  datetime = null

as

if @i_operacion = 'RV' --Registro vaticinio
begin
    insert into cob_bvirtual..tb_registra_quiniela
    values (@i_nombre, @i_apellido, @i_grupo_liga, @i_nombre_liga, @i_equipo1, @i_puntaje1, @i_equipo2, @i_puntaje2)
    
end --if @i_operacion = 'RQ'

if @i_operacion = 'RP' --Registro de personas en liga
begin
    insert into cob_bvirtual..tb_registra_usuario_liga
    values (@i_nombre, @i_apellido, null, @i_nombre_liga, @i_puntaje_persona)
    
end -- @i_operacion = 'RP'

if @i_operacion = 'RG' --Registro de grupos en liga
begin
    insert into cob_bvirtual..tb_registra_usuario_liga
    values (null, null, @i_grupo_liga, @i_nombre_liga, @i_puntaje_grupo)
    
end -- @i_operacion = 'RG'

if @i_operacion = 'EN' --Registro de los encuentros de los equipos
begin
    insert into cob_bvirtual..tb_encuentro_equipos
    values (@i_equipo1, @i_puntaje1, @i_equipo2, @i_puntaje2, @i_fecha_encuentro)
    
end --@i_operacion = 'EN'

if @i_operacion = 'AP' --Actualizacion de puntaje de los equipos
begin
    update  cob_bvirtual..tb_encuentro_equipos
    set     tee_puntaje1 = @i_puntaje1,
            tee_puntaje2 = @i_puntaje2
    where   tee_equipo1 = @i_equipo1
    and     tee_equipo2 = @i_equipo2
    
end --@i_operacion = 'AP'

if @i_operacion = 'APP' --Actualizacion de los puntajes de personas
begin
    update  cob_bvirtual..tb_registra_usuario_liga
    set     trul_puntaje = @i_puntaje_persona
    where   trul_nombre = @i_nombre
    and     trul_apellido = @i_apellido
    
end --@i_operacion = 'APP'

if @i_operacion = 'APG' --Actualizacion de los puntajes de grupos
begin
    update  cob_bvirtual..tb_registra_usuario_liga
    set     trul_puntaje = @i_puntaje_grupo
    where   trul_nombre_grupo = @i_grupo_liga
    
end --@i_operacion = 'APP'

if @i_operacion = 'CP' --Consulta puntajes de personas/grupos
begin
    select  trul_nombre,
            trul_apellido,
            trul_nombre_grupo,
            trul_puntaje
    from    cob_bvirtual..tb_registra_usuario_liga
    where   trul_nombre_liga = @i_nombre_liga
end --@i_operacion = 'CP'

go
EXEC sp_procxmode 'dbo.sp_quiniela_mundial', 'unchained'
go
IF OBJECT_ID('dbo.sp_quiniela_mundial') IS NOT NULL
    PRINT '<<< CREATED PROCEDURE dbo.sp_quiniela_mundial >>>'
ELSE
    PRINT '<<< FAILED CREATING PROCEDURE dbo.sp_quiniela_mundial >>>'
go