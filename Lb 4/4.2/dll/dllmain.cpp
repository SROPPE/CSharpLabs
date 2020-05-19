
#include "pch.h"
#include <utility>
#include <limits.h>
#include <cstdint>

static int previous;
static int  current;
static int  index;

extern "C" __declspec(dllexport) 
void FibonacciInit(
	int  a,
	int b)
{
	index = 0;
	current = a;
	previous = b;
}
extern "C" __declspec(dllexport)
int FibonacciNext()
{
	if (index > 40)
	{
		return 0;
	}

	if (index > 0)
	{
		previous += current;
	}
	std::swap(current, previous);
	index++;
	return 1;
}
extern "C" __declspec(dllexport)
int __stdcall FibonacciCurrent()
{
	return current;
}
extern "C" __declspec(dllexport)
int __stdcall  FibonacciIndex()
{
	return index;
}
BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

