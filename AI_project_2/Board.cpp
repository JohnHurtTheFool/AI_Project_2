#include "Board.h"


Board::Board(void)
{
}


Board::~Board(void)
{
}
bool Board::isLegal(Move m)
{
	//Please add me.
	return true;//Placeholder
}
Board Board::result (Move m)
{
	//Please add me.
	return *this;//Placeholder
}
int Board::materialCount()
{
	int score;
	for(int i = 0; i < NUM_RANKS; i++)
		for(int j = 0; j < NUM_FILES; j++)
			if(!squares[i][j].empty)
			{
				if(squares[i][j].white)
				{
					if(squares[i][j].piece==Q)
						score+=Q_V;
					else if(squares[i][j].piece==R)
						score+=R_V;
					else if(squares[i][j].piece==B)
						score+=B_V;
					else if(squares[i][j].piece==N)
						score+=N_V;
					else if(squares[i][j].piece==P)
						score+=P_V;
				}
				else
				{
				}
			}
	//Please add me.
	return 0;//Placeholder
}
int weightedCount()
{
	//Please add me
	return 0;//Placeholder
}