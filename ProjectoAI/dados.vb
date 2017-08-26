Option Strict Off
Option Explicit On 

'aqui ficam guardados todos os dados usados globalmente
Module dados
    'A directoria da aplicação
    Public path As String = Application.StartupPath()

    Public Imagem(170) As System.Windows.Forms.PictureBox

    Public posQuartel As Integer = -1
    Public posCarro As Integer = -1
    Public posBombeiro As Integer = -1
    Public posBombeiroCAgua As Integer = -1

    Public copyElementos() As Integer

    Public Const ocupado As Integer = 101
    'é o número de posições do painel
    Public Const nPosicoes = 168

    'Conta o numero total de fogos
    Public contafogos As Integer = 0

    'Representa o cenário
    Public cenario(nPosicoes) As Integer

    'representa as posições dos elementos do programa.
    Public elementos(nPosicoes) As Integer


    'Estas constantes são utilizadas para representar o estado de cada posição.

    'elementos do cenario
    '10 relva
    Public Const relva As Integer = 10
    '11 estrada
    Public Const estrada As Integer = 11

    'elementos animados.
    '2 aviao
    Public Const aviao As Integer = 20
    '3 bombeiro sem agua
    Public Const bombeiroSAgua As Integer = 21
    '4 bombeiro com agua
    Public Const bombeiroCAgua As Integer = 22
    '5 carro com agua
    Public Const carroCAgua As Integer = 23
    '6 carro sem agua
    Public Const carroSAgua As Integer = 24
    '7 quartel
    Public Const quartel As Integer = 25
    '8 fogo
    Public Const fogo As Integer = 27
    '9 bombeiro sem agua na estrada
    'Public Const bombeiroSAguaEstrada As integer = 0
    '10 bombeiro com agua na estrada
    'Public Const bombeiroCAguaEstrada As integer = 0

    'Representa os estados de cada quadrícula.
    Public estado(168) As Integer

    'Aqui é a declaração dos valores do programa.

    'Aqui são atribuidas as imagens.
    'a relva
    Public relva_Img As Bitmap = Image.FromFile(path + "\imagens\relva.bmp")
    'a estrada horizontal
    Public estradaH_Img As Bitmap = Image.FromFile(path + "\imagens\estrada1.bmp")
    'a estrada vertical
    Public estradaV_Img As Bitmap = Image.FromFile(path + "\imagens\estrada2.bmp")
    'o fogo
    Public fogo_Img As Bitmap = Image.FromFile(path + "\imagens\fogo.bmp")
    'o avião
    Public aviao_Img As Bitmap = Image.FromFile(path + "\imagens\aviao2.bmp")
    'o bombeiro com agua
    Public bombeiroCAgua_Img As Bitmap = Image.FromFile(path + "\imagens\bombeiro_com_Agua.bmp")
    'o bombeiro sem agua
    Public bombeiroSAgua1_Img As Bitmap = Image.FromFile(path + "\imagens\bombeiro_sem_agua1.bmp")
    Public bombeiroSAgua2_Img As Bitmap = Image.FromFile(path + "\imagens\bombeiro_sem_agua2.bmp")

    Public quartel_Img As Bitmap = Image.FromFile(path + "\imagens\casa.bmp")
    'o carro com agua
    Public carroCAgua_Img As Bitmap = Image.FromFile(path + "\imagens\carro_agua.bmp")
    'o carro sem agua
    Public carroSAgua_Img As Bitmap = Image.FromFile(path + "\imagens\carro_sem_agua.bmp")

    'devolve a imagem correspondente ao código dado
    Function getImagem(ByRef cod As Integer, ByRef cenario As Integer)
        If cod = relva Then
            getImagem = relva_Img
        ElseIf cod = estrada Then
            getImagem = estradaH_Img
        ElseIf cod = aviao Then
            getImagem = aviao_Img
        ElseIf cod = bombeiroSAgua And cenario = relva Then
            getImagem = bombeiroSAgua2_Img
        ElseIf cod = bombeiroSAgua And cenario = estrada Then
            getImagem = bombeiroSAgua1_Img
        ElseIf cod = bombeiroCAgua Then
            getImagem = bombeiroCAgua_Img
        ElseIf cod = carroSAgua Then
            getImagem = carroSAgua_Img
        ElseIf cod = carroCAgua Then
            getImagem = carroCAgua_Img
        ElseIf cod = quartel Then
            getImagem = quartel_Img
        ElseIf cod = fogo Then
            getImagem = fogo_Img
        End If
    End Function

    'limpa o array que armazena os dados dos elementos
    Function limpa()
        'painel.Label3.Text = "Estou a limpari"
        Dim pos As Integer
        For pos = 0 To nPosicoes
            elementos(pos) = 0
        Next
    End Function

    'funcao que verifica se um determinado elemento e um bombeiro
    Function eBombeiro(ByRef elem As Integer) As Boolean
        eBombeiro = (elem = bombeiroSAgua Or elem = bombeiroCAgua)
    End Function

    'funcao que verifica se um determinado elemento e um carro
    Function eCarro(ByRef elem As Integer) As Boolean
        eCarro = (elem = carroSAgua Or elem = carroCAgua)
    End Function
End Module
