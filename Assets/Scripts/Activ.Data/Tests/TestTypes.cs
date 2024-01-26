namespace Activ.XML.TestTypes{

    public enum Shape{ Square, Circle, Cross };

    public class ChessPiece{

        public string name;
        public int value;

        // NOTE - Custom types need a default constructor
        public ChessPiece(){}

        public ChessPiece(string name, int value){
            this.name = name;
            this.value = value;
        }

        override public string ToString()
        => $"ChessPiece{{name: {name} value: {value}}}";

    }

}
