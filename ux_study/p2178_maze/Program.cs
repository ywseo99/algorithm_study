#define SOLVE
//#define RECURSIVE

/*
N×M크기의 배열로 표현되는 미로가 있다.

    1	0	1	1	1	1
    1	0	1	0	1	0
    1	0	1	0	1	1
    1	1	1	0	1	1
미로에서 1은 이동할 수 있는 칸을 나타내고, 0은 이동할 수 없는 칸을 나타낸다. 
이러한 미로가 주어졌을 때, (1, 1)에서 출발하여 (N, M)의 위치로 이동할 때 지나야 하는 최소의 칸 수를 구하는 프로그램을 작성하시오. 
한 칸에서 다른 칸으로 이동할 때, 서로 인접한 칸으로만 이동할 수 있다.

위의 예에서는 15칸을 지나야 (N, M)의 위치로 이동할 수 있다. 
칸을 셀 때에는 시작 위치와 도착 위치도 포함한다.

입력
    첫째 줄에 두 정수 N, M(2 ≤ N, M ≤ 100)이 주어진다. 
    다음 N개의 줄에는 M개의 정수로 미로가 주어진다. 
    각각의 수들은 붙어서 입력으로 주어진다.

출력
    첫째 줄에 지나야 하는 최소의 칸 수를 출력한다. 항상 도착위치로 이동할 수 있는 경우만 입력으로 주어진다.

예제 입력 1 
    4 6
    101111
    101010
    101011
    111011
예제 출력 1 
    15

예제 입력 2 
    4 6
    110110
    110110
    111111
    111101
예제 출력 2 
    9

예제 입력 3 
    2 25
    1011101110111011101110111
    1110111011101110111011101
예제 출력 3 
    38
예제 입력 4 
    7 7
    1011111
    1110001
    1000001
    1000001
    1000001
    1000001
    1111111
예제 출력 4 
    13

모든 경로(열려진 방향으로)로 진행을 시도한다. 
끝까지 가본 경우가 생긴다면 이를 최소값으로 삼고, 
다음 시도부터 이 최소값보다 커지는 경우는 중도에 포기한다. 
이전에 찾았던 최소값보다 작은 값이 나올경우 최소값을 갱신한다.


*/

#if SOLVE

using System.Collections.Concurrent;
using System.Drawing;


int min_cost = 1000000;
string input = Console.ReadLine();
string[] mn = input.Split(" ", StringSplitOptions.TrimEntries);
int m = int.Parse(mn[0]);
int n = int.Parse(mn[1]);

// m번의 입력 라인 받기
int[,] pathmap = new int[m, n];
int[,] maze = new int[m, n];
for (int row = 0; row < m; row++)
{
    string line = Console.ReadLine();
    for (int c = 0; c < line.Trim().Length; c++)
    {
        if (line[c] == '1')
            maze[row, c] = 1;
        else
            maze[row, c] = 0;
    }
}


bool is_valid_cell(int row, int col)
{
    if (row < 0 || row >= maze.GetLength(0))
        return false;
    if (col < 0 || col >= maze.GetLength(1))
        return false;

    return true;
}


void move()
{
    int cost = 0;
    
    int curr_row = 0;
    int curr_col = 0;
    int next_row = 0;
    int next_col = 0;

    // 9시방향부터 시계반대방향으로 0, 1, 2, 3
    int[,] dir = new int[2, 4] { { 0, 1, 0, -1 }, { -1, 0, 1, 0 } };

    ConcurrentQueue<Point> queue = new ConcurrentQueue<Point>();
    queue.Enqueue(new Point(curr_col, curr_row));
    pathmap[curr_row, curr_col] = 1;

    while (queue.IsEmpty == false)
    {
       
        Point pt;        
        queue.TryDequeue(out pt);
        curr_row = pt.Y;
        curr_col = pt.X;

        if ((curr_row == m - 1) &&
            (curr_col == n - 1))
        {
            cost = pathmap[curr_row, curr_col];
            if (cost < min_cost)
            {
                min_cost = cost;
            }
        }

        //Console.WriteLine(" curr_row: {0}, curr_col: {1}", curr_row, curr_col);
        for (int i = 0; i < 4; i++)
        {
            // 진행이 가능한 방향만 Queue에 넣는다.
            next_row = curr_row + dir[0, i];
            next_col = curr_col + dir[1, i];

            // 벽으로 막힌길
            // 갈수 없는 길
            // 이미 가본 길

            if (is_valid_cell(next_row, next_col) == false) continue;
            if (maze[next_row, next_col] != 1) continue;
            if (pathmap[next_row, next_col] != 0) continue;

            pathmap[next_row, next_col] = pathmap[curr_row, curr_col] + 1;
            queue.Enqueue(new Point(next_col, next_row));
        }
    }
}

move();
Console.WriteLine(min_cost);

#elif RECURSIVE

using System.Diagnostics;
using System.Drawing;
using System.Text;

int min_cost = 1000000;

//int m = 4;
//int n = 6;

//string str_maze = @"101111
//101010
//101011
//111011";

//int m = 8;
//int n = 8;
//string str_maze = @"10111111
//11111011
//10010011
//10010011
//10110011
//10100011
//10100011
//11111111";


int m = 120;
int n = 25;
string str_maze = @"1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101
1011101110111011101110111
1110111011101110111011101";


string[] lines = str_maze.Split("\n", StringSplitOptions.TrimEntries);
int[,] maze = new int[m, n];


for (int row = 0; row < lines.Length; row++)
{
    string line = lines[row];

    for (int c = 0; c < line.Length; c++)
    {
        if (line[c] == '1')
            maze[row, c] = 1;
        else
            maze[row, c] = 0;

    }
}


void print_maze(int[,] _maze)
{
    StringBuilder sb = new StringBuilder();
    int _m = _maze.GetLength(0);
    int _n = _maze.GetLength(1);

    sb.AppendFormat(" m: {0}, n: {1} \n", _m, _n);
    for (int row = 0; row < _m; row++)
    {
        for (int col = 0; col < _n; col++)
        {
            sb.AppendFormat("{0,2:x} ", _maze[row, col]);
        }
        sb.AppendFormat("\n");
    }
    Console.WriteLine(sb.ToString());
}


bool move(Point pt, int[,] _maze, Stack<Point> _arr_pt)
{
    int _m = _maze.GetLength(0);
    int _n = _maze.GetLength(1);

    // 만약 비용이 이미 최소값을 초과한 경우 탐색을 포기한다.
    if (_arr_pt.Count >= min_cost)
    {
        //Console.WriteLine("탐색포기. 비용쵸과 min_cost:{0}", min_cost);        
        return false;
    }

    _arr_pt.Push(pt);

    //Console.WriteLine("move [{0},{1}]", pt.Y, pt.Y);
    if (pt.X == _n - 1 && 
        pt.Y == _m - 1)
    {
        //Console.WriteLine("--- Found destination ----");
        //Console.WriteLine(" path: {0}", string.Join(",", _arr_pt));
        //Console.WriteLine(" cost: {0}", _arr_pt.Count);
        _arr_pt.Pop();

        // 최소값 갱신
        if (_arr_pt.Count < min_cost)
        {
            Console.WriteLine("최소값 갱신: {0} -> {1}", min_cost, _arr_pt.Count);
            min_cost = _arr_pt.Count;
         
        }
        return true;
    }

    // 진행할 수 있는 셀들을 찾는다.    
    {
        // move up
        Point next_pt = new Point(pt.X, pt.Y - 1);        
        if (next_pt.Y >= 0 &&
            _maze[next_pt.Y, next_pt.X] != 0 &&
            _arr_pt.Contains(next_pt) == false)
        {            
            move(next_pt, _maze, _arr_pt);            
        }
    }
    {
        // move down
        Point next_pt = new Point(pt.X, pt.Y + 1);
        if (next_pt.Y < _m &&
            _maze[next_pt.Y, next_pt.X] != 0 &&
            _arr_pt.Contains(next_pt) == false)
        {
            move(next_pt, _maze, _arr_pt);
        }
    }
    {
        // move left
        Point next_pt = new Point(pt.X - 1, pt.Y);
        if (next_pt.X >= 0 &&
            _maze[next_pt.Y, next_pt.X] != 0 &&
            _arr_pt.Contains(next_pt) == false)
        {
            move(next_pt, _maze, _arr_pt);
        }
    }
    {
        // move right
        Point next_pt = new Point(pt.X + 1, pt.Y);
        if (next_pt.X < _n &&
            _maze[next_pt.Y, next_pt.X] != 0 &&
            _arr_pt.Contains(next_pt) == false)
        {
            move(next_pt, _maze, _arr_pt);
        }
    }


    // 끝을 (m,n)을 찾았다면 true 리턴
    // 못찾고 갈길이 없는 경우 false 리턴
    //Console.WriteLine("더이상 갈 곳이 없다. y:{0}, x:{1}", pt.Y, pt.X);
    _arr_pt.Pop();
    return false;
}


print_maze(maze);

Stopwatch sw = Stopwatch.StartNew();
Console.WriteLine("begin");
Stack<Point> arr_pt = new Stack<Point>();
move(new Point(0, 0), maze, arr_pt);
Console.WriteLine("최소경로: {0}", min_cost);

sw.Stop();
Console.WriteLine("elapsed {0:N0} ms", sw.ElapsedMilliseconds);

#else

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Text;

int min_cost = 1000000;

// m, n은 2이상 100이하

//int m = 10;
//int n = 25;
//string str_maze = @"1011101110111011101110111
//1110111011101110111011101
//1011101110111011101110111
//1110111011101110111011101
//1011101110111011101110111
//1110111011101110111011101
//1011101110111011101110111
//1110111011101110111011101
//1011101110111011101110111
//1110111011101110111011101";

int m = 4;
int n = 6;
string str_maze = @"101111
101010
101011
111011";


string[] lines = str_maze.Split("\n", StringSplitOptions.TrimEntries);
int[,] maze = new int[m, n];
int[,] pathmap = new int[m, n];


//입력 받기
for (int row = 0; row < lines.Length; row++)
{
    string line = lines[row].Trim();

    for (int c = 0; c < line.Length; c++)
    {
        if (line[c] == '1')
            maze[row, c] = 1;
        else
            maze[row, c] = 0;

    }
}

bool is_valid_cell(int row, int col)
{
    if (row < 0 || row >= maze.GetLength(0))
        return false;
    if (col < 0 || col >= maze.GetLength(1))
        return false;

    return true;
}

void print_maze(int[,] _maze)
{
    StringBuilder sb = new StringBuilder();
    int _m = _maze.GetLength(0);
    int _n = _maze.GetLength(1);

    sb.AppendFormat(" m: {0}, n: {1} \n", _m, _n);
    for (int row = 0; row < _m; row++)
    {
        for (int col = 0; col < _n; col++)
        {
            sb.AppendFormat("{0,2:x} ", _maze[row, col]);
        }
        sb.AppendFormat("\n");
    }
    Console.WriteLine(sb.ToString());
}


void move()
{
    int cost = 0;
    
    int curr_row = 0;
    int curr_col = 0;
    int next_row = 0;
    int next_col = 0;

    // 9시방향부터 시계반대방향으로 0, 1, 2, 3
    int[,] dir = new int[2, 4] { { 0, 1, 0, -1 }, { -1, 0, 1, 0 } };

    ConcurrentQueue<Point> queue = new ConcurrentQueue<Point>();
    queue.Enqueue(new Point(curr_col, curr_row));
    pathmap[curr_row, curr_col] = 1;

    while (queue.IsEmpty == false)
    {
       
        Point pt;        
        queue.TryDequeue(out pt);
        curr_row = pt.Y;
        curr_col = pt.X;

        if ((curr_row == m - 1) &&
            (curr_col == n - 1))
        {
            //Console.WriteLine("목적지를 찾았다. m:{0}, n:{1}", curr_row, curr_col);
            //Console.WriteLine("비용 : {0}", pathmap[curr_row, curr_col]);

            cost = pathmap[curr_row, curr_col];
            if (cost < min_cost)
            {
                //Console.WriteLine(" 최소비용 갱신.  {0} -> {1}", min_cost, cost);
                min_cost = cost;
            }
        }

        //Console.WriteLine(" curr_row: {0}, curr_col: {1}", curr_row, curr_col);
        for (int i = 0; i < 4; i++)
        {
            // 진행이 가능한 방향만 Queue에 넣는다.
            next_row = curr_row + dir[0, i];
            next_col = curr_col + dir[1, i];

            // 벽으로 막힌길
            // 갈수 없는 길
            // 이미 가본 길

            if (is_valid_cell(next_row, next_col) == false) continue;
            if (maze[next_row, next_col] != 1) continue;
            if (pathmap[next_row, next_col] != 0) continue;

            //Console.WriteLine("  i:{0}, next_row:{1}, next_col:{2}", i, next_row, next_col);


            pathmap[next_row, next_col] = pathmap[curr_row, curr_col] + 1;
            queue.Enqueue(new Point(next_col, next_row));

        }

        //print_maze(pathmap);
    }

    // (0,0)에서부터 시작한다.
    // 4가지 방향에 대해서 탐색을 시작한다.
    // 이전에 왔던 길로는 가는 않는다.

    // 목적지에 도착한 경우 비용을 기억해둔다. 
    // 새로운 길 탐색중 이미 이전 도착 비용을 초과한 경우 더이상의 탐색을 포기한다.

  
    
}


print_maze(maze);

Stopwatch sw = Stopwatch.StartNew();
Stack<Point> arr_pt = new Stack<Point>();

move();

Console.WriteLine("최소경로: {0}", min_cost);

sw.Stop();
Console.WriteLine("elapsed {0:N0} ms", sw.ElapsedMilliseconds);

#endif