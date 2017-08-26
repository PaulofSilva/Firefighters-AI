:-use_module(library(lists)).
:-use_module(library(random)).

% ************************************
% As estradas possiveis de percorrer *
% X - coordenada X                   *
% Y - coordenada Y                   *
% ************************************
estrada(1,X,Y) :- X >= 0, X =< 12, Y == 0.  
estrada(2,X,Y) :- X >= 0, X =< 12, Y == 4. 
estrada(3,X,Y) :- X >= 0, X =< 12, Y == 8. 
estrada(4,X,Y) :- X >= 0, X =< 12, Y == 10.
estrada(5,X,Y) :- Y >= 0, Y =< 12, X == 0.
estrada(6,X,Y) :- Y >= 0, Y =< 12, X == 6. 
estrada(7,X,Y) :- Y >= 0, Y =< 12, X == 12.


% **************************************
% Os espaços verdes possiveis de arder *
% X - coordenada X                     *
% Y - coordenada Y                     *
% **************************************
relva(1,X,Y) :- X > 0, X < 6, Y > 0, Y < 4.
relva(2,X,Y) :- X > 6, X < 12, Y > 0, Y < 4.
relva(3,X,Y) :- X > 0, X < 6, Y > 4, Y < 8.
relva(4,X,Y) :- X > 6, X < 12, Y > 4, Y < 8.
relva(5,X,Y) :- X > 0, X < 6, Y > 8, Y < 12.
relva(6,X,Y) :- X > 6, X < 12, Y > 8, Y < 12.

% **************************************
% Calcula posicoes aleatorias          *
% **************************************
random1(Y):-
	random(0, 168, Y).

% **************************************
% Deslocacao dos bombeiros             *
% Xi - coordenada X inicial            *
% Yi - coordenada Y final              *
% Xf - coordenada X final              *
% Yf - coordenada Y final              *
% Xf - lista de onde quero partir      *
% L - lista com todas as posicoes percorridas*
% **************************************
movsBombeiros(Xi,Yi,Xf,Yf,X,Y) :- Xi =< Xf, Yi =< Yf, andarBomb(Xi,Yi,Xf,Yf,X,Y).
movsBombeiros(Xi,Yi,Xf,Yf,X,Y) :- Xi > Xf, Yi > Yf, andarBomb2(Xi,Yi,Xf,Yf,X,Y).
movsBombeiros(Xi,Yi,Xf,Yf,X,Y) :- Xi =< Xf, Yi > Yf, andarBomb4(Xi,Yi,Xf,Yf,X,Y).
movsBombeiros(Xi,Yi,Xf,Yf,X,Y) :- Xi > Xf, Yi =< Yf, andarBomb3(Xi,Yi,Xf,Yf,X,Y).

andarBomb(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi+1, Y is Yi.
andarBomb(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi+1.
andarBomb(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi+1, Y is Yi+1.
andarBomb(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

andarBomb2(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi-1, Y is Yi.
andarBomb2(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi-1.
andarBomb2(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi-1, Y is Yi-1.
andarBomb2(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

andarBomb3(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi-1, Y is Yi.
andarBomb3(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi+1.
andarBomb3(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi-1, Y is Yi+1.
andarBomb3(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

andarBomb4(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi+1, Y is Yi.
andarBomb4(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi-1.
andarBomb4(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi+1, Y is Yi-1.
andarBomb4(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

% **************************************
% Deslocacao dos carros                *
% Xi - coordenada X inicial            *
% Yi - coordenada Y final              *
% Xf - coordenada X final              *
% Yf - coordenada Y final              *
% Xf - lista de onde quero partir      *
% L - lista com todas as posicoes percorridas*
% **************************************
movsCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi =< Xf, Yi =< Yf, andarCarros(Xi,Yi,Xf,Yf,X,Y).
movsCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi > Xf, Yi > Yf, andarCarros2(Xi,Yi,Xf,Yf,X,Y).
movsCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi =< Xf, Yi > Yf, andarCarros4(Xi,Yi,Xf,Yf,X,Y).
movsCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi > Xf, Yi =< Yf, andarCarros3(Xi,Yi,Xf,Yf,X,Y).

andarCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi+1, Y is Yi.
andarCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi+1.
andarCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi+1, Y is Yi.
andarCarros(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

andarCarros2(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi-1, Y is Yi.
andarCarros2(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi-1.
andarCarros2(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi-1, Y is Yi.
andarCarros2(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

andarCarros3(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi-1, Y is Yi.
andarCarros3(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi+1.
andarCarros3(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi-1, Y is Yi.
andarCarros3(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.

andarCarros4(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi==Yf, X is Xi+1, Y is Yi.
andarCarros4(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi\==Yf, X is Xi, Y is Yi-1.
andarCarros4(Xi,Yi,Xf,Yf,X,Y) :- Xi\==Xf, Yi\==Yf, X is Xi+1, Y is Yi.
andarCarros4(Xi,Yi,Xf,Yf,X,Y) :- Xi==Xf, Yi==Yf, X is Xi, Y is Yi.


% **************************************
% Calcula tamanho da lista             *
% **************************************
tamanho_lista(L,Y):-
	length(L,Y).

distancia(X,Y,D) :-L is sqrt((X*X)+(Y*Y)), D is round(L).