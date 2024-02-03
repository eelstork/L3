using System.Collections.Generic;
using Traversal = Activ.Data.Traversal;
using System.Linq;

namespace R1{
public class History : TNode{

    public List<Record> records = new ();

    public int count => records.Count;

    public Record last
    => records.Count == 0 ? null : records[0];

    public TNode[] children
    => (from x in records select (TNode)x).ToArray();

    public void Add(Record arg) => records.Insert(0, arg);

    public bool Did(string arg) => Traversal.Find<TNode>(
        this, x => x.children, x => x.Matches(arg)
    );

    public bool DidCall(string func, object[] args)
    => Traversal.Find<TNode>(
        this, x => x.children, x => x.Matches(func, args)
    );

    public bool Matches(string arg, object[] args) => false;

}}
