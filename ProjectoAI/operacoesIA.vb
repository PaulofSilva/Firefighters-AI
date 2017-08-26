Option Strict Off
Option Explicit On 

Module operacoesIA
    'guarda a posicao do bombeiro sem agua para o voltar a colocar depois de apagado o incendio
    Dim posicaoI As Integer

    'armazena o cenário
    Function putCenario()
        Dim i As Integer

        'Desenha a relva
        For i = 0 To nPosicoes
            cenario(i) = relva
        Next

        'a estrada horizontal em cima
        For i = 0 To 12
            cenario(i) = estrada
        Next

        'estrada vertical á esquerda
        For i = 13 To 156
            cenario(i) = estrada
            i = i + 12
        Next

        'estrada horizontal em baixo
        For i = 156 To 168
            cenario(i) = estrada
        Next

        'estrada vertical á direita
        For i = 25 To 168
            cenario(i) = estrada
            i = i + 12
        Next

        'estrada vertical no meio
        For i = 19 To 149
            cenario(i) = estrada
            i = i + 12
        Next

        'estrada horizontal no meio, abaixo
        For i = 105 To 115
            cenario(i) = estrada
        Next

        'estrada horizontal no meio, abaixo
        For i = 53 To 63
            cenario(i) = estrada
        Next
    End Function

    'armazena a posição de um novo fogo
    Function putFogo(ByRef pos As Integer) As Boolean
        If inflamavel(pos) And temElemento(pos) = False Then
            elementos(pos) = fogo
            contafogos = contafogos + 1
            putFogo = True
        Else
            putFogo = False
        End If
    End Function

    'Deveolve verdadeiro se a posição em questão representa algo inflamavel
    Function inflamavel(ByRef pos As Integer) As Boolean
        Dim x As Integer
        Dim y As Integer
        x = convertePosicao(pos)(0)
        y = convertePosicao(pos)(1)
        If (cenario(pos) = relva And x < 6) Then
            inflamavel = True
        Else
            inflamavel = False
        End If
    End Function

    'armazena a posição de um quartel
    Function putQuartel(ByRef pos As Integer) As Boolean
        If cenario(pos) = relva And juntoEstrada(pos) And Not temElemento(pos) Then
            elementos(pos) = quartel
            putQuartel = True
        Else
            putQuartel = False
        End If
    End Function

    'armazena a posição de um bombeiro
    Function putBombeiro(ByRef pos As Integer)
        If temElemento(pos) = False Then
            elementos(pos) = bombeiroSAgua
            putBombeiro = True
        Else
            putBombeiro = False
        End If
    End Function

    'devolve verdadeiro se a posição se encontra junto á estrada
    Function juntoEstrada(ByRef pos As Integer) As Boolean
        If pos - 1 > 0 And pos - 13 > 0 And pos + 1 < nPosicoes And pos + 13 < nPosicoes Then
            If cenario(pos + 1) = estrada Or cenario(pos - 1) = estrada Or (pos + 13 < nPosicoes And cenario(pos + 13) = estrada) Or (pos - 13 > 0 And cenario(pos - 13) = estrada) Then
                juntoEstrada = True
            Else
                juntoEstrada = False
            End If
        Else
            juntoEstrada = False
        End If
    End Function
    Function percorreFogos()
        Dim posFogo As Integer
        Dim bombOuQuartel As Integer
        For posFogo = 0 To nPosicoes
            If (elementos(posFogo) = fogo) Then
                bombOuQuartel = verificaMaisProximo(posFogo)
                'painel.Label4.Text = "O mais proximo eh: " & bombOuQuartel
            End If
        Next

        If (elementos(bombOuQuartel) = bombeiroSAgua) Then
            If move(posBombeiro, fogo, bombeiroSAgua) = False Then
                copyElementos(fogo) = ocupado

                extingueFogo(fogo)
                elementos(bombOuQuartel) = 0

                copyElementos(bombOuQuartel) = 0
            End If
        End If
        If (elementos(bombOuQuartel) = quartel) Then
            If cenario(bombOuQuartel + 1) = estrada Then
                elementos(bombOuQuartel + 1) = carroSAgua
                copyElementos(bombOuQuartel + 1) = ocupado
                If moveCarros(bombOuQuartel + 1, fogo, carroSAgua) = False Then
                    copyElementos(fogo) = ocupado

                    extingueFogo2(fogo)
                    elementos(bombOuQuartel + 1) = 0

                    copyElementos(bombOuQuartel + 1) = 0
                End If
            End If
            If (bombOuQuartel + 13 < nPosicoes And cenario(bombOuQuartel + 13) = estrada) Then
                elementos(bombOuQuartel + 13) = carroSAgua
                copyElementos(bombOuQuartel + 13) = ocupado
                If moveCarros(bombOuQuartel + 13, fogo, carroSAgua) = False Then
                    copyElementos(fogo) = ocupado

                    extingueFogo2(fogo)
                    elementos(bombOuQuartel + 13) = 0

                    copyElementos(bombOuQuartel + 13) = 0
                End If
            End If
            If (bombOuQuartel - 13 > 0 And cenario(bombOuQuartel - 13) = estrada) Then
                elementos(bombOuQuartel - 13) = carroSAgua
                copyElementos(bombOuQuartel - 13) = ocupado
                If moveCarros(bombOuQuartel - 13, fogo, carroSAgua) = False Then
                    copyElementos(fogo) = ocupado

                    extingueFogo2(fogo)
                    elementos(bombOuQuartel - 13) = 0

                    copyElementos(bombOuQuartel - 13) = 0
                End If
            End If
            If cenario(bombOuQuartel - 1) = estrada Then
                elementos(bombOuQuartel - 1) = carroSAgua
                copyElementos(bombOuQuartel - 1) = ocupado
                If moveCarros(bombOuQuartel - 1, fogo, carroSAgua) = False Then
                    copyElementos(fogo) = ocupado

                    extingueFogo2(fogo)
                    elementos(bombOuQuartel - 1) = 0

                    copyElementos(bombOuQuartel - 1) = 0
                End If
            End If
        End If
    End Function
    'move um elemento de uma posição inicial para uma posição final, pelo caminho mais curto
    Function moveCarros(ByRef posInicial As Integer, ByRef posFinal As Integer, ByRef elemento As Integer) As Boolean
        'identifica a pesquisa prolog
        Dim QueryID As Long
        'as coordenadas do proximo passo.
        Dim prox(2) As Integer
        'array com a posição inicial
        Dim inicial() As Integer = convertePosicao(posInicial)
        'array com a posição destino
        Dim final() As Integer = convertePosicao(posFinal)
        'a resposta em formato string
        Dim Resposta As Long
        Dim naoAndou As Integer = 1


        If (posInicial = posFinal) Then
            Return False
        End If
        While (naoAndou = 1)
            'painel.Label4.Text = " bombeiro desde X:" & inicial(0) & "Y: " & inicial(1) & " ate X:" & final(0) & "Y: " & final(1)

            QueryID = PrologOpenQuery("movsCarros(" & inicial(0) & "," & inicial(1) & "," & final(0) & "," & final(1) & ",X,Y)")
            PrologNextSolution(QueryID)
            If (PrologGetLong(QueryID, "X", Resposta) <> 0) Then
                prox(0) = Int(Resposta)
                'painel.Label5.Text = "X2: " & prox(0)
            End If
            If (PrologGetLong(QueryID, "Y", Resposta) <> 0) Then
                prox(1) = Int(Resposta)
                'painel.Label6.Text = "Y: " & Resposta
                'MsgBox("bla")
                'painel.Label6.Text = "Y2: " & prox(1)
            End If
            PrologCloseQuery(QueryID)

            'painel.Label4.Text = "a posicao inicial " & posInicial & " e igual a posicao que me estao a devolver " & converteCoordenadas(prox(0), prox(1))
            'painel.Label3.Text = "a prox casa pa eu andar eh " & converteCoordenadas(prox(0), prox(1))

            If (elementos(converteCoordenadas(prox(0), prox(1))) = fogo And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
                Return False
            End If

            If (elementos(converteCoordenadas(prox(0), prox(1))) = bombeiroSAgua And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
                posBombeiro = converteCoordenadas(prox(0), prox(1))
                'painel.Label3.Text = "a prox casa e um bombeiroSAgua " & converteCoordenadas(prox(0), prox(1))
                copyElementos(posInicial) = 0
                elementos(posInicial) = 0
                elementos(converteCoordenadas(prox(0), prox(1))) = elemento

                copyElementos(converteCoordenadas(prox(0), prox(1))) = ocupado
            End If

            If (elementos(converteCoordenadas(prox(0), prox(1))) = carroSAgua And ((converteCoordenadas(prox(0), prox(1))) <> posInicial) And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
                'MsgBox("a posicao para onde eu kero ir e um fogo")

                poscarro = converteCoordenadas(prox(0), prox(1))
                'painel.Label3.Text = "a prox casa e um carroSAgua " & converteCoordenadas(prox(0), prox(1))
                copyElementos(posInicial) = 0
                elementos(posInicial) = 0
                elementos(converteCoordenadas(prox(0), prox(1))) = elemento

                copyElementos(converteCoordenadas(prox(0), prox(1))) = ocupado

                Return True
            End If

            If (cenario(converteCoordenadas(prox(0), prox(1)))) = estrada And (copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
                naoAndou = 0
                copyElementos(posInicial) = 0
                elementos(posInicial) = 0
                elementos(converteCoordenadas(prox(0), prox(1))) = elemento
                copyElementos(converteCoordenadas(prox(0), prox(1))) = ocupado

                'painel.Label3.Text = "a prox casa nao tem nada " & converteCoordenadas(prox(0), prox(1))
                If (posBombeiro <> -1) Then
                    elementos(posBombeiro) = bombeiroSAgua
                    posBombeiro = -1
                    'painel.Label3.Text = "passei em cima de um bombeiro sem agua " & posInicial
                End If
                If (posCarro <> -1) Then
                    elementos(posCarro) = carroSAgua
                    posCarro = -1
                    'painel.Label3.Text = "passei em cima de um bombeiro sem agua " & posInicial
                End If

            End If
        End While
        Return False
    End Function
    Function verificaMaisProximo(ByVal fogo) As Integer
        Dim result As Integer = -1
        Dim posFogo As Integer
        Dim posBombeiro As Integer
        Dim posQuartel As Integer
        Dim indexFogo As Integer = -1
        Dim distanciaBF As Integer = nPosicoes
        copyElementos = elementos.Clone()

        For posBombeiro = 0 To nPosicoes
            If (copyElementos(posBombeiro) = bombeiroSAgua) Then
                If (distanciaBF >= distancia(convertePosicao(posBombeiro), convertePosicao(fogo))) Then
                    distanciaBF = distancia(convertePosicao(posBombeiro), convertePosicao(fogo))
                    result = posBombeiro
                End If
            End If
        Next

        For posQuartel = 0 To nPosicoes
            If (copyElementos(posQuartel) = quartel) Then
                If (distanciaBF >= distancia(convertePosicao(posBombeiro), convertePosicao(fogo))) Then
                    distanciaBF = distancia(convertePosicao(posBombeiro), convertePosicao(fogo))
                    result = posQuartel
                End If
            End If
        Next
        result = verificaMaisProximo
    End Function

    'função que determina o movimentos dos bombeiros, de acordo com os fogos.
    Function moveBombeiros() As Boolean
        Dim posFogo As Integer
        Dim posBombeiro As Integer
        Dim indexFogo As Integer = -1
        Dim distanciaBF As Integer = nPosicoes
        copyElementos = elementos.Clone()
        Dim i As Integer
        'Const ocupado As Integer = 101

        For posBombeiro = 0 To nPosicoes
            If (copyElementos(posBombeiro) = bombeiroSAgua) Then
                indexFogo = -1
                For posFogo = 0 To nPosicoes
                    If (copyElementos(posFogo) = fogo) Then
                        If (distanciaBF >= distancia(convertePosicao(posBombeiro), convertePosicao(posFogo))) Then
                            distanciaBF = distancia(convertePosicao(posBombeiro), convertePosicao(posFogo))
                            indexFogo = posFogo
                        End If
                    End If
                Next
                'teste()
                If indexFogo <> -1 Then
                    'painel.Label7.Text = "bombeiro de " & posBombeiro & " para " & indexFogo
                    If move(posBombeiro, indexFogo, bombeiroSAgua) = False Then
                        'elementos(indexFogo) = bombeiroCAgua
                        'For i = 0 To 10000
                        'Next
                        copyElementos(indexFogo) = ocupado

                        extingueFogo(indexFogo)
                        elementos(posBombeiro) = 0

                        copyElementos(posBombeiro) = 0
                    End If
                    distanciaBF = nPosicoes
                End If
            End If
        Next
        Return True
    End Function

    'move um elemento de uma posição inicial para uma posição final, pelo caminho mais curto
    Function move(ByRef posInicial As Integer, ByRef posFinal As Integer, ByRef elemento As Integer) As Boolean
        'identifica a pesquisa prolog
        Dim QueryID As Long
        'as coordenadas do proximo passo.
        Dim prox(2) As Integer
        'array com a posição inicial
        Dim inicial() As Integer = convertePosicao(posInicial)
        'array com a posição destino
        Dim final() As Integer = convertePosicao(posFinal)
        'a resposta em formato string
        Dim Resposta As Long

        'If (posInicial = posFinal) Then
        'Return False
        'End If

        'painel.Label4.Text = " bombeiro desde X:" & inicial(0) & "Y: " & inicial(1) & " ate X:" & final(0) & "Y: " & final(1)

        QueryID = PrologOpenQuery("movsBombeiros(" & inicial(0) & "," & inicial(1) & "," & final(0) & "," & final(1) & ",X,Y)")
        PrologNextSolution(QueryID)
        If (PrologGetLong(QueryID, "X", Resposta) <> 0) Then
            prox(0) = Int(Resposta)
            'painel.Label5.Text = "X2: " & prox(0)
        End If
        If (PrologGetLong(QueryID, "Y", Resposta) <> 0) Then
            prox(1) = Int(Resposta)
            'painel.Label6.Text = "Y: " & Resposta
            'MsgBox("bla")
            'painel.Label6.Text = "Y2: " & prox(1)
        End If
        PrologCloseQuery(QueryID)

        'painel.Label4.Text = "a posicao inicial " & posInicial & " e igual a posicao que me estao a devolver " & converteCoordenadas(prox(0), prox(1))
        'painel.Label3.Text = "a prox casa pa eu andar eh " & converteCoordenadas(prox(0), prox(1))

        If (elementos(converteCoordenadas(prox(0), prox(1))) = fogo And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then

            Return False
        End If

        If (elementos(converteCoordenadas(prox(0), prox(1))) = quartel And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
            'MsgBox("a posicao para onde eu kero ir e um fogo")
            posQuartel = converteCoordenadas(prox(0), prox(1))
            'painel.Label3.Text = "a prox casa e um quartel " & converteCoordenadas(prox(0), prox(1))
            copyElementos(posInicial) = 0
            elementos(posInicial) = 0
            elementos(converteCoordenadas(prox(0), prox(1))) = elemento

            copyElementos(converteCoordenadas(prox(0), prox(1))) = ocupado

            Return True
        End If

        If (elementos(converteCoordenadas(prox(0), prox(1))) = bombeiroSAgua And ((converteCoordenadas(prox(0), prox(1))) <> posInicial) And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
            'MsgBox("a posicao para onde eu kero ir e um fogo")

            posBombeiro = converteCoordenadas(prox(0), prox(1))
            'painel.Label3.Text = "a prox casa e um bombeiroSAgua " & converteCoordenadas(prox(0), prox(1))
            copyElementos(posInicial) = 0
            elementos(posInicial) = 0
            elementos(converteCoordenadas(prox(0), prox(1))) = elemento

            copyElementos(converteCoordenadas(prox(0), prox(1))) = ocupado

            Return True



        ElseIf (temElemento(elementos(converteCoordenadas(prox(0), prox(1)))) = False And copyElementos(converteCoordenadas(prox(0), prox(1))) <> ocupado) Then
            copyElementos(posInicial) = 0
            elementos(posInicial) = 0
            elementos(converteCoordenadas(prox(0), prox(1))) = elemento
            copyElementos(converteCoordenadas(prox(0), prox(1))) = ocupado
            'painel.Label3.Text = "a prox casa nao tem nada " & converteCoordenadas(prox(0), prox(1))
            If (posQuartel <> -1) Then
                elementos(posQuartel) = quartel
                posQuartel = -1
                'painel.Label3.Text = "passei em cima de um quartel na posicao " & posInicial
            End If
            If (posBombeiro <> -1) Then
                elementos(posBombeiro) = bombeiroSAgua
                posBombeiro = -1
                'painel.Label3.Text = "passei em cima de um bombeiro sem agua " & posInicial
            End If
            If (posBombeiroCAgua <> -1) Then
                elementos(posBombeiroCAgua) = bombeiroCAgua
                posBombeiroCAgua = -1
                'painel.Label3.Text = "passei em cima de um bombeiro com agua " & posInicial
            End If
            Return True
        End If

        Return False
    End Function


    'devolve verdadeiro se aposição dada já tem algum elemento contido.
    Function temElemento(ByRef pos As Integer) As Boolean
        temElemento = elementos(pos) <> 0
    End Function

    'devolve o numero de fogos activos mudado by susy
    Function totalFogosActivos() As Integer
        Dim i, total As Integer
        total = 0
        For i = 0 To nPosicoes
            If (elementos(i) = fogo) Then
                total = total + 1
            End If
        Next
        totalFogosActivos = total
    End Function

    'devolve o numero de bombeiros activos mudado by susy
    Function totalBombeirosActivos() As Integer
        Dim i, total As Integer
        total = 0
        For i = 0 To nPosicoes
            If (eBombeiro(elementos(i))) Then
                total = total + 1
            End If
        Next
        totalBombeirosActivos = total
    End Function

    'devolve o numero de carros activos mudado by susy
    Function totalCarrosActivos() As Integer
        Dim i, total As Integer
        total = 0
        For i = 0 To nPosicoes
            If (eCarro(elementos(i))) Then
                total = total + 1
            End If
        Next
        totalCarrosActivos = total
    End Function

    'devolve o numero de avioes activos mudado by susy
    Function totalAvioesActivos() As Integer
        Dim i, total As Integer
        total = 0
        For i = 0 To nPosicoes
            If (elementos(i) = aviao) Then
                total = total + 1
            End If
        Next
        totalAvioesActivos = total
    End Function

    Public Sub extingueFogo(ByRef posicao As Integer) 'mudado by susy
        'Private Declare Sub sleep Lib "kernel32" (ByVal milisegundos As Long)
        Dim i As Long
        elementos(posicao) = bombeiroCAgua
        'painel.Label3.Text = "mudei o bombeiro para agua e agr vou esperar"


        'Application.OnTime(When:=Now + TimeValue("00:00:15"))
        'Name:="DummyMacro"
        'elementos(posicao) = bombeiroSAgua
        'painel.Label3.Text = "mudei o bombeiro para sem agua e acabei"
    End Sub

    Public Sub extingueFogo2(ByRef posicao As Integer) 'mudado by susy
        'Private Declare Sub sleep Lib "kernel32" (ByVal milisegundos As Long)
        Dim i As Long
        elementos(posicao) = carroCAgua
        'painel.Label3.Text = "mudei o carro para agua e agr vou esperar"


        'Application.OnTime(When:=Now + TimeValue("00:00:15"))
        'Name:="DummyMacro"
        'elementos(posicao) = bombeiroSAgua
        'painel.Label3.Text = "mudei o carro para sem agua e acabei"
    End Sub


End Module
