#pragma once
#pragma once

#ifdef MATHDLL_EXPORTS
#define MATHDLL_API __declspec(dllexport)
#else
#define MATHDLL_API __declspec(dllimport)
#endif

extern "C" MATHDLL_API void FibonacciInit(
	int  a, int  b);

extern "C" MATHDLL_API bool FibonacciNext();

extern "C" MATHDLL_API unsigned long long FibonacciCurrent();


extern "C" MATHDLL_API unsigned  FibonacciIndex();