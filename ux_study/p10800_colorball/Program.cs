//#define SOLVE
//#define FAIL_CASE
//#define MEMORY_OVER

/*
문제
지훈이가 최근에 즐기는 컴퓨터 게임이 있다. 
이 게임은 여러 플레이어가 참여하며, 각 플레이어는 특정한 색과 크기를 가진 자기 공 하나를 조종하여 게임에 참여한다. 
각 플레이어의 목표는 자기 공보다 크기가 작고 색이 다른 공을 사로잡아 그 공의 크기만큼의 점수를 얻는 것이다. 
그리고 다른 공을 사로잡은 이후에도 본인의 공의 색과 크기는 변하지 않는다. 
다음 예제는 네 개의 공이 있다. 
편의상 색은 숫자로 표현한다.

공 번호	색	크기
1	1	10
2	3	15
3	1	3
4	4	8
이 경우, 2번 공은 다른 모든 공을 사로잡을 수 있다. 
반면, 1번 공은 크기가 더 큰 2번 공과 색이 같은 3번 공은 잡을 수 없으며, 
단지 4번 공만 잡을 수 있다. 

공들의 색과 크기가 주어졌을 때, 각 플레이어가 사로잡을 수 있는 모든 공들의 크기의 합을 출력하는 프로그램을 작성하시오. 

입력
첫 줄에는 공의 개수를 나타내는 자연수 N이 주어진다(1 ≤ N ≤ 200,000). 
다음 N개의 줄 중 i번째 줄에는 i번째 공의 색을 나타내는 자연수 Ci와 그 크기를 나타내는 자연수 Si가 주어진다(1 ≤ Ci ≤ N, 1 ≤ Si ≤ 2,000). 
서로 같은 크기 혹은 같은 색의 공들이 있을 수 있다.

출력
N개의 줄을 출력한다. 
N개의 줄 중 i번째 줄에는 i번째 공을 가진 플레이어가 잡을 수 있는 모든 공들의 크기 합을 출력한다.

예제 입력 1 
    4
    1 10
    3 15
    1 3
    4 8
예제 출력 1 
    8
    21
    0
    3
예제 입력 2 
    3
    2 3
    2 5
    2 4
예제 출력 2 
    0
    0
    0
*/


#if SOLVE

using System.Text;

int n = 0;
string input = Console.ReadLine();
int.TryParse(input, out n);

Dictionary<UInt16, UInt16[]> dict = new Dictionary<UInt16, UInt16[]>();
int[] areas_sum = new int[2001];
int[] players = new int[n];



for (int i = 0; i < n; i++)
{
    input = Console.ReadLine();
    string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    
    int color = int.Parse(words[0]);
    UInt16 area = UInt16.Parse(words[1]);

    players[i] = (area << 20);
    players[i] |= color;


    if (dict.ContainsKey(area) == false)
    {
        dict.Add(area, new UInt16[n + 1]);
    }
    dict[area][color] += 1;
    areas_sum[area] += area;   // 각 면적별로 합계 구하기
}

StringBuilder sb = new StringBuilder();
for (int i = 0; i < n; i++)
{
    int color = (0x000fffff & players[i]);
    int area = (players[i] >> 20);

    int sum = 0;
    for (UInt16 a = 1; a < area; a++)
    {
        sum += areas_sum[a];
        if (dict.ContainsKey(a) == true)
        {
            sum -= dict[a][color] * a;
        }
    }
    sb.AppendFormat("{0}\n", sum);
}
Console.WriteLine(sb.ToString());


#elif FAIL_CASE


// n은 최대 1~200000개
// area는 1~2000

using System.Diagnostics;
using System.Text;

int n = 20000;
int[,] players = new int[n, 2];
for (int i = 0; i < n; i++)
{
    Random rand = new Random();
    int color = rand.Next(1, n);
    Random rand2 = new Random();
    int area = rand2.Next(1, 2000);

    Console.WriteLine("player {0,3}  color:{1,5}, area:{2,4}", i, color, area);
    players[i, 0] = color;
    players[i, 1] = area;
}


//int n = 0;
//string input = Console.ReadLine();
//int.TryParse(input, out n);
//int[,] players = new int[n, 2];
//for (int i = 0; i < n; i++)
//{
//    input = Console.ReadLine();
//    string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
//    players[i, 0] = int.Parse(words[0]);
//    players[i, 1] = int.Parse(words[1]);
//}

// 플레이어 하나씩 계산한다
Stopwatch sw = Stopwatch.StartNew();
StringBuilder sb = new StringBuilder();
for (int i = 0; i < n; i++)
{
    int color = players[i, 0];
    int area = players[i, 1];
    //Console.WriteLine("player {0}, color: {1}, area: {2}", i, color, area);

    // 자신을 제외한 나머지 플레이어들과 비교한다
    int area_sum = 0;
    for (int j = 0; j < n; j++)
    {
        if (i == j) continue;   // 자기 자신 제외
        if (color == players[j, 0]) continue;   // 같은 색 제외
        if (area <= players[j, 1]) continue;    // 크기가 크거나 같은 것 제외

        area_sum += players[j, 1];
    }

    //sb.AppendFormat("{0}\n", area_sum);
}
//Console.WriteLine(sb.ToString());

sw.Stop();
Console.Write("---> n:{0}, elapsed {1:N0} ms", n, sw.ElapsedMilliseconds);

// 20000개만 해도 10초이상 ㅠㅠ

#elif MEMORY_OVER


// n은 최대 1~200000개
// area는 1~2000

using System.Diagnostics;
using System.Text;

int n = 200000;


//Dictionary<int, int[]> dict = new Dictionary<int, int[]>();


UInt16[,] areas = new UInt16[2001, n + 1];
int[] areas_sum = new int[2001];
int[] players = new int[n];

Console.WriteLine("areas size: {0:N0} kb", Buffer.ByteLength(areas) / 1024);
Console.WriteLine("areas_sum size: {0}", Buffer.ByteLength(areas_sum));
Console.WriteLine("players size: {0:N0} kb",  Buffer.ByteLength(players) / 1024);


for (int i = 0; i < n; i++)
{
    Random rand = new Random();
    int color = rand.Next(1, n);
    Random rand2 = new Random();
    int area = rand2.Next(1, 2000);

    //Console.WriteLine("player {0,3}  color:{1,5}, area:{2,4}", i, color, area);
    players[i] = (area << 20);
    players[i] |= color;


    //if (dict.ContainsKey(area) == false)
    //{
    //    dict.Add(area, new int[n+1]);
    //}
    //dict[area][color] += 1;
    //areas[area, color] += 1;
    areas_sum[area] += area;   // 각 면적별로 합계 구하기
}


//for (int i = 0; i < n; i++)
//{
//    Random rand = new Random();
//    int color = rand.Next(1, n);
//    Random rand2 = new Random();
//    int area = rand2.Next(1, 2000);

//    Console.WriteLine("player {0,3}  color:{1,5}, area:{2,4}", i, color, area);
//    players[i, 0] = color;
//    players[i, 1] = area;

//    areas[area, color] += 1;
//    areas_sum[area] += area;   // 각 면적별로 합계 구하기
//}


// 면적별로 면적의 합계 구하기
//for (int i = 0; i < 2000; i++)
//{
//    Console.WriteLine("area:{0,4:N0}, sum: {1,8:N0}", i, areas_sum[i]);
//}

Stopwatch sw = Stopwatch.StartNew();
StringBuilder sb = new StringBuilder();
for (int i = 0; i < n; i++)
{
    int color = (0x000fffff & players[i]);
    int area = (players[i] >> 20);


    // 크기가 작고, 색이 다른 모든 플레이어의 면적
    int sum = 0;
    for (int a = 1; a < area; a++)
    {
        sum += areas_sum[a];

        // 같은 색은 빼야함
        sum -= areas[a, color] * a;

        //if (dict.ContainsKey(a) == true)
        //{
        //    sum -= dict[a][color] * a;
        //}
    }
    sb.AppendFormat("{0}\n", sum);
}
Console.WriteLine(sb.ToString());

sw.Stop();
Console.Write("answer  n:{0}, elapsed {1:N0} ms", n, sw.ElapsedMilliseconds);

return;

//int n = 0;
//string input = Console.ReadLine();
//int.TryParse(input, out n);
//int[,] players = new int[n, 2];
//for (int i = 0; i < n; i++)
//{
//    input = Console.ReadLine();
//    string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
//    players[i, 0] = int.Parse(words[0]);
//    players[i, 1] = int.Parse(words[1]);
//}

// 플레이어 하나씩 계산한다
sw = Stopwatch.StartNew();
sb = new StringBuilder("\n\n");
for (int i = 0; i < n; i++)
{
    int color = (0x000fffff & players[i]);
    int area = (players[i] >> 20);
    //Console.WriteLine("player {0}, color: {1}, area: {2}", i, color, area);

    // 자신을 제외한 나머지 플레이어들과 비교한다
    int area_sum = 0;
    for (int j = 0; j < n; j++)
    {
        int player_color = (0x000fffff & players[j]);
        int player_area = (players[j] >> 20);

        if (i == j) continue;   // 자기 자신 제외
        if (color == player_color) continue;   // 같은 색 제외
        if (area <= player_area) continue;    // 크기가 크거나 같은 것 제외

        area_sum += player_area;
    }

    sb.AppendFormat("{0}\n", area_sum);
}
Console.WriteLine(sb.ToString());

sw.Stop();
Console.Write("---> n:{0}, elapsed {1:N0} ms", n, sw.ElapsedMilliseconds);

#else 


// n은 최대 1~200000개
// area는 1~2000

using System.Diagnostics;
using System.Text;

int n = 10000;


Dictionary<int, ushort> dict = new Dictionary<int, ushort>();
// key: area + color, value: count 


//UInt16[,] areas = new UInt16[2001, n + 1];
int[] areas_sum = new int[2001];
int[] players = new int[n];

//Console.WriteLine("areas size: {0:N0} kb", Buffer.ByteLength(areas) / 1024);
Console.WriteLine("areas_sum size: {0}", Buffer.ByteLength(areas_sum));
Console.WriteLine("players size: {0:N0} kb", Buffer.ByteLength(players) / 1024);


for (int i = 0; i < n; i++)
{
    Random rand = new Random();
    int color = rand.Next(1, n);
    Random rand2 = new Random();
    int area = rand2.Next(1, 2000);

    //Console.WriteLine("player {0,3}  color:{1,5}, area:{2,4}", i, color, area);
    players[i] = (area << 20);
    players[i] |= color;

    if (dict.ContainsKey(players[i]) == false)
    {
        dict.Add(players[i], 0);
    }
    dict[players[i]] += 1;
    areas_sum[area] += area;   // 각 면적별로 합계 구하기
}


//for (int i = 0; i < n; i++)
//{
//    Random rand = new Random();
//    int color = rand.Next(1, n);
//    Random rand2 = new Random();
//    int area = rand2.Next(1, 2000);

//    Console.WriteLine("player {0,3}  color:{1,5}, area:{2,4}", i, color, area);
//    players[i, 0] = color;
//    players[i, 1] = area;

//    areas[area, color] += 1;
//    areas_sum[area] += area;   // 각 면적별로 합계 구하기
//}


//면적별로 면적의 합계 구하기
for (int i = 0; i < 2000; i++)
{
    Console.WriteLine("area:{0,4:N0}, sum: {1,8:N0}", i, areas_sum[i]);
}

Stopwatch sw = Stopwatch.StartNew();
StringBuilder sb = new StringBuilder();
for (int i = 0; i < n; i++)
{
    int color = (0x000fffff & players[i]);
    int area = (players[i] >> 20);


    // 크기가 작고, 색이 다른 모든 플레이어의 면적
    int sum = 0;
    for (int a = 1; a < area; a++)
    {
        sum += areas_sum[a];

        int cnt = 0;
        foreach (KeyValuePair<int, ushort> pair in dict) 
        { 
            int other_color = (0x000fffff & pair.Key);
            int other_area = (pair.Key >> 20);

            if (color == other_color &&
                other_area == a)
            {
                sum -= other_area * pair.Value;
                Console.WriteLine(" subtract {0}, cnt: {1}", other_area, cnt);
            }
            cnt++;

        }

        // 같은 색은 빼야함
        //sum -= areas[a, color] * a;

        //if (dict.ContainsKey(a) == true)
        //{
        //    sum -= dict[a][color] * a;
        //}
    }
    sb.AppendFormat("{0}\n", sum);
}
Console.WriteLine(sb.ToString());

sw.Stop();
Console.Write("answer  n:{0}, elapsed {1:N0} ms", n, sw.ElapsedMilliseconds);


//int n = 0;
//string input = Console.ReadLine();
//int.TryParse(input, out n);
//int[,] players = new int[n, 2];
//for (int i = 0; i < n; i++)
//{
//    input = Console.ReadLine();
//    string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
//    players[i, 0] = int.Parse(words[0]);
//    players[i, 1] = int.Parse(words[1]);
//}

// 플레이어 하나씩 계산한다
sw = Stopwatch.StartNew();
sb = new StringBuilder("\n\n");
for (int i = 0; i < n; i++)
{
    int color = (0x000fffff & players[i]);
    int area = (players[i] >> 20);
    //Console.WriteLine("player {0}, color: {1}, area: {2}", i, color, area);

    // 자신을 제외한 나머지 플레이어들과 비교한다
    int area_sum = 0;
    for (int j = 0; j < n; j++)
    {
        int player_color = (0x000fffff & players[j]);
        int player_area = (players[j] >> 20);

        if (i == j) continue;   // 자기 자신 제외
        if (color == player_color) continue;   // 같은 색 제외
        if (area <= player_area) continue;    // 크기가 크거나 같은 것 제외

        area_sum += player_area;
    }

    sb.AppendFormat("{0}\n", area_sum);
}
Console.WriteLine(sb.ToString());

sw.Stop();
Console.Write("---> n:{0}, elapsed {1:N0} ms", n, sw.ElapsedMilliseconds);

Console.WriteLine(" dict size: {0:N0} kb", Buffer.ByteLength(dict.ToArray()) / 1024);
#endif