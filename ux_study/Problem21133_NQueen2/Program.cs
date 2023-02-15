

using System.Diagnostics;
using System.Text;

List<string> arr_answer = new List<string>();
int[] answer;

bool is_exit = false;

//bool move(byte[] arr, int row = 0)
//{
//    if (row >= arr.Length)
//    {
//        return true;
//    }

//    /*
//     * Cell의 상태는 모두 4가지 
//     * 0x0 (0000) : 비어있는 셀. 여기에 새로 Queen을 놓을 수 있다.
//     * 0x1 (0001) : 우측아래로 확장하는 셀. 다음 row의 col+1도 같은 속성을 가짐 ( |= 0x1 )
//     * 0x2 (0010) : 아래로 확장하는 셀. 다음 row의 col도 같은 속성을 가짐 ( |= 0x2 )
//     * 0x4 (0100) : 좌측아래로 확장하는 셀. 다음 row의 col-1도 같은 속성을 가짐 ( |= 0x4 )
//     * 0x7 (0111) : 새로 놓여지는 Queen은 위의 3가지 속성을 모두 가짐 ( |= 0x7 )
//     * 
//     * 현재 row값들을 기준으로 다음 next_row를 채우고, 비어있는 셀에 Queen을 놓는 시도를 재귀적으로 반복한다. 
//     * Queen을 놓을 수 있는 셀이 존재하지 않는다면 실패이다. 
//     * Queen을 계속 놓으며 마지막 row까지 진행했다면 정답이다.
//     */    
//    for (int col = 0; col < arr.Length; col++)
//    {
//        if (is_exit == true) break;

//        byte[] next_arr = new byte[arr.Length];
//        for (int _col = 0; _col < arr.Length; _col++)
//        {
//            if (arr[_col] == 0) continue;

//            if (_col >= 1)
//                next_arr[_col - 1] |= (byte)(arr[_col] & 0x4);

//            next_arr[_col] |= (byte)(arr[_col] & 0x2);

//            if (_col < arr.Length - 1)
//                next_arr[_col + 1] |= (byte)(arr[_col] & 0x1);
//        }

//        // 새로 놓을 Queen에 의한 다음 row의 변화
//        if (arr[col] != 0) continue;
//        if ((col - 1) >= 0)
//            next_arr[col - 1] |= 0x4;

//        next_arr[col] |= 0x2;

//        if ((col + 1) < arr.Length)
//            next_arr[col + 1] |= 0x1;

//        answer[row] = col;
//        bool ret = move(next_arr, row + 1);

//        if (ret == true)
//        {
//            arr_answer.Add(string.Join(",", answer));
//            foreach (int value in answer)
//            {
//                Console.WriteLine("{0}", value);

//            }
//            //is_exit = true;
//        }
//    }
//    return false;
//}






string print_matrix(int[] _answer)
{
    int n = _answer.Length;
    int[,] matrix = new int[n, n];

    for (int row = 0; row < n; row++)
    {
        int col = _answer[row];
        matrix[row, col - 1] = 1;
    }

    StringBuilder sb = new StringBuilder();
    for (int row = 0; row < n; row++)
    {
        for (int col = 0; col < matrix.GetLength(1); col++)
        {
            if (matrix[row, col] == 1) sb.AppendFormat("■ ");
            else sb.AppendFormat("_ ");
        }
        sb.AppendFormat("\n");
    }    
    return sb.ToString();
}


bool is_validate(int[] _answer)
{
    int n = _answer.Length;
    int[,] matrix = new int[n, n];
    for (int row = 0; row < n; row++)
    {
        int col = _answer[row];
        matrix[row, col - 1] = 1;
    }

    Console.WriteLine(print_matrix(_answer));


    // 1부터 n까지 모든 숫자가 들어있는지 확인한다.    
    for (int i = 1; i <= n; i++)
    {
        int[] arr_match = Array.FindAll(_answer, ele => ele == i);
        if (arr_match.Length != 1)
        {
            Console.WriteLine(" fail. i:{0}, match_count:{1}", i, arr_match.Length);
            //Console.WriteLine(print_matrix(_answer));
            return false;
        }
    }

    // Queen 규칙에 어긋나는 경우 찾기
    // 대각선 찾기. 
    for (int col = 0; col < n; col++)
    {
        int left_count = 0;
        int right_count = 0;

        int left_col = 0;
        int right_col = 0;

        for (int row = 0; row < n; row++)
        {
            // 왼쪽, 오른쪽 대각선으로 점유한 셀의 개수가 각각 1개 뿐인지를 확인한다.
            if (row == 0)
            {
                left_col = col;
                right_col = col;
            }

            if (left_col >= 0)
            {
                if (matrix[row, left_col] == 1) left_count++;
            }
            if (right_col < n)
            {
                if (matrix[row, right_col] == 1) right_count++;
            }

            left_col--;
            right_col++;
        }

        if (left_count > 1 || right_count > 1)
        {
            Console.WriteLine("n: {0}, col:{1}, 좌측대각:{2}, 우측대각:{3}", n, col, left_count, right_count);
            //Console.WriteLine(print_matrix(_answer));
            return false;
        }        
    }

    return true;
}


int n = 8;
string input = Console.ReadLine();
n = int.Parse(input);
int[] arr = new int[n];

void solve(int n = 10)
{
    int begin_col = 1;
    int delta = 2;
    arr[0] = begin_col + 1;

    int col = 1;
    for (int row = 1; row < n; row++)
    {
        col = col + delta;

        if (col > n - 1)
        {
            if (n % 2 == 0)
            {
                col = col % n - 1;
            }
            else
            {
                col = col % n;
            }
        }
        arr[row] = col + 1;
    }
}


void solve_pattern_2(int n = 10)
{
    int begin_col = 1;
    int delta = 2;
    arr[0] = begin_col + 1;

    int col = 1;

    int r_jump = 1;
    int l_jump = 8;

    bool direction_right = true;
    for (int row = 1; row < n; row++)
    {
        if (direction_right == true)
        {
            col = col + delta;
        }
        else {
            col = col - delta;
        }

        if (direction_right == true &&
            col > n - 1)
        {
            // 행이 넘어갔고 시작 col과 같다면 방향 바꿈
            col = col + r_jump;
            direction_right = false;
            r_jump = r_jump + 1;
        }
        else if(direction_right == false && 
            (col % n) < 0) 
        {
            col = col + l_jump;
            direction_right = true;

            r_jump = 4;
        }

        col = col % n;
        arr[row] = col + 1;
    }
}



List<int> arr_invalid = new List<int>();
//for (int i = 4; i < 1000; i++)
{
    //n = i;
    arr = new int[n];
    if (n < 8) { solve(n); }
    else
    {
        if ((n - 8) % 6 == 0)
        {
            // 8, 14, 20, 26, ... 
            solve_pattern_2(n);
            //continue;
        }
        else if ((n - 9) % 6 == 0)
        {
            // 9, 15, 21, 27, ...
            //continue;
        }
        else
        {
            solve(n);
        }
    }


    if (is_validate(arr) == false)
    {
        //Console.WriteLine("유효하지 않은 답 발견");
        arr_invalid.Add(n);
    }
}
Console.WriteLine(string.Join("\n", arr));


//foreach (int _invalid in arr_invalid)
//{
//    Console.Write("{0},", _invalid);
//}


//foreach (int _answer in arr)
//{
//    Console.WriteLine("{0}", _answer);
//}


/*
 * 4 ~ 1000 중에서 유효하지 않은 정답
 * 8,9,14,15,20,21,26,27,32,33,38,39,44,45,50,51,56,57,62,63,68,69,74,75,80,81,86,87,92,93,98,99,
 * 104,105,110,111,116,117,122,123,128,129,134,135,140,141,146,147,152,153,158,159,164,165,170,
 * 171,176,177,182,183,188,189,194,195,200,201,206,207,212,213,218,219,224,225,230,231,236,237,242,
 * 243,248,249,254,255,260,261,266,267,272,273,278,279,284,285,290,291,296,297,302,303,308,309,314,315,
 * 320,321,326,327,332,333,338,339,344,345,350,351,356,357,362,363,368,369,374,375,380,381,386,387,392,393,
 * 398,399,404,405,410,411,416,417,422,423,428,429,434,435,440,441,446,447,452,453,458,459,464,465,470,471,
 * 476,477,482,483,488,489,494,495,500,501,506,507,512,513,518,519,524,525,530,531,536,537,542,543,548,549,
 * 554,555,560,561,566,567,572,573,578,579,584,585,590,591,596,597,602,603,608,609,614,615,620,621,626,627,
 * 632,633,638,639,644,645,650,651,656,657,662,663,668,669,674,675,680,681,686,687,692,693,698,699,704,705,
 * 710,711,716,717,722,723,728,729,734,735,740,741,746,747,752,753,758,759,764,765,770,771,776,777,782,783,
 * 788,789,794,795,800,801,806,807,812,813,818,819,824,825,830,831,836,837,842,843,848,849,854,855,860,861,
 * 866,867,872,873,878,879,884,885,890,891,896,897,902,903,908,909,914,915,920,921,926,927,932,933,938,939,
 * 944,945,950,951,956,957,962,963,968,969,974,975,980,981,986,987,992,993,998,999,
 * 
 */