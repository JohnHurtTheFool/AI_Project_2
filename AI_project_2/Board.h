#pragma once
#include "Move.h"
class Board
{
public:
	static const enum pieces{K,Q,R,B,N,P};//(Pawns are not really pieces, but we include them anyway.)
	const double Q_V = 9.2;
	const double R_V = 5.0;
	const double B_V = 3.3;
	const double N_V = 3.0;
	const double P_V = 1.0;
	struct content
	{
		bool empty;
		bool white;//Color of piece.
		pieces piece;
	};
	static const int NUM_RANKS = 8;
	static const int NUM_FILES = 8;
	bool isLegal(Move m);//Whether the move is legal
	Board result (Move m);//The board after the move is made.
	int materialCount();
	int weightedCount();//Our heuristic. Could be based on material count and other factors
	Board(void);
	~Board(void);
private:
	content squares[NUM_RANKS][NUM_FILES];//An array of the contents in each square.
	bool whiteKingMoved;//We keep track of whether the kings have moved, so that we know if they can castle
	bool blackKingMoved;
	Move last;//For en passant.
};

