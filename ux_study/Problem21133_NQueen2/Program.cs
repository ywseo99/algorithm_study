

using System.Diagnostics;

List<string> arr_answer = new List<string>();
int[] answer;

bool is_exit = false;

bool move(byte[] arr, int row = 0)
{
    if (row >= arr.Length)
    {
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
    for (int col = 0; col < arr.Length; col++)
    {
        if (is_exit == true) break;

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
        bool ret = move(next_arr, row + 1);

        if (ret == true)
        {
            arr_answer.Add(string.Join(",", answer));
            foreach (int value in answer)
            {
                Console.WriteLine("{0}", value);

            }
            //is_exit = true;
        }
    }
    return false;
}



// n개의 숫자중에서 랜덤하게 1개 뽑기

List<int> values = new List<int>();



void PickAnswer(int n = 10)
{

    // 중간정도의 짝수 값부터 시작해본다.

    
    int begin_col = 0;

    if (n % 2 == 0)
    {
        begin_col = n / 2 - 1;
    }
    else
    {
        begin_col = (n + 1) / 2 - 1;
    }

    Console.WriteLine("begin_col : {0}", begin_col);


    int delta = 2;
    int limit_col = 0;

    limit_col = begin_col + n;
    Random rand = new Random();

    int cnt = 1;
    for (int row = 1; row < n; row ++)
    {
        // 역적되지 않는다면 begin_col + 2씩 증가


        int col = begin_col + delta * cnt;
        
        // 유효 범위 안에 들어온 경우 
        if (col > begin_col && 
            col < limit_col)
        {
            cnt++;
        }
        else
        {
            // 유효한 범위를 벗어난 경우 
            delta = delta + 1;
            cnt = 1;

            col = begin_col + delta;
        }

        if (col <= (n - 1))
        {
            Console.WriteLine(" row: {0}, col: {1}", row, col);
        }
        else
        {
            Console.WriteLine(" row: {0}, col: {1}", row, col % (n - 1));
        }
    }
    

    //for (int index = 0; index < n; index++)
    //{
    //    values.Add(index);
    //}

    //int prev = 0;
    
    //for (int index = 0; index < n; index++)
    //{
        
    //    PICK_AGAIN: 
    //    int rand_index = rand.Next(0, values.Count);
    //    Console.WriteLine("index: {0}, value: {1}", index, values[rand_index]);

    //    // 랜덤하게 하나를 뽑고 후보들을 지워나간다. 
        

    //    if (index == 0)
    //    {
    //        prev = values[rand_index];
    //    }
    //    else
    //    {
    //        if ((prev - 1) == values[rand_index]) goto PICK_AGAIN;
    //        if ((prev + 1) == values[rand_index]) goto PICK_AGAIN;
    //    }
    //    values.Remove(values[rand_index]);
    //}
}




int n = 8;
string input = Console.ReadLine();
int.TryParse(input, out n);

Stopwatch sw = Stopwatch.StartNew();

byte[] arr = new byte[n];
answer = new int[n];
//move(arr);


PickAnswer(n);

sw.Stop();

foreach (string _answer in arr_answer)
{
    Console.WriteLine(_answer);
}
Console.WriteLine("elapsed {0:N0}ms", sw.ElapsedMilliseconds);
