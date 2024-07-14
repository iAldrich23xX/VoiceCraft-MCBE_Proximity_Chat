﻿using System.Numerics;
using System;
using System.Collections.Generic;

namespace VoiceCraft.Core.Packets.VoiceCraft
{
    public class FullUpdatePosition : VoiceCraftPacket
    {
        const int Packed8BitLimit = 256; //2 ^ 8
        public override byte PacketId => (byte)VoiceCraftPacketTypes.FullUpdatePosition;
        public override bool IsReliable => false;

        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public float EchoFactor { get; set; }
        public bool Muffled { get; set; }
        public bool IsDead { get; set; }

        //26 byte overhead previous
        //17 byte overhead
        public override int ReadPacket(ref byte[] dataStream, int offset = 0)
        {
            offset = base.ReadPacket(ref dataStream, offset);

            Position = new Vector3(BitConverter.ToSingle(dataStream, offset), BitConverter.ToSingle(dataStream, offset += sizeof(float)), BitConverter.ToSingle(dataStream, offset += sizeof(float))); //Read Position - 12 bytes.
            offset += sizeof(float);

            Rotation = BitConverter.ToSingle(dataStream, offset); //read rotation - 4 bytes.
            offset += sizeof(float);

            var packedEcho = dataStream[offset]; //read echo factor - 1 byte.
            EchoFactor = packedEcho / (float)Packed8BitLimit;
            offset++;

            Muffled = BitConverter.ToBoolean(dataStream, offset); //read muffled value - 1 byte.
            offset += sizeof(bool);

            IsDead = BitConverter.ToBoolean(dataStream, offset); //read dead value - 1 byte.
            offset += sizeof(bool);

            return offset;
        }

        public override void WritePacket(ref List<byte> dataStream)
        {
            base.WritePacket(ref dataStream);
            dataStream.AddRange(BitConverter.GetBytes(Position.X));
            dataStream.AddRange(BitConverter.GetBytes(Position.Y));
            dataStream.AddRange(BitConverter.GetBytes(Position.Z));
            dataStream.AddRange(BitConverter.GetBytes(Rotation));
            dataStream.Add((byte)(EchoFactor * Packed8BitLimit));
            dataStream.AddRange(BitConverter.GetBytes(Muffled));
            dataStream.AddRange(BitConverter.GetBytes(IsDead));
        }
    }
}