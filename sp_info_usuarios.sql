USE cob_bvirtual
go
IF OBJECT_ID('dbo.sp_info_usuarios') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.sp_info_usuarios
    IF OBJECT_ID('dbo.sp_info_usuarios') IS NOT NULL
        PRINT '<<< FAILED DROPPING PROCEDURE dbo.sp_info_usuarios >>>'
    ELSE
        PRINT '<<< DROPPED PROCEDURE dbo.sp_info_usuarios >>>'
END
go
create proc dbo.sp_info_usuarios
@i_operacion    char(1),
@i_usuario      varchar(30),
@i_pass         varchar(50) =  null

as

if @i_operacion = 'I'
begin
    insert into cob_bvirtual..tb_usuario_quiniela
    values (@i_usuario, @i_pass)
    
end --@i_operacion = 'I'

if @i_operacion = 'C'
begin
    select  tuq_usuario,
            tuq_pass
    from    cob_bvirtual..tb_usuario_quiniela
    where   tuq_usuario = @i_usuario
    
end --@i_operacion = 'C'

if @i_operacion = 'A'
begin
    update  cob_bvirtual..tb_usuario_quiniela
    set     tuq_pass = @i_pass
    where   tuq_usuario = @i_usuario
    
end --@i_operacion = 'A'

go
EXEC sp_procxmode 'dbo.sp_info_usuarios', 'unchained'
go
IF OBJECT_ID('dbo.sp_info_usuarios') IS NOT NULL
    PRINT '<<< CREATED PROCEDURE dbo.sp_info_usuarios >>>'
ELSE
    PRINT '<<< FAILED CREATING PROCEDURE dbo.sp_info_usuarios >>>'
go