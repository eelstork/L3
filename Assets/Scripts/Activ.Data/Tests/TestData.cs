namespace Activ.XML{
public static class TestData{

    public static string @array
        = "<Int32-Array>"
            + "<Int32>1</Int32>"
            + "<Int32>2</Int32>"
            + "<Int32>3</Int32>"
        + "</Int32-Array>";

    public static string @enum
        = "<Shape>Circle</Shape>";

    public static string @int
        = "<Int32>5</Int32>";

    public static string @list
        = "<List>"
            + "<Int32>1</Int32>"
            + "<Int32>2</Int32>"
            + "<Int32>3</Int32>"
        + "</List>";

    public static string @chess
        = "<ChessPiece>"
            + "<name t='String'>Rook</name>"
            + "<value t='Int32'>5</value>"
        + "</ChessPiece>";

    public static string @param =
        "<Parameter>"
            + "<type t='String'>int</type>"
            + "<name t='String'>index</name>"
        + "</Parameter>";

    public static string @string
        = "<String>Hello</String>";

}}
