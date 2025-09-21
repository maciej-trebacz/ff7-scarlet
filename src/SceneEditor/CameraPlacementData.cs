﻿using FF7Scarlet.Shared;

namespace FF7Scarlet.SceneEditor
{
    public class CameraPlacementData
    {
        public const int POSITION_COUNT = 4, BLOCK_SIZE = 48;
        private readonly Point3D[]
            cameraPositions = new Point3D[POSITION_COUNT],
            cameraDirections = new Point3D[POSITION_COUNT];

        public Point3D[] CameraPositions
        {
            get { return cameraPositions; }
        }
        public Point3D[] CameraDirections
        {
            get { return cameraDirections; }
        }

        public CameraPlacementData()
        {
            for (int i = 0; i < POSITION_COUNT; ++i)
            {
                CameraPositions[i] = new Point3D(HexParser.NULL_OFFSET_16_BIT_SIGNED, HexParser.NULL_OFFSET_16_BIT_SIGNED,
                    HexParser.NULL_OFFSET_16_BIT_SIGNED);
                CameraDirections[i] = new Point3D(HexParser.NULL_OFFSET_16_BIT_SIGNED, HexParser.NULL_OFFSET_16_BIT_SIGNED,
                    HexParser.NULL_OFFSET_16_BIT_SIGNED);
            }
        }

        public CameraPlacementData(byte[] data)
        {
            short x, y, z;
            using (var ms = new MemoryStream(data, false))
            using (var reader = new BinaryReader(ms))
            {
                for (int i = 0; i < POSITION_COUNT; ++i)
                {
                    x = reader.ReadInt16();
                    y = reader.ReadInt16();
                    z = reader.ReadInt16();
                    CameraPositions[i] = new Point3D(x, y, z);

                    x = reader.ReadInt16();
                    y = reader.ReadInt16();
                    z = reader.ReadInt16();
                    CameraDirections[i] = new Point3D(x, y, z);
                }
            }
        }

        public byte[] GetRawData()
        {
            var data = new byte[BLOCK_SIZE];
            using (var ms = new MemoryStream(data, true))
            using (var writer = new BinaryWriter(ms))
            {
                for (int i = 0; i < POSITION_COUNT; ++i)
                {
                    writer.Write(CameraPositions[i].X);
                    writer.Write(CameraPositions[i].Y);
                    writer.Write(CameraPositions[i].Z);

                    writer.Write(CameraDirections[i].X);
                    writer.Write(CameraDirections[i].Y);
                    writer.Write(CameraDirections[i].Z);
                }
            }
            return data;
        }
    }
}
