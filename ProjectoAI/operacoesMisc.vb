Option Strict Off
Option Explicit On 

Module operacoesMisc
    'Converte uma coordenada na sua posição bidimensional, no painel. 
    Function convertePosicao(ByRef x As Integer) As Integer()
        Dim result(2) As Integer
        result(0) = x Mod 13
        result(1) = x / 13
        convertePosicao = result
    End Function

    'Converte duas coordenadas x, y para a sua posicao no painel.
    Function converteCoordenadas(ByVal x As Integer, ByVal y As Integer) As Integer
        converteCoordenadas = y * 13 + x
    End Function

    'determina a distancia, pelo caminho mais curto entre dois pontos.
    Function distancia(ByRef ponto1() As Integer, ByRef ponto2() As Integer) As Integer
        distancia = System.Math.Sqrt(quad(ponto1(0) - ponto2(0)) + quad(ponto1(1) - ponto2(1)))
    End Function

    'Dá o quadrado de um número
    Function quad(ByRef num As Integer) As Integer
        quad = num * num
    End Function


End Module
