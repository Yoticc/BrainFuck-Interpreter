namespace BrainFuckInterpreter;
public class StringInterpreter
{
    public static char[] Execute(string code, int allocSize = 30000)
    {
        var(z,v,o)=(new char[allocSize],0,new Dictionary<int,int>{{-9,0},{-2,0},{-1,0},{0,0}});
        while(o[o[-2]]<code.Length)o[-9]=((Action[])[()=>v++,()=>v--,()=>z[v]++,()=>z[v]--,()=>Console.Write(z[v]),()=>z[v]=Console.ReadLine()[0],()=>{
            if(z[v]==0)
                do o[-1]+=code[++o[o[-2]]-1]==91?1:(code[o[o[-2]]-1]==93?-1:0);
                while(code[o[o[-2]]-1]!=93||o[-1]!=0);
            else o[++o[-2]]=o[o[-2]-1]+1;
            o[o[-2]]--;
        },()=>o[-9]=(z[v]!=0?(Action)(()=>o[o[-2]]=o[o[-2]-1]+1):(()=>o[-2]--))()is int?0:o[o[-2]]--])[((List<int>)[62,60,43,45,46,44,91,93]).IndexOf((byte)code[o[o[-2]]])]()is int?0:o[o[-2]]++;
        return z;
    }
}
