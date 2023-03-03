#define SOLVE


/*
https://www.acmicpc.net/problem/10158
*/


#if SOLVE

using System.Drawing;

const int NW = 0;   // 북서
const int NE = 1;   // 남서
const int SW = 2;   // 북동
const int SE = 3;   // 남동


string input = Console.ReadLine();
string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
int WIDTH = int.Parse(words[0]);
int HEIGHT = int.Parse(words[1]);

input = Console.ReadLine();
words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
int p = int.Parse(words[0]);
int q = int.Parse(words[1]);

input = Console.ReadLine();
int t = int.Parse(input);


Point pt = new Point(p, q);
int dir = NE;
int routine_cnt = 0;


int move = 0;
int d = 1;

Point ne_pt = new Point(0, 0);
int ne_dir = -1;
int ne_move_first = 0;

int dx = 0;
int dy = 0;


while (move < t)
{
    d = 0;  // 이동해야 할 거리
    switch (dir)
    {
        case NE:    
            {
                dx = WIDTH - pt.X;
                dy = HEIGHT - pt.Y;

                d = dx;
                if (d > dy) d = dy;

                if ((move + d) < t)
                {
                    if (ne_dir < 0)
                    {
                        ne_dir = NE;
                        ne_pt = pt;
                        ne_move_first = move;
                    }
                    else
                    {
                        if (ne_pt.X == pt.X &&
                            ne_pt.Y == pt.Y)
                        {
                            routine_cnt = (move - ne_move_first);
                            //int skip_cnt = (int)((t - move) / routine_cnt);
                            int remain = (t - move) % routine_cnt;
                            move += (t - move - remain);
                            //move += (skip_cnt * routine_cnt);
                        }
                    }
                }
                else
                {
                    d = (t - move);
                }
                pt.X += d;
                pt.Y += d;
            }
            break;
        case SE:
            {
                dx = WIDTH - pt.X;
                dy = pt.Y;

                d = dx;
                if (d > dy) d = dy;


                if ((move + d) >= t)
                {
                    d = (t - move);
                }
                pt.X += d;
                pt.Y -= d;
            }
            break;
        case SW:
            {
                dx = pt.X;
                dy = pt.Y;

                d = dx;
                if (d > dy) d = dy;


                if ((move + d) >= t)
                {
                    d = (t - move);
                }

                pt.X -= d;
                pt.Y -= d;
            }
            break;
        case NW:
            {
                dx = pt.X;
                dy = HEIGHT - pt.Y;

                d = dx;
                if (d > dy) d = dy;


                if ((move + d) >= t)
                {
                    d = (t - move);
                }

                pt.X -= d;
                pt.Y += d;
            }
            break;
    }

    move += d;

    int next_dir = dir;
    if (pt.X == 0)
    {
        if (pt.Y == 0) next_dir = NE;
        else if (pt.Y == HEIGHT) next_dir = SE;
        else
        {
            if (dir == NW) next_dir = NE;
            if (dir == SW) next_dir = SE;
        }
    }
    else if (pt.X == WIDTH)
    {
        if (pt.Y == 0) next_dir = NW;
        else if (pt.Y == HEIGHT) next_dir = SW;
        else
        {
            if (dir == NE) next_dir = NW;
            if (dir == SE) next_dir = SW;
        }
    }
    else
    {
        if (pt.Y == 0)
        {
            if (dir == SW) next_dir = NW;
            if (dir == SE) next_dir = NE;
        }
        else if (pt.Y == HEIGHT)
        {
            if (dir == NW) next_dir = SW;
            if (dir == NE) next_dir = SE;
        }
    }

    dir = next_dir;

}

Console.WriteLine("{0} {1}", pt.X, pt.Y);

#else

using System.Diagnostics;
using System.Drawing;
using System.Text;

int w = 0;  // 너비 2~40,000
int h = 0;
int p = 0;
int q = 0;
int t = 0;

string input = Console.ReadLine();
string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
int.TryParse(words[0], out w);
int.TryParse(words[1], out h);


input = Console.ReadLine();
words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
int.TryParse(words[0], out p);
int.TryParse(words[1], out q);

input = Console.ReadLine();
int.TryParse(input, out t);


const int NW = 0;   // 북서
const int NE = 1;   // 남서
const int SW = 2;   // 북동
const int SE = 3;   // 남동

Rectangle bounds = new Rectangle(0, 0, w, h);
Point pt = new Point(p, q);
int dir = NE;

int routine_cnt = 0;


//string DirToString(int dir)
//{
//    StringBuilder sb = new StringBuilder();
//    switch (dir)
//    {
//        case NW: sb.AppendFormat(" NW ↖"); break;
//        case NE: sb.AppendFormat(" NE ↗"); break;
//        case SW: sb.AppendFormat(" SW ↙"); break;
//        case SE: sb.AppendFormat(" SE ↘"); break;
//    }
//    return sb.ToString();
//}


int GetDir(Point curr_pos, int dir)
{
    // 현재 위치에서 경계 확인. 모두 12가지 경우의 수에 따라 진행 방향을 바꿔준다.    
    //int next_dir = dir;
    //if (pt.X == 0 &&
    //    (pt.Y > 0 && pt.Y < bounds.Height))
    //{
    //    if (dir == NW) next_dir = NE;
    //    if (dir == SW) next_dir = SE;
    //    return next_dir;
    //}

    //if (pt.X == bounds.Width &&
    //    (pt.Y > 0 && pt.Y < bounds.Height))
    //{
    //    if (dir == NE) next_dir = NW;
    //    if (dir == SE) next_dir = SW;
    //    return next_dir;
    //}

    //if ((pt.X > 0 && pt.X < bounds.Width) &&
    //     pt.Y == 0)
    //{
    //    if (dir == SW) next_dir = NW;
    //    if (dir == SE) next_dir = NE;
    //    return next_dir;

    //}

    //if ((pt.X > 0 && pt.X < bounds.Width) &&
    //     pt.Y == bounds.Height)
    //{
    //    if (dir == NW) next_dir = SW;
    //    if (dir == NE) next_dir = SE;
    //    return next_dir;

    //}

    //if (pt.X == 0 && pt.Y == 0)
    //{
    //    next_dir = NE;
    //    return next_dir;
    //}
    //if (pt.X == 0 && pt.Y == bounds.Height)
    //{
    //    next_dir = SE;
    //    return next_dir;
    //}
    //if (pt.X == bounds.Width && pt.Y == 0)
    //{
    //    next_dir = NW;
    //    return next_dir;
    //}

    //if (pt.X == bounds.Width && pt.Y == bounds.Height)
    //    next_dir = SW;


    int next_dir = dir;
    if (pt.X == 0)
    {
        if (pt.Y == 0) next_dir = NE;
        else if (pt.Y == bounds.Height) next_dir = SE;
        else
        {
            if (dir == NW) next_dir = NE;
            if (dir == SW) next_dir = SE;
        }
    }
    else if (pt.X == bounds.Width)
    {
        if (pt.Y == 0) next_dir = NW;
        else if (pt.Y == bounds.Height) next_dir = SW;
        else
        {
            if (dir == NE) next_dir = NW;
            if (dir == SE) next_dir = SW;
        }
    }
    else
    {
        if (pt.Y == 0)
        {
            if (dir == SW) next_dir = NW;
            if (dir == SE) next_dir = NE;
        }
        else if (pt.Y == bounds.Height)
        {
            if (dir == NW) next_dir = SW;
            if (dir == NE) next_dir = SE;
        }
    }
    return next_dir;
}

int move = 0;
int d = 1;


//Console.WriteLine("init_pos {0}  이동거리:{1,3},  방향:{2}", pt, move, DirToString(dir));
//Console.WriteLine("---- begin ----\n");

Point ne_pt = new Point(0, 0);
int ne_dir = -1;
int ne_move_first = 0;


//Stopwatch sw = Stopwatch.StartNew();


while (true)
{
    d = 0;  // 이동해야 할 거리
    switch (dir)
    {
        case NE:    // 초기 시작이 NE이므로 첫번째 벽은 반드시 여기서 만난다.
            // x, y 중 어떤부분지 먼저 닿는지 본다. 
            {
                int dx = bounds.Width - pt.X;
                int dy = bounds.Height - pt.Y;

                d = dx;
                if (d > dy) d = dy;

                if ((move + d) < t)
                {
                    // 남은 거리가 충분하다
                    // 미킹해놓고, 다음에 동일하게 온다면 거리단축하기
                    if (ne_dir < 0)
                    {
                        ne_dir = NE;
                        //ne_pt = new Point(pt.X, pt.Y);
                        ne_pt = pt;

                        //Console.WriteLine(" 첫번째 북동벽 기억해두기 {0,8}", ne_pt);
                        ne_move_first = move;
                    }
                    else
                    {
                        if (ne_pt.X == pt.X &&
                            ne_pt.Y == pt.Y)
                        {
                            routine_cnt = (move - ne_move_first);
                            //Console.WriteLine("  다시 북동벽 만남. {0} 이동마다 반복됨. ", routine_cnt);

                            int skip_cnt = (int)((t - move) / routine_cnt);

                            //int prev_move = move;
                            move += (skip_cnt * routine_cnt);
                            //Console.WriteLine("    순간이동 : {0} -> {1}", prev_move, move);

                        }
                    }

                }
                else
                {
                    d = (t - move);
                }
                pt.X += d;
                pt.Y += d;
            }
            break;
        case SE:
            {
                int dx = bounds.Width - pt.X;
                int dy = pt.Y;

                d = dx;
                if (d > dy) d = dy;


                if ((move + d) >= t)
                {
                    d = (t - move);
                }
                pt.X += d;
                pt.Y -= d;
            }
            break;
        case SW:
            {
                int dx = pt.X;
                int dy = pt.Y;
                
                d = dx;
                if (d > dy) d = dy;


                if ((move + d) >= t)
                {
                    d = (t - move);
                }

                pt.X -= d;
                pt.Y -= d;
            }
            break;
        case NW:
            {
                int dx = pt.X;
                int dy = bounds.Height - pt.Y;

                d = dx;
                if (d > dy) d = dy;


                if ((move + d) >= t)
                {
                    d = (t - move);
                }

                pt.X -= d;
                pt.Y += d;
            }
            break;
    }

    move += d;
    //int next_dir = GetDir(pt, dir);

    int next_dir = dir;
    if (pt.X == 0)
    {
        if (pt.Y == 0) next_dir = NE;
        else if (pt.Y == bounds.Height) next_dir = SE;
        else
        {
            if (dir == NW) next_dir = NE;
            if (dir == SW) next_dir = SE;
        }
    }
    else if (pt.X == bounds.Width)
    {
        if (pt.Y == 0) next_dir = NW;
        else if (pt.Y == bounds.Height) next_dir = SW;
        else
        {
            if (dir == NE) next_dir = NW;
            if (dir == SE) next_dir = SW;
        }
    }
    else
    {
        if (pt.Y == 0)
        {
            if (dir == SW) next_dir = NW;
            if (dir == SE) next_dir = NE;
        }
        else if (pt.Y == bounds.Height)
        {
            if (dir == NW) next_dir = SW;
            if (dir == NE) next_dir = SE;
        }
    }

    dir = next_dir;

    // 현재위치와 그 위치에서의 방향 표시
    //Console.WriteLine(" 현재위치:{0,8}  이동거리:{1,4}  방향:{2}", pt, move, DirToString(dir));

    if (move >= t)
    {
        //Console.WriteLine("이동거리 초과: {0}", move);
        Console.WriteLine("{0} {1}", pt.X, pt.Y);
        break;
    }
}


//sw.Stop();
//Console.WriteLine("elapsed {0:N0} ms", sw.ElapsedMilliseconds);

#endif