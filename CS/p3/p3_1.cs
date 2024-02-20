namespace p3;

public class ArrayST<Value>
{
    private IComparable[] keys;
    private Value?[] values;
    private int N;
    public ArrayST(int len){
        keys = new IComparable[len];
        values = new Value[len];
    }
    public bool Contains(IComparable key){
        return Get(key) != null;
    }
    public bool isEmpty(){
        return N == 0;
    }
    public int Size(){
        return N;
    }
    public void Delete(IComparable key){
        for(int i = 0; i < N; i++){
            if(keys[i].CompareTo(key) == 0) values[i] = default(Value);
        }
    }
    public Value? Get(IComparable key){
        for(int i = 0; i < N; i++){
            if(keys[i].CompareTo(key) == 0) return values[i];
        }
        return default(Value);
    }
    public void Put(IComparable key, Value value){
        for(int i = 0; i < N; i++){
            if(keys[i].CompareTo(key) == 0) {
                values[i] = value;
                return;
            }
        }
        keys[N] = key;
        values[N] = value;
        N++;
    }
}
public class OrderedSequentialSearchST<Value>{
    private class Node{
        public IComparable index;
        public Value? value;
        public Node? next;
        public Node(IComparable index, Value value, Node next){
            this.index = index;
            this.value = value;
            this.next = next;
        }
    }
    private Node? head;

    public void Put(IComparable index, Value value){

    }
    public Value? Get(IComparable index){
        for(Node? node = head; node != null; node = node.next){
            if(node.index.Equals(index)) return node.value;
        }
        return default(Value);
    }
}
public class Test{
    static void Main(){
        ArrayST<int> arrayST = new ArrayST<int>(10);
        arrayST.Put(12, 34);
        arrayST.Put(13, 5);
        arrayST.Delete(13);
        Console.WriteLine(arrayST.Get(13));
    }
}