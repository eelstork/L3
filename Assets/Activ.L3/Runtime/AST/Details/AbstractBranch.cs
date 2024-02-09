using System; using System.Collections.Generic; using System.Linq;
using InvOp = System.InvalidOperationException;

namespace L3{
public partial class AbstractBranch<T> : Branch{

    [Hierarchy] public List<T> nodes;

    override public Node[] children
    => nodes == null ? null
    : (from x in nodes select x as Node).ToArray();

     protected List<T> rnodes{ get{
         if(nodes == null) nodes = new (); return nodes;
     }}

     public T FindNode(Predicate<T> cond)
     => nodes.Find(cond);

     override public void AddChild(Node child){
         if(nodes == null) nodes = new ();
         nodes.Add((T)(object)child);
     }

     override public void DeleteChild(Node child){
         //if(nodes == null) return;
         nodes?.Remove((T)(object)child);
     }

     override public void ReplaceChild(Node x, Node y){
         throw new InvOp();
     }

}}
