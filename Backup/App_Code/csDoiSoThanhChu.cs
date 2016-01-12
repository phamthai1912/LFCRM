using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for csDoiSoThanhChu
/// </summary>
public class csDoiSoThanhChu
{
	public csDoiSoThanhChu()
	{
        
	}

    private string[] strSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
    int a, b, c;
    public string[] slipArray(string input)
    {
        int i = 0;
        string[] strArray;
        int length = input.Length;
        if (length % 3 == 0)//Nếu chuỗi chia hết cho 3 thì lấy độ dài bằng phần nguyên
            strArray = new string[length / 3];
        else//Nếu chuỗi không chia hết cho 3 thì lấy độ dài bằng phần nguyên+1
            strArray = new string[length / 3 + 1];
        if (length < 3)
            strArray[0] = input;
        else
        {
            while (length >= 3)
            {
                strArray[i] = input.Remove(0, length - 3);
                input = input.Remove(length - 3, 3);
                i++;
                length = length - 3;
            }
            if (length > 0)
                strArray[i] = input;
        }
        return strArray;
    }
    public string converNumToString(string[] list)
    {

        int i;
        string results = "";
        int length = list.Length;
        if (length <= 4)
        {
            if (length == 1)
                results = readThousand(list[0]);
            if (length == 2)
                results = readThousand(list[1]) + " nghìn " + readThousand(list[0]);
            if (length == 3)
            {
                if (readThousand(list[1]) != "" && readThousand(list[2]) != "")
                    results = readThousand(list[2]) + " triệu " + readThousand(list[1]) + " nghìn " + readThousand(list[0]);
                if (readThousand(list[1]) == "" && readThousand(list[2]) != "")
                    results = readThousand(list[2]) + " triệu";
                if (readThousand(list[1]) == "" && readThousand(list[2]) == "")
                    results = "";
            }
            if (length == 4)
            {
                if (readThousand(list[2]) != "" && readThousand(list[1]) != "")
                    results = readThousand(list[3]) + " tỷ " + readThousand(list[2]) + " triệu " + readThousand(list[1]) + " nghìn " + readThousand(list[0]);
                if (readThousand(list[2]) == "" && readThousand(list[1]) != "")
                    results = readThousand(list[3]) + " tỷ " + readThousand(list[1]) + " nghìn " + readThousand(list[0]);
                if (readThousand(list[2]) != "" && readThousand(list[1]) == "")
                    results = readThousand(list[3]) + " tỷ " + readThousand(list[2]) + " triệu " + readThousand(list[0]);
                if (readThousand(list[2]) == "" && readThousand(list[1]) == "")
                    results = readThousand(list[3]) + " tỷ " + readThousand(list[0]);
            }
        }
        if (length > 4)
        {
            string[] strArray1 = new string[3];
            string[] strArray2 = new string[length - 3];
            for (i = 0; i < 3; i++)
            {
                strArray1[i] = list[i];
            }
            for (i = 0; i < length - 3; i++)
            {
                strArray2[i] = list[3 + i];
            }
            //Gọi đệ quy
            results = converNumToString(strArray2) + " tỷ " + converNumToString(strArray1);
        }
        return results;
    }
    //hàm đọc một chuỗi có 3 chữ số ra chữ
    public string readThousand(string input)
    {
        string output = "";
        input = input.Trim();
        string numStr = input;
        int length = numStr.Length;
        if (length == 1)
            output = strSo[Convert.ToInt32(input)];
        if (length == 2)
        {
            a = Convert.ToInt32(Convert.ToString(numStr[0]));
            b = Convert.ToInt32(Convert.ToString(numStr[1]));
            if (a != 1)
            {
                if (b != 5 && b != 0 && b != 1)
                    output = strSo[a] + " mươi " + readThousand(Convert.ToString(numStr[1]));
                if (b == 5)
                    output = strSo[a] + " mươi lăm";
                if (b == 0)
                    output = strSo[a] + " mươi";
                if (b == 1)
                    output = strSo[a] + " mươi mốt";
            }
            if (a == 1)
            {
                if (b != 5)//chỗ này phải thêm đoạn &&b!==0 khưng mà nó đè ở dưới rồi nên lười viết kết quả vẫn đúng
                    output = "mười " + readThousand(Convert.ToString(numStr[1]));
                else
                    output = "mười lăm";
                if (b == 0)
                    output = "mười";
            }
        }
        if (length == 3)
        {
            a = Convert.ToInt32(Convert.ToString(numStr[0]));
            b = Convert.ToInt32(Convert.ToString(numStr[1]));
            c = Convert.ToInt32(Convert.ToString(numStr[2]));
            if (b == 0 && c != 0)
                output = strSo[a] + " trăm linh " + readThousand(Convert.ToString(numStr[2]));
            if (b != 0 && c != 0)
                output = strSo[a] + " trăm " + readThousand(Convert.ToString(numStr[1]) + Convert.ToString(numStr[2]));
            if (b == 0 && c == 0)
            {
                output = strSo[a] + " trăm";
            }
            if (a != 0 && b != 0 && c == 0)
            {
                output = strSo[a] + " trăm " + readThousand(Convert.ToString(numStr[1]) + Convert.ToString(numStr[2]));
            }
            if (a == 0 && b == 0 && c == 0)
            {
                output = "";
            }
        }
        return output;
    }
}
