using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainFuckInterpreter;
public class InCodeInterpreter
{
    private int pos = 0;
    private int allocSize;
    private MemoryStream stream;
    public InCodeInterpreter(int allocSize)
    {
        this.allocSize = allocSize;
        stream = new MemoryStream(allocSize);
    }
    public InCodeInterpreter(int allocSize, MemoryStream memoryStream, int pos = 0)
    {
        this.allocSize = allocSize;
        this.pos = pos;
        stream = memoryStream;
    }
    public Stream GS()
    {
        stream.Position = 0;
        return stream;
    }
    public byte[] GB()
    {
        stream.Position = 0;
        byte[] bytes = new byte[allocSize];
        stream.Read(bytes, 0, bytes.Length);
        return bytes;
    }
    public InCodeInterpreter p
    {
        get
        {
            Console.Write(stream.ReadByte());
            stream.Position = pos;
            return this;
        }
    }
    public InCodeInterpreter w
    {
        get
        {
            stream.WriteByte((byte)(Console.ReadLine() ?? "")[0]);
            stream.Position = pos;
            return this;
        }
    }
    public static InCodeInterpreter operator /(InCodeInterpreter bf, short rem)
    {
        byte newByte = (byte)(bf.stream.ReadByte() - rem);
        bf.stream.Position = bf.pos;
        bf.stream.WriteByte(newByte);
        bf.stream.Position = bf.pos;
        return new(bf.allocSize, bf.stream, bf.pos);
    }
    public static InCodeInterpreter operator *(InCodeInterpreter bf, short add)
    {
        byte newByte = (byte)(bf.stream.ReadByte() + add);
        bf.stream.Position = bf.pos;
        bf.stream.WriteByte(newByte);
        bf.stream.Position = bf.pos;
        return new(bf.allocSize, bf.stream, bf.pos);
    }
    public static InCodeInterpreter operator %(InCodeInterpreter bf, int length)
    {
        bf.stream.Position = bf.pos = bf.pos + length;
        return new(bf.allocSize, bf.stream, bf.pos);
    }
}