﻿// See https://aka.ms/new-console-template for more information
/*
    https://www.acmicpc.net/problem/1193

분수찾기
시간 제한	메모리 제한	제출	정답	맞힌 사람	정답 비율
0.5 초 (추가 시간 없음)	256 MB	97860	48881	42183	51.211%
문제

무한히 큰 배열에 다음과 같이 분수들이 적혀있다.

1/1	1/2	1/3	1/4	1/5	…
2/1	2/2	2/3	2/4	…	…
3/1	3/2	3/3	…	…	…
4/1	4/2	…	…	…	…
5/1	…	…	…	…	…
…	…	…	…	…	…
이와 같이 나열된 분수들을 1/1 → 1/2 → 2/1 → 3/1 → 2/2 → … 과 같은 지그재그 순서로 차례대로 1번, 2번, 3번, 4번, 5번, … 분수라고 하자.
X가 주어졌을 때, X번째 분수를 구하는 프로그램을 작성하시오.

입력
    첫째 줄에 X(1 ≤ X ≤ 10,000,000)가 주어진다.

출력
    첫째 줄에 분수를 출력한다.

예제 입력 1 
1
예제 출력 1 
1/1

예제 입력 2 
2
예제 출력 2 
1/2

예제 입력 3 
3
예제 출력 3 
2/1

예제 입력 4 
4
예제 출력 4 
3/1

예제 입력 5 
5
예제 출력 5 
2/2

예제 입력 6 
6
예제 출력 6 
1/3

예제 입력 7 
7
예제 출력 7 
1/4

예제 입력 8 
8
예제 출력 8 
2/3

예제 입력 9 
9
예제 출력 9 
3/2

예제 입력 10 
14
예제 출력 10 
2/4

*/


string input = Console.ReadLine();
int x = 0;
int.TryParse(input, out x);
int n = 0;
int index = 0;  // 진행한 분수 개수 
string answer = string.Empty;
while (true)
{
    n++;
    bool is_ne = (n % 2 == 1);  // 짝수라면 남서방향(SW). 홀수라면 북동방향(NE)
    for (int i = 0; i < n; i++)
    {
        int row = 0;
        int col = 0;
        if (is_ne)
        {
            row = n - i;
            col = 1 + i;
        }
        else
        {
            row = 1 + i;
            col = n - i;
        }
        index++;
        if (index == x)
        {
            answer = string.Format("{0}/{1}", row, col);
            goto END_OF_SOLVE;   
        }
    }
}
END_OF_SOLVE:
Console.WriteLine(answer);