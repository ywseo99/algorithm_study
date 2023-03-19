#define SOLVE

//https://www.acmicpc.net/problem/17611



#if SOLVE 

using System.Drawing;


List<Point> arr = new List<Point>();

string input = Console.ReadLine();
int n = 0;
int.TryParse(input, out n);

for (int i = 0; i < n; i++)
{
    input = Console.ReadLine();
    string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    arr.Add(new Point(int.Parse(words[0]), int.Parse(words[1])));
}

int[] arr_x = new int[1000001];
int[] arr_y = new int[1000001];

Point pt_curr = new Point();
Point pt_prev = new Point(arr.Last().X, arr.Last().Y);
for (int i = 0; i < n; i++)
{
    pt_curr.X = arr[i].X;
    pt_curr.Y = arr[i].Y;

    if (pt_prev.Y != pt_curr.Y)
    {
        // y축으로 이동
        int min_y = Math.Min(pt_prev.Y, pt_curr.Y);
        int max_y = Math.Max(pt_prev.Y, pt_curr.Y);
        for (int k = min_y; k < max_y; k++)
        {
            arr_y[k + 500000]++;
        }
    }
    if (pt_prev.X != pt_curr.X)
    {
        // x축으로 이동
        int min_x = Math.Min(pt_prev.X, pt_curr.X);
        int max_x = Math.Max(pt_prev.X, pt_curr.X);
        for (int k = min_x; k < max_x; k++)
        {
            arr_x[k + 500000]++;
        }
    }
    pt_prev.X = pt_curr.X;
    pt_prev.Y = pt_curr.Y;
}
Console.WriteLine(Math.Max(arr_x.Max(), arr_y.Max()));


#else 


using System.Drawing;


List<Point> arr = new List<Point>();

string input = Console.ReadLine();
int n = 0;
int.TryParse(input, out n);

for (int i = 0; i < n; i++)
{
    input = Console.ReadLine();
    string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

    try
    {
        arr.Add(new Point(int.Parse(words[0]), int.Parse(words[1])));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Console.WriteLine("n: {0}", n);
foreach (Point pt in arr)
{
    Console.WriteLine("{0}", pt);
}

arr.Add(arr[0]);    // 다시 최초 시작점으로 이어져야 하므로

int[] arr_x = new int[1000000];
int[] arr_y = new int[1000000];


Point pt_curr = new Point();
Point pt_prev = new Point();
for (int i = 0; i < n; i++)
{
    pt_curr = arr[i];
    if (i == 0)
    {
        Console.WriteLine("처음이다.");
        pt_prev = arr[i];
        continue;
    }
  

    if (pt_prev.X == pt_curr.X)
    {
        // y축으로 이동
        int min_y = Math.Min(pt_prev.Y, pt_curr.Y);
        int max_y = Math.Max(pt_prev.Y, pt_curr.Y);

        for (int k = min_y; k < max_y; k++)
        {
            arr_y[k + 500000]++;
        }
    }
    else
    {
        // x축으로 이동
        int min_x = Math.Min(pt_prev.X, pt_curr.X);
        int max_x = Math.Max(pt_prev.X, pt_curr.X);

        for (int k = min_x; k < max_x; k++)
        {
            arr_x[k + 500000]++;
        }
    }
    pt_prev = pt_curr;
}

int max_value = 0;
for (int i = 0; i < arr_x.Length; i++)
{
    if (arr_x[i] == 0) continue;
    Console.WriteLine(" x[{0}] : {1}", i - 500000, arr_x[i]);
    if (max_value < arr_x[i])
    {
        max_value = arr_x[i];
        Console.WriteLine("최대값 갱신 {0} -> {1}", max_value, arr_x[i]);
    }
}
for (int i = 0; i < arr_y.Length; i++)
{
    if (arr_y[i] == 0) continue;
    Console.WriteLine(" y[{0}] : {1}", i - 500000, arr_y[i]);
    if (max_value < arr_y[i])
    {
        max_value = arr_y[i];
        Console.WriteLine("최대값 갱신 {0} -> {1}", max_value, arr_y[i]);
    }
}

Console.WriteLine("최대값은 : {0}", max_value);



#endif
