Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.OleDb

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class QuinielaMundial
    Inherits System.Web.Services.WebService

    Const CONNECTIONSTRINGCOB As String = "SybConnStringCob"
    Dim dbConnectionOleDb As New OleDb.OleDbConnection

    Dim objCommandOleDb As OleDbCommand
    Dim daAdapterOleDb As OleDbDataAdapter
    Dim objParamOleDb As OleDbParameter
    Dim dsDataSet As DataSet

    Dim strRetorno As String

    'Se crea el usuario
    <WebMethod()>
    Public Function RegistroUsuario(ByVal usuario As String, ByVal contrasena As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_info_usuarios"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 1)
            objParamOleDb.Value = "I"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_usuario", OleDbType.VarChar, 30)
            objParamOleDb.Value = usuario
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_pass", OleDbType.VarChar, 50)
            objParamOleDb.Value = contrasena

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<RegistroUsuario><resultado codigo=""0""><![CDATA[USUARIO CREADO]]></resultado>"
            strRetorno = strRetorno & "</RegistroUsuario>"

        Catch ex As Exception
            strRetorno = "<RegistroUsuario>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</RegistroUsuario>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Consulta el usuario y la contraseña de la persona registrada
    <WebMethod()>
    Public Function ConsultaUsuario(ByVal usuario As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_info_usuarios"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 1)
            objParamOleDb.Value = "C"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_usuario", OleDbType.VarChar, 30)
            objParamOleDb.Value = usuario
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_pass", OleDbType.VarChar, 50)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            If dsDataSet.Tables.Count > 0 Then
                If dsDataSet.Tables(0).Rows.Count > 0 Then
                    strRetorno = "<ConsultaUsuario><resultado codigo=""0""><![CDATA[CONSULTA EXITOSA]]></resultado>"
                    For i = 0 To (dsDataSet.Tables(0).Rows.Count - 1)
                        strRetorno = strRetorno & "<consulta>"
                        strRetorno = strRetorno & "<usuario><![CDATA[" & Trim(dsDataSet.Tables(0).Rows(i).Item(0).ToString) & "]]></usuario>"
                        strRetorno = strRetorno & "<contrasena><![CDATA[" & Trim(dsDataSet.Tables(0).Rows(i).Item(1).ToString) & "]]></contrasena>"
                        strRetorno = strRetorno & "</consulta>"
                    Next
                    strRetorno = strRetorno & "</ConsultaUsuario>"

                Else
                    strRetorno = "<ConsultaUsuario>" &
                          "<resultado codigo=""2""><![CDATA[NO SE OBTUVIERON DATOS DE LA CONSULTA.]]></resultado>" &
                        "</ConsultaUsuario>"
                End If
            Else
                strRetorno = "<ConsultaUsuario>" &
                      "<resultado codigo=""2""><![CDATA[OCURRIO UN ERROR EN LA CONSULTA.]]></resultado>" &
                    "</ConsultaUsuario>"
            End If

        Catch ex As Exception
            strRetorno = "<ConsultaUsuario>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</ConsultaUsuario>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Actualización de contraseña del usuario
    <WebMethod()>
    Public Function ActualizaUsuario(ByVal usuario As String, ByVal contrasena As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_info_usuarios"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 1)
            objParamOleDb.Value = "A"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_usuario", OleDbType.VarChar, 30)
            objParamOleDb.Value = usuario
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_pass", OleDbType.VarChar, 50)
            objParamOleDb.Value = contrasena

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<RegistroUsuario><resultado codigo=""0""><![CDATA[USUARIO ACTUALIZADO]]></resultado>"
            strRetorno = strRetorno & "</RegistroUsuario>"

        Catch ex As Exception
            strRetorno = "<RegistroUsuario>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</RegistroUsuario>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se registra el vaticinio
    <WebMethod()>
    Public Function RegistroVaticinio(ByVal nombre As String, ByVal apellido As String, ByVal grupo As String, ByVal liga As String, ByVal equipo1 As String, ByVal puntaje1 As Integer, ByVal equipo2 As String, ByVal puntaje2 As Integer) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "RV"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = equipo1
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = equipo2
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = puntaje1
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = puntaje2
            If nombre = "" Then
                objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
                objParamOleDb.Value = DBNull.Value
            Else
                objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
                objParamOleDb.Value = nombre
            End If
            If apellido = "" Then
                objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
                objParamOleDb.Value = DBNull.Value
            Else
                objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
                objParamOleDb.Value = apellido
            End If
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = liga
            If grupo = "" Then
                objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
                objParamOleDb.Value = DBNull.Value
            Else
                objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
                objParamOleDb.Value = grupo
            End If
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<RegistroVaticinio><resultado codigo=""0""><![CDATA[VATICINIO CREADO]]></resultado>"
            strRetorno = strRetorno & "</RegistroVaticinio>"

        Catch ex As Exception
            strRetorno = "<RegistroVaticinio>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</RegistroVaticinio>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se registra a las personas participantes en la liga
    <WebMethod()>
    Public Function RegistroPersonaLiga(ByVal nombre As String, ByVal apellido As String, ByVal liga As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "RP"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = nombre
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = apellido
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = liga
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<RegistroPersonaLiga><resultado codigo=""0""><![CDATA[PARTICIPANTE CREADO]]></resultado>"
            strRetorno = strRetorno & "</RegistroPersonaLiga>"

        Catch ex As Exception
            strRetorno = "<RegistroPersonaLiga>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</RegistroPersonaLiga>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se registra a los grupos participantes en la liga
    <WebMethod()>
    Public Function RegistroGrupoLiga(ByVal grupo As String, ByVal liga As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "RG"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = liga
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = grupo
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<RegistroGrupoLiga><resultado codigo=""0""><![CDATA[GRUPO PARTICIPANTE CREADO]]></resultado>"
            strRetorno = strRetorno & "</RegistroGrupoLiga>"

        Catch ex As Exception
            strRetorno = "<RegistroGrupoLiga>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</RegistroGrupoLiga>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se registra los encuentros de los equipos
    <WebMethod()>
    Public Function RegistroEncuentroEquipos(ByVal equipo1 As String, ByVal equipo2 As String, ByVal fecha As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "EN"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = equipo1
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = equipo2
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = fecha

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<RegistroEncuentroEquipos><resultado codigo=""0""><![CDATA[ENCUENTRO CREADO]]></resultado>"
            strRetorno = strRetorno & "</RegistroEncuentroEquipos>"

        Catch ex As Exception
            strRetorno = "<RegistroEncuentroEquipos>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</RegistroEncuentroEquipos>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se actualiza los puntajes de los equipos
    <WebMethod()>
    Public Function ActualizaPuntajeEquipos(ByVal equipo1 As String, ByVal equipo2 As String, ByVal puntaje1 As Integer, ByVal puntaje2 As Integer) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "AP"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = equipo1
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = equipo2
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = puntaje1
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = puntaje2
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<ActualizaPuntajeEquipos><resultado codigo=""0""><![CDATA[PUNTAJE ACTUALIZADO]]></resultado>"
            strRetorno = strRetorno & "</ActualizaPuntajeEquipos>"

        Catch ex As Exception
            strRetorno = "<ActualizaPuntajeEquipos>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</ActualizaPuntajeEquipos>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se actualiza los puntajes de las personas de la liga
    <WebMethod()>
    Public Function ActualizaPuntajePersona(ByVal nombre As String, ByVal apellido As String, ByVal puntaje As Integer) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "APP"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = nombre
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = apellido
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = puntaje
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<ActualizaPuntajePersona><resultado codigo=""0""><![CDATA[PUNTAJE ACTUALIZADO]]></resultado>"
            strRetorno = strRetorno & "</ActualizaPuntajePersona>"

        Catch ex As Exception
            strRetorno = "<ActualizaPuntajePersona>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</ActualizaPuntajePersona>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Se actualiza los puntajes de los grupos de la liga
    <WebMethod()>
    Public Function ActualizaPuntajeGrupo(ByVal grupo As String, ByVal puntaje As Integer) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "APG"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = grupo
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = puntaje
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            strRetorno = "<ActualizaPuntajeGrupo><resultado codigo=""0""><![CDATA[PUNTAJE ACTUALIZADO]]></resultado>"
            strRetorno = strRetorno & "</ActualizaPuntajeGrupo>"

        Catch ex As Exception
            strRetorno = "<ActualizaPuntajeGrupo>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</ActualizaPuntajeGrupo>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function

    'Consulta de los resultados de las personas/grupos por liga
    <WebMethod()>
    Public Function ConsultaPuntajesPG(ByVal liga As String) As String

        Try
            dbConnectionOleDb.ConnectionString = ConfigurationManager.AppSettings.Get(CONNECTIONSTRINGCOB)

            '---------------[CONEXION A SERVIDOR]-------------
            daAdapterOleDb = New OleDbDataAdapter()
            dsDataSet = New DataSet()
            objCommandOleDb = dbConnectionOleDb.CreateCommand
            objCommandOleDb.CommandType = CommandType.StoredProcedure
            objCommandOleDb.CommandText = "cob_bvirtual..sp_quiniela_mundial"

            '----------------[PARAMETROS DE ENTRADA]---------------
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_operacion", OleDbType.Char, 3)
            objParamOleDb.Value = "CP"
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo1", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_equipo2", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje1", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje2", OleDbType.Integer)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_apellido", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_nombre_liga", OleDbType.VarChar, 100)
            objParamOleDb.Value = liga
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_grupo_liga", OleDbType.VarChar, 30)
            objParamOleDb.Value = DBNull.Value
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_persona", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_puntaje_grupo", OleDbType.Integer)
            objParamOleDb.Value = 0
            objParamOleDb = objCommandOleDb.Parameters.Add("@i_fecha_encuentro", OleDbType.Date)
            objParamOleDb.Value = DBNull.Value

            daAdapterOleDb.SelectCommand = objCommandOleDb

            dbConnectionOleDb.Open()
            daAdapterOleDb.Fill(dsDataSet)

            If dsDataSet.Tables.Count > 0 Then
                If dsDataSet.Tables(0).Rows.Count > 0 Then
                    strRetorno = "<ConsultaPuntajesPG><resultado codigo=""0""><![CDATA[CONSULTA EXITOSA]]></resultado>"
                    For i = 0 To (dsDataSet.Tables(0).Rows.Count - 1)
                        strRetorno = strRetorno & "<consulta>"
                        strRetorno = strRetorno & "<nombre><![CDATA[" & Trim(dsDataSet.Tables(0).Rows(i).Item(0).ToString) & "]]></nombre>"
                        strRetorno = strRetorno & "<apellido><![CDATA[" & Trim(dsDataSet.Tables(0).Rows(i).Item(1).ToString) & "]]></apellido>"
                        strRetorno = strRetorno & "<grupo><![CDATA[" & Trim(dsDataSet.Tables(0).Rows(i).Item(2).ToString) & "]]></grupo>"
                        strRetorno = strRetorno & "<puntaje><![CDATA[" & Trim(dsDataSet.Tables(0).Rows(i).Item(3).ToString) & "]]></puntaje>"
                        strRetorno = strRetorno & "</consulta>"
                    Next
                    strRetorno = strRetorno & "</ConsultaPuntajesPG>"

                Else
                    strRetorno = "<ConsultaPuntajesPG>" &
                          "<resultado codigo=""2""><![CDATA[NO SE OBTUVIERON DATOS DE LA CONSULTA.]]></resultado>" &
                        "</ConsultaPuntajesPG>"
                End If
            Else
                strRetorno = "<ConsultaPuntajesPG>" &
                      "<resultado codigo=""2""><![CDATA[OCURRIO UN ERROR EN LA CONSULTA.]]></resultado>" &
                    "</ConsultaPuntajesPG>"
            End If

        Catch ex As Exception
            strRetorno = "<ConsultaPuntajesPG>" &
                     "<resultado codigo=""4""><![CDATA[" & ex.Message & "]]></resultado>" &
                   "</ConsultaPuntajesPG>"
        Finally
            dbConnectionOleDb.Close()
            dbConnectionOleDb.Dispose()
            dbConnectionOleDb = Nothing
            objCommandOleDb = Nothing
            daAdapterOleDb = Nothing
            dsDataSet = Nothing
            objParamOleDb = Nothing
        End Try

        Return strRetorno

    End Function
End Class