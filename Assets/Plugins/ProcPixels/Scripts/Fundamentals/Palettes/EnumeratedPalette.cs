using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{
    public class EnumeratedPalette<T> : AbstractPalette where T : System.IConvertible
    {

		public static int EnumLength {
			get {
				return System.Enum.GetValues (typeof(T)).Length;
			}
		}

		public static string FieldLabel(int index) {
			return System.Enum.GetName (typeof(T), index);
		}			

        protected void Reset()
        {
            colors = new ShadedColor[EnumLength];
        }

        public ShadedColor this[T index]
        {
            get
            {
                if (!typeof(T).IsEnum)
                    throw new System.ArgumentException("Must be an enum");

                return colors[(int) System.Enum.ToObject(typeof(T), index)];
            }

            set
            {
                if (!typeof(T).IsEnum)
                    throw new System.ArgumentException("Must be an enum");

                colors[(int)System.Enum.ToObject(typeof(T), index)] = value;

            }
        }

        public UnityEngine.Color32 this[T index, ColorShade shade]
        {
            get
            {
                return this[index][shade];
            }
        }
    }
}