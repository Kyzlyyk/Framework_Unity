using System;
using UnityEngine;
using Kyzlyk.Helpers.Utils;
using static UnityEngine.Mathf;
using static Kyzlyk.Helpers.Math.MathUtility;

namespace Kyzlyk.Helpers.Math
{
    public struct UnitVector : IEquatable<UnitVector>
    {
        public UnitVector(float xUnit, float yUnit)
        {
            _x = xUnit;
            _y = yUnit;

            if (!IsUnit(xUnit) || !IsUnit(yUnit))
                Normalize_Internal();
        }
        
        internal UnitVector(float xUnit, float yUnit, OverloadAction_Intrl @throwException)
        {
            if (!IsUnit(xUnit) || !IsUnit(yUnit))
                throw new Exception("One of the UnitVector components is not unit!");

            _x = xUnit;
            _y = yUnit;
        }

        public float this[int index]
        {
            get
            {
                if (index == 0) return _x;
                else if (index == 1) return _y;

                else return 0f;
            }
            set
            {
                if (index == 0) _x = value;
                else if (index == 1) _y = value;
            }
        }

        public static UnitVector Zero => new(0f, 0f);
        public static UnitVector One => new(1f, 1f);
        public static UnitVector MinusOne => new(-1f, -1f);
        
        public static UnitVector Up => new(0f, 1f);
        public static UnitVector Down => new(0f, -1f);
        public static UnitVector Left => new(-1f, 0f);
        public static UnitVector Right => new(1f, 0f);
        
        public static UnitVector UpRight => new(1f, 1f);
        public static UnitVector DownRight => new(1f, -1f);
        public static UnitVector UpLeft => new(-1f, 1f);
        public static UnitVector DownLeft => new(-1f, -1f);

        public float X => _x;
        public float Y => _y;

        public Vector2 Vector2 => new(X, Y);

        private float _x;
        private float _y;

        public bool Compare(UnitVector other, float tolerance)
        {
            tolerance = IsUnit(tolerance) ? Abs(tolerance) : 0f;
            return (Abs(_x - other._x) <= tolerance) && (Abs(_y - other._y) <= tolerance);
        }

        public static bool IsUnit(float f, float scale = 1f)
        {
            scale = Abs(scale);
            return (f >= -scale && f <= scale);
        }
        
        public static bool IsUnit(Vector2 vector, float scale = 1f)
        {
            return IsUnit(vector.x, scale) && IsUnit(vector.y, scale);
        }

        internal void Normalize_Internal()
        {
            float magnitude = Sqrt(_x * _x + _y * _y);
            if (magnitude > 1E-05f)
            {
                _x /= magnitude;
                _y /= magnitude;
            }
            else
            {
                _x = 0f;
                _y = 0f;
            }
        }

        public static UnitVector Deg180ToVector(float degrees)
        {
            return RadToVector(DegToRad(degrees));
        }
        
        public static UnitVector Deg360ToVector(float degrees)
        {
            return RadToVector(NormalizeRad(DegToRad(degrees)));
        }
        
        public static float NormalizeRad(float radians)
        {
            float range = 2 * PI;
            
            radians %= range;
            if (radians < 0)
                radians += range;

            return radians;
        }

        public static UnitVector RadToVector(float radians)
        {
            return new UnitVector(Cos(radians), Sin(radians), OverloadAction_Intrl.Mark);
        }

        public static float VectorToDeg180(UnitVector unitVector)
        {
            return RadToDeg(VectorToRad(unitVector));
        }
        
        public static float VectorToDeg360(UnitVector unitVector)
        {
            return Angle180To360(VectorToDeg180(unitVector));
        }

        public static float VectorToRad(UnitVector unitVector)
        {
            return Atan2(unitVector.Y, unitVector.X);
        }

        public override bool Equals(object obj)
        {
            if (obj is UnitVector other)
            {
                return other._x == this._x && other._y == this._y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (int)(_x * _y + Time.time) 
                + GUtility.ConvertStringToInt(GetType().FullName) 
                + UnityEngine.Random.Range(1, DateTime.Now.Millisecond);
        }

        public override string ToString()
        {
            return $"UnitVector({X}, {Y})";
        }

        public bool Equals(UnitVector other)
        {
            return other._x == this._x && other._y == this._y;
        }

        internal static UnitVector Cast_Unsafe_Internal(Vector2 vector2) 
            => new(vector2.x, vector2.y, OverloadAction_Intrl.Mark);

        public static UnitVector operator -(UnitVector a) => new UnitVector(0f - a.X, 0f - a.Y);
        public static UnitVector operator -(UnitVector a, UnitVector b) => new(a._x - b._x, a._y - b._y);
        public static UnitVector operator +(UnitVector a) => new UnitVector(Abs(a.X), Abs(a.Y));
        public static UnitVector operator +(UnitVector a, UnitVector b) => new(a._x + b._x, a._y + b._y);
        
        public static bool operator >(UnitVector a, UnitVector b) => (a._x > b._x) && (a._y > b._y);
        public static bool operator <(UnitVector a, UnitVector b) => (a._x < b._x) && (a._y < b._y);
        public static bool operator >=(UnitVector a, UnitVector b) => (a._x >= b._x) && (a._y >= b._y);
        public static bool operator <=(UnitVector a, UnitVector b) => (a._x <= b._x) && (a._y <= b._y);

        public static bool operator ==(UnitVector a, UnitVector b) => a.Equals(b);
        public static bool operator !=(UnitVector a, UnitVector b) => !a.Equals(b);

        public static implicit operator Vector2(UnitVector a) => a.Vector2;
        public static explicit operator UnitVector(Vector2 a) => new UnitVector(a.x, a.y);
    }
}
