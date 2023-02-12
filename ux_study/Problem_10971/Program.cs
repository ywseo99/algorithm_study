// See https://aka.ms/new-console-template for more information




//input 
/*
4
0 10 15 20
5 0 9 10
6 13 0 12
8 8 9 0
*/

//answer 35

/*
using System.Collections.Concurrent;
using System.Text;
//string input = Console.ReadLine();
//int n = int.Parse(input);
//int[,] city = new int[n, n];


string problem = @"0 10 15 20
5 0 9 10
6 13 0 12
8 8 9 0";


int n = 4;
int[,] city = new int[n, n];

string[] lines = problem.Split("\n");
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

n = 4;


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
        Console.WriteLine("--- complete. Queue count: {0}, {1}", _stack.Count, string.Join(",", _stack));

        StringBuilder sb = new StringBuilder();
        int cost = 0;

        int city_from = -1;
        int city_begin = -1;
        int city_to = 0;
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
            cost += city[city_from, city_to];
            sb.AppendFormat("{0},", city_to);
            city_from = city_to;
        }

        cost += city[city_to, city_begin];
        answers.Add(sb.ToString(), cost);

        return true;
    }

    for (int col = 0; col < n; col++)
    {
        if (_stack.Contains(col) == true)
        {
            continue;
        }

        _stack.Push(col);
        Console.WriteLine("\t ---> Push {0}", col);
        if (move(_stack) == true)
        {

            int result = _stack.Pop();
            Console.WriteLine("\t <--- Pop {0}", result);
        }
    }    
    return true;
}


int[] arr = new int[n];
move(stack);


foreach (var pair in answers)
{
    Console.WriteLine("key: {0}, cost: {1}", pair.Key, pair.Value);        
    
}


int max_cost = answers.Values.Max();
int min_cost = answers.Values.Min();

Console.WriteLine(" max_cost: {0}, min_cost: {1}", max_cost, min_cost);
*/



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

//List<int> arr_cost = new List<int>();
List<int> stack = new List<int>();

int min_cost = 10000000;

bool move(List<int> _stack)
{
    if (_stack.Count >= n)
    {
        int cost = 0;
        int city_from = -1;
        int city_begin = -1;
        int city_to = 0;
        for (int i = 0; i < _stack.Count; i++)
        {
            if (city_begin < 0)
            {
                city_begin = _stack.ElementAt(n - i - 1);
                city_from = city_begin;
                continue;
            }

            city_to = _stack.ElementAt(n - i - 1);
            cost += city[city_from, city_to];
            city_from = city_to;
        }

        cost += city[city_to, city_begin];
        //arr_cost.Add(cost);
        if (min_cost > cost)
        {
            min_cost = cost;
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
            _stack.RemoveAt(_stack.Count-1);
        }
    }
    return true;
}

move(stack);
Console.WriteLine(min_cost);
//Console.WriteLine("{0}", arr_cost.Min());