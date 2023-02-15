//#define SOLVE


using System.Text;

List<string> arr_answer = new List<string>();
int[] answer;

void MSG(string msg, params object[] args)
{
    //Console.WriteLine(msg, args);
}


bool move(byte[] arr, int row = 0)
{
    if (row >= arr.Length)
    {
        MSG(" end. row: {0}\n", row);
        return true;
    }

    /*
     * Cell의 상태는 모두 4가지 
     * 0x0 (0000) : 비어있는 셀. 여기에 새로 Queen을 놓을 수 있다.
     * 0x1 (0001) : 우측아래로 확장하는 셀. 다음 row의 col+1도 같은 속성을 가짐 ( |= 0x1 )
     * 0x2 (0010) : 아래로 확장하는 셀. 다음 row의 col도 같은 속성을 가짐 ( |= 0x2 )
     * 0x4 (0100) : 좌측아래로 확장하는 셀. 다음 row의 col-1도 같은 속성을 가짐 ( |= 0x4 )
     * 0x7 (0111) : 새로 놓여지는 Queen은 위의 3가지 속성을 모두 가짐 ( |= 0x7 )
     * 
     * 현재 row값들을 기준으로 다음 next_row를 채우고, 비어있는 셀에 Queen을 놓는 시도를 재귀적으로 반복한다. 
     * Queen을 놓을 수 있는 셀이 존재하지 않는다면 실패이다. 
     * Queen을 계속 놓으며 마지막 row까지 진행했다면 정답이다.
     */

    // 현재 row 값으로 다음 row를 계산한다
    bool col_exist = false;
    for (int col = 0; col < arr.Length; col++)
    {
        byte[] next_arr = new byte[arr.Length];
        for (int _col = 0; _col < arr.Length; _col++)
        {
            if (arr[_col] == 0) continue;

            if (_col >= 1)
                next_arr[_col - 1] |= (byte)(arr[_col] & 0x4);

            next_arr[_col] |= (byte)(arr[_col] & 0x2);

            if (_col < arr.Length - 1)
                next_arr[_col + 1] |= (byte)(arr[_col] & 0x1);
        }

        // 새로 놓을 Queen에 의한 다음 row의 변화
        if (arr[col] != 0) continue;
        if ((col - 1) >= 0)
            next_arr[col - 1] |= 0x4;

        next_arr[col] |= 0x2;

        if ((col + 1) < arr.Length)
            next_arr[col + 1] |= 0x1;


        answer[row] = col;

        MSG("\t ---> Queen place row:{0}, col:{1}", row, col);
        MSG("\t      row:{0}, curr_row:{1}", row, string.Join(",", arr));
        MSG("\t      row:{0}, next_row:{1}", row + 1, string.Join(",", next_arr));


        bool ret = move(next_arr, row + 1);

        col_exist = true;        
        if (ret == true)
        {
            arr_answer.Add(string.Join(",", answer));
            MSG("\t\t FOUND ANSWER. {0}\n\n", string.Join(",", answer));
        }
    }

    if (col_exist == false)
    {
        MSG("\t FAIL. Cannot locate Queen. row:{0} \n", row);
    }

    return false;
}

#if SOLVE
int n = 8;
string input = Console.ReadLine();
int.TryParse(input, out n);

byte[] arr = new byte[n];
answer = new int[n];
move(arr);
foreach (string _answer in arr_answer)
{
    string[] words = _answer.Split(",");
    StringBuilder sb = new StringBuilder();
    sb.AppendFormat(" answer : {0}\n", _answer);
    foreach (string word in words)
    {
        int col = int.Parse(word);
        for (int i = 0; i < words.Length; i++)
        {
            if (col == i) sb.AppendFormat("■ ");
            else sb.AppendFormat("_ ");
        }
        sb.AppendFormat("\n");
    }
    sb.AppendFormat("\n");    
    Console.WriteLine(sb.ToString());
}
MSG("result : {0}", arr_answer.Count);
#else
int n = 8;
string input = Console.ReadLine();
int.TryParse(input, out n);

byte[] arr = new byte[n];
answer = new int[n];
move(arr);
foreach (string _answer in arr_answer)
{
    string[] words = _answer.Split(",");
    StringBuilder sb = new StringBuilder();
    sb.AppendFormat(" answer : {0}\n", _answer);
    foreach (string word in words)
    {
        int col = int.Parse(word);
        for (int i = 0; i < words.Length; i++)
        {
            if (col == i) sb.AppendFormat("■ ");
            else sb.AppendFormat("_ ");
        }
        sb.AppendFormat("\n");
    }
    sb.AppendFormat("\n");    
    Console.WriteLine(sb.ToString());
}
MSG("result : {0}", arr_answer.Count);


#endif