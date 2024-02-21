using System.Collections;
using System.Drawing;

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
public class SequentialSearchST<Value>: IEnumerable<Value?>
{
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
    private int N;
    public int Size(){
        return N;
    }
    public void Put(IComparable index, Value value){
        if(head == null){
            head = new Node(index, value, head);
            N++;
        }

        for(Node node = head; node != null; node = node.next){
            if(index.Equals(node.index)) node.value = value;
        }

        head = new Node(index, value, head);
        N++;        
    }
    public Value? Get(IComparable index){
        if(head == null) return default(Value);
        for(Node node = head; node != null; node = node.next){
            if(index.Equals(node.index)) return node.value;
        }
        return default(Value);
    }
    public void Delete(IComparable index){
        if(head == null) return;
        if(index.Equals(head.index)){
            head = head.next;
            N--;
        }

        for(Node node = head; node != null; node = node.next){
            if(node.next != null && index.Equals(node.next.index)){
                node.next = node.next.next;
                N--;
            }
        }
    }

    public IEnumerator<Value?> GetEnumerator()
    {
        if(head == null) yield return default;
        for(Node node = head; node != null; node = node.next){
            yield return node.value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
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
    private int N;
    public int Size(){
        return N;
    }
    public void Put(IComparable index, Value value){
        if(head == null){
            head = new Node(index, value, head);
            N++;
            return;
        }
        
        if(index.Equals(head.index)){
            head.value = value;
            return;
        }else if(index.CompareTo(head.index) < 0){
            head = new Node(index, value, head);
            N++;
            return;
        }

        for(Node node = head; node != null; node = node.next){
            if(node.next !=null){
                if(index.Equals(node.next.index)){
                    node.value = value;
                    N++;
                    return;
                }else if(index.CompareTo(node.next.index) < 0){
                    node.next = new Node(index, value, node.next);
                    N++;
                    return;
                }
            }else{
                Node newNode = new Node(index, value, null);
                node.next = newNode;
                N++;
                return;
            }
        }

    }
    public Value? Get(IComparable index){
        for(Node? node = head; node != null; node = node.next){
            if(node.index.Equals(index)) return node.value;
        }
        return default(Value);
    }
    public void Delete(IComparable index){
        if(head == null) return;
        
        if(index.Equals(head.index)){
            head = head.next;
            N--;
            return;
        }

        for(Node node = head; node != null; node = node.next){
            if(node.next != null && index.Equals(node.next.index)){
                node.next = node.next.next;
                N--;
                return;
            }
        }
    }
}
public class ModifiedBinarySearchST<Value>{
    private class Item{
        public IComparable key;
        public int value;
    }
    private Item[] items;
    public ModifiedBinarySearchST(IComparable[] keys, int[] values){
        int len = keys.Length;
        items = new Item[len];
        for(int i = 0; i < len; i++){
            items[i] = new Item();
            items[i].key = keys[i];
            items[i].value = values[i];
        }
        MergeSort(items);
    }
    private void MergeSort(Item[] items){
        Item[] temp = new Item[items.Length];

        MergeSort(items, temp, 0, items.Length - 1);
    }
    private void MergeSort(Item[] items, Item[] temp, int low, int high){
        if(low >= high) return;
        int middle = low + (high - low) / 2;

        MergeSort(items, temp, low, middle);
        MergeSort(items, temp, middle + 1, high);
        Merge(items, temp, low, middle, high);
    }
    private void Merge(Item[] items, Item[] temp, int low, int middle, int high){
        int left = low;
        int right = middle + 1;

        for(int i = low; i <= high; i++){
            temp[i] = items[i];
        }

        for(int i = low; i <= high; i++){
            if(left > middle){
                items[i] = temp[right++];
            }else if(right > high){
                items[i] = temp[left++];
            }else if(temp[left].key.CompareTo(temp[right].key) <= 0){
                items[i] = temp[left++];
            }else{
                items[i] = temp[right++];
            }
        }
    }
    private int BinarySearch(IComparable key, int low, int high){
        for(int i = low; i <= high; i++){
            int middle = low + (high - low) / 2;
            if(key.CompareTo(items[middle].key) > 0){
                low = middle + 1;
            }else if(key.CompareTo(items[middle].key) < 0){
                high = middle - 1;
            }else{
                return middle;
            }
        }
        return low;
    }
    public void Delete(IComparable key){
        int index = BinarySearch(key, 0, items.Length);
        if(index < items.Length && items[index].key.Equals(key)){
            
        }
    }
    public override string ToString()
    {
        string s = "";
        for(int i = 0; i < items.Length; i++){
            s += "key:" + items[i].key + ", value:" + items[i].value + "\n";
        }
        return s;
    }
}
public class Test{
    static void Main(){
        IComparable[] keys = {1, 3, 6, 8, 5, 9, 2};
        int[] values = {34, 22, 4, 5, 32, 55, 6};
        ModifiedBinarySearchST<int> modifiedBinarySearchST = new ModifiedBinarySearchST<int>(keys, values);
        Console.WriteLine(modifiedBinarySearchST);
    }
}