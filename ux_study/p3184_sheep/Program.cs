//#define SOLVE

/*
양 

문제
미키의 뒷마당에는 특정 수의 양이 있다. 
그가 푹 잠든 사이에 배고픈 늑대는 마당에 들어와 양을 공격했다.
마당은 행과 열로 이루어진 직사각형 모양이다. 
글자 '.' (점)은 빈 필드를 의미하며, 글자 '#'는 울타리를, 'o'는 양, 'v'는 늑대를 의미한다.

한 칸에서 수평, 수직만으로 이동하며 울타리를 지나지 않고 다른 칸으로 이동할 수 있다면, 두 칸은 같은 영역 안에 속해 있다고 한다. 
마당에서 "탈출"할 수 있는 칸은 어떤 영역에도 속하지 않는다고 간주한다.

다행히 우리의 양은 늑대에게 싸움을 걸 수 있고 영역 안의 양의 수가 늑대의 수보다 많다면 이기고, 늑대를 우리에서 쫓아낸다. 
그렇지 않다면 늑대가 그 지역 안의 모든 양을 먹는다.
맨 처음 모든 양과 늑대는 마당 안 영역에 존재한다.
아침이 도달했을 때 살아남은 양과 늑대의 수를 출력하는 프로그램을 작성하라.

입력
    첫 줄에는 두 정수 R과 C가 주어지며(3 ≤ R, C ≤ 250), 각 수는 마당의 행과 열의 수를 의미한다.
    다음 R개의 줄은 C개의 글자를 가진다. 이들은 마당의 구조(울타리, 양, 늑대의 위치)를 의미한다.

출력
    하나의 줄에 아침까지 살아있는 양과 늑대의 수를 의미하는 두 정수를 출력한다.

예제 입력 1 
    6 6
    ...#..
    .##v#.
    #v.#.#
    #.o#.#
    .###.#
    ...###
예제 출력 1 
    0 2
예제 입력 2 

    8 8
    .######.
    #..o...#
    #.####.#
    #.#v.#.#
    #.#.o#o#
    #o.##..#
    #.v..v.#
    .######.
예제 출력 2 
    3 1

예제 입력 3 
    9 12
    .###.#####..
    #.oo#...#v#.
    #..o#.#.#.#.
    #..##o#...#.
    #.#v#o###.#.
    #..#v#....#.
    #...v#v####.
    .####.#vv.o#
    .......####.
예제 출력 3 
    3 5
*/



#if SOLVE
using System.Collections.Concurrent;
using System.Drawing;
using System.Text;

// m, n은 3이상 250이하

const int EMPTY = 0;
const int WALL = 1;
const int WOLF = 2;
const int SHEEP = 3;


string input = Console.ReadLine();
string[] mn = input.Split(" ", StringSplitOptions.TrimEntries);
int r = int.Parse(mn[0]);
int c = int.Parse(mn[1]);

// m번의 입력 라인 받기
int[,] pathmap = new int[r, c];
int[,] maze = new int[r, c];
for (int row = 0; row < r; row++)
{
    string line = Console.ReadLine().Trim();
    for (int col = 0; col < line.Length; col++)
    {
        if (line[col] == '.')
            maze[row, col] = EMPTY;
        else if (line[col] == '#')
            maze[row, col] = WALL;
        else if (line[col] == 'o')
            maze[row, col] = SHEEP;
        else if (line[col] == 'v')
            maze[row, col] = WOLF;
    }
}



int cnt_sheep = 0;
int cnt_wolf = 0;

int total_sheep = 0;
int total_wolf = 0;

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
    int curr_row = 0;
    int curr_col = 0;
    int next_row = 0;
    int next_col = 0;

    int[,] dir = new int[2, 4] { { 0, 1, 0, -1 }, { -1, 0, 1, 0 } };
    ConcurrentQueue<Point> queue = new ConcurrentQueue<Point>();
    bool enable_loop = true;
    while (enable_loop)
    {
        // 큐가 비어있다면 비어있는 셀을 찾아 처음부터 다시 시작한다.
        if (queue.IsEmpty == true)
        {
            if (cnt_sheep > cnt_wolf) 
            {
                total_sheep += cnt_sheep; 
            }
            else
            {
                total_wolf += cnt_wolf;
            }

            cnt_sheep = 0;
            cnt_wolf = 0;

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
            next_row = curr_row + dir[0, i];
            next_col = curr_col + dir[1, i];

            if (is_valid_cell(next_row, next_col) == false) continue;
            if (maze[next_row, next_col] == WALL) continue;
            if (pathmap[next_row, next_col] != 0) continue;

            if (maze[next_row, next_col] == SHEEP) cnt_sheep++;
            if (maze[next_row, next_col] == WOLF) cnt_wolf++;

            pathmap[next_row, next_col] = pathmap[curr_row, curr_col] + 1;
            queue.Enqueue(new Point(next_col, next_row));
        }
    }
}

move();
Console.WriteLine("{0} {1}", total_sheep, total_wolf);




#else 

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Text;

// m, n은 3이상 250이하

const int EMPTY = 0;
const int WALL = 1;
const int WOLF = 2;
const int SHEEP = 3;


//int r = 9;
//int c = 12;
//string str_maze =
// @".###.#####..
//#.oo#...#v#.
//#..o#.#.#.#.
//#..##o#...#.
//#.#v#o###.#.
//#..#v#....#.
//#...v#v####.
//.####.#vv.o#
//.......####.";

int r = 6;
int c = 6;

string str_maze = @"...#..
.##v#.
#v.#.#
#.o#.#
.###.#
...###";


int cnt_sheep = 0;
int cnt_wolf = 0;

int total_sheep = 0;
int total_wolf = 0;

string[] lines = str_maze.Split("\n", StringSplitOptions.TrimEntries);
int[,] maze = new int[r, c];
int[,] pathmap = new int[r, c];


//입력 받기
for (int row = 0; row < lines.Length; row++)
{
    string line = lines[row].Trim();
    for (int col = 0; col < line.Length; col++)
    {
        if (line[col] == '.')
            maze[row, col] = EMPTY;
        else if (line[col] == '#')
            maze[row, col] = WALL;
        else if (line[col] == 'o')
            maze[row, col] = SHEEP;
        else if (line[col] == 'v')
            maze[row, col] = WOLF;
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
    int curr_row = 0;
    int curr_col = 0;
    int next_row = 0;
    int next_col = 0;

    // 9시방향부터 시계반대방향으로 0, 1, 2, 3
    int[,] dir = new int[2, 4] { { 0, 1, 0, -1 }, { -1, 0, 1, 0 } };

    ConcurrentQueue<Point> queue = new ConcurrentQueue<Point>();

    bool enable_loop = true;
    while (enable_loop)
    {
        // 큐가 비어있다면 비어있는 셀을 찾아 처음부터 다시 시작한다.
        if (queue.IsEmpty == true)
        {
            Console.WriteLine(" cnt_sheep: {0}, cnt_wolf: {1}", cnt_sheep, cnt_wolf);
            if (cnt_sheep > cnt_wolf) 
            {
                Console.WriteLine("\t sheep {0} alive", cnt_sheep);
                total_sheep += cnt_sheep; 
            }
            else
            {
                Console.WriteLine("\t wolf {0} alive", cnt_wolf);
                total_wolf += cnt_wolf;
            }

            cnt_sheep = 0;
            cnt_wolf = 0;

            // 비어있는 셀을 찾아본다
            bool found_empty_cell = false;
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
                    found_empty_cell = true;

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
            if (maze[next_row, next_col] == WALL) continue;
            if (pathmap[next_row, next_col] != 0) continue;

            //Console.WriteLine("  i:{0}, next_row:{1}, next_col:{2}", i, next_row, next_col);
            if (maze[next_row, next_col] == SHEEP) cnt_sheep++;
            if (maze[next_row, next_col] == WOLF) cnt_wolf++;

            pathmap[next_row, next_col] = pathmap[curr_row, curr_col] + 1;
            queue.Enqueue(new Point(next_col, next_row));
        }
    }

}


print_maze(maze);

Stopwatch sw = Stopwatch.StartNew();
move();

sw.Stop();
Console.WriteLine(" elapsed {0:N0} ms", sw.ElapsedMilliseconds);
Console.WriteLine(" sheep : {0}", total_sheep);
Console.WriteLine(" wolf  : {0}", total_wolf);
#endif