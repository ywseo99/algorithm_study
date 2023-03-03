// See https://aka.ms/new-console-template for more information


/*
https://www.acmicpc.net/problem/10158
*/



using System.Diagnostics;
using System.Drawing;
using System.Text;



int w = 0;  // 너비 2~40,000
int h = 0;

int p = 0;
int q = 0;

int t = 0;


//Console.WriteLine("10158 개미");

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
Point pt_first = new Point(p, q);

int dir = NE;
int dir_first = dir;

Stopwatch sw = Stopwatch.StartNew();
bool is_repeat = false;

int move_cnt_to_final = t;  // 반복 개수 확인 후, 순간이동 후에 추가 이동할 개수
int routine_cnt = 0;





int GetDir(Point curr_pos, int dir)
{
    // 현재 위치에서 경계 확인. 모두 12가지 경우의 수에 따라 진행 방향을 바꿔준다.
    int next_dir = dir;
    if (pt.X == 0 &&
        (pt.Y > 0 && pt.Y < bounds.Height))
    {
        if (dir == NW) next_dir = NE;
        if (dir == SW) next_dir = SE;
    }

    if (pt.X == bounds.Width &&
        (pt.Y > 0 && pt.Y < bounds.Height))
    {
        if (dir == NE) next_dir = NW;
        if (dir == SE) next_dir = SW;

    }

    if ((pt.X > 0 && pt.X < bounds.Width) &&
         pt.Y == 0)
    {
        if (dir == SW) next_dir = NW;
        if (dir == SE) next_dir = NE;
    }

    if ((pt.X > 0 && pt.X < bounds.Width) &&
         pt.Y == bounds.Height)
    {
        if (dir == NW) next_dir = SW;
        if (dir == NE) next_dir = SE;
    }

    if (pt.X == 0 && pt.Y == 0) next_dir = NE;
    if (pt.X == 0 && pt.Y == bounds.Height) next_dir = SE;
    if (pt.X == bounds.Width && pt.Y == 0) next_dir = NW;
    if (pt.X == bounds.Width && pt.Y == bounds.Height) next_dir = SW;
    return next_dir;
}



int move = 0;
int d = 1;

while (move < t)
{
    // 현재위치와 그 위치에서의 방향 표시
    //Console.WriteLine(" pos ({0},{1})   dir:{2}   move:{3}", pt.X, pt.Y, dir, move);
    StringBuilder sb = new StringBuilder();
    sb.AppendFormat("curr_pos {1},{2}  move_sum:{3},  ", move, pt.X, pt.Y, move);
    switch (dir)
    {
        case NW: sb.AppendFormat("NW"); break;
        case NE: sb.AppendFormat("NE"); break;
        case SW: sb.AppendFormat("SW"); break;
        case SE: sb.AppendFormat("SE"); break;
    }
    Console.WriteLine(sb.ToString());


    // 벽에 다을 때까지 빠르게 이동
    Point next_pt = new Point(pt.X, pt.Y);
    switch (dir)
    {
        case NE:
            // x, y 중 어떤부분지 먼저 닿는지 본다. 
            {
                int dx = bounds.Width - pt.X;
                int dy = bounds.Height - pt.Y;
                d = Math.Min(dx, dy);

                next_pt.X += d;
                next_pt.Y += d;
                Console.WriteLine("\t NE move {0}", d);
            }
            break;
        case SE:
            {
                int dx = bounds.Width - pt.X;
                int dy = pt.Y;
                d = Math.Min(dx, dy);

                next_pt.X += d;
                next_pt.Y -= d;
                Console.WriteLine("\t SE move {0}", d);
            }
            break;
        case SW:
            {
                int dx = pt.X;
                int dy = pt.Y;
                d = Math.Min(dx, dy);

                next_pt.X -= d;
                next_pt.Y -= d;
                Console.WriteLine("\t SW move {0}", d);
            }
            break;
        case NW:
            {
                int dx = pt.X;
                int dy = bounds.Height - pt.Y;
                d = Math.Min(dx, dy);

                next_pt.X -= d;
                next_pt.Y += d;
                Console.WriteLine("\t NW move {0}", d);
            }
            break;
    }

    move += d;

    pt = next_pt;

    // 이동한 위치에서의 방향 다시 찾기
    int next_dir = GetDir(pt, dir);
    //if (dir == next_dir)
    //{
    //    Console.WriteLine("아직 벽에 닿지 않았다. ");
    //}


    dir = next_dir;
}

Console.WriteLine("{0} {1}", pt.X, pt.Y);
Console.WriteLine("end");


//for (int i = 0; i < t; i++)
//{
//    // 동일한 좌표와 방향을 갖는 경우가 있었는지 확인한다. 
//    // 만약 그런 경우를 찾는다면 이후는 동일하다. 


//    Point next_pt = new Point(pt.X, pt.Y);



    
//    if (i > 0 &&
//        pt_first.X == pt.X &&
//        pt_first.Y == pt.Y &&
//        dir_first == dir) 
//    {

//        move_cnt_to_final = t % routine_cnt;
//        Console.WriteLine("동일한 경우 발견. {0}개 마다 반복되는 것으로 보임", routine_cnt);
//        Console.WriteLine("이후 반복이 예상되는 {0} 루틴은 건너뜀", (int)(t / routine_cnt));
//        Console.WriteLine("추가 진행할 이동 수 : {0}", move_cnt_to_final);

//        i += ((int)(t / routine_cnt) * routine_cnt);
//        is_repeat = true;        
//    }



//    if (i % 1000 == 0 ||
//        i < 100)
//    {
//        Console.WriteLine("[{0,3}], pos ({1}, {2}),  dir:{3}", i, pt.X, pt.Y, dir);
//    }

//    // 이동한다
//    // 다음 방향 전환하는 지점까지 빠르게 이동한다.


//    int d = 1;

//    switch (dir)
//    {
//        case NE:

//            // x, y 중 어떤부분지 먼저 닿는지 본다. 
//            {
//                int dx = bounds.Width - pt.X;
//                int dy = bounds.Height - pt.Y;
//                d = Math.Min(dx, dy);

//                next_pt.X += d;
//                next_pt.Y += d;
//            }
//            break;
//        case SE:
//            {
//                int dx = bounds.Width - pt.X;
//                int dy = pt.Y;
//                d = Math.Min(dx, dy);

//                next_pt.X += d;
//                next_pt.Y -= d;
//            }
//            break;
//        case SW:
//            {
//                int dx = pt.X;
//                int dy = pt.Y;
//                d = Math.Min(dx, dy);

//                next_pt.X -= d;
//                next_pt.Y -= d;
//            }
//            break;
//        case NW:
//            {
//                int dx = pt.X;
//                int dy = bounds.Height - pt.Y;
//                d = Math.Min(dx, dy);

//                next_pt.X -= d;
//                next_pt.Y += d;
//            }
//            break;
//    }



//    //if (move_cnt_to_final == 0)
//    //{
//    //    Console.WriteLine("종료");
//    //    Console.WriteLine("{0} {1}", pt.X, pt.Y);
//    //    break;
//    //}
//    //move_cnt_to_final--;


//    routine_cnt += d;
//    i += d;


//    pt = next_pt;
//}


////if (is_repeat == false)
//{
//    Console.WriteLine("{0} {1}", pt.X, pt.Y);
//}



////StringBuilder sb = new StringBuilder();
////sb.AppendFormat("time: {0}  p:{1} q:{2} ", i, pt.X, pt.Y);
////switch (dir)
////{
////    case NW: sb.AppendFormat("NW"); break;
////    case NE: sb.AppendFormat("NE"); break;
////    case SW: sb.AppendFormat("SW"); break;
////    case SE: sb.AppendFormat("SE"); break;
////}
////Console.WriteLine(sb.ToString());


//Console.WriteLine("elapsed {0:N0} ms", sw.ElapsedMilliseconds);
