public class MyTestType{

    public string message => "Hello world";
    public int index = 0;

    public Vec tuple => new Vec(){x = 1, y = 2 };

    public void Log(object arg)
    => UnityEngine.Debug.Log(arg);

    public class Vec{
        public int x, y;
    }

}
