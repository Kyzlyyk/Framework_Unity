using System;
using UnityEngine;
using Kyzlyk.Helpers.Utils;

namespace Kyzlyk.Helpers
{
    public struct VectorNormalized : IEquatable<VectorNormalized>
    {
        public VectorNormalized(float xFromMinusOneToOne, float yFromMinusOneToOne)
        {
            _x = xFromMinusOneToOne;
            _y = yFromMinusOneToOne;

            if (IsFromMinusOneToOne(xFromMinusOneToOne) || IsFromMinusOneToOne(yFromMinusOneToOne))
                Normalize();
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

        public readonly static VectorNormalized Zero = new(0f, 0f);
        public readonly static VectorNormalized One = new(1f, 1f);
        public readonly static VectorNormalized MinusOne = new(-1f, -1f);

        public float X => _x;
        public float Y => _y;

        public Vector2 Vector => new(X, Y);

        private float _x;
        private float _y;

        private bool IsFromMinusOneToOne(float f)
        {
            return (f < -1 || f > 1);
        }

        private void Normalize()
        {
            float magnitude = Mathf.Sqrt(_x * _x + _y * _y);
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

        public override bool Equals(object obj)
        {
            if (obj is VectorNormalized other)
            {
                return other._x == this._x && other._y == this._y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (int)(_x * _y + Time.time) 
                + GUtility.ConvertStringToInt(GetType().FullName) 
                + UnityEngine.Random.Range(1, DateTime.Now.Minute);
        }

        public override string ToString()
        {
            return $"VectorNormalized({X}, {Y})";
        }

        public bool Equals(VectorNormalized other)
        {
            return other._x == this._x && other._y == this._y;
        }

        public static VectorNormalized operator -(VectorNormalized a) => new VectorNormalized(0f - a.X, 0f - a.Y);
        public static VectorNormalized operator -(VectorNormalized a, VectorNormalized b) => new(a._x - b._x, a._y - b._y);
        public static VectorNormalized operator +(VectorNormalized a) => new VectorNormalized(Mathf.Abs(a.X), Mathf.Abs(a.Y));
        public static VectorNormalized operator +(VectorNormalized a, VectorNormalized b) => new(a._x + b._x, a._y + b._y);

        public static bool operator ==(VectorNormalized a, VectorNormalized b) => a.Equals(b);
        public static bool operator !=(VectorNormalized a, VectorNormalized b) => !a.Equals(b);

        public static implicit operator Vector2(VectorNormalized a) => a.Vector;
        public static explicit operator VectorNormalized(Vector2 a) => new VectorNormalized(a.x, a.y);
    }
}
