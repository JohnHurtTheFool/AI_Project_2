#include <iostream>
#include "Move.h"
#include "Board.h"
using std::cout;
void printBoard(Board& b);
int main()
{
	Move::square s={4,4};//Test code.
	Move::square s2={3,5};
	Move m(s,s2);
	Board b;
	printBoard(b);
	cout << "\n" << b.materialCount();
	return 0;
}
void printBoard(Board& b)
{
	for(int i = Board::NUM_FILES-1; i >=0 ; i--)//Since we are printing the board from White's point of view.
	{
		for(int j = 0; j < Board::NUM_RANKS; j++)
		{
			Board::content c;
			c = b.getSquare(j,i);
			if(c.empty)//If the square is empty
				cout << "__ ";
			else//If the square has a piece, print it.
			{
				if(c.white)
					cout << "w";
				else
					cout << "b";
				switch(c.piece)
				{
				case Board::K:
					cout << "K";
					break;
				case Board::Q:
					cout << "Q";
					break;
				case Board::R:
					cout << "R";
					break;
				case Board::B:
					cout << "B";
					break;
				case Board::N:
					cout << "N";
					break;
				case Board::P:
					cout << "P";
					break;
				}
				cout << " ";
			}
		}
		cout << "\n";
	}
			
}
