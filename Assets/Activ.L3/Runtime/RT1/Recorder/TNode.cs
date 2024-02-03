using System.Collections.Generic;

public interface TNode{

    TNode[] children { get; }

    bool Matches(string arg)
    => (this as object).ToString().Contains(arg);

}
