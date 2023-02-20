//#define SOLVE 

/*
문제
눈금의 간격이 1인 M×N(M, N≤100)크기의 모눈종이가 있다. 
이 모눈종이 위에 눈금에 맞추어 K개의 직사각형을 그릴 때, 
이들 K개의 직사각형의 내부를 제외한 나머지 부분이 몇 개의 분리된 영역으로 나누어진다.

예를 들어 M=5, N = 7 인 모눈종이 위에 <그림 1>과 같이 직사각형 3개를 그렸다면, 
그 나머지 영역은 <그림 2>와 같이 3개의 분리된 영역으로 나누어지게 된다.


<그림 2>와 같이 분리된 세 영역의 넓이는 각각 1, 7, 13이 된다.
M, N과 K 그리고 K개의 직사각형의 좌표가 주어질 때, 
K개의 직사각형 내부를 제외한 나머지 부분이 몇 개의 분리된 영역으로 나누어지는지, 
그리고 분리된 각 영역의 넓이가 얼마인지를 구하여 이를 출력하는 프로그램을 작성하시오.

입력 -------------------------------------------------------------
    첫째 줄에 M과 N, 그리고 K가 빈칸을 사이에 두고 차례로 주어진다. 
    M, N, K는 모두 100 이하의 자연수이다. 
    둘째 줄부터 K개의 줄에는 한 줄에 하나씩 직사각형의 왼쪽 아래 꼭짓점의 x, y좌표값과 오른쪽 위 꼭짓점의 x, y좌표값이 빈칸을 사이에 두고 차례로 주어진다. 
    모눈종이의 왼쪽 아래 꼭짓점의 좌표는 (0,0)이고, 
    오른쪽 위 꼭짓점의 좌표는(N, M)이다.입력되는 K개의 직사각형들이 모눈종이 전체를 채우는 경우는 없다.

출력 -------------------------------------------------------------
    첫째 줄에 분리되어 나누어지는 영역의 개수를 출력한다. 
    둘째 줄에는 각 영역의 넓이를 오름차순으로 정렬하여 빈칸을 사이에 두고 출력한다.

예제 입력 1 
    5 7 3
    0 2 4 4
    1 1 2 5
    4 0 6 2
예제 출력 1 
    3
    1 7 13
*/

#if SOLVE 


using System.Collections.Concurrent;
using System.Drawing;
using System.Text;

string input = Console.ReadLine();
string[] mn = input.Split(" ", StringSplitOptions.TrimEntries);
int m = int.Parse(mn[0]);
int n = int.Parse(mn[1]);
int k = int.Parse(mn[2]);

int[,] maze = new int[m, n];
int[,] pathmap = new int[m, n];

//입력 받기
for (int row = 0; row < k; row++)
{
    string line = Console.ReadLine().Trim();
    string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    int x1 = int.Parse(words[0]);
    int y1 = int.Parse(words[1]);
    int x2 = int.Parse(words[2]);
    int y2 = int.Parse(words[3]);

    // 사각형이 포함하는 영역 칠하기
    for (int r = y1; r < y2; r++)
    {
        for (int c = x1; c < x2; c++)
        {
            maze[r, c] += 1;
        }
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

List<int> areas = new List<int>();
void move()
{
    int curr_row = 0;
    int curr_col = 0;
    int next_row = 0;
    int next_col = 0;

    // 9시방향부터 시계반대방향으로 0, 1, 2, 3
    int[,] dir = new int[2, 4] { { 0, 1, 0, -1 }, { -1, 0, 1, 0 } };

    ConcurrentQueue<Point> queue = new ConcurrentQueue<Point>();
    queue.Enqueue(new Point(curr_col, curr_row));

    int area = 0;
    bool enable_loop = true;
    while (enable_loop)
    {
        // 큐가 비어있다면 비어있는 셀을 찾아 처음부터 다시 시작한다.
        if (queue.IsEmpty == true)
        {
            if (area != 0)
            {
                areas.Add(area);
                area = 0;
            }

            // 비어있는 셀을 찾아본다
            for (int row = 0; row < pathmap.GetLength(0); row++)
            {
                for (int col = 0; col < pathmap.GetLength(1); col++)
                {
                    if (pathmap[row, col] != 0)
                    {
                        continue;
                    }
                    curr_row = row;
                    curr_col = col;

                    queue.Enqueue(new Point(curr_col, curr_row));
                    pathmap[curr_row, curr_col] = 1;

                    goto MOVE_CONTINUE;
                }
            }

            // 비어있는 셀을 찾지 못했다면
            enable_loop = false;
            continue;
        }

    MOVE_CONTINUE:


        Point pt;
        queue.TryDequeue(out pt);
        curr_row = pt.Y;
        curr_col = pt.X;

        for (int i = 0; i < 4; i++)
        {
            // 진행이 가능한 방향만 Queue에 넣는다.
            next_row = curr_row + dir[0, i];
            next_col = curr_col + dir[1, i];

            if (is_valid_cell(next_row, next_col) == false) continue;
            if (maze[next_row, next_col] != 0) continue;
            if (pathmap[next_row, next_col] != 0) continue;
            
            area += 1;
            pathmap[next_row, next_col] = pathmap[curr_row, curr_col] + 1;
            queue.Enqueue(new Point(next_col, next_row));
        }
    }
}
move();
Console.WriteLine("{0}", areas.Count);
Console.WriteLine("{0}", string.Join(" ", areas.OrderBy(i => i).ToList()));

#else 


using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Text;

//int m = 5;  // 행의 수
//int n = 7;  // 열의 수
//int k = 3;  // 그려지는 사각형의 수

//string str_input = @"0 2 4 4
//1 1 2 5
//4 0 6 2";


int m = 5;  // 행의 수
int n = 5;  // 열의 수
int k = 5;  // 그려지는 사각형의 수

string str_input = @"0 0 2 2
3 3 5 5
0 4 1 5
1 1 4 4
4 0 5 1";

// 예상되는 답
//4
//2 2 2 2

string[] lines = str_input.Split("\n", StringSplitOptions.TrimEntries);
int[,] maze = new int[m, n];
int[,] pathmap = new int[m, n];



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


//입력 받기
for (int row = 0; row < lines.Length; row++)
{
    string line = lines[row].Trim();
    string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    int x1 = int.Parse(words[0]);
    int y1 = int.Parse(words[1]);
    int x2 = int.Parse(words[2]);
    int y2 = int.Parse(words[3]);

    // 사각형이 포함하는 영역 칠하기
    Console.WriteLine("x1:{0}, y1:{1}, x2:{2}, y2:{3}", x1, y1, x2, y2);
    Trace.TraceInformation("x1:{0}, y1:{1}, x2:{2}, y2:{3}", x1, y1, x2, y2);
    for (int r = y1; r < y2; r++)
    {
        for (int c = x1; c < x2; c++)
        {
            maze[r, c] += 1;
        }
    }
}

print_maze(maze);


bool is_valid_cell(int row, int col)
{
    if (row < 0 || row >= maze.GetLength(0))
        return false;
    if (col < 0 || col >= maze.GetLength(1))
        return false;

    return true;
}



List<int> areas = new List<int>();

void move()
{
    int curr_row = 0;
    int curr_col = 0;
    int next_row = 0;
    int next_col = 0;

    // 9시방향부터 시계반대방향으로 0, 1, 2, 3
    int[,] dir = new int[2, 4] { { 0, 1, 0, -1 }, { -1, 0, 1, 0 } };

    ConcurrentQueue<Point> queue = new ConcurrentQueue<Point>();
    queue.Enqueue(new Point(curr_col, curr_row));

    int area = 0;
    bool enable_loop = true;
    while (enable_loop)
    {
        // 큐가 비어있다면 비어있는 셀을 찾아 처음부터 다시 시작한다.
        if (queue.IsEmpty == true)
        {
            if (area != 0)
            {
                Console.WriteLine("---> area found. {0}", area);
                areas.Add(area);
                area = 0;
            }

            // 비어있는 셀을 찾아본다
            for (int row = 0; row < pathmap.GetLength(0); row++)
            {
                for (int col = 0; col < pathmap.GetLength(1); col++)
                {
                    if (pathmap[row, col] != 0)
                    {
                        continue;
                    }
                    curr_row = row;
                    curr_col = col;

                    queue.Enqueue(new Point(curr_col, curr_row));
                    pathmap[curr_row, curr_col] = 1;

                    goto MOVE_CONTINUE;
                }
            }

            // 비어있는 셀을 찾지 못했다면
            enable_loop = false;
            continue;
        }

    MOVE_CONTINUE:


        Point pt;
        queue.TryDequeue(out pt);
        curr_row = pt.Y;
        curr_col = pt.X;

        Console.WriteLine(" curr_row: {0}, curr_col: {1}", curr_row, curr_col);
        for (int i = 0; i < 4; i++)
        {
            // 진행이 가능한 방향만 Queue에 넣는다.
            next_row = curr_row + dir[0, i];
            next_col = curr_col + dir[1, i];

            // 벽으로 막힌길
            // 갈수 없는 길
            // 이미 가본 길

            if (is_valid_cell(next_row, next_col) == false) continue;
            if (maze[next_row, next_col] != 0) continue;
            if (pathmap[next_row, next_col] != 0) continue;

            Console.WriteLine("  i:{0}, next_row:{1}, next_col:{2}", i, next_row, next_col);

            area += 1;
            pathmap[next_row, next_col] = pathmap[curr_row, curr_col] + 1;
            queue.Enqueue(new Point(next_col, next_row));
        }
    }

}


Stopwatch sw = Stopwatch.StartNew();
move();

sw.Stop();
Console.WriteLine(" elapsed {0:N0} ms", sw.ElapsedMilliseconds);

Console.WriteLine(" block count: {0}", areas.Count);
Console.WriteLine(" area : {0}", string.Join(" ", areas.OrderBy(i => i).ToList()));

#endif