Option Strict Off
Option Explicit On

Module operacoesProlog

    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    '%%%%%%%%% Carregar ficheiros prolog %%%%%%%%
    '%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    Function StartProlog(ByVal file As String)
        'MsgBox file
        If PrologInit() <> 1 Then
            MsgBox("Erro ao inicializar a interface para Prolog", vbCritical, "Erro!")
            End
        Else
            If PrologQueryCutFail("[" & file & "]") <> 1 Then
                MsgBox("Erro ao ler ficheiro " & file & ".pl", vbCritical, "Erro!")
                End
            End If
        End If
    End Function

End Module
