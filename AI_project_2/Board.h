#pragma once
#include "Move.h"
#include <vector>
namespace boardNS
{
	const double Q_V = 9.2;
	const double R_V = 5.0;
	const double B_V = 3.3;
	const double N_V = 3.0;
	const double P_V = 1.0;
}
class Board
{
public:
	static const enum pieces{K,Q,R,B,N,P};//(Pawns are not really pieces, but we include them anyway.)
	struct content
	{
		bool empty;
		bool white;//Color of piece.
		pieces piece;
	};
	static const int NUM_RANKS = 8;
	static const int NUM_FILES = 8;
	
	Board result (Move m);//The board after the move is made.
	int materialCount();//Positive for white has more material.
	int weightedCount();//Our heuristic. Could be based on material count and other factors
	std::vector<Move> getLegal();//Returns a vector of all legal moves.
	Board(void);//Creates board at initial position
	~Board(void);
private:
	bool isLegal(Move m);//Whether the move is legal
	content squares[NUM_FILES][NUM_RANKS];//An array of the contents in each square. (0,0) is a1, (7,0) is h1, (0,7) is a8, etc
	bool whiteKingMoved;//We keep track of whether the kings have moved, so that we know if they can castle
	bool blackKingMoved;
	Move* last;//For en passant.
};

