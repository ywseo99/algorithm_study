//#define SOLVE


#if SOLVE 
string? input = Console.ReadLine();
int n = 0;
int.TryParse(input, out n);
int[,] city = new int[n, n];


for (int i = 0; i < n; i++)
{
    input = Console.ReadLine();
    string[] words = input!.Split(" ");
    for (int j = 0; j < words.Length; j++)
    {
        city[i, j] = int.Parse(words[j]);
    }
}


List<int> stack = new List<int>();
int min_cost = 100000000;

bool move(List<int> _stack)
{
    if (_stack.Count >= n)
    {
        int cost = 0;
        int city_from = -1;
        int city_begin = -1;
        int city_to = 0;
        bool invalid_path = false;
        for (int i = 0; i < _stack.Count; i++)
        {
            if (city_begin < 0)
            {
                city_begin = _stack.ElementAt(n - i - 1);
                city_from = city_begin;
                continue;
            }

            city_to = _stack.ElementAt(n - i - 1);

            if (city[city_from, city_to] == 0)
            {
                // 비용이 0인 경우는 갈 수 없는 경로이다.
                invalid_path = true;
            }

            cost += city[city_from, city_to];
            city_from = city_to;
        }

        if (city[city_to, city_begin] == 0)
        {
            invalid_path = true;
        }

        cost += city[city_to, city_begin];
        if (invalid_path == false)
        {
            if (cost <= min_cost)
            {
                min_cost = cost;
            }
        }
        return true;
    }

    for (int col = 0; col < n; col++)
    {
        if (_stack.Contains(col) == true)
        {
            continue;
        }
        _stack.Add(col);
        if (move(_stack) == true)
        {
            _stack.RemoveAt(_stack.Count - 1);
        }
    }
    return true;
}

move(stack);
Console.WriteLine(min_cost);
////Console.WriteLine("{0}", arr_cost.Min());
#else


//input 
/*
4
0 10 15 20
5 0 9 10
6 13 0 12
8 8 9 0
*/

//answer 35


using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
//string input = Console.ReadLine();
//int n = int.Parse(input);
//int[,] city = new int[n, n];


string problem = @"0 10 15 20
5 0 9 10
6 13 0 12
8 8 9 0";

string problem2 = @"0 200 300 700 800
11 0 10 1 2
12 13 0 19 10
30 1 2 0 22
5 2 1 7 0";
// 정답 218


string problem9 = @"0 20 30 70 80 20 20 30 50
11 0 10 1 2 13 12 13 10
12 13 0 19 10 6 7 1 0
30 1 2 0 22 0 0 5 0
5 2 1 7 0 0 0 0 0
0 40 0 0 0 0 12 15 17
0 50 60 0 10 0 0 15 17
0 10 0 0 10 12 0 0 17
0 0 70 10 10 12 12 0 0";
// 정답 79

string problem10 = @"0 20 30 70 80 20 20 30 50 5
11 0 10 1 2 13 12 13 10 2
12 13 0 19 10 6 7 1 0 1
30 1 2 0 22 0 0 5 0 2
5 2 1 7 0 0 0 0 0 1
0 40 0 0 0 0 12 15 17 0
0 50 60 0 10 0 0 15 17 0
0 10 0 0 10 12 0 0 17 0
0 0 70 10 10 12 12 0 0 0
0 0 70 10 10 12 12 0 0 0";
// 정답 76


// 비용이 0인 경우는 갈 수 없는 곳이다.

int n = 10;
int[,] city = new int[n, n];

string[] lines = problem10.Split("\n");
for (int i = 0; i < lines.Length; i++)
{
    string[] words = lines[i].Split(" ");
    for (int j = 0; j < words.Length; j++)
    {
        city[i, j] = int.Parse(words[j]);
    }
}

//for (int i = 0; i < n; i++)
//{
//    input = Console.ReadLine();
//    string[] words = input.Split(" ");
//    for (int j = 0; j < words.Length; j++)
//    {
//        city[i, j] = int.Parse(words[j]);
//    }
//}

Console.WriteLine("City Count (n) : {0}", n);
StringBuilder sb = new StringBuilder();
for (int i = 0; i < city.GetLength(0); i++)
{
    for (int j = 0; j < n; j++)
    {
        sb.AppendFormat("{0,2} ", city[i, j]);
    }
    sb.AppendFormat("\n");
}
Console.WriteLine(sb.ToString());

Dictionary<string, int> answers = new Dictionary<string, int>();
Stack<int> stack = new Stack<int>();

bool move(Stack<int> _stack)
{    
    if (_stack.Count >= n)
    {
        StringBuilder sb = new StringBuilder();
        int cost = 0;

        int city_from = -1;
        int city_begin = -1;
        int city_to = 0;
        bool invalid_path = false;
        for (int i = 0; i < _stack.Count; i++)
        {

            if (city_begin < 0)
            {
                city_begin = _stack.ElementAt(n - i - 1);
                city_from = city_begin;
                sb.AppendFormat("{0},", city_from);
                continue;
            }

            city_to = _stack.ElementAt(n - i - 1);

            if (city[city_from, city_to] == 0)
            {
                // 비용이 0인 경우는 갈 수 없는 경로이다.
                invalid_path = true;
            }

            cost += city[city_from, city_to];
            sb.AppendFormat("{0},", city_to);
            city_from = city_to;
        }

        if (city[city_to, city_begin] == 0)
        {
            invalid_path = true;
        }
        cost += city[city_to, city_begin];

        if (invalid_path == false)
        {
            answers.Add(sb.ToString(), cost);
        }        
        //Console.WriteLine("--- complete. Queue count: {0}, {1}, cost:{2}", _stack.Count, string.Join(",", _stack), cost);

        return true;
    }

    for (int col = 0; col < n; col++)
    {
        if (_stack.Contains(col) == true)
        {
            continue;
        }
        _stack.Push(col);
        //Console.WriteLine("\t ---> Push {0}", col);
        if (move(_stack) == true)
        {
            int result = _stack.Pop();
            //Console.WriteLine("\t <--- Pop {0}", result);
        }
    }
    return true;
}

Stopwatch sw = Stopwatch.StartNew();
int[] arr = new int[n];
move(stack);

string min_value = "";
int min_cost = 1000000;
string max_value = "";
int max_cost = 0;
foreach (var pair in answers)
{
    //Console.WriteLine("key: {0}, cost: {1}", pair.Key, pair.Value);

    if (min_cost > pair.Value)
    {
        min_value = pair.Key;
        min_cost = pair.Value;
    }
    if (max_cost < pair.Value)
    {
        max_value = pair.Key;
        max_cost = pair.Value;
    }
}
//int max_cost = answers.Values.Max();
//int min_cost = answers.Values.Min();
//Console.WriteLine(" max_cost: {0}, min_cost: {1}", max_cost, min_cost);
Console.WriteLine(" max_cost: {0}, {1}", max_cost, max_value);
Console.WriteLine(" min_cost: {0}, {1}", min_cost, min_value);

sw.Stop();
Console.WriteLine(" elapsed {0:N0} ms", sw.ElapsedMilliseconds);

#endif

