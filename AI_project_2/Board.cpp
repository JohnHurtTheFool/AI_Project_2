#include "Board.h"
using std::vector;

Board::Board(void)
{
	Move::square A1 = {0,0};
	whiteKingMoved = false;
	blackKingMoved = false;
	last = Move(A1,A1);

	//all the empty squares
	content empty = {true,true,P};
	for(int i = 0; i < NUM_FILES; i++)
		for(int j = 3; j < 7; j++)
			squares[i][j] = empty;

	//Pawns
	content whitePawn = {false,true,P};
	content blackPawn = {false,false,P};
	for(int i = 0; i < NUM_FILES; i++)
			squares[i][1] = whitePawn;
	for(int i = 0; i < NUM_FILES; i++)
			squares[i][NUM_RANKS-2] = blackPawn;
	//Back ranks.
	content a1 = {false,true,R};
	content b1 = {false,true,N};
	content c1 = {false,true,B};
	content d1 = {false,true,Q};
	content e1 = {false,true,K};
	content f1 = {false,true,B};
	content g1 = {false,true,N};
	content h1 = {false,true,R};
	content a8 = {false,false,R};
	content b8 = {false,false,N};
	content c8 = {false,false,B};
	content d8 = {false,false,Q};
	content e8 = {false,false,K};
	content f8 = {false,false,B};
	content g8 = {false,false,N};
	content h8 = {false,false,R};
	squares[0][0]= a1;
	squares[1][0]= b1;
	squares[2][0]= c1;
	squares[3][0]= d1;
	squares[4][0]= e1;
	squares[5][0]= f1;
	squares[6][0]= g1;
	squares[7][0]= h1;
	squares[0][NUM_RANKS-1]= a8;
	squares[1][NUM_RANKS-1]= b8;
	squares[2][NUM_RANKS-1]= c8;
	squares[3][NUM_RANKS-1]= d8;
	squares[4][NUM_RANKS-1]= e8;
	squares[5][NUM_RANKS-1]= f8;
	squares[6][NUM_RANKS-1]= g8;
	squares[7][NUM_RANKS-1]= h8;
}


Board::~Board(void)
{
}
bool Board::isLegal(Move m)
{
	int file = m.getTo().file;
	int rank = m.getTo().rank;
	if (squares[file][rank].empty) return true;
	else return false;
}
bool Board::isMated()
{
	//Please add me.
	return true;//Placeholder
}
Board Board::result (Move m)
{
	Board b = *this;
	if( squares[m.getFrom().file][m.getFrom().rank].piece == K && (abs(m.getTo().rank-m.getFrom().rank) == 2) )//If castling
	{
		if(squares[m.getFrom().file][m.getFrom().rank].white)//If king is moving, it can no longer castle.
			b.whiteKingMoved = true;
		else
			b.blackKingMoved = true;
		//Please add the rest of me.
		if (squares[m.getTo().file][m.getTo().rank + 1].piece == R) {
			// If the rook is in the correct position
			//squares[m.getTo().file][m.getTo().rank + 1].piece = ;	// Not sure how to clear a square's piece
			squares[m.getTo().file][m.getTo().rank + 1].empty = true;
			//squares[m.getFrom().file][m.getFrom().rank].piece = ;	// Same ^
			squares[m.getFrom().file][m.getFrom().rank].empty = true;

			// Moves the King to the left 2 spaces
			squares[m.getFrom().file][m.getFrom().rank + 1].piece = R;
			squares[m.getFrom().file][m.getFrom().rank + 1].empty = false;
			if (b.whiteKingMoved)
				squares[m.getFrom().file][m.getFrom().rank].white = true;
			else
				squares[m.getFrom().file][m.getFrom().rank].white = false;
			
			// Moves the Rook to the right 2 spaces
			squares[m.getTo().file][m.getTo().rank - 1].piece = K;
			squares[m.getTo().file][m.getTo().rank - 1].empty = false;
			if (b.whiteKingMoved)
				squares[m.getTo().file][m.getTo().rank - 1].white = true;
			else
				squares[m.getTo().file][m.getTo().rank - 1].white = false;

		}
	}
	else if(false)//Please add condition and code for en passant
	{
	}
	else
	{
		if(squares[m.getFrom().file][m.getFrom().rank].piece == K)//If king is moving, it can no longer castle.
		{
			if(squares[m.getFrom().file][m.getFrom().rank].white)
				b.whiteKingMoved = true;
			else
				b.blackKingMoved = true;
		}

		b.squares[m.getTo().file][m.getTo().rank]=squares[m.getFrom().file][m.getFrom().rank];
	}
	last = m;
	return b;//Placeholder
}
int Board::materialCount()
{
	double score = 0;
	for(int i = 0; i < NUM_FILES; i++)
		for(int j = 0; j < NUM_RANKS; j++)
			if(!squares[i][j].empty)//If there is a piece.
			{
				if(squares[i][j].white)//If the piece is white.
				{
					if(squares[i][j].piece==Q)
						score+=boardNS::Q_V;
					else if(squares[i][j].piece==R)
						score+=boardNS::R_V;
					else if(squares[i][j].piece==B)
						score+=boardNS::B_V;
					else if(squares[i][j].piece==N)
						score+=boardNS::N_V;
					else if(squares[i][j].piece==P)
						score+=boardNS::P_V;
				}
				else
				{
					if(squares[i][j].piece==Q)
						score-=boardNS::Q_V;
					else if(squares[i][j].piece==R)
						score-=boardNS::R_V;
					else if(squares[i][j].piece==B)
						score-=boardNS::B_V;
					else if(squares[i][j].piece==N)
						score-=boardNS::N_V;
					else if(squares[i][j].piece==P)
						score-=boardNS::P_V;
				}
			}
	return score;
}
int Board::weightedCount()
{
	//Please add me
	return 0;//Placeholder
}
std::vector<Move> Board::getLegal()
{
	vector<Move> legal;
	//Please add me
	return legal;
}
bool Board::isCheck()
{
	//Please add me
	return true;//Placeholder
}
bool Board::isCheck(int file, int rank)
{
	//Please add me
	return true;//Placeholder
}