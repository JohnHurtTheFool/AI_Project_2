#pragma once
class Move
{
public:
	struct square
	{
		int rank;
		int file;//1 cooresponds to a, 2 to be etc.
	};
	Move(void){}
	Move(square f, square t){from=f;to=t;}
	square getFrom(){return from;}
	square getTo(){return to;}
	~Move(void);
private:
	square from;
	square to;
};

