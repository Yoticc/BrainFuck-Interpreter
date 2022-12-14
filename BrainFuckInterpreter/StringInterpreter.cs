using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreter;
public class StringInterpreter
{
    public static byte[] Execute(string code, int allocSize = 30000, int pos = 0)
    {
        var bytes = new byte[allocSize];
        Dictionary<int, int> point = new Dictionary<int, int> { { -9, 0 }, { -2, 0 }, { -1, 0 }, { 0, 0 } };
        Dictionary<byte, Action> types = new(){
            { 62, new(() => pos++) }, { 60, new(() => pos-- ) },
            { 43, new(() => bytes[pos]++ ) }, { 45, new(() => bytes[pos]--) },
            { 46, new(() => Console.Write((char)bytes[pos])) }, { 44, new(() => bytes[pos] = (byte)(Console.ReadLine()??" ")[0]) },
            { 91, new(() => {
                if (bytes[pos] == 0)
                    do point[-1] += (code[++point[point[-2]] - 1] == 91 ? 1 : (code[point[point[-2]] - 1] == 93 ? -1 : 0));
                    while (code[point[point[-2]] - 1] != 93 || point[-1] != 0);
                else point[++point[-2]] = point[point[-2] - 1] + 1;
                point[point[-2]]--;
            })},
            { 93, new(() => point[-9] = (bytes[pos] != 0 ? new(() => point[point[-2]] = point[point[-2] - 1] + 1) : new Action(() => point[-2]--))() is int ? point[point[-2]]-- : point[point[-2]]--)}
        };
        while (point[point[-2]] < code.Length)
            point[-9] = types[(byte)code[point[point[-2]]]]() is int ? point[point[-2]]++ : point[point[-2]]++;
        return bytes;
    }
}