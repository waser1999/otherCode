namespace p3;

public class SeperateChainingST<Value>
{
    private int M;
    private SequentialSearchST<Value>[] valueChain;

    public SeperateChainingST(int M){
        valueChain = new SequentialSearchST<Value>[M];
        this.M = M;
        for(int i = 0; i < M; i++){
            valueChain[i] = new SequentialSearchST<Value>();
        }
    }
    public int Hash(IComparable key){
        return (key.GetHashCode() & 0x7fffffff) % M;
    }

    public virtual void Put(IComparable key, Value value){
        valueChain[Hash(key)].Put(key, value);
    }
    public virtual Value? Get(IComparable key){
        return valueChain[Hash(key)].Get(key);
    }
    public virtual void Delete(IComparable key){
        valueChain[Hash(key)].Delete(key);
    }
}
public class LinearProbingST<Value>
{
    private int N;
    private int M;
    private IComparable[] keys;
    private Value[] values;
    public LinearProbingST(int M){
        this.M = M;
        keys = new IComparable[M];
        values = new Value[M];
    }

    private void Resize(int size){
        //注意插入时要重新散列，不能直接复制
        LinearProbingST<Value> linearProbingST = new LinearProbingST<Value>(size);

        for(int i = 0; i < keys.Length; i++){
            IComparable tempKey = keys[i];
            Value tempValue = values[i];
            if(tempKey != null) linearProbingST.Put(tempKey, tempValue);
        }
        keys = linearProbingST.keys;
        values = linearProbingST.values;
        M = size;
    }
    private bool Contains(IComparable key){
        for(int i = Hash(key); keys[i] != null; i = (i + 1) % M){
            if(keys[i].Equals(key)) return true;
        }
        return false;
    }
    public int Hash(IComparable key){
        return (key.GetHashCode() & 0x7fffffff) % M;
    }
    public Value? Get(IComparable key){
        for(int i = Hash(key); keys[i] != null; i = (i + 1) % M){
            if(keys[i].Equals(key)) return values[i];
        }
        return default;
    }
    public void Put(IComparable key, Value value){
        if(N >= M / 2) Resize(2 * M);

        int i;
        for(i = Hash(key); keys[i] != null; i = (i + 1) % M){
            if(keys[i].Equals(key)){
                values[i] = value;
                return;
            } 
        }

        keys[i] = key;
        values[i] = value;
        N++;
    }
    public void Delete(IComparable key){
        if(!Contains(key)) return;
        
        int i = Hash(key);
        while(!keys[i].Equals(key)){
            i = (i + 1) % M;
        }
        keys[i] = null;
        values[i] = default;
        N--;
        i = (i + 1) % M;
        while(keys[i] != null){
            IComparable tempKey = keys[i];
            Value tempValue = values[i];
            keys[i] = null;
            values[i] = default;
            Put(tempKey, tempValue);
            i = (i + 1) % M;
        }

        if(N > 0 && N == M / 8) Resize(M / 2);
    }
    public void PrintKeys(){
        for(int i = 0; i < keys.Length; i++){
            if(keys[i] == null){
                Console.Write("null ");
            }else{
                Console.Write(keys[i] + " ");
            }
        }
    }
}
public class Test4{
    static void Main(){
        IComparable[] keys = {"E", "A", "S", "Y", "Q", "U", "T", "I", "O", "N"};
        string[] values = {"E", "A", "S", "Y", "Q", "U", "T", "I", "O", "N"};
        LinearProbingST<string> seperateChainingST = new LinearProbingST<string>(11);
        for(int i = 0; i < keys.Length; i++){
            seperateChainingST.Put(keys[i], values[i]);
        }
        seperateChainingST.Delete("I");
        seperateChainingST.PrintKeys();
    }
}