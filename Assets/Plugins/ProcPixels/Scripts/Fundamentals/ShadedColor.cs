using UnityEngine;
using System.Collections;

namespace ProcPixel.Fundamentals
{

    public enum ColorShade { None, Reference, Lighter, Darker};

    [System.Serializable]
    public struct ShadedColor
    {
        public string name {
            get
            {
                return referenceColor.ToString();
            }
        }

        public UnityEngine.Color32 referenceColor;
        public UnityEngine.Color32 lighterColor;
        public UnityEngine.Color32 darkerColor;

        [SerializeField, HideInInspector]
        float shading;

		public ShadedColor(UnityEngine.Color32 referenceColor, UnityEngine.Color32 lighterColor, UnityEngine.Color32 darkerColor, float shading) {
			this.referenceColor = referenceColor;
			this.lighterColor = lighterColor;
			this.darkerColor = darkerColor;
			this.shading = shading;
		}

        public ShadedColor(UnityEngine.Color32 referenceColor, UnityEngine.Color32 lighterColor, UnityEngine.Color32 darkerColor)
        {
            this.referenceColor = referenceColor;
            this.lighterColor = lighterColor;
            this.darkerColor = darkerColor;
            shading = 0f;
        }

        public ShadedColor(UnityEngine.Color32 referenceColor)
        {
            shading = 0.1f;
            this.referenceColor = referenceColor;
			lighterColor = CalculateShade (referenceColor, shading, ColorShade.Lighter);
			darkerColor = CalculateShade (referenceColor, shading, ColorShade.Darker);            
        }

        public ShadedColor(UnityEngine.Color32 referenceColor, float shading)
        {
            this.referenceColor = referenceColor;
			lighterColor = CalculateShade (referenceColor, shading, ColorShade.Lighter);
			darkerColor = CalculateShade (referenceColor, shading, ColorShade.Darker);
            this.shading = shading;
        }

		/*
		public Color() {
			shading = 0.1f;
			referenceColor = UnityEngine.Color.white;
			referenceColor.a = 255;
			lighterColor = CalculateShade (referenceColor, shading, ColorShade.Lighter);
			darkerColor = CalculateShade (referenceColor, shading, ColorShade.Darker); 
		}*/

		public static ShadedColor RandomColor {
			get {
				return new ShadedColor (new UnityEngine.Color (Random.value, Random.value, Random.value, 1f));
			}
		}

        public void SetShadeStrength(float shading)
        {
            SetShadeStrength(shading, shading);
        }

        public void SetShadeStrength(float lightShade, float darkShade)
        {
            shading = lightShade;
			lighterColor = CalculateShade (referenceColor, lightShade, ColorShade.Lighter);
			darkerColor = CalculateShade (referenceColor, darkShade, ColorShade.Darker);

        }

		public static Color32 CalculateShade(Color32 baseColor, float shading, ColorShade shade) {
			if (shade == ColorShade.Reference) {
				return new Color32 (baseColor.r, baseColor.g, baseColor.b, baseColor.a);
			}
			var col = Color32.Lerp (baseColor, shade == ColorShade.Darker ? UnityEngine.Color.black : UnityEngine.Color.white, shading);
			col.a = baseColor.a;
			return col;
		}

		public ShadedColor Copy() {
			return new ShadedColor (referenceColor, lighterColor, darkerColor, shading);
		}

        public UnityEngine.Color32 this[ColorShade shade]
        {
            get
            {
                switch (shade)
                {
                    case ColorShade.Reference:
                        return referenceColor;
                    case ColorShade.Lighter:
                        return lighterColor;
                    case ColorShade.Darker:
                        return darkerColor;
                    default:
                        return referenceColor;
                }

            }

            set
            {
                switch(shade)
                {
                    case ColorShade.Reference:
                        referenceColor = value;
                        break;
                    case ColorShade.Lighter:
                        lighterColor = value;
                        break;
                    case ColorShade.Darker:
                        darkerColor = value;
                        break;
                }
            }
        }

		public static ColorShade RandomShade {
			get {
				if (Random.value < 0.34f)
					return ColorShade.Reference;
				else
					return Random.value <= 0.5f ? ColorShade.Lighter : ColorShade.Darker;
			}
		}
    }
}
