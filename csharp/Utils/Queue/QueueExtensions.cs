namespace Utils.Queue;

public static class QueueExtensions
{
    public static Queue<T> ModifyHead<T>(this Queue<T> q, Func<T, T> apply)
    {
        var oldQ = new Queue<T>(q);
        var head = oldQ.Dequeue();
        head = apply(head);
        var newQ = new Queue<T>();
        newQ.Enqueue(head);
        while (oldQ.Any())
            newQ.Enqueue(oldQ.Dequeue());

        return newQ;
    }

    public static Queue<T> WithoutHead<T>(this Queue<T> q)
    {
        var oldQ = new Queue<T>(q);
        oldQ.Dequeue();

        return oldQ;
    }
}