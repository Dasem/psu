#include <iostream>
using namespace std;

int main()
{
    //std::cout << "Hello World!\n";
	//long int x = 0xDA0CF;
	//long int a = 0xB764F;
	//long int b = 0x2CAE9;
	long long int x = 0xB764F;
	long long int a = 0x2CAE9;
	long long int b = 0xDA0CF;

	for (int i = 0; i < 10; i++)
	{
		//x = (a * x + b);
		//x = ((x / 0x100) % 0x8000) + 65536;
		x = ((((b * x + a) % 0x7FFFFFFF) / 0x100) % 0x8000) + 0x10000;
		cout << x << endl;
	}
}
