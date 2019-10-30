#include <iostream>

using namespace std;

int main(int argc, char** argv) {
	long int x = 0xB764F;
	long int a = 0x2CAE9;
	long int b = 0xDA0CF;

	for (int i = 0; i < 10; ++i){
		cout<<b<<" "<<x<<" "<<a;
		x = ((((b * x + a) % 0x7FFFFFFF) / 0x100) % 0x8000) + 0x10000;
		cout<<endl;
		//cout<<x<<endl;
	}

	return 0;
}
