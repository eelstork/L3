using UnityEngine;

public static class ColorExt{

    public static Color Lighter(this Color self, float x=0.1f)
    => Color.Lerp(self, Color.white, x);

    public static Color Darker(this Color self, float x=0.1f)
    => Color.Lerp(self, Color.black, x);

    public static Color Grayer(this Color self, float x=0.1f)
    => Color.Lerp(self, Color.gray, x);

    public static Color RandomHue()
    => Color.HSVToRGB(Random.value, 1f, 1f);

}
