#include <iostream>
#include <string>
#include "Move.h"
#include "Board.h"
using std::cout;
using std::cin;
void printBoard(Board& b);
int main()
{
	std::string moveDescription;
	Move::square s={0,0};//Test code.
	Move::square s2={0,0};
	cin >> moveDescription;
	Move::square s3 = {moveDescription[0]-48,(int)moveDescription[1]-48};
	Move::square s4 = {moveDescription[2]-48,(int)moveDescription[3]-48};
	Move m(s,s2);
	Move m2(s3,s4);
	Board b;
	printBoard(b);
	if(b.result(m2).isLegal(m2))
		b = b.result(m2);
	cout << "\n";
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
