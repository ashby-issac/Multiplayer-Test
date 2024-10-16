using System.IO;
using UnityEngine;

public class VectorSerialization
{
    public static byte[] Vector3ToBytes(Vector3 vector)
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(stream);

        writer.Write(vector.x);
        writer.Write(vector.y);
        writer.Write(vector.z);

        return stream.ToArray();
    }

    public static Vector3 BytesToVector3(byte[] bytes)
    {
        MemoryStream stream = new MemoryStream(bytes);
        BinaryReader reader = new BinaryReader(stream);

        float x = reader.ReadSingle();
        float y = reader.ReadSingle();
        float z = reader.ReadSingle();

        return new Vector3(x, y, z);
    }
}