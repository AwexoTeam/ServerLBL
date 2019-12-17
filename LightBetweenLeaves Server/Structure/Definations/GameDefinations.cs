using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDefinations
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
    }

    public class ItemData
    {
        public int id;
        public string name;
        public string description;
        public int usableID;
    }

    public enum Rank
    {
        Rank_Novice,
        Rank_F,
        Rank_E,
        Rank_D,
        Rank_C,
        Rank_B,
        Rank_A,
        Rank_9,
        Rank_8,
        Rank_7,
        Rank_6,
        Rank_5,
        Rank_4,
        Rank_3,
        Rank_2,
        Rank_1,
        Rank_Master,
        Dan_1,
        Dan_2,
        Dan_3,
    }
}
