// See https://aka.ms/new-console-template for more information
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
Console.WriteLine("{0}\n{1}", areas.Count, string.Join(" ", areas.OrderBy(i => i).ToList()));

